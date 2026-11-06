# Evidencias de pruebas

## Entorno validado

- Fecha de corte: 2026-11-06
- Sistema operativo: Windows
- Framework objetivo: .NET 8
- SDK reproducible: 9.0.203
- Base de datos: SQL Server Express, esquema 12
- Configuración: Debug local; Release en integración continua

## Ejecuciones registradas

| Evidencia | Comando o procedimiento | Resultado del corte |
|---|---|---|
| Compilación completa | `dotnet build Sistema_Agricola.sln --configuration Release --no-restore` | Aprobada: 0 errores y 0 advertencias |
| Pruebas aptas para CI | Filtro sin `Integracion`, `AdministracionBD` ni `Restauracion` | Aprobadas: 236 pruebas, 0 fallidas y 0 omitidas |
| Integración SQL restringida | Variable `SISTEMA_AGRICOLA_RUN_DB_TESTS=1` y filtro `Categoria=Integracion` | Aprobadas: 4 pruebas de esquema, permisos, duplicados y reversión |
| Restauración real | Filtro `Categoria=Restauracion` con `.bak` temporal | Aprobada: 1 prueba con versión 12 y `DBCC CHECKDB` |
| Escalado visual | `FormulariosSmokeTests` | Aprobado a 125 % y 150 %, sin formularios vacíos ni texto de botones cortado |
| Arquitectura | `DependenciasArquitecturaTests` y `DependenciasUiTests` | Aprobada: UI sin referencia a reglas agrícolas ni clientes SQL |
| Integridad de entrega | `scripts/Verificar_Entrega_Academica.ps1` | Aprobada: SQL oficial íntegro, sin publicaciones, ZIP ni correo personal fijo |

## Trazabilidad de pruebas

| Requisito | Evidencia automatizada principal |
|---|---|
| RF-02, RF-03 | `PoliticaContrasenaTests`, `CorreoTests` |
| RF-06 | `ReglasAgricolasTests`, `CalendarioAgricolaTests`, `DependenciasArquitecturaTests` |
| RF-07, RF-08, RF-12, RF-15 | `ContratosSqlTests`, `BaseDatosIntegracionTests` |
| RF-13, RF-14, RF-16 | `ResumenesYReportesTests` |
| RF-17 | `ResumenesYReportesTests`, `FormulariosSmokeTests` |
| RF-19 | `RestauracionRespaldoTests` |
| RF-20 | `AutorizacionOperacionTests`, `SeguridadTests` |
| RNF-02 | `BaseDatosIntegracionTests` |
| RNF-03 | `ContratosSqlTests` |
| RNF-07 | `FormulariosSmokeTests` |
| RNF-08 | `DependenciasArquitecturaTests`, `DependenciasUiTests` |
| RNF-10 | Workflow CI y verificador de entrega |

## Línea base y limitaciones

- La cobertura medida antes de este corte fue 12.09 % de líneas. Debe aumentar en iteraciones posteriores.
- Las pruebas visuales verifican construcción, escalado, solapes, texto y tabulación; no sustituyen pruebas de aceptación con usuarios.
- Las pruebas SQL administrativas y de restauración requieren variables externas y no se ejecutan automáticamente con secretos en CI.
- El instalador, el manual técnico y el manual de usuario permanecen fuera del alcance de esta versión por decisión explícita del proyecto.

Los archivos `.trx` y `coverage.cobertura.xml` generados por CI se conservan como
artefactos durante 30 días y constituyen evidencia verificable de cada ejecución.
