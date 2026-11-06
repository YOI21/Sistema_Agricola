# Correo de Gmail en otra PC

La clave del Sistema Agricola se cifra con DPAPI para el usuario de Windows que
la guarda. Una clave cifrada en una PC no funciona en otra: en cada equipo se
debe pegar una vez la clave de aplicacion.

## Configurar el correo

1. Cierre el Sistema Agricola.
2. Abra PowerShell dentro de la carpeta publicada.
3. Ejecute:

```powershell
powershell.exe -ExecutionPolicy Bypass -File .\Configurar_Correo_Sistema_Agricola.ps1
```

4. En el paquete entregado, abra `CONFIGURAR_CORREO_EN_ESTA_PC.cmd`. El Gmail
   del Sistema Agricola ya esta preseleccionado y solo se solicita la clave.
5. Pegue la clave de aplicacion de Google de 16 caracteres cuando se solicite.
   La clave no se muestra en pantalla.
6. Abra nuevamente el programa y pruebe el envio.

La configuracion correcta es:

```text
Servidor: smtp.gmail.com
Puerto: 587
SSL/TLS: activado
```

La version actual intenta primero el puerto 587 con STARTTLS. Si la red cierra
esa negociacion, reintenta automaticamente por el puerto 465 con SSL directo.
No es necesario cambiar manualmente el servidor, el puerto ni SSL.

Tambien puede hacerlo dentro del programa en `Configuracion > Correo y
notificaciones`: pegue la clave y pulse `Guardar y probar`. Si Gmail acepta el
mensaje, la clave queda guardada automaticamente en esa PC.

No use la contraseña normal de Gmail. La clave de aplicacion debe haber sido
creada en la misma cuenta escrita como remitente.

## Diagnosticar el envio

Con el programa cerrado, ejecute:

```powershell
powershell.exe -ExecutionPolicy Bypass -File .\Diagnosticar_Correo_Sistema_Agricola.ps1
```

La herramienta ejecuta el mismo servicio MailKit utilizado por el programa,
prueba los puertos seguros 587 y 465 y realiza un envio real hacia el Gmail
remitente. Ya no utiliza el componente antiguo `System.Net.Mail` que mostraba
el mensaje generico `Failure sending mail`.

No imprime la clave ni el contenido cifrado. El ultimo mensaje indica si el
problema corresponde a la configuracion, Gmail, firewall, antivirus, DNS o TLS.

## Importante

La clave de aplicacion del sistema de fotografia no se modifica. Utilice la
clave independiente que fue creada para el Sistema Agricola.
