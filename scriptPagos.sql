--para entra aBD
--use TarjetasCredito
--Procedimiento para CrearPago
--use TarjetasCredito
CREATE PROCEDURE CrearPago
    @IdCompra INT,
    @IdTarjeta INT,
	@Cuenta NVARCHAR(MAX),
    @Nombre NVARCHAR(MAX),
	@Apellido NVARCHAR(MAX),
    @Fecha DATETIME,
    @Descripcion NVARCHAR(MAX),
    @Monto DECIMAL,
	@Abono DECIMAl
AS
BEGIN
     INSERT INTO Pagos(IdCompra,IdTarjeta,Cuenta,Nombre,Apellido,Fecha ,Descripcion, Monto,Abono)
    VALUES (@IdCompra,@IdTarjeta,@Cuenta ,@Nombre,@Apellido,@Fecha, @Descripcion, @Monto,@Abono)
END
GO

-- Procedimiento almacenado para obtener la lista de pagos
CREATE PROCEDURE ObtenerListaPagos
AS
BEGIN
    SELECT  
	    p.IdPago,
        p.IdCompra,
		c.IdTarjeta,
		p.Cuenta,
        p.Nombre,
        p.Apellido,
        p.Fecha,
        p.Descripcion,
        p.Monto,
		p.Abono
    FROM 
        Pagos p
    LEFT JOIN 
        Compras c ON c.IdCompra = c.IdCompra
	LEFT JOIN 
        Tarjetas t ON c.IdTarjeta = t.IdTarjeta
END
GO

use TarjetasCredito