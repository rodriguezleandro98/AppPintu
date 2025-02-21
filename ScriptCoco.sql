USE master
GO
CREATE DATABASE CocoPintureria
GO
USE CocoPintureria
GO

/* Tablas */

CREATE TABLE Permisos (
	ID INT NOT NULL IDENTITY(1,1),
    NombrePermiso VARCHAR(20) NOT NULL,
    PRIMARY KEY(ID)
);
GO

CREATE TABLE Marcas (
	ID INT NOT NULL IDENTITY(1,1),
    NombreMarca VARCHAR(30) NOT NULL,
    Activo BIT NOT NULL default 1,
    PRIMARY KEY(ID)
);
GO

CREATE TABLE Tamanio (
	ID INT NOT NULL IDENTITY(1,1),
    Tamanio VARCHAR(30) NOT NULL,
	PRIMARY KEY(ID)
);
GO

CREATE TABLE Categorias (
	ID INT NOT NULL IDENTITY(1,1),
    NombreCategoria VARCHAR(30) NOT NULL,
    Activo BIT NOT NULL default 1,
    PRIMARY KEY(ID)
);
GO

CREATE TABLE Imagenes(
	ID BIGINT NOT NULL IDENTITY(1,1),
	ImagenURL VARCHAR(1000),
	Activo BIT NOT NULL DEFAULT 1
	PRIMARY KEY(ID)
);
GO

CREATE TABLE Contactos (
    ID INT NOT NULL IDENTITY(1,1),
    Nombre VARCHAR(50) NULL,
    Apellido VARCHAR(50) NULL,
    CorreoElectronico VARCHAR(50) NULL,
    Telefono VARCHAR(15) NULL,
    Direccion VARCHAR(100) NULL, -- Para proveedores
    PRIMARY KEY (ID)
);
GO

CREATE TABLE Usuarios(
	ID INT NOT NULL IDENTITY(1,1),
	IDPermiso INT NOT NULL,
	IDContacto INT NOT NULL,
	IDImagen BIGINT NULL,
	NombreUsuario VARCHAR(30) NOT NULL,
	Contrasenia VARCHAR(30) NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
	Activo BIT NOT NULL default 1,
	PRIMARY KEY(ID),
	FOREIGN KEY (IDPermiso) REFERENCES Permisos(ID),
	FOREIGN KEY (IDImagen) REFERENCES Imagenes(ID),
	FOREIGN KEY (IDContacto) REFERENCES Contactos(ID)
);
GO

--CREATE TABLE Clientes(
--	ID BIGINT NOT NULL IDENTITY(1,1),
--	DNI INT NOT NULL,
--	Nombre VARCHAR(30) NOT NULL,
--	Apellido VARCHAR(30) NOT NULL,
--	Direccion VARCHAR(50) NOT NULL,
--	Telefono VARCHAR(15) NOT NULL,
--	Correo VARCHAR(50) NOT NULL,
--	Fecha_reg DATETIME DEFAULT GETDATE(),
--	Activo BIT NOT NULL default 1,
--	PRIMARY KEY(ID),
--);
--GO

CREATE TABLE Proveedores(
	ID INT NOT NULL IDENTITY(1,1),
	CUIT BIGINT NOT NULL,
	Siglas VARCHAR(5) NOT NULL UNIQUE,
	IDContacto INT NOT NULL,
	Activo BIT NOT NULL default 1,
	PRIMARY KEY(ID),
	FOREIGN KEY (IDContacto) REFERENCES Contactos(ID)
);
GO

CREATE TABLE Productos(
	ID BIGINT NOT NULL IDENTITY(1,1),
	Nombre VARCHAR(30) NOT NULL,
	Descripcion VARCHAR(150) NULL,
	IDMarca INT NOT NULL,
	IDCategoria INT NOT NULL,
	IDImagen BIGINT NULL,
	Stock_Actual INT NOT NULL, 
	Stock_Minimo INT NOT NULL,
	Activo BIT NOT NULL default 1,
	PRIMARY KEY(ID),
	FOREIGN KEY (IDMarca) REFERENCES Marcas(ID),
	FOREIGN KEY (IDCategoria) REFERENCES Categorias(ID),
	FOREIGN KEY (IDImagen) REFERENCES Imagenes(ID)
);
GO

