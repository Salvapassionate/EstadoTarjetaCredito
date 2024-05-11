--para entra aBD
--use TarjetasCredito

CREATE PROCEDURE CrearCompra
  
    @IdTarjeta INT,
	@Cuenta NVARCHAR(MAX),
    @Nombre NVARCHAR(MAX),
	@Apellido NVARCHAR(MAX),
    @Fecha DATETIME,
    @Descripcion NVARCHAR(MAX),
    @Monto DECIMAL
AS
BEGIN
    INSERT INTO Compras (IdTarjeta,Cuenta,Nombre,Apellido,Fecha ,Descripcion, Monto)
    VALUES (@IdTarjeta,@Cuenta ,@Nombre,@Apellido,@Fecha, @Descripcion, @Monto)
END
GO
CREATE PROCEDURE ObtenerListaCompras
AS
BEGIN
    SELECT        
        c.IdCompra,
		t.IdTarjeta,
		c.Cuenta,
        c.Nombre,
        c.Apellido,
        c.Fecha,
        c.Descripcion,
        c.Monto
    FROM 
        Compras c
    LEFT JOIN 
        Tarjetas t ON c.IdTarjeta = t.IdTarjeta
END
GO

--Para obtener lista de procedimientos existentes
SELECT *
FROM INFORMATION_SCHEMA.ROUTINES
WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_SCHEMA = 'dbo';

use TarjetasCredito
select*from Tarjetas
select*from Compras
select*from Pagos