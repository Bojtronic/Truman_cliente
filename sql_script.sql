CREATE DATABASE TRUMAN;
USE TRUMAN;


CREATE TABLE Usuarios (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(20),
    contrasena VARCHAR(20)
);





INSERT INTO Usuarios (nombre, contrasena) VALUES ('user_1', '123');
INSERT INTO Usuarios (nombre, contrasena) VALUES ('user_2', '456');