CREATE TABLE Tamanio_x_producto(
	IDProducto BIGINT NOT NULL,
	IDTamanio INT NOT NULL,
	Precio_Venta MONEY NOT NULL,
	PRIMARY KEY(ID),
	FOREIGN KEY (IDProducto) REFERENCES Productos(ID),
	FOREIGN KEY (IDTamanio) REFERENCES Tamanio(ID)
);
GO

CREATE TABLE Producto_x_Proveedores(
	IDProducto BIGINT NOT NULL,
	IDProveedor INT NOT NULL,
	Precio_Compra MONEY NOT NULL,
	Precio_Venta MONEY NOT NULL,
	Porcentaje_Ganancia decimal NOT NULL,
	PRIMARY KEY(IDProducto,IDProveedor),
	FOREIGN KEY (IDProducto) REFERENCES Productos(ID),
	FOREIGN KEY (IDProveedor) REFERENCES Proveedores(ID)
);
GO

CREATE SEQUENCE NroFacturaSeq
    START WITH 1
    INCREMENT BY 1;
GO

CREATE TABLE Ventas(
    ID BIGINT NOT NULL IDENTITY(1,1),
    Total MONEY NOT NULL,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    Nro_Factura BIGINT NOT NULL DEFAULT NEXT VALUE FOR NroFacturaSeq,
    PRIMARY KEY(ID),
);
GO

CREATE TABLE Productos_x_venta(
	ID BIGINT NOT NULL IDENTITY(1,1),
	IDVenta BIGINT NOT NULL,
	IDProducto BIGINT NOT NULL,
	Cantidad INT NOT NULL,
	Precio_UnitarioV MONEY NOT NULL,
	Subtotal MONEY NOT NULL,
	PRIMARY KEY(ID,IDVenta,IDProducto),
	FOREIGN KEY (IDProducto) REFERENCES Productos(ID),
	FOREIGN KEY (IDVenta) REFERENCES Ventas(ID)
);
GO

CREATE SEQUENCE NroReciboSeq
    START WITH 1
    INCREMENT BY 1;
GO

CREATE TABLE Compras(
    ID BIGINT NOT NULL IDENTITY(1,1),
    Nro_Recibo BIGINT NOT NULL DEFAULT NEXT VALUE FOR NroReciboSeq,
    IDProveedor INT NOT NULL,
    FechaCreacion DATETIME DEFAULT GETDATE(),
	FechaEntrega DATETIME NULL,
    Total MONEY NOT NULL,
	Estado bit default 0,
    PRIMARY KEY(ID),
    FOREIGN KEY (IDProveedor) REFERENCES Proveedores(ID)
);
GO

CREATE TABLE Productos_x_compra(
	ID BIGINT NOT NULL IDENTITY(1,1),
	IDCompra BIGINT NOT NULL,
	IDProducto BIGINT NOT NULL,
	Cantidad INT NOT NULL,
	CantidadVieja INT NOT NULL,
	Precio_UnitarioC MONEY NOT NULL,
	Subtotal MONEY NOT NULL,
	PRIMARY KEY(ID,IDCompra,IDProducto),
	FOREIGN KEY (IDProducto) REFERENCES Productos(ID),
	FOREIGN KEY (IDCompra) REFERENCES Compras(ID)
);
GO

/* Vistas */

CREATE VIEW VW_ListaMarcas AS 
SELECT * FROM Marcas
GO

CREATE VIEW VW_ListaTamanio AS 
SELECT * FROM Tamanio
GO

CREATE VIEW VW_ListaPermisos AS
SELECT * FROM Permisos
GO

CREATE VIEW VW_ListaCategorias AS
SELECT * FROM Categorias
GO

CREATE VIEW VW_ListaProveedores AS
SELECT * FROM Proveedores WHERE Activo = 1
GO

--CREATE VIEW VW_ListaClientes AS
--SELECT * FROM Clientes
--GO

CREATE VIEW VW_ListaUsuarios AS
SELECT U.ID,
    U.IDPermiso,
    P.NombrePermiso,
    U.NombreUsuario,
    U.Contrasenia,
    U.Activo,
    U.Nombre,
    U.Apellido,
    U.CorreoElectronico,
    U.Telefono,
    U.FechaCreacion,
    U.IDImagen,
    I.ImagenURL
