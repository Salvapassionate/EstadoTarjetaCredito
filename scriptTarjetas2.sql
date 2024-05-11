use TarjetasCredito
drop table Compras
drop table Pagos
drop table Tarjetas

select*from Compras
select*from Pagos
select*from Tarjetas

-- Crear tabla Tarjetas
CREATE TABLE Tarjetas (
    Id INT PRIMARY KEY,
    Cuenta VARCHAR(50),
    Nombre VARCHAR(50),
    Apellido VARCHAR(50)
);

-- Crear tabla Compras
CREATE TABLE Compras (
    Id INT PRIMARY KEY,
    Id_Tarjeta INT,
    Fecha DATE,
    Descripcion VARCHAR(100),
    Monto DECIMAL(10,2),
    FOREIGN KEY (Id_Tarjeta) REFERENCES Tarjetas(Id)
);

-- Crear tabla Pagos
CREATE TABLE Pagos (
    Id INT PRIMARY KEY,
    Id_Compra INT,
    Monto_Pago DECIMAL(10,2),
    Fecha DATE,
    Descripcion VARCHAR(100),
    Cargo DECIMAL(10,2),
    Abono DECIMAL(10,2),
    FOREIGN KEY (Id_Compra) REFERENCES Compras(Id)
);