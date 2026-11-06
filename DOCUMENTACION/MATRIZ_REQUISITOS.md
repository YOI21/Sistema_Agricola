# Matriz de requisitos

## Requisitos funcionales

| ID | Requisito | Actor | Prioridad | Criterio de aceptación | Estado |
|---|---|---|---|---|---|
| RF-01 | Registrar el primer administrador | Propietario | Alta | Si no existe administrador, el sistema muestra el registro inicial y almacena credenciales seguras | Implementado |
| RF-02 | Autenticar administrador o agricultor | Ambos | Alta | Credenciales válidas crean sesión; intentos fallidos producen bloqueo temporal | Implementado |
| RF-03 | Recuperar contraseña por correo | Ambos | Alta | Se envía un código temporal al correo verificado y se permite establecer una nueva contraseña | Implementado |
| RF-04 | Gestionar agricultores sin duplicados | Administrador | Alta | Usuario y cédula son únicos; crear y actualizar respetan validaciones | Implementado |
| RF-05 | Gestionar terrenos y condiciones | Administrador | Alta | Se registran área, suelo, pH, humedad, drenaje, pendiente, lluvia, riego y zona | Implementado |
| RF-06 | Recomendar cultivos mediante reglas agrícolas | Ambos | Alta | La recomendación muestra criterios, advertencias y fuentes institucionales | Implementado |
| RF-07 | Registrar siembras controlando área disponible | Ambos | Alta | No se exceden las hectáreas disponibles ni se ocupa dos veces la misma área | Implementado |
| RF-08 | Registrar y finalizar cosechas | Ambos | Alta | La cosecha se asocia a una siembra, actualiza su estado y conserva consistencia transaccional | Implementado |
| RF-09 | Consultar control de plagas | Ambos | Media | La consulta inicia vacía y muestra recomendaciones después de buscar | Implementado |
| RF-10 | Gestionar alertas y notificaciones | Ambos | Alta | Se crean, completan, anulan y notifican alertas según permisos | Implementado |
| RF-11 | Gestionar trabajadores sin acceso al sistema | Administrador | Media | Cédula y correo duplicados se rechazan; se administra rol laboral y sueldo | Implementado |
| RF-12 | Gestionar inventario y movimientos | Administrador | Alta | Entradas y salidas actualizan existencias, costos y alertas de stock | Implementado |
| RF-13 | Gestionar maquinaria y asignaciones | Administrador | Alta | Se registra, actualiza, asigna, devuelve y elimina maquinaria según dependencias | Implementado |
| RF-14 | Registrar mantenimiento de equipos | Administrador | Media | Se controlan costo, proveedor, fecha, horómetro y próximo mantenimiento | Implementado |
| RF-15 | Registrar producción e integración financiera | Ambos | Alta | La producción usa una cosecha disponible y registra su efecto financiero atómicamente | Implementado |
| RF-16 | Gestionar movimientos financieros | Administrador | Alta | Ingresos, gastos, pendientes y balance se calculan con totales verificables | Implementado |
| RF-17 | Generar reportes | Administrador | Media | Los reportes filtran, totalizan, exportan e imprimen con paginación | Implementado |
| RF-18 | Configurar sistema y SMTP | Administrador | Alta | La clave de aplicación se cifra por usuario de Windows y puede probarse el envío | Implementado |
| RF-19 | Respaldar y restaurar la base | Administrador técnico | Alta | El respaldo puede restaurarse en una base temporal y superar `DBCC CHECKDB` | Implementado |
| RF-20 | Autorizar y auditar operaciones sensibles | Sistema | Alta | Cada escritura valida permiso y registra usuario, operación, fecha y resultado | Implementado |

## Requisitos no funcionales

| ID | Requisito | Medida verificable | Evidencia | Estado |
|---|---|---|---|---|
| RNF-01 | Seguridad de contraseñas | PBKDF2, política mínima y cambio inicial | `PoliticaContrasenaTests` | Implementado |
| RNF-02 | Mínimo privilegio SQL | La cuenta no pertenece a roles administrativos ni escribe tablas directamente | `BaseDatosIntegracionTests` | Implementado |
| RNF-03 | Atomicidad | Escrituras críticas usan transacción, `XACT_ABORT` y reversión | `ContratosSqlTests` | Implementado |
| RNF-04 | Esquema versionado | La aplicación exige esquema 12 | `EsquemaBaseDatosTests` | Implementado |
| RNF-05 | Errores comprensibles | La UI muestra código de seguimiento y registra detalle técnico | `RegistroErrores` y manejador UI | Implementado |
| RNF-06 | Portabilidad SMTP | Solo se agrega la clave de aplicación en cada PC | `CorreoTests` | Implementado |
| RNF-07 | Usabilidad visual | 1366×768, escalado 125 % y 150 %, sin solapes interactivos | `FormulariosSmokeTests` | Implementado |
| RNF-08 | Separación por capas | UI no referencia reglas agrícolas ni clientes SQL | `DependenciasArquitecturaTests` y `DependenciasUiTests` | Implementado |
| RNF-09 | Pruebas automatizadas | Compilación y pruebas ejecutables desde línea de comandos | `Sistema_Agricola.Tests` y `Sistema_Agricola.UI.Tests` | Implementado |
| RNF-10 | Entrega reproducible | SDK, versión, SQL oficial, Git y CI definidos | Verificador de entrega y workflow CI | Implementado |
| RNF-11 | Cobertura suficiente | Aumentar progresivamente la cobertura desde la línea base de 12.09 % | Cobertura Cobertura XML | En progreso |
| RNF-12 | Instalación asistida | Instalador versionado y probado en PC limpia | Pendiente por decisión del proyecto | Pendiente |

## Trazabilidad

Los identificadores `RF` y `RNF` se reutilizan en los casos de uso y evidencias
de prueba. Toda modificación funcional debe actualizar esta matriz y agregar o
ajustar una prueba automatizada asociada.