FROM Usuarios AS U
INNER JOIN Permisos AS P ON P.ID = U.IDPermiso
LEFT JOIN Imagenes AS I ON I.ID = U.IDImagen;
GO

CREATE VIEW VW_ListaProductos AS
SELECT P.ID, P.Nombre, P.Descripcion, P.Activo
FROM Productos AS P
GO

CREATE VIEW VW_TraerUltimo
AS
SELECT TOP 1 ID FROM Compras ORDER BY FechaCreacion DESC
GO

CREATE VIEW VW_TraerUltimaVenta
AS
SELECT TOP 1 ID FROM Ventas ORDER BY FechaCreacion DESC
GO
--ESTO TIENE QUE SER UN SP, PORQUE TENGO QUE ENVIARLE EL ID DEL TAMAÑO Y DEL PROVEEDOR.
--CREATE OR ALTER VIEW VW_ALLProducto AS
--SELECT P.ID, P.Nombre, P.Descripcion, P.Stock_Actual, P.Stock_Minimo, PXP.Precio_Compra, PXP.Precio_Venta, PXP.Porcentaje_Ganancia, I.ImagenURL, M.NombreMarca, C.NombreCategoria,P.Activo FROM Productos AS P
--INNER JOIN Producto_x_Proveedores AS PXP ON PXP.IDProducto = P.ID
--INNER JOIN Imagenes AS I ON I.ID = P.IDImagen
--INNER JOIN Marcas AS M ON M.ID = P.IDMarca
--INNER JOIN Categorias AS C ON C.ID = P.IDCategoria
--GO

/* SP */

-- Marcas -- 

CREATE PROCEDURE SP_Alta_Marca(
    @NombreMarca VARCHAR(30)
)
AS
BEGIN
    INSERT INTO Marcas (NombreMarca) VALUES (@NombreMarca)
END
GO

CREATE PROCEDURE SP_ActivarMarca(
	@ID INT
)
AS BEGIN
UPDATE Marcas SET Activo = 1 WHERE ID = @ID
END
GO

CREATE PROCEDURE SP_BajaMarca(
	@ID INT
)
AS
BEGIN 
UPDATE Marcas SET Activo = 0 WHERE ID = @ID
END
GO

CREATE PROCEDURE SP_ModificarMarca(
	@ID INT,
	@NombreMarca VARCHAR(30)
)
AS
BEGIN 
UPDATE Marcas SET NombreMarca = @NombreMarca WHERE ID = @ID
END
GO

CREATE PROCEDURE SP_ExisteMarca(
    @NombreMarca NVARCHAR(50)
)
AS
BEGIN
    SELECT COUNT(*)
    FROM Marcas
    WHERE NombreMarca COLLATE SQL_Latin1_General_CP1_CI_AI = @NombreMarca COLLATE SQL_Latin1_General_CP1_CI_AI
END
GO

CREATE PROCEDURE SP_ExisteNombreMarcaModificado(
    @NombreMarca NVARCHAR(50),
    @IDMarca INT
)
AS
BEGIN
    SELECT COUNT(*) 
    FROM Marcas 
    WHERE NombreMarca COLLATE SQL_Latin1_General_CP1_CI_AI = @NombreMarca COLLATE SQL_Latin1_General_CP1_CI_AI 
    AND Id <> @IDMarca
END
GO

CREATE PROCEDURE SP_ObtenerMarcasConMasProductos
AS
BEGIN
    -- Obtener la cantidad máxima de productos asociados a una marca
    WITH CTE_CantidadProductos AS (
        SELECT 
            M.Id,
            M.NombreMarca,
            COUNT(P.Id) AS CantidadProductos
        FROM Marcas M
        JOIN Productos P ON P.IdMarca = M.Id
        WHERE P.Activo = 1
        GROUP BY M.Id, M.NombreMarca
    )
    SELECT 
        Id,
        NombreMarca,
        CantidadProductos
    FROM CTE_CantidadProductos
    WHERE CantidadProductos = (SELECT MAX(CantidadProductos) FROM CTE_CantidadProductos);
END
GO

CREATE PROCEDURE SP_MarcasSinProductos
AS
BEGIN
    SELECT 
        M.Id,
        M.NombreMarca
    FROM Marcas M
    LEFT JOIN Productos P ON P.IDMarca = M.Id
    WHERE P.ID IS NULL;
END
GO

