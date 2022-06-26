
-- Tipos de planta
INSERT INTO seminarioumg.TipoPlanta(Id, Descripcion, Status, FechaInsert, UsuarioInsert) VALUES(1, 'EXTRACCION', 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.TipoPlanta(Id, Descripcion, Status, FechaInsert, UsuarioInsert) VALUES(2, 'PROCESAMIENTO', 'A', current_time, 'ADMIN');

-- Paises
INSERT INTO seminarioumg.Pais(Id, Descripcion, Status, FechaInsert, UsuarioInsert) VALUES(1, 'Guatemala', 'A', current_time, 'ADMIN');

-- Departamentos
INSERT INTO seminarioumg.Departamento(Id, Descripcion, PaisId, Status, FechaInsert, UsuarioInsert) VALUES(1, 'Guatemala', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Departamento(Id, Descripcion, PaisId, Status, FechaInsert, UsuarioInsert) VALUES(2, 'Quetzaltenango', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Departamento(Id, Descripcion, PaisId, Status, FechaInsert, UsuarioInsert) VALUES(3, 'Chiquimula', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Departamento(Id, Descripcion, PaisId, Status, FechaInsert, UsuarioInsert) VALUES(4, 'Jutiapa', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Departamento(Id, Descripcion, PaisId, Status, FechaInsert, UsuarioInsert) VALUES(5, 'Escuintla', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Departamento(Id, Descripcion, PaisId, Status, FechaInsert, UsuarioInsert) VALUES(6, 'Alta Verapaz', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Departamento(Id, Descripcion, PaisId, Status, FechaInsert, UsuarioInsert) VALUES(7, 'Baja Verapaz', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Departamento(Id, Descripcion, PaisId, Status, FechaInsert, UsuarioInsert) VALUES(8, 'Chimaltenango', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Departamento(Id, Descripcion, PaisId, Status, FechaInsert, UsuarioInsert) VALUES(9, 'El Progreso', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Departamento(Id, Descripcion, PaisId, Status, FechaInsert, UsuarioInsert) VALUES(10, 'Huehuetenango', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Departamento(Id, Descripcion, PaisId, Status, FechaInsert, UsuarioInsert) VALUES(11, 'Izabal', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Departamento(Id, Descripcion, PaisId, Status, FechaInsert, UsuarioInsert) VALUES(12, 'Jalapa', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Departamento(Id, Descripcion, PaisId, Status, FechaInsert, UsuarioInsert) VALUES(13, 'Peten', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Departamento(Id, Descripcion, PaisId, Status, FechaInsert, UsuarioInsert) VALUES(14, 'Quiche', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Departamento(Id, Descripcion, PaisId, Status, FechaInsert, UsuarioInsert) VALUES(15, 'Retalhuleu', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Departamento(Id, Descripcion, PaisId, Status, FechaInsert, UsuarioInsert) VALUES(16, 'Sacatepequez', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Departamento(Id, Descripcion, PaisId, Status, FechaInsert, UsuarioInsert) VALUES(17, 'San Marcos', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Departamento(Id, Descripcion, PaisId, Status, FechaInsert, UsuarioInsert) VALUES(18, 'Santa Rosa', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Departamento(Id, Descripcion, PaisId, Status, FechaInsert, UsuarioInsert) VALUES(19, 'Solola', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Departamento(Id, Descripcion, PaisId, Status, FechaInsert, UsuarioInsert) VALUES(20, 'Suchitepequez', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Departamento(Id, Descripcion, PaisId, Status, FechaInsert, UsuarioInsert) VALUES(21, 'Totonicapan', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Departamento(Id, Descripcion, PaisId, Status, FechaInsert, UsuarioInsert) VALUES(22, 'Zacapa', 1, 'A', current_time, 'ADMIN');

-- Municipios
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(1, 'Guatemala', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(2, 'Mixco', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(3, 'Quetzaltenango', 2, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(4, 'Chiquimula', 3, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(5, 'Quezaltepeque', 3, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(6, 'Jutiapa', 4, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(7, 'Escuintla', 5, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(8, 'Iztapa', 5, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(9, 'Villa Nueva', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(10, 'Villa Canales', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(11, 'San Miguel Petapa', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(12, 'Amatitlan', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(13, 'San Jose Pinula', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(14, 'Santa Catarina Pinula', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(15, 'Fraijanes', 1, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(16, 'Salcaja', 2, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(17, 'Cantel', 2, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(18, 'Coatepeque', 2, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(19, 'Esquipulas', 3, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(20, 'Jocotan', 3, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(21, 'Olopa', 3, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(22, 'Ipala', 3, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(23, 'Concepcion las Minas', 3, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(24, 'Coban', 6, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(25, 'Chahal', 6, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(26, 'Salama', 7, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(27, 'Chimaltenango', 8, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(28, 'Guastatoya', 9, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(29, 'Huehuetenango', 10, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(30, 'Puerto Barrios', 11, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(31, 'Jalapa', 12, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(32, 'Flores', 13, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(33, 'Santa Cruz del Quiche', 14, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(34, 'Retalhuleu', 15, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(35, 'Antigua Guatemala', 16, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(36, 'Sumpango', 16, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(37, 'San Marcos', 17, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(38, 'Malacatan', 17, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(39, 'Cuilapa', 18, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(40, 'Solola', 19, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(41, 'Mazatenango', 20, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(42, 'Totonicapan', 21, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(43, 'Zacapa', 22, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(44, 'Estanzuela', 22, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(45, 'Rio Hondo', 22, 'A', current_time, 'ADMIN');
INSERT INTO seminarioumg.Municipio(Id, Descripcion, DepartamentoId, Status, FechaInsert, UsuarioInsert) VALUES(46, 'Teculutan', 22, 'A', current_time, 'ADMIN');

-- Tipos de material
INSERT INTO seminarioumg.TipoMaterial(Id, Descripcion, Status, FechaInsert, UsuarioInsert) VALUES(1, 'MATERIA PRIMA', 'A', CURRENT_TIME, 'ADMIN');
INSERT INTO seminarioumg.TipoMaterial(Id, Descripcion, Status, FechaInsert, UsuarioInsert) VALUES(2, 'MATERIAL DE CONSTRUCCION', 'A', CURRENT_TIME, 'ADMIN');

-- Estados pedidos
INSERT INTO seminarioumg.EstadoPedidoMaterial(Id, Descripcion, Status, FechaInsert, UsuarioInsert) VALUES(1, 'CREADO', 'A', CURRENT_TIME, 'ADMIN');
INSERT INTO seminarioumg.EstadoPedidoMaterial(Id, Descripcion, Status, FechaInsert, UsuarioInsert) VALUES(2, 'PENDIENTE', 'A', CURRENT_TIME, 'ADMIN');
INSERT INTO seminarioumg.EstadoPedidoMaterial(Id, Descripcion, Status, FechaInsert, UsuarioInsert) VALUES(3, 'APROBADO', 'A', CURRENT_TIME, 'ADMIN');
INSERT INTO seminarioumg.EstadoPedidoMaterial(Id, Descripcion, Status, FechaInsert, UsuarioInsert) VALUES(4, 'PROGRAMADO', 'A', CURRENT_TIME, 'ADMIN');
INSERT INTO seminarioumg.EstadoPedidoMaterial(Id, Descripcion, Status, FechaInsert, UsuarioInsert) VALUES(5, 'COMPLETADO', 'A', CURRENT_TIME, 'ADMIN');
INSERT INTO seminarioumg.EstadoPedidoMaterial(Id, Descripcion, Status, FechaInsert, UsuarioInsert) VALUES(6, 'ANULADO', 'A', CURRENT_TIME, 'ADMIN');

-- Estados movimientos bodega
INSERT INTO seminarioumg.EstadoMovimientoBodega(Id, Descripcion, Status, FechaInsert, UsuarioInsert) VALUES(1, 'PENDIENTE', 'A', CURRENT_TIME, 'ADMIN');
INSERT INTO seminarioumg.EstadoMovimientoBodega(Id, Descripcion, Status, FechaInsert, UsuarioInsert) VALUES(2, 'PROGRAMADO', 'A', CURRENT_TIME, 'ADMIN');
INSERT INTO seminarioumg.EstadoMovimientoBodega(Id, Descripcion, Status, FechaInsert, UsuarioInsert) VALUES(3, 'CARGADO', 'A', CURRENT_TIME, 'ADMIN');
INSERT INTO seminarioumg.EstadoMovimientoBodega(Id, Descripcion, Status, FechaInsert, UsuarioInsert) VALUES(4, 'ENTREGADO', 'A', CURRENT_TIME, 'ADMIN');
INSERT INTO seminarioumg.EstadoMovimientoBodega(Id, Descripcion, Status, FechaInsert, UsuarioInsert) VALUES(5, 'ANULADO', 'A', CURRENT_TIME, 'ADMIN');

COMMIT;