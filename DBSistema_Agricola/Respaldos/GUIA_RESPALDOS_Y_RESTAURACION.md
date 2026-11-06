# Respaldos y recuperacion - Sistema Agricola

## Objetivo

Mantener respaldos diarios cifrados de `DBSistema_Agricola`, con retencion definida, copia fuera del equipo principal, verificacion automatica y prueba documentada de restauracion.

## Archivos

- `Backup_Diario_Sistema_Agricola.ps1`: crea, verifica, cifra y copia el respaldo.
- `Restaurar_Respaldo_Sistema_Agricola.ps1`: descifra y restaura un respaldo.
- `Crear_Tarea_Backup_Diario.ps1`: crea una tarea programada diaria de Windows.
- `Activar_Respaldos.ps1`: configura permisos, ejecuta el primer respaldo y crea la tarea diaria.

## Politica recomendada

- Frecuencia: diaria.
- Hora recomendada: 10:00 PM, cuando el sistema no este en uso.
- Retencion local: 14 dias.
- Copia externa: unidad USB, disco externo, NAS o carpeta sincronizada en nube.
- Cifrado: AES-256-CBC + HMAC-SHA256.
- Verificacion: `RESTORE VERIFYONLY WITH CHECKSUM` antes y despues del cifrado.
- Prueba de restauracion: minimo una vez al mes.

## Primera configuracion

Abra PowerShell como administrador y ejecute:

```powershell
Set-ExecutionPolicy RemoteSigned -Scope CurrentUser
cd "RUTA_DEL_PROYECTO\DBSistema_Agricola\Respaldos"

.\Activar_Respaldos.ps1 `
  -ServerInstance ".\SQLEXPRESS" `
  -Database "DBSistema_Agricola" `
  -BackupRoot "$env:USERPROFILE\Documents\Sistema_Agricola_Respaldos" `
  -OffsitePath "D:\Respaldos_Sistema_Agricola" `
  -RetentionDays 14 `
  -DailyTime "22:00"
```

Cambie `D:\Respaldos_Sistema_Agricola` por una ubicacion que no sea el mismo disco principal. Puede ser un disco externo, una carpeta de red o una carpeta sincronizada con nube.

## Ejecutar un respaldo manual

```powershell
.\Backup_Diario_Sistema_Agricola.ps1 `
  -ServerInstance ".\SQLEXPRESS" `
  -Database "DBSistema_Agricola" `
  -BackupRoot "$env:USERPROFILE\Documents\Sistema_Agricola_Respaldos" `
  -OffsitePath "D:\Respaldos_Sistema_Agricola" `
  -RetentionDays 14
```

El primer respaldo crea una clave en:

```text
%USERPROFILE%\Documents\Sistema_Agricola_Respaldos\keys\backup.key
```

Guarde una copia de esa clave fuera del equipo. Sin esa clave no se pueden restaurar los respaldos cifrados.

## Verificacion diaria

Revise los logs en:

```text
%USERPROFILE%\Documents\Sistema_Agricola_Respaldos\logs
```

Un respaldo valido debe terminar con:

```text
Respaldo finalizado correctamente.
```

El archivo `estado_ultimo_respaldo.json` indica la fecha, la ruta y si la ultima ejecucion fue exitosa. Cada `.bak.aes` incluye una huella `.sha256` y la copia externa se compara automaticamente con ella.

## Prueba mensual de restauracion

Use una base de prueba para no tocar produccion:

```powershell
.\Restaurar_Respaldo_Sistema_Agricola.ps1 `
  -EncryptedBackupPath "C:\Respaldos\Sistema_Agricola\backups\DBSistema_Agricola_YYYYMMDD_HHMMSS.bak.aes" `
  -ServerInstance "DESKTOP-C4NV8AB\SQLEXPRESS" `
  -TargetDatabase "DBSistema_Agricola_RestoreTest" `
  -KeyPath "C:\Respaldos\Sistema_Agricola\keys\backup.key" `
  -ReplaceExisting
```

Despues de restaurar, abra SQL Server Management Studio y ejecute:

```sql
USE DBSistema_Agricola_RestoreTest;
SELECT COUNT(*) AS Usuarios FROM dbo.Usuario;
SELECT TOP 10 * FROM dbo.Esquema_Version ORDER BY Version DESC;
```

Si las consultas responden, la restauracion de prueba fue exitosa.

## Restaurar produccion

Use esto solo si la base real se perdio o quedo corrupta.

1. Cierre el sistema en todas las PCs.
2. Verifique que tiene el archivo `.bak.aes` y `backup.key`.
3. Ejecute:

```powershell
.\Restaurar_Respaldo_Sistema_Agricola.ps1 `
  -EncryptedBackupPath "D:\Respaldos_Sistema_Agricola\DBSistema_Agricola_YYYYMMDD_HHMMSS.bak.aes" `
  -ServerInstance "DESKTOP-C4NV8AB\SQLEXPRESS" `
  -TargetDatabase "DBSistema_Agricola" `
  -KeyPath "C:\Respaldos\Sistema_Agricola\keys\backup.key" `
  -ReplaceExisting `
  -RestoreProduction
```

4. Abra el sistema y confirme que puede iniciar sesion.
5. Registre en una nota la fecha, archivo usado y resultado.

## Recomendaciones

- No guarde la unica copia en `C:`.
- No guarde `backup.key` solamente junto a los respaldos.
- Pruebe restauracion mensualmente.
- Revise los logs semanalmente.
- En produccion, use un usuario SQL con permisos de backup/restauracion solo para estas tareas administrativas.
