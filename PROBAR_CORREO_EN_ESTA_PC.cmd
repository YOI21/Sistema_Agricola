@echo off
setlocal
cd /d "%~dp0"
echo Diagnostico de Gmail para Sistema Agricola
echo.
powershell.exe -NoProfile -ExecutionPolicy Bypass -File "%~dp0Diagnosticar_Correo_Sistema_Agricola.ps1"
echo.
pause
endlocal