CREATE PROCEDURE SP_MarcasConProductosBajoStock
AS
BEGIN
    SELECT 
        M.ID AS MarcaID,
        M.NombreMarca,
        P.ID AS ProductoID,
        P.Nombre AS NombreProducto,
        P.Stock_Actual,
        P.Stock_Minimo
    FROM Marcas M
    JOIN Productos P ON M.ID = P.IDMarca
    WHERE P.Stock_Actual < P.Stock_Minimo
    ORDER BY M.NombreMarca, P.Nombre; -- Ordenado por marca y producto
END
GO

CREATE PROCEDURE SP_ConteoMarcasPorEstado
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Activo,
        COUNT(*) AS Total
    FROM Marcas
    GROUP BY Activo;
END
GO

-- Categoria --

CREATE PROCEDURE SP_Alta_Categoria(
    @NombreCategoria VARCHAR(30)
)
AS
BEGIN
    INSERT INTO Categorias(NombreCategoria) VALUES (@NombreCategoria)
END
GO

CREATE PROCEDURE SP_ActivarCategoria(
	@ID INT
)
AS BEGIN
UPDATE Categorias SET Activo = 1 WHERE ID = @ID
END
GO

CREATE PROCEDURE SP_BajaCategoria(
	@ID INT
)
AS
BEGIN 
UPDATE Categorias SET Activo = 0 WHERE ID = @ID
END
GO

CREATE PROCEDURE SP_ModificarCategoria(
	@ID INT,
	@NombreCategoria VARCHAR(30)
)
AS
BEGIN 
UPDATE Categorias SET NombreCategoria = @NombreCategoria WHERE ID = @ID
END
GO

CREATE PROCEDURE SP_ExisteCategoria(
    @NombreCategoria NVARCHAR(50)
)
AS
BEGIN
    SELECT COUNT(*)
    FROM Categorias
    WHERE NombreCategoria COLLATE SQL_Latin1_General_CP1_CI_AI = @NombreCategoria COLLATE SQL_Latin1_General_CP1_CI_AI
END
GO

CREATE PROCEDURE SP_ExisteNombreCategoriaModificado(
    @NombreCategoria NVARCHAR(50),
    @IDCategoria INT
)
AS
BEGIN
    SELECT COUNT(*) 
    FROM Categorias 
    WHERE NombreCategoria COLLATE SQL_Latin1_General_CP1_CI_AI = @NombreCategoria COLLATE SQL_Latin1_General_CP1_CI_AI 
    AND Id <> @IDCategoria
END
GO

CREATE PROCEDURE SP_ObtenerCategoriasConMasProductos
AS
BEGIN
    -- Obtener la cantidad máxima de productos asociados a una categoría
    WITH CTE_CantidadProductos AS (
        SELECT 
            C.Id,
            C.NombreCategoria,
            COUNT(P.Id) AS CantidadProductos
        FROM Categorias C
        JOIN Productos P ON P.IdCategoria = C.Id
        WHERE P.Activo = 1
        GROUP BY C.Id, C.NombreCategoria
    )
    SELECT 
        Id,
        NombreCategoria,
        CantidadProductos
    FROM CTE_CantidadProductos
    WHERE CantidadProductos = (SELECT MAX(CantidadProductos) FROM CTE_CantidadProductos);
END
GO

CREATE PROCEDURE SP_CategoriasSinProductos
AS
BEGIN
    SELECT 
        C.Id,
        C.NombreCategoria
    FROM Categorias C
    LEFT JOIN Productos P ON P.IdCategoria = C.Id
    WHERE P.Id IS NULL;
END
GO

CREATE PROCEDURE SP_CategoriasConProductosBajoStock
AS
BEGIN
    SELECT 
        C.ID AS CategoriaID,
        C.NombreCategoria,
        P.ID AS ProductoID,
        P.Nombre AS NombreProducto,
        P.Stock_Actual,
        P.Stock_Minimo
    FROM Categorias C
    JOIN Productos P ON C.ID = P.IDCategoria
    WHERE P.Stock_Actual < P.Stock_Minimo
    ORDER BY C.NombreCategoria, P.Nombre; -- Ordenado por categoría y producto
END
GO

CREATE PROCEDURE SP_ConteoCategoriasPorEstado
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Activo,
        COUNT(*) AS Total
    FROM Categorias
    GROUP BY Activo;
END
GO

