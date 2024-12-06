-- Insertar datos en la tabla Categorias
INSERT INTO Categorias (descripcion, activo) 
VALUES ('Electrónica', 1), 
       ('Ropa', 1), 
       ('Muebles', 1);

-- Insertar datos en la tabla Productos
INSERT INTO Productos (idCategoria, nombreProducto, descripcionProducto, precio, stock, alertaStock, urlImagen, activo) 
VALUES (1, 'Smartphone', 'Smartphone de última generación', 599.99, 50, 10, 'https://example.com/smartphone.jpg', 1),
       (2, 'Camiseta', 'Camiseta de algodón', 19.99, 200, 10, 'https://example.com/camiseta.jpg', 1),
       (3, 'Sofá', 'Sofá de 3 plazas', 899.99, 20, 5, 'https://example.com/sofa.jpg', 1);

-- Insertar datos en la tabla Clientes
INSERT INTO Clientes (nombreCliente, apellidosCliente, emailCliente, contrasena, direccion, telefono, activo) 
VALUES ('Juan', 'Pérez', 'juan.perez@example.com', 'password123', 'Calle Falsa 123', '555123456', 1),
       ('Ana', 'Gómez', 'ana.gomez@example.com', 'password456', 'Avenida Siempre Viva 456', '555654321', 1),
       ('Carlos', 'Rodríguez', 'carlos.rodriguez@example.com', 'password789', 'Calle Real 789', '555987654', 1);

-- Insertar datos en la tabla Carritos
INSERT INTO Carritos (idCliente) 
VALUES (1), (2), (3);

-- Insertar datos en la tabla DetalleCarrito
INSERT INTO DetalleCarrito (idCarrito, idProducto, cantidad, subtotal) 
VALUES (1, 1, 2, 1199.98), 
       (2, 2, 5, 99.95), 
       (3, 3, 1, 899.99);

-- Insertar datos en la tabla Pedidos
INSERT INTO Pedidos (idCliente, estadoPedido, metodoPago, total) 
VALUES (1, 'Pendiente', 'Tarjeta', 1199.98), 
       (2, 'Enviado', 'Paypal', 99.95), 
       (3, 'Cancelado', 'Transferencia', 899.99);

-- Insertar datos en la tabla DetallePedido
INSERT INTO DetallePedido (idPedido, idProducto, cantidad, precio) 
VALUES (1, 1, 2, 599.99), 
       (2, 2, 5, 19.99), 
       (3, 3, 1, 899.99);

-- Insertar datos en la tabla Resenas
INSERT INTO Resenas (idProducto, idCliente, calificacion, comentario) 
VALUES (1, 1, 5, 'Excelente producto, totalmente recomendado!'), 
       (2, 2, 4, 'Muy buena calidad, pero un poco ajustada.'), 
       (3, 3, 3, 'Cumple con las expectativas, aunque podría ser más cómodo.');
