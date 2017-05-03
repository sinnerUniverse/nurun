INSERT INTO Roles(Descripcion)
VALUES('Usuario');

INSERT INTO Roles(Descripcion)
VALUES('Administrador');

INSERT INTO Usuarios(Usuario, Password, Nombres, Apellidos, IdRol, EstaActivo, FechaCreacion)
VALUES('admin', 'adminadmin', 'Administrador', 'del Sistema', 2, 1, GETDATE());