CREATE PROCEDURE SP_ReporteCategorias
AS
BEGIN
    SET NOCOUNT ON;

    -- Subconsulta para el conteo de categorías activas e inactivas
    SELECT 
        'Categorias Activas' AS Descripcion,
        COUNT(*) AS Total
    FROM Categorias
    WHERE Activo = 1

    UNION ALL

    SELECT 
        'Categorias Inactivas' AS Descripcion,
        COUNT(*) AS Total
    FROM Categorias
    WHERE Activo = 0

    UNION ALL

    -- Subconsulta para categorías sin productos
    SELECT 
        'Categorias Sin Productos' AS Descripcion,
        COUNT(*) AS Total
    FROM Categorias C
    LEFT JOIN Productos P ON P.IdCategoria = C.Id
    WHERE P.Id IS NULL;
END
GO

-- Permiso -- 

CREATE PROCEDURE SP_Alta_Permiso(
    @NombrePermiso VARCHAR(30)
)
AS
BEGIN
    INSERT INTO Permisos(NombrePermiso) VALUES (@NombrePermiso)
END
GO

CREATE PROCEDURE SP_ModificarPermiso(
	@ID INT,
	@NombrePermiso VARCHAR(30)
)
AS
BEGIN 
UPDATE Permisos SET NombrePermiso = @NombrePermiso WHERE ID = @ID
END
GO

-- Proveedor --

CREATE PROCEDURE SP_Alta_Proveedor(
	@CUIT BIGINT,
    @Siglas VARCHAR(5),
	@Nombre VARCHAR(30),
	@Direccion VARCHAR(100),
	@Correo VARCHAR(50),
	@Telefono VARCHAR(15)
)
AS
BEGIN
    INSERT INTO Proveedores(CUIT,Siglas,Nombre,Direccion,Correo,Telefono) VALUES (@CUIT,@Siglas,@Nombre,@Direccion,@Correo,@Telefono)
END
GO

CREATE PROCEDURE SP_ActivarProveedor(
	@ID INT
)
AS BEGIN
UPDATE Proveedores SET Activo = 1 WHERE ID = @ID
END
GO

CREATE PROCEDURE SP_BajaProveedor(
	@ID INT
)
AS
BEGIN 
UPDATE Proveedores SET Activo = 0 WHERE ID = @ID
END
GO

CREATE PROCEDURE SP_ModificarProveedor(
	@ID INT,
	@CUIT BIGINT,
	@Siglas VARCHAR(5),
	@Nombre VARCHAR(30),
	@Direccion VARCHAR(100),
	@Correo VARCHAR(50),
	@Telefono VARCHAR(15),
	@Activo BIT
)
AS
BEGIN 
UPDATE Proveedores SET CUIT = @CUIT, Siglas = @Siglas, Nombre = @Nombre, Direccion = @Direccion, Correo = @Correo, Telefono = @Telefono, Activo = @Activo
WHERE ID = @ID
END
GO

CREATE PROCEDURE SP_ExisteCUITProveedor(
	@CUIT BIGINT
)
AS
BEGIN
    SELECT COUNT(*) FROM Proveedores WHERE CUIT = @CUIT
END
GO

CREATE PROCEDURE SP_ExisteCUITProveedorModificado(
	@CUIT BIGINT,
	@IDProveedor INT
)
AS
BEGIN
    SELECT COUNT(*) 
    FROM Proveedores 
    WHERE CUIT = @CUIT AND ID <> @IDProveedor
END
GO

CREATE PROCEDURE SP_Proveedor_de_producto(
	@IDPRODUCTO BIGINT
)
AS
BEGIN
	SELECT ID,Siglas FROM Proveedores WHERE ID IN (SELECT IDProveedor FROM Producto_x_Proveedores WHERE IDProducto = @IDPRODUCTO)
END
GO

CREATE PROCEDURE SP_ConteoProveedoresPorEstado
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Activo,
        COUNT(*) AS Total
    FROM Proveedores
    GROUP BY Activo;
END
GO

-- Imagen --

