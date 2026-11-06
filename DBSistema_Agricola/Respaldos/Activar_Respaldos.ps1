param(
    [string]$ServerInstance = ".\SQLEXPRESS",
    [string]$Database = "DBSistema_Agricola",
    [string]$BackupRoot = "$env:USERPROFILE\Documents\Sistema_Agricola_Respaldos",
    [string]$OffsitePath = "",
    [int]$RetentionDays = 14,
    [string]$DailyTime = "22:00",
    [string]$TaskName = "Sistema Agricola - Backup Diario",
    [string]$SqlCmdPath = "sqlcmd"
)

$ErrorActionPreference = "Stop"

function Invoke-Step {
    param([string]$Name, [scriptblock]$Action)
    Write-Host "[$Name]" -ForegroundColor Cyan
    & $Action
}

function Get-SqlServiceIdentity {
    param([string]$Instance)

    $instanceName = if ($Instance -match "\\([^\\]+)$") {
        $Matches[1]
    }
    else {
        "MSSQLSERVER"
    }

    $serviceName = if ($instanceName -eq "MSSQLSERVER") {
        "MSSQLSERVER"
    }
    else {
        "MSSQL`$$instanceName"
    }

    $service = Get-CimInstance Win32_Service -Filter "Name='$serviceName'"
    if ($null -eq $service) {
        throw "No se encontro el servicio SQL Server $serviceName."
    }

    return $service.StartName
}

$backupScript = Join-Path $PSScriptRoot "Backup_Diario_Sistema_Agricola.ps1"
$taskScript = Join-Path $PSScriptRoot "Crear_Tarea_Backup_Diario.ps1"
$permissionsScript = Join-Path $PSScriptRoot "Configurar_Permisos_Respaldos.sql"

foreach ($path in @($backupScript, $taskScript, $permissionsScript)) {
    if (-not (Test-Path -LiteralPath $path)) {
        throw "Falta el archivo requerido: $path"
    }
}

if ($null -eq (Get-Command $SqlCmdPath -ErrorAction SilentlyContinue)) {
    throw "No se encontro sqlcmd. Instale SQL Server Command Line Utilities."
}

if ([string]::IsNullOrWhiteSpace($OffsitePath) -and (Test-Path -LiteralPath "D:\")) {
    $OffsitePath = "D:\Respaldos_Sistema_Agricola"
}

$rootFull = [IO.Path]::GetFullPath($BackupRoot).TrimEnd('\')
if (-not [string]::IsNullOrWhiteSpace($OffsitePath)) {
    $offsiteFull = [IO.Path]::GetFullPath($OffsitePath).TrimEnd('\')
    if ($offsiteFull.Equals($rootFull, [StringComparison]::OrdinalIgnoreCase) -or
        $offsiteFull.StartsWith($rootFull + "\", [StringComparison]::OrdinalIgnoreCase)) {
        throw "La copia externa debe estar fuera de la carpeta local de respaldos."
    }
}

Invoke-Step "Preparar carpetas" {
    New-Item -ItemType Directory -Force -Path $BackupRoot | Out-Null
    if (-not [string]::IsNullOrWhiteSpace($OffsitePath)) {
        New-Item -ItemType Directory -Force -Path $OffsitePath | Out-Null
    }

    $serviceIdentity = Get-SqlServiceIdentity -Instance $ServerInstance
    & icacls.exe $BackupRoot /grant "${serviceIdentity}:(OI)(CI)M" | Out-Host
    if ($LASTEXITCODE -ne 0) {
        throw "No fue posible conceder al servicio SQL Server acceso a $BackupRoot."
    }
}

Invoke-Step "Configurar permisos SQL" {
    & $SqlCmdPath -S $ServerInstance -E -C -b -i $permissionsScript
    if ($LASTEXITCODE -ne 0) {
        throw "No fue posible configurar los permisos de respaldo en SQL Server."
    }
}

Invoke-Step "Crear y verificar respaldo inicial" {
    & $backupScript `
        -ServerInstance $ServerInstance `
        -Database $Database `
        -BackupRoot $BackupRoot `
        -OffsitePath $OffsitePath `
        -RetentionDays $RetentionDays `
        -SqlCmdPath $SqlCmdPath
    if ($LASTEXITCODE -ne 0) {
        throw "El respaldo inicial fallo; la tarea diaria no sera creada."
    }
}

Invoke-Step "Programar respaldo diario" {
    & $taskScript `
        -TaskName $TaskName `
        -ScriptPath $backupScript `
        -ServerInstance $ServerInstance `
        -Database $Database `
        -BackupRoot $BackupRoot `
        -OffsitePath $OffsitePath `
        -RetentionDays $RetentionDays `
        -DailyTime $DailyTime
}

$statusPath = Join-Path $BackupRoot "estado_ultimo_respaldo.json"
Write-Host "Respaldos activados correctamente." -ForegroundColor Green
Write-Host "Estado: $statusPath"
Write-Host "IMPORTANTE: guarde una copia separada de keys\backup.key; sin ella no podra restaurar."
