CREATE DATABASE TRUMAN;
USE TRUMAN;


CREATE TABLE Usuarios (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(20),
    contrasena VARCHAR(20)
);


CREATE TABLE Productos (
    Id INT PRIMARY KEY IDENTITY(1,1), -- Id único autoincremental
    Categoria NVARCHAR(100) NOT NULL, -- Categoría del producto
    Nombre NVARCHAR(100) NOT NULL,    -- Nombre del producto
    Descripcion NVARCHAR(MAX),        -- Descripción del producto
    Cantidad INT NOT NULL DEFAULT 0   -- Cantidad en inventario, valor por defecto 0
);



INSERT INTO Usuarios (nombre, contrasena) VALUES ('user_1', '123');
INSERT INTO Usuarios (nombre, contrasena) VALUES ('user_2', '456');


INSERT INTO Productos (Categoria, Nombre, Descripcion, Cantidad)
VALUES 
('Electrónica', 'Audífonos Bluetooth', 'Audífonos inalámbricos con cancelación de ruido y 10 horas de batería.', 50),
('Hogar', 'Cafetera Eléctrica', 'Cafetera con capacidad para 12 tazas y apagado automático.', 20);
