[CmdletBinding()]
param(
    [string]$Destinatario = ""
)

$ErrorActionPreference = "Stop"

function Find-SystemExecutable {
    $candidates = @(
        (Join-Path $PSScriptRoot "Sistema_Agricola.UI.exe"),
        (Join-Path $PSScriptRoot "Sistema_Agricola.UI\bin\Release\net8.0-windows\win-x64\publish\Sistema_Agricola.UI.exe"),
        (Join-Path $PSScriptRoot "Sistema_Agricola.UI\bin\Release\net8.0-windows\win-x64\Sistema_Agricola.UI.exe"),
        (Join-Path $PSScriptRoot "Sistema_Agricola.UI\bin\Debug\net8.0-windows\Sistema_Agricola.UI.exe")
    )

    foreach ($candidate in $candidates) {
        if (Test-Path -LiteralPath $candidate) {
            return (Resolve-Path -LiteralPath $candidate).Path
        }
    }

    throw "No se encontro Sistema_Agricola.UI.exe. Ejecute este archivo dentro de la carpeta publicada del sistema."
}

$configPath = Join-Path $env:APPDATA "Sistema_Agricola\configuracion_sistema.json"
if (-not (Test-Path -LiteralPath $configPath)) {
    throw "No existe la configuracion de correo de esta PC. Abra Configuracion > Correo y notificaciones y agregue la clave de aplicacion."
}

$executable = Find-SystemExecutable
$resultPath = Join-Path ([IO.Path]::GetTempPath()) (
    "Sistema_Agricola_correo_" + [Guid]::NewGuid().ToString("N") + ".txt")

$arguments = @(
    "--diagnosticar-correo",
    "--resultado-correo",
    ('"' + $resultPath + '"')
)

if (-not [string]::IsNullOrWhiteSpace($Destinatario)) {
    $arguments += "--destinatario"
    $arguments += ('"' + $Destinatario.Trim() + '"')
}

Write-Host "Probando Gmail con el mismo servicio MailKit del sistema..." -ForegroundColor Cyan
Write-Host "Se probaran automaticamente los puertos seguros 587 y 465."

try {
    $process = Start-Process `
        -FilePath $executable `
        -ArgumentList $arguments `
        -WindowStyle Hidden `
        -Wait `
        -PassThru

    if (-not (Test-Path -LiteralPath $resultPath)) {
        throw "El diagnostico termino sin producir un resultado. Codigo del proceso: $($process.ExitCode)."
    }

    $lines = @(Get-Content -LiteralPath $resultPath -Encoding UTF8)
    $status = if ($lines.Count -gt 0) { $lines[0].Trim() } else { "ERROR" }
    $message = if ($lines.Count -gt 1) {
        ($lines | Select-Object -Skip 1) -join [Environment]::NewLine
    }
    else {
        "El diagnostico no devolvio detalles."
    }

    if ($status -eq "OK") {
        Write-Host $message -ForegroundColor Green
        exit 0
    }

    Write-Host $message -ForegroundColor Red
    exit 1
}
finally {
    Remove-Item -LiteralPath $resultPath -Force -ErrorAction SilentlyContinue
}
