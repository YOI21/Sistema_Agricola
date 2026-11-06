[CmdletBinding()]
param(
    [string]$GmailRemitente = ""
)

$ErrorActionPreference = "Stop"

function Set-ConfigProperty {
    param(
        [Parameter(Mandatory = $true)]
        [object]$Config,

        [Parameter(Mandatory = $true)]
        [string]$Name,

        [AllowNull()]
        [object]$Value
    )

    $property = $Config.PSObject.Properties[$Name]
    if ($null -eq $property) {
        $Config | Add-Member -MemberType NoteProperty -Name $Name -Value $Value
        return
    }

    $property.Value = $Value
}

$configDirectory = Join-Path $env:APPDATA "Sistema_Agricola"
$configPath = Join-Path $configDirectory "configuracion_sistema.json"

if (-not (Test-Path -LiteralPath $configPath)) {
    New-Item -ItemType Directory -Path $configDirectory -Force | Out-Null
    [pscustomobject]@{
        NombreSistema = "Sistema Agricola"
        NombreFinca = ""
        Propietario = ""
        Telefono = ""
        Direccion = ""
        Moneda = "RD$"
        DiasAlertaAnticipacion = 7
        StockMinimoGeneral = 10
        NotificarAlertasGmail = $true
        CrearAlertasAutomaticas = $true
        GmailRemitente = ""
        GmailClaveAplicacion = ""
        GmailClaveAplicacionCifrada = ""
        NombreRemitente = "Sistema Agricola"
        SmtpHost = "smtp.gmail.com"
        SmtpPuerto = 587
        SmtpSsl = $true
        FechaActualizacion = (Get-Date).ToString("o")
    } | ConvertTo-Json -Depth 10 |
        Set-Content -LiteralPath $configPath -Encoding UTF8
}

$config = Get-Content -LiteralPath $configPath -Raw | ConvertFrom-Json
if ($null -eq $config) {
    throw "El archivo de configuracion esta vacio o no es valido."
}

$email = $GmailRemitente.Trim()
if ([string]::IsNullOrWhiteSpace($email)) {
    $email = ([string]$config.GmailRemitente).Trim()
}

if ([string]::IsNullOrWhiteSpace($email)) {
    $email = (Read-Host "Gmail remitente").Trim()
}
else {
    Write-Host "Gmail remitente conservado: $email" -ForegroundColor Cyan
}

try {
    $mailAddress = [System.Net.Mail.MailAddress]::new($email)
}
catch {
    throw "El correo ingresado no tiene un formato valido."
}

if ($mailAddress.Host -notin @("gmail.com", "googlemail.com")) {
    throw "El correo remitente debe ser una cuenta Gmail."
}

$securePassword = Read-Host "Pegue la clave de aplicacion de 16 caracteres" -AsSecureString
$passwordPointer = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($securePassword)
$passwordBytes = $null

try {
    $appPassword = [Runtime.InteropServices.Marshal]::PtrToStringBSTR($passwordPointer)
    $appPassword = $appPassword.Replace(" ", "")

    if ($appPassword.Length -ne 16) {
        throw "La clave de aplicacion debe tener 16 caracteres."
    }

    if (-not ("System.Security.Cryptography.ProtectedData" -as [type])) {
        Add-Type -AssemblyName System.Security
    }

    $passwordBytes = [Text.Encoding]::UTF8.GetBytes($appPassword)
    $entropy = [Text.Encoding]::UTF8.GetBytes(
        "Sistema_Agricola.GmailClaveAplicacion.v1")
    $encryptedBytes = [Security.Cryptography.ProtectedData]::Protect(
        $passwordBytes,
        $entropy,
        [Security.Cryptography.DataProtectionScope]::CurrentUser)

    Set-ConfigProperty $config "GmailRemitente" $email
    Set-ConfigProperty $config "GmailClaveAplicacion" ""
    Set-ConfigProperty $config "GmailClaveAplicacionCifrada" ([Convert]::ToBase64String($encryptedBytes))
    Set-ConfigProperty $config "NombreRemitente" "Sistema Agricola"
    Set-ConfigProperty $config "SmtpHost" "smtp.gmail.com"
    Set-ConfigProperty $config "SmtpPuerto" 587
    Set-ConfigProperty $config "SmtpSsl" $true
    Set-ConfigProperty $config "FechaActualizacion" ((Get-Date).ToString("o"))

    $backupPath = $configPath + ".bak"
    $temporaryPath = $configPath + ".tmp"
    Copy-Item -LiteralPath $configPath -Destination $backupPath -Force
    $config | ConvertTo-Json -Depth 10 |
        Set-Content -LiteralPath $temporaryPath -Encoding UTF8
    Move-Item -LiteralPath $temporaryPath -Destination $configPath -Force

    Write-Host "Correo del Sistema Agricola configurado correctamente." -ForegroundColor Green
    Write-Host "La clave quedo cifrada para este usuario de Windows." -ForegroundColor Green
    Write-Host "Cierre y abra el programa antes de probar el envio." -ForegroundColor Yellow
}
finally {
    if ($null -ne $passwordBytes) {
        [Array]::Clear($passwordBytes, 0, $passwordBytes.Length)
    }

    [Runtime.InteropServices.Marshal]::ZeroFreeBSTR($passwordPointer)
}
