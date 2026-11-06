USE DBSistema_Agricola;
GO

IF OBJECT_ID('dbo.Esquema_Version', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Esquema_Version
    (
        Version INT NOT NULL CONSTRAINT PK_Esquema_Version PRIMARY KEY,
        Nombre VARCHAR(150) NOT NULL,
        Aplicada_En DATETIME NOT NULL CONSTRAINT DF_Esquema_Version_Aplicada DEFAULT GETDATE()
    );
END;
GO

IF OBJECT_ID('dbo.Administrador', 'U') IS NOT NULL
BEGIN
    UPDATE U
       SET Rol = 'Administrador'
      FROM dbo.Usuario U
      INNER JOIN dbo.Administrador A ON A.ID_Usuario = U.ID_Usuario
     WHERE U.Rol <> 'Administrador';
END;
GO

IF OBJECT_ID('dbo.Agricultor', 'U') IS NOT NULL
BEGIN
    UPDATE U
       SET Rol = 'Agricultor'
      FROM dbo.Usuario U
      INNER JOIN dbo.Agricultor A ON A.ID_Usuario = U.ID_Usuario
     WHERE U.Rol <> 'Agricultor'
       AND NOT EXISTS (
           SELECT 1
             FROM dbo.Administrador ADM
            WHERE ADM.ID_Usuario = U.ID_Usuario
       );
END;
GO

UPDATE dbo.Usuario
   SET Rol = 'Agricultor'
 WHERE Rol IS NULL
    OR LTRIM(RTRIM(Rol)) = ''
    OR Rol NOT IN ('Administrador', 'Agricultor');
GO

IF OBJECT_ID('dbo.CK_Usuario_Rol_Acceso', 'C') IS NULL
BEGIN
    ALTER TABLE dbo.Usuario WITH CHECK
    ADD CONSTRAINT CK_Usuario_Rol_Acceso
    CHECK (Rol IN ('Administrador', 'Agricultor'));
END;
GO

MERGE dbo.Esquema_Version AS destino
USING (SELECT 3 AS Version, 'Acceso limitado a administrador y agricultor' AS Nombre) AS origen
   ON destino.Version = origen.Version
WHEN MATCHED THEN
    UPDATE SET Nombre = origen.Nombre, Aplicada_En = GETDATE()
WHEN NOT MATCHED THEN
    INSERT (Version, Nombre) VALUES (origen.Version, origen.Nombre);
GO

PRINT 'Migracion 003 aplicada: acceso de usuarios.';
GO
