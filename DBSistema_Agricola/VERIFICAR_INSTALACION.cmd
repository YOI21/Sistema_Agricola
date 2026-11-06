@echo off
setlocal
title Verificacion - Sistema Agricola
powershell.exe -NoProfile -ExecutionPolicy Bypass -File "%~dp0Verificar_Instalacion.ps1"
echo.
pause
endlocal
