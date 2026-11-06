# Casos de uso

## Actores

- **Administrador:** propietario con control total de registros sensibles.
- **Agricultor:** usuario operativo para siembras, cosechas, producción, alertas y seguimiento.
- **Administrador técnico:** responsable de SQL Server, despliegue y respaldos.
- **Sistema:** procesos automáticos de validación, auditoría y notificación.

## Catálogo

| ID | Caso de uso | Actor principal | Requisitos |
|---|---|---|---|
| CU-01 | Registrar administrador inicial | Administrador | RF-01 |
| CU-02 | Iniciar sesión | Administrador, Agricultor | RF-02 |
| CU-03 | Recuperar contraseña | Administrador, Agricultor | RF-03 |
| CU-04 | Registrar agricultor | Administrador | RF-04 |
| CU-05 | Gestionar terreno | Administrador | RF-05 |
| CU-06 | Evaluar recomendación agrícola | Administrador, Agricultor | RF-06 |
| CU-07 | Registrar o actualizar siembra | Administrador, Agricultor | RF-07 |
| CU-08 | Registrar cosecha | Administrador, Agricultor | RF-08 |
| CU-09 | Consultar control de plagas | Administrador, Agricultor | RF-09 |
| CU-10 | Gestionar alertas | Administrador, Agricultor | RF-10 |
| CU-11 | Gestionar trabajadores | Administrador | RF-11 |
| CU-12 | Gestionar inventario | Administrador | RF-12 |
| CU-13 | Gestionar maquinaria | Administrador | RF-13, RF-14 |
| CU-14 | Registrar producción | Administrador, Agricultor | RF-15 |
| CU-15 | Gestionar finanzas | Administrador | RF-16 |
| CU-16 | Generar reportes | Administrador | RF-17 |
| CU-17 | Configurar correo | Administrador | RF-18 |
| CU-18 | Respaldar y restaurar | Administrador técnico | RF-19 |

## Flujos principales

### CU-02: Iniciar sesión

- Precondición: existe un usuario activo y el esquema está actualizado.
- Flujo: ingresar usuario y contraseña; validar hash y bloqueo; crear sesión; aplicar permisos; abrir pantalla principal.
- Alternativas: credenciales incorrectas incrementan intentos; cuenta bloqueada informa tiempo restante; primer ingreso exige cambio de contraseña.
- Postcondición: sesión activa con rol y usuario auditables.

### CU-05: Gestionar terreno

- Precondición: sesión de administrador.
- Flujo: capturar identificación, área y condiciones agroclimáticas; validar rangos; guardar; mostrar terreno disponible.
- Alternativas: cédula lógica del terreno inexistente, área inválida o datos incompletos cancelan la escritura.
- Postcondición: terreno disponible para recomendaciones y siembras.

### CU-06: Evaluar recomendación agrícola

- Precondición: cultivo y terreno seleccionados.
- Flujo: obtener zona, suelo, pH, humedad, drenaje, pendiente, lluvia, riego, fecha y variedad; evaluar reglas; mostrar puntuación, criterios, advertencias y fuentes.
- Alternativas: información insuficiente devuelve “requiere revisión”; condiciones críticas desfavorables muestran recomendación no favorable.
- Postcondición: orientación visible sin modificar el terreno ni reemplazar el criterio profesional.

### CU-07: Registrar o actualizar siembra

- Precondición: sesión autorizada y terreno con hectáreas disponibles.
- Flujo: seleccionar terreno y cultivo; indicar área y fechas; validar compatibilidad y disponibilidad; guardar siembra, fertilización y ocupación en transacción.
- Alternativas: área excedida, tipo incompatible o transacción fallida revierten toda la operación.
- Postcondición: siembra activa y hectáreas ocupadas correctamente.

### CU-08: Registrar cosecha

- Precondición: existe una siembra activa apta para cosecha.
- Flujo: seleccionar cultivo; capturar cantidad, calidad, costo y fecha; guardar cosecha; finalizar o actualizar estado de la siembra.
- Alternativas: datos inválidos o fallo financiero revierten la transacción.
- Postcondición: cosecha disponible para producción y reportes.

### CU-12: Gestionar inventario

- Precondición: sesión de administrador.
- Flujo: registrar producto único; capturar entrada o salida; validar existencia; actualizar stock y movimiento financiero cuando corresponda.
- Alternativas: código duplicado o salida mayor al stock producen error y reversión.
- Postcondición: inventario, movimientos y costos consistentes.

### CU-13: Gestionar maquinaria

- Precondición: sesión de administrador.
- Flujo: registrar máquina; asignarla a un trabajador; devolverla; registrar mantenimiento; actualizar estado.
- Alternativas: una máquina asignada no se elimina hasta retirar la asignación; códigos y series duplicados se rechazan.
- Postcondición: historial de disponibilidad, asignación y mantenimiento actualizado.

### CU-14: Registrar producción

- Precondición: cosecha con cantidad disponible.
- Flujo: seleccionar cosecha; indicar producto, cantidad, calidad, costo y venta; guardar producción; actualizar disponibilidad y finanzas.
- Alternativas: cantidad excesiva o fallo financiero revierten la operación.
- Postcondición: producción y movimiento financiero relacionados por referencia única.

### CU-18: Respaldar y restaurar

- Precondición: cuenta técnica autorizada y destino de respaldo accesible por SQL Server.
- Flujo: crear respaldo con verificación; copiar fuera del equipo; restaurar con nombre temporal; ejecutar `DBCC CHECKDB`; validar versión; eliminar base temporal.
- Alternativas: ruta inaccesible, respaldo corrupto o versión incompatible detienen el proceso y generan evidencia de error.
- Postcondición: respaldo restaurable demostrado sin afectar la base operativa.
