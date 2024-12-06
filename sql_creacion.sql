-- 1
-- Crear base de datos y usarla
CREATE DATABASE TRUMAN;

--2
USE TRUMAN;

--3
-- Crear tablas sin claves foráneas
CREATE TABLE Categorias (
    idCategoria INT PRIMARY KEY IDENTITY(1,1),
    descripcion VARCHAR(50) NOT NULL,
    activo BIT NOT NULL DEFAULT 1,
    fechaRegistro DATETIME DEFAULT GETDATE()
);

CREATE TABLE Productos (
    idProducto INT PRIMARY KEY IDENTITY(1,1),
    idCategoria INT NULL, 
    nombreProducto VARCHAR(100) NOT NULL,
    descripcionProducto VARCHAR(255),
    precio DECIMAL(10,2) NOT NULL,
    stock INT NOT NULL CHECK (stock >= 0),
    alertaStock INT DEFAULT 10,
    urlImagen VARCHAR(500),
    activo BIT NOT NULL DEFAULT 1,
    fechaRegistro DATETIME DEFAULT GETDATE()
);

CREATE TABLE Clientes (
    idCliente INT PRIMARY KEY IDENTITY(1,1),
    nombreCliente VARCHAR(50) NOT NULL,
    apellidosCliente VARCHAR(50),
    emailCliente VARCHAR(100) UNIQUE NOT NULL,
    contrasena VARCHAR(255) NOT NULL,
    direccion VARCHAR(255),
    telefono VARCHAR(20),
    activo BIT NOT NULL DEFAULT 1,
    fechaRegistro DATETIME DEFAULT GETDATE()
);

CREATE TABLE Carritos (
    idCarrito INT PRIMARY KEY IDENTITY(1,1),
    idCliente INT NOT NULL,
    fechaCreacion DATETIME DEFAULT GETDATE()
);

CREATE TABLE DetalleCarrito (
    idDetalle INT PRIMARY KEY IDENTITY(1,1),
    idCarrito INT NOT NULL,
    idProducto INT NOT NULL,
    cantidad INT NOT NULL CHECK (cantidad > 0),
    subtotal DECIMAL(10,2) NOT NULL
);

CREATE TABLE Pedidos (
    idPedido INT PRIMARY KEY IDENTITY(1,1),
    idCliente INT NULL,  -- Permitir NULL para referenciar con SET NULL
    fechaPedido DATETIME DEFAULT GETDATE(),
    estadoPedido VARCHAR(20) NOT NULL CHECK (estadoPedido IN ('Pendiente', 'En Proceso', 'Enviado', 'Entregado', 'Cancelado')),
    metodoPago VARCHAR(20) NOT NULL CHECK (metodoPago IN ('Tarjeta', 'Transferencia', 'Paypal')),
    total DECIMAL(10,2) NOT NULL CHECK (total >= 0)
);

CREATE TABLE DetallePedido (
    idDetalle INT PRIMARY KEY IDENTITY(1,1),
    idPedido INT NOT NULL,
    idProducto INT NOT NULL,
    cantidad INT NOT NULL CHECK (cantidad > 0),
    precio DECIMAL(10,2) NOT NULL
);

CREATE TABLE Resenas (
    idResena INT PRIMARY KEY IDENTITY(1,1),
    idProducto INT NULL,
    idCliente INT NULL,
    calificacion INT NOT NULL CHECK (calificacion BETWEEN 1 AND 5),
    comentario VARCHAR(255),
    fechaResena DATETIME DEFAULT GETDATE()
);

-- Agregar las claves foráneas con las acciones de eliminación correctas
ALTER TABLE Productos ADD CONSTRAINT FK_Productos_Categorias FOREIGN KEY (idCategoria) REFERENCES Categorias(idCategoria) ON DELETE SET NULL;
ALTER TABLE Carritos ADD CONSTRAINT FK_Carritos_Clientes FOREIGN KEY (idCliente) REFERENCES Clientes(idCliente) ON DELETE CASCADE;
ALTER TABLE DetalleCarrito ADD CONSTRAINT FK_DetalleCarrito_Carritos FOREIGN KEY (idCarrito) REFERENCES Carritos(idCarrito) ON DELETE CASCADE;
ALTER TABLE DetalleCarrito ADD CONSTRAINT FK_DetalleCarrito_Productos FOREIGN KEY (idProducto) REFERENCES Productos(idProducto);
ALTER TABLE Pedidos ADD CONSTRAINT FK_Pedidos_Clientes FOREIGN KEY (idCliente) REFERENCES Clientes(idCliente) ON DELETE SET NULL;
ALTER TABLE DetallePedido ADD CONSTRAINT FK_DetallePedido_Pedidos FOREIGN KEY (idPedido) REFERENCES Pedidos(idPedido) ON DELETE CASCADE;
ALTER TABLE DetallePedido ADD CONSTRAINT FK_DetallePedido_Productos FOREIGN KEY (idProducto) REFERENCES Productos(idProducto);
ALTER TABLE Resenas ADD CONSTRAINT FK_Resenas_Productos FOREIGN KEY (idProducto) REFERENCES Productos(idProducto) ON DELETE CASCADE;
ALTER TABLE Resenas ADD CONSTRAINT FK_Resenas_Clientes FOREIGN KEY (idCliente) REFERENCES Clientes(idCliente) ON DELETE SET NULL;
