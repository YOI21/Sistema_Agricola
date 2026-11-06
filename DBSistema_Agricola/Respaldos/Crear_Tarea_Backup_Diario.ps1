param(
    [string]$TaskName = "Sistema Agricola - Backup Diario",
    [string]$ScriptPath = "",
    [string]$ServerInstance = ".\SQLEXPRESS",
    [string]$Database = "DBSistema_Agricola",
    [string]$BackupRoot = "$env:USERPROFILE\Documents\Sistema_Agricola_Respaldos",
    [string]$OffsitePath = "",
    [int]$RetentionDays = 14,
    [string]$DailyTime = "22:00"
)

$ErrorActionPreference = "Stop"

if ([string]::IsNullOrWhiteSpace($ScriptPath)) {
    $ScriptPath = Join-Path $PSScriptRoot "Backup_Diario_Sistema_Agricola.ps1"
}

if (-not (Test-Path -LiteralPath $ScriptPath)) {
    throw "No se encontro el script de respaldo: $ScriptPath"
}

if ($RetentionDays -lt 1 -or $RetentionDays -gt 3650) {
    throw "RetentionDays debe estar entre 1 y 3650 dias."
}

$parsedTime = [DateTime]::MinValue
if (-not [DateTime]::TryParseExact(
        $DailyTime,
        "HH:mm",
        [Globalization.CultureInfo]::InvariantCulture,
        [Globalization.DateTimeStyles]::None,
        [ref]$parsedTime)) {
    throw "DailyTime debe usar el formato HH:mm, por ejemplo 22:00."
}

$argument = @(
    "-NoProfile",
    "-ExecutionPolicy", "Bypass",
    "-File", "`"$ScriptPath`"",
    "-ServerInstance", "`"$ServerInstance`"",
    "-Database", "`"$Database`"",
    "-BackupRoot", "`"$BackupRoot`"",
    "-RetentionDays", $RetentionDays
)

if (-not [string]::IsNullOrWhiteSpace($OffsitePath)) {
    $argument += @("-OffsitePath", "`"$OffsitePath`"")
}

$argument = $argument -join " "

$action = New-ScheduledTaskAction -Execute "powershell.exe" -Argument $argument
$trigger = New-ScheduledTaskTrigger -Daily -At $parsedTime
$settings = New-ScheduledTaskSettingsSet `
    -AllowStartIfOnBatteries `
    -DontStopIfGoingOnBatteries `
    -StartWhenAvailable `
    -MultipleInstances IgnoreNew

Register-ScheduledTask `
    -TaskName $TaskName `
    -Action $action `
    -Trigger $trigger `
    -Settings $settings `
    -Description "Backup diario cifrado de DBSistema_Agricola con verificacion y retencion." `
    -Force

Write-Host "Tarea programada creada: $TaskName"
Write-Host "Hora diaria: $DailyTime"
Write-Host "Respaldo local: $BackupRoot"
Write-Host "Copia externa: $OffsitePath"

$task = Get-ScheduledTask -TaskName $TaskName -ErrorAction Stop
if ($task.State -eq "Disabled") {
    throw "La tarea fue creada, pero quedo deshabilitada."
}

Write-Host "Estado de la tarea: $($task.State)"
