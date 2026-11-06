@echo off
setlocal
cd /d "%~dp0"
echo Configuracion de Gmail para Sistema Agricola
echo.
set /p "GMAIL_REMITENTE=Ingrese el correo Gmail remitente: "
if "%GMAIL_REMITENTE%"=="" (
    echo Debe indicar un correo Gmail.
    exit /b 1
)
powershell.exe -NoProfile -ExecutionPolicy Bypass -File "%~dp0Configurar_Correo_Sistema_Agricola.ps1" -GmailRemitente "%GMAIL_REMITENTE%"
echo.
pause
endlocal
