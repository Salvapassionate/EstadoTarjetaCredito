--para entra aBD
--use TarjetasCredito
-- Procedimiento almacenado para crear una tarjeta
CREATE PROCEDURE CrearTarjeta
	@Cuenta NVARCHAR(MAX),
    @Nombre NVARCHAR(MAX),
	@Apellido NVARCHAR(MAX)
AS
BEGIN
     INSERT INTO Tarjetas (Cuenta,Nombre,Apellido)
    VALUES (@Cuenta ,@Nombre,@Apellido)
END
GO

-- Procedimiento almacenado para obtener la lista de tarjetas
CREATE PROCEDURE ObtenerListaTarjetas
AS
BEGIN
    SELECT * FROM TARJETAS
END
GO