CREATE PROCEDURE SP_Nueva_Imagen(
    @imagenURL VARCHAR(1000),
    @UltimoID BIGINT OUTPUT
)
AS
BEGIN
    BEGIN TRY
        -- Insertar la URL de la imagen
        INSERT INTO Imagenes(ImagenURL) 
        VALUES (@imagenURL);

        -- Obtener el último ID insertado (ID de la imagen)
        SET @UltimoID = SCOPE_IDENTITY();

        -- Verificar que el ID no sea NULL
        IF @UltimoID IS NULL
        BEGIN
            THROW 50001, 'Error al obtener el ID de la imagen insertada', 1;
        END
    END TRY
    BEGIN CATCH
        -- En caso de error, devolver NULL en @UltimoID
        SET @UltimoID = NULL;
        THROW;
    END CATCH
END
GO

-- Usuario --

CREATE PROCEDURE SP_Alta_Usuario
(
    @IDPermiso INT,
    @NombreUsuario VARCHAR(30),
    @Contrasenia VARCHAR(30),
    @Nombre VARCHAR(30) = NULL,
    @Apellido VARCHAR(30) = NULL,
    @CorreoElectronico VARCHAR(50) = NULL,
    @Telefono VARCHAR(15) = NULL,
    @ImagenURL VARCHAR(1000) = NULL
)
AS
BEGIN
    DECLARE @IDImagen BIGINT;

    -- Insertar la imagen y obtener el ID
    EXEC SP_Nueva_Imagen @ImagenURL, @IDImagen OUTPUT;

    -- Insertar el usuario con el ID de la imagen
    INSERT INTO Usuarios (
        IDPermiso,
        NombreUsuario,
        Contrasenia,
        Nombre,
        Apellido,
        CorreoElectronico,
        Telefono,
        IDImagen
    ) 
    VALUES (
        @IDPermiso,
        @NombreUsuario,
        @Contrasenia,
        @Nombre,
        @Apellido,
        @CorreoElectronico,
        @Telefono,
        @IDImagen
    );
END;
GO

CREATE PROCEDURE SP_BajaUsuario(
	@ID INT
)
AS
BEGIN 
UPDATE Usuarios SET Activo = 0 WHERE ID = @ID
END
GO

CREATE PROCEDURE SP_ModificarUsuario
(
    @ID INT,
    @IDPermiso INT,
    @NombreUsuario VARCHAR(30),
    @Contrasenia VARCHAR(30),
    @Nombre VARCHAR(30) = NULL,
    @Apellido VARCHAR(30) = NULL,
    @CorreoElectronico VARCHAR(50) = NULL,
    @Telefono VARCHAR(15) = NULL,
    @ImagenURL VARCHAR(1000) = NULL,
    @Activo BIT
)
AS
BEGIN
    DECLARE @IDImagen BIGINT;

    -- Insertar o actualizar la imagen
    EXEC SP_Nueva_Imagen @ImagenURL, @IDImagen OUTPUT;

    -- Actualizar el usuario con el ID de la nueva imagen
    UPDATE Usuarios SET 
        IDPermiso = @IDPermiso,
        NombreUsuario = @NombreUsuario,
        Contrasenia = @Contrasenia,
        Nombre = @Nombre,
        Apellido = @Apellido,
        CorreoElectronico = @CorreoElectronico,
        Telefono = @Telefono,
        IDImagen = @IDImagen,
        Activo = @Activo
    WHERE ID = @ID;
END;
GO

-- Producto --

CREATE PROCEDURE SP_BajaProducto(
	@ID BIGINT
)
AS
BEGIN 
UPDATE Productos SET Activo = 0 WHERE ID = @ID
END
GO

CREATE PROCEDURE SP_TieneProductosActivosCategoria(
	@IdCategoria INT
)
AS
BEGIN
    SELECT COUNT(*)
    FROM Productos
    WHERE IDCategoria = @IdCategoria AND Activo = 1
END
GO

CREATE PROCEDURE SP_TieneProductosActivosMarca(
	@IdMarca INT
)
AS
BEGIN
    SELECT COUNT(*)
    FROM Productos
    WHERE IDMarca = @IdMarca AND Activo = 1
END
GO

CREATE PROCEDURE SP_ObtenerMarcasConMasProductos
AS
BEGIN
    -- Obtener la cantidad máxima de productos asociados a una marca
    WITH CTE_CantidadProductos AS (
        SELECT 
            M.Id,
            M.NombreMarca,
            COUNT(P.Id) AS CantidadProductos
        FROM Marcas M
        JOIN Productos P ON P.IdMarca = M.Id
        WHERE P.Activo = 1
        GROUP BY M.Id, M.NombreMarca
    )
    SELECT 
        Id,
        NombreMarca,
        CantidadProductos
    FROM CTE_CantidadProductos
    WHERE CantidadProductos = (SELECT MAX(CantidadProductos) FROM CTE_CantidadProductos);
