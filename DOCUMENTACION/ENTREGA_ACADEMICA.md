# Manifiesto de entrega académica

## Identificación

- Sistema: Sistema Agrícola
- Versión del software: 1.1.0
- Versión requerida del esquema: 12
- Etiqueta Git: `v1.1.0`
- Solución compilable: `Sistema_Agricola.sln`
- SDK fijado: .NET SDK 9.0.203

## Artefactos oficiales

| Artefacto | Ruta oficial |
|---|---|
| Código fuente | Raíz del repositorio |
| Solución .NET | `Sistema_Agricola.sln` |
| Base para instalación nueva | `DBSistema_Agricola/Script_Completo_Sistema_Agricola.sql` |
| Huella de integridad SQL | `DBSistema_Agricola/Script_Completo_Sistema_Agricola.sha256` |
| Migraciones para bases existentes | `DBSistema_Agricola/Migraciones/` |
| Configuración de conexión restringida | `DBSistema_Agricola/Configurar_Conexion_Restringida.ps1` |
| Verificación automatizada de entrega | `scripts/Verificar_Entrega_Academica.ps1` |
| Integración continua | `.github/workflows/ci.yml` |
| Matriz de requisitos | `DOCUMENTACION/MATRIZ_REQUISITOS.md` |
| Casos de uso | `DOCUMENTACION/CASOS_DE_USO.md` |
| Evidencias de prueba | `DOCUMENTACION/EVIDENCIAS_PRUEBAS.md` |

El único SQL autorizado para crear una base nueva es
`Script_Completo_Sistema_Agricola.sql`. Las migraciones numeradas no son
instaladores alternativos: documentan y aplican cambios incrementales sobre una
base ya existente.

La huella SHA-256 se calcula sobre el contenido SQL canónico en UTF-8 y con
saltos de línea LF. De esta forma produce el mismo resultado en Windows y en
GitHub Actions sin modificar las instrucciones SQL.

## Elementos aplazados

Por decisión del propietario del proyecto, esta versión no incluye todavía:

- Instalador oficial de la aplicación.
- Manual técnico.
- Manual de usuario.

Esos elementos deben generarse en una versión posterior y no deben sustituirse
con ZIP o publicaciones manuales sin identificación.

## Reproducción de la validación

```powershell
.\scripts\Verificar_Entrega_Academica.ps1
dotnet restore .\Sistema_Agricola.sln
dotnet build .\Sistema_Agricola.sln --configuration Release --no-restore
dotnet test .\Sistema_Agricola.sln --configuration Release --no-build
```

Las pruebas que requieren SQL Server, permisos administrativos o un respaldo
real se ejecutan con las variables indicadas en
`DOCUMENTACION/PRUEBAS_AUTOMATIZADAS.md`.
