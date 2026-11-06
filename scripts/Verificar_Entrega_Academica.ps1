[CmdletBinding()]
param()

$ErrorActionPreference = "Stop"
$root = (Resolve-Path (Join-Path $PSScriptRoot "..")).Path
$officialSql = Join-Path $root "DBSistema_Agricola\Script_Completo_Sistema_Agricola.sql"
$hashFile = Join-Path $root "DBSistema_Agricola\Script_Completo_Sistema_Agricola.sha256"

if (-not (Test-Path -LiteralPath $officialSql -PathType Leaf)) {
    throw "Falta el script SQL oficial: $officialSql"
}

if (-not (Test-Path -LiteralPath $hashFile -PathType Leaf)) {
    throw "Falta la huella SHA-256 del script SQL oficial."
}

$expectedHash = (Get-Content -LiteralPath $hashFile -Raw).Trim().Split(' ')[0]
$actualHash = (Get-FileHash -LiteralPath $officialSql -Algorithm SHA256).Hash
if (-not [string]::Equals($expectedHash, $actualHash, [StringComparison]::OrdinalIgnoreCase)) {
    throw "La huella del script SQL oficial no coincide. Actualice el script y su SHA-256 juntos."
}

$forbiddenDirectories = Get-ChildItem -LiteralPath $root -Directory |
    Where-Object Name -Match '^Publicacion_'
if ($forbiddenDirectories) {
    throw "La entrega contiene directorios de publicacion generados: $($forbiddenDirectories.Name -join ', ')"
}

$forbiddenPackages = Get-ChildItem -LiteralPath $root -Recurse -File -ErrorAction SilentlyContinue |
    Where-Object Extension -In '.zip', '.rar', '.7z'
if ($forbiddenPackages) {
    throw "La entrega contiene paquetes comprimidos no versionables."
}

$allowedExampleEmails = @('correo@gmail.com', 'usuario@gmail.com')
$emailPattern = '[A-Za-z0-9._%+-]+@gmail\.com'
$emailHits = foreach ($file in Get-ChildItem -LiteralPath $root -Recurse -File |
    Where-Object {
        $_.Extension -In '.cmd', '.ps1', '.json', '.cs', '.md', '.sql', '.pubxml', '.txt' -and
        $_.FullName -NotMatch '[\\/](bin|obj|\.git)[\\/]'
    }) {
    foreach ($line in Select-String -LiteralPath $file.FullName -Pattern $emailPattern -AllMatches) {
        foreach ($match in $line.Matches) {
            if ($match.Value.ToLowerInvariant() -notin $allowedExampleEmails) {
                $line
            }
        }
    }
}
if ($emailHits) {
    throw "La entrega contiene un correo personal fijo en archivos de configuracion."
}

$versionRequired = 'public const int VersionRequerida = 12;'
$schemaFile = Join-Path $root 'Sistema_Agricola.BLL\BLL_Esquema.cs'
if (-not (Select-String -LiteralPath $schemaFile -SimpleMatch $versionRequired -Quiet)) {
    throw "La version requerida del esquema no coincide con la entrega academica."
}

Write-Host "Entrega academica verificada correctamente." -ForegroundColor Green
