# Pruebas automatizadas

La solucion separa las pruebas en cuatro niveles:

1. Unitarias y de contratos: no requieren SQL Server.
2. Interfaz estructural: construye formularios WinForms en un hilo STA.
3. Integracion: usan la conexion restringida de la aplicacion y revierten sus datos de prueba.
4. Administracion y restauracion: requieren credenciales administrativas explicitas.
5. Visuales locales: muestran formularios fuera de pantalla, aplican escalado y
   validan capturas mediante `DrawToBitmap` en una sesion de escritorio Windows.

Una prueba que no tenga su configuracion externa aparece como **omitida** con la causa.
Nunca debe terminar como aprobada sin ejecutar sus comprobaciones.

## Ejecucion normal

Desde la carpeta de la solucion:

```powershell
dotnet restore .\Sistema_Agricola.sln
dotnet build .\Sistema_Agricola.sln --configuration Debug --no-restore
dotnet test .\Sistema_Agricola.sln --configuration Debug --no-build
```

## Pruebas aptas para integracion continua

GitHub Hosted Runner no proporciona una sesion de escritorio interactiva
confiable para `Form.Show()` y `DrawToBitmap`. Por eso la integracion continua
ejecuta las pruebas deterministas y excluye solo la categoria `Visual`:

```powershell
dotnet test .\Sistema_Agricola.sln --configuration Release --no-build `
  --filter "Categoria!=Integracion&Categoria!=AdministracionBD&Categoria!=Restauracion&Categoria!=Visual"
```

## Escalado y renderizado visual local

Estas pruebas deben ejecutarse en una sesion Windows iniciada, como evidencia
separada para 1366x768 con escalado de 125 % y 150 %:

```powershell
dotnet test .\Sistema_Agricola.UI.Tests\Sistema_Agricola.UI.Tests.csproj `
  --configuration Release `
  --filter "Categoria=Visual"
```

Excluirlas del runner hospedado no las marca como aprobadas ni omitidas: se
ejecutan y documentan localmente porque dependen de un escritorio real.

## Base de datos restringida

Primero configure la conexion de la aplicacion con
`Configurar_Conexion_Restringida.ps1`. Luego ejecute:

```powershell
$env:SISTEMA_AGRICOLA_RUN_DB_TESTS = "1"
dotnet test .\Sistema_Agricola.Tests\Sistema_Agricola.Tests.csproj `
  --filter "Categoria=Integracion"
Remove-Item Env:SISTEMA_AGRICOLA_RUN_DB_TESTS
```

Estas pruebas comprueban la version del esquema, el rol de minimo privilegio,
la falta de acceso directo a tablas y la reversion ante un codigo de inventario
duplicado.

## Catalogo administrativo

La cadena debe apuntar a `DBSistema_Agricola` y pertenecer a una cuenta que pueda
leer los metadatos del catalogo. No se debe guardar la clave en el repositorio.

```powershell
$env:SISTEMA_AGRICOLA_ADMIN_CONNECTION_STRING = `
  "Server=.\SQLEXPRESS;Database=DBSistema_Agricola;Integrated Security=True;TrustServerCertificate=True"

dotnet test .\Sistema_Agricola.Tests\Sistema_Agricola.Tests.csproj `
  --filter "Categoria=AdministracionBD"

Remove-Item Env:SISTEMA_AGRICOLA_ADMIN_CONNECTION_STRING
```

## Restauracion real del respaldo

Esta prueba crea una base temporal, ejecuta `DBCC CHECKDB`, valida tablas,
procedimientos y version del esquema, y elimina la base al terminar. El archivo
`.bak` debe ser accesible para el servicio de SQL Server. Use una instancia de
pruebas, no el servidor de produccion.

```powershell
$env:SISTEMA_AGRICOLA_ADMIN_CONNECTION_STRING = `
  "Server=.\SQLEXPRESS;Database=master;Integrated Security=True;TrustServerCertificate=True"
$env:SISTEMA_AGRICOLA_BACKUP_PATH = "C:\Respaldos\Sistema_Agricola.bak"

dotnet test .\Sistema_Agricola.Tests\Sistema_Agricola.Tests.csproj `
  --filter "Categoria=Restauracion"

Remove-Item Env:SISTEMA_AGRICOLA_ADMIN_CONNECTION_STRING
Remove-Item Env:SISTEMA_AGRICOLA_BACKUP_PATH
```

## Cobertura

```powershell
dotnet test .\Sistema_Agricola.sln --configuration Debug --no-build `
  --collect:"XPlat Code Coverage"
```

Los archivos `coverage.cobertura.xml` quedan dentro de `TestResults`. La
cobertura es una medida de apoyo: cada prueba debe comprobar resultados y reglas
del negocio, no ejecutar lineas solamente para aumentar un porcentaje.
