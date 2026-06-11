CREATE TABLE `clientes` (
  `id_cliente` int PRIMARY KEY AUTO_INCREMENT,
  `nombre` varchar(100),
  `telefono` varchar(20),
  `correo` varchar(100)
);

CREATE TABLE `vehiculos` (
  `id_vehiculo` int PRIMARY KEY AUTO_INCREMENT,
  `placa` varchar(15),
  `marca` varchar(50),
  `modelo` varchar(50),
  `id_cliente` int
);

CREATE TABLE `mecanicos` (
  `id_mecanico` int PRIMARY KEY AUTO_INCREMENT,
  `nombre` varchar(100),
  `especialidad` varchar(50)
);

CREATE TABLE `ordenes_servicio` (
  `id_orden` int PRIMARY KEY AUTO_INCREMENT,
  `falla_reportada` text,
  `estado` varchar(30),
  `costo_estimado` decimal(10,2),
  `id_vehiculo` int,
  `id_mecanico` int
);

ALTER TABLE `vehiculos` ADD FOREIGN KEY (`id_cliente`) REFERENCES `clientes` (`id_cliente`);

ALTER TABLE `ordenes_servicio` ADD FOREIGN KEY (`id_vehiculo`) REFERENCES `vehiculos` (`id_vehiculo`);

ALTER TABLE `ordenes_servicio` ADD FOREIGN KEY (`id_mecanico`) REFERENCES `mecanicos` (`id_mecanico`);