END
GO

CREATE PROCEDURE SP_ObtenerCategoriasConMasProductos
AS
BEGIN
    -- Obtener la cantidad máxima de productos asociados a una categoría
    WITH CTE_CantidadProductos AS (
        SELECT 
            C.Id,
            C.NombreCategoria,
            COUNT(P.Id) AS CantidadProductos
        FROM Categorias C
        JOIN Productos P ON P.IdCategoria = C.Id
        WHERE P.Activo = 1
        GROUP BY C.Id, C.NombreCategoria
    )
    SELECT 
        Id,
        NombreCategoria,
        CantidadProductos
    FROM CTE_CantidadProductos
    WHERE CantidadProductos = (SELECT MAX(CantidadProductos) FROM CTE_CantidadProductos);
END
GO

-- Compras --

CREATE TYPE ProductoCompraType AS TABLE
(
    IDProducto INT,
    Cantidad INT,
    Precio_UnitarioC MONEY,
    Subtotal MONEY
);
GO


CREATE PROCEDURE SP_RegistrarCompra
    @IDProveedor INT,
    @Productos ProductoCompraType READONLY, -- Tipo de tabla para los productos
    @Total MONEY
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Insertar la compra
        DECLARE @IDCompra BIGINT;
        INSERT INTO Compras (IDProveedor, FechaCreacion, Total)
        VALUES (@IDProveedor, GETDATE(), @Total);

        SET @IDCompra = SCOPE_IDENTITY();

        -- Insertar productos en Productos_x_Compra
		DECLARE @Subtotal MONEY
		SELECT @Subtotal = Cantidad * Precio_UnitarioC FROM @Productos 
        INSERT INTO Productos_x_Compra (IDCompra, IDProducto, Cantidad, Precio_UnitarioC, Subtotal)
        SELECT @IDCompra, IDProducto, Cantidad, Precio_UnitarioC, @Subtotal
        FROM @Productos;

        -- (Opcional) Actualizar el stock actual de los productos
        UPDATE p
        SET p.Stock_Actual = p.Stock_Actual + t.Cantidad
        FROM Productos p
        INNER JOIN @Productos t ON p.ID = t.IDProducto;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

CREATE PROCEDURE SP_Alta_Compra(
	@idproveedor int,
	@total money
)
AS
BEGIN
	INSERT INTO Compras(IDProveedor,FechaCreacion,FechaEntrega,Total) VALUES (@idproveedor,GETDATE(),DATEADD(DAY, 5, GETDATE()),@total)
END
GO

CREATE PROCEDURE SP_AgregarProductoCompra(
	@idcompra bigint,
	@idproducto bigint,
	@cantidad int,
	@cantidadvieja int,
	@preciounitario money,
	@subtotal money
)
AS
BEGIN
	INSERT INTO Productos_x_compra(IDCompra,IDProducto,Cantidad,CantidadVieja,Precio_UnitarioC,Subtotal)
	VALUES (@idcompra,@idproducto,@cantidad,@cantidadvieja,@preciounitario,@subtotal)
END
GO

CREATE PROCEDURE SP_ActualizarMontoEnCompra(
	@idcompra bigint,
	@total money
)
as
begin
	UPDATE Compras SET Total = @total WHERE ID = @idcompra
end
GO

CREATE OR ALTER PROCEDURE SP_ActualizarStock(
	@idproducto bigint,
	@stock int
)
as
begin
	UPDATE Productos SET Stock_Actual += @stock WHERE ID = @idproducto
end
GO

CREATE PROCEDURE SP_ConfirmarCompra(
	@idcompra BIGINT
)
AS 
BEGIN
	UPDATE Compras SET FechaEntrega = GETDATE(), Estado = 1 WHERE ID = @idcompra
END
GO

-- Ventas --

CREATE PROCEDURE SP_Alta_Venta(
	@idcliente bigint,
	@total money
)
AS
BEGIN
	INSERT INTO Ventas(IDCliente,Total,FechaCreacion) VALUES (@idcliente,@total,GETDATE())
END
GO

