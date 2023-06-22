-- phpMyAdmin SQL Dump
-- version 4.0.4.2
-- http://www.phpmyadmin.net
--
-- Servidor: localhost
-- Tiempo de generación: 22-06-2023 a las 15:25:02
-- Versión del servidor: 5.6.13
-- Versión de PHP: 5.4.17

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de datos: `banco_sangre`
--
CREATE DATABASE IF NOT EXISTS `banco_sangre` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `banco_sangre`;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `almacen`
--

CREATE TABLE IF NOT EXISTS `almacen` (
  `id_almacen` int(11) NOT NULL AUTO_INCREMENT,
  `tipo_sangre` tinytext NOT NULL,
  `fecha_expiracion` date NOT NULL,
  `cantidad` int(11) NOT NULL,
  `id_donante` int(11) NOT NULL,
  PRIMARY KEY (`id_almacen`,`id_donante`),
  KEY `fk_almacen_donante1_idx` (`id_donante`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=35 ;

--
-- Volcado de datos para la tabla `almacen`
--

INSERT INTO `almacen` (`id_almacen`, `tipo_sangre`, `fecha_expiracion`, `cantidad`, `id_donante`) VALUES
(1, 'O+', '2023-06-30', 100, 1),
(2, 'A+', '2023-09-02', 51, 2),
(3, 'B-', '2023-06-30', 75, 3),
(4, 'AB+', '2023-06-30', 60, 4),
(5, 'O-', '2023-06-30', 80, 5),
(6, 'A-', '2023-06-30', 40, 6),
(7, 'B+', '2023-06-30', 70, 7),
(8, 'AB-', '2023-06-30', 55, 8),
(9, 'O+', '2023-06-30', 90, 9),
(10, 'A+', '2022-05-18', 65, 10),
(11, 'A+', '2023-07-15', 250, 11),
(12, 'B+', '2023-06-30', 300, 12),
(13, 'O+', '2023-08-10', 350, 13),
(14, 'AB+', '2023-07-22', 400, 14),
(15, 'A-', '2023-07-31', 200, 15),
(16, 'B-', '2023-08-05', 250, 16),
(17, 'O-', '2023-08-20', 300, 17),
(18, 'AB-', '2023-07-29', 350, 18),
(19, 'A+', '2023-08-08', 200, 19),
(20, 'O+', '2023-08-17', 250, 20),
(21, 'A+', '2023-07-15', 300, 21),
(22, 'B+', '2023-06-30', 350, 22),
(23, 'O+', '2023-08-10', 200, 23),
(24, 'AB+', '2023-07-22', 250, 24),
(25, 'A-', '2023-07-31', 300, 25),
(26, 'B-', '2023-08-05', 350, 26),
(27, 'O-', '2023-08-20', 200, 27),
(28, 'AB-', '2023-07-29', 250, 28),
(29, 'A+', '2023-08-08', 300, 29),
(30, 'O+', '2023-08-17', 350, 30),
(31, 'O+', '2012-02-01', 150, 32),
(32, 'O+', '2012-02-01', 250, 32),
(33, 'O+', '2012-02-01', 150, 33),
(34, 'O-', '2023-09-22', 300, 34);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `citas`
--

CREATE TABLE IF NOT EXISTS `citas` (
  `id_citas` int(11) NOT NULL AUTO_INCREMENT,
  `fecha_cita` datetime NOT NULL,
  `lugar_cita` tinytext NOT NULL,
  `id_donante` int(11) NOT NULL,
  PRIMARY KEY (`id_citas`,`id_donante`),
  KEY `fk_citas_donante1_idx` (`id_donante`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=35 ;

--
-- Volcado de datos para la tabla `citas`
--

INSERT INTO `citas` (`id_citas`, `fecha_cita`, `lugar_cita`, `id_donante`) VALUES
(1, '2023-06-17 10:00:00', 'Hospital General', 1),
(2, '2023-06-18 11:30:00', 'Hospital Metropolitano', 2),
(3, '2023-06-19 13:45:00', 'Hospital Universitario', 3),
(4, '2023-06-20 09:15:00', 'Hospital San Lucas', 4),
(5, '2023-06-21 16:30:00', 'Hospital Santa María', 5),
(6, '2023-06-22 14:00:00', 'Hospital San José', 6),
(7, '2023-06-23 10:30:00', 'Hospital Nacional', 7),
(8, '2023-06-24 12:45:00', 'Hospital A', 8),
(9, '2023-06-25 15:00:00', 'Hospital B', 9),
(10, '2023-06-26 11:00:00', 'Hospital C', 10),
(11, '2023-06-27 10:00:00', 'Hospital General de la Ciudad', 11),
(12, '2023-06-28 11:30:00', 'Hospital Universitario', 12),
(13, '2023-06-29 13:45:00', 'Hospital Regional', 13),
(14, '2023-06-30 09:15:00', 'Clínica Santa María', 14),
(15, '2023-07-01 14:30:00', 'Hospital San Juan de Dios', 15),
(16, '2023-07-02 16:45:00', 'Clínica Internacional', 16),
(17, '2023-07-03 12:00:00', 'Hospital Nacional', 17),
(18, '2023-07-04 15:30:00', 'Hospital Metropolitano', 18),
(19, '2023-07-05 08:45:00', 'Centro Médico ABC', 19),
(20, '2023-07-06 11:15:00', 'Hospital Español', 20),
(21, '2023-07-07 13:30:00', 'Hospital Central', 21),
(22, '2023-07-08 10:45:00', 'Clínica del Carmen', 22),
(23, '2023-07-09 16:00:00', 'Hospital San Lucas', 23),
(24, '2023-07-10 14:15:00', 'Hospital Santa Rosa', 24),
(25, '2023-07-11 09:30:00', 'Hospital San Francisco', 25),
(26, '2023-07-12 12:45:00', 'Clínica San Rafael', 26),
(27, '2023-07-13 15:00:00', 'Hospital del Valle', 27),
(28, '2023-07-14 11:00:00', 'Hospital San José', 28),
(29, '2023-07-15 13:15:00', 'Centro Médico Nacional', 29),
(30, '2023-07-16 16:30:00', 'Clínica Ángeles', 30),
(31, '2023-06-29 00:00:00', 'Hospital A', 32),
(32, '2023-06-29 00:00:00', 'Hospital A', 32),
(33, '2023-06-29 18:32:00', 'Hospital B', 33),
(34, '2023-06-22 16:56:00', 'Hospital B', 34);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `donacion`
--

CREATE TABLE IF NOT EXISTS `donacion` (
  `id_hospitales` int(11) NOT NULL,
  `id_almacen` int(11) NOT NULL,
  `id_donante` int(11) NOT NULL,
  `fecha_envio` date NOT NULL,
  PRIMARY KEY (`id_hospitales`,`id_almacen`,`id_donante`),
  KEY `fk_hospitales_has_almacen_almacen1_idx` (`id_almacen`,`id_donante`),
  KEY `fk_hospitales_has_almacen_hospitales1_idx` (`id_hospitales`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `donacion`
--

INSERT INTO `donacion` (`id_hospitales`, `id_almacen`, `id_donante`, `fecha_envio`) VALUES
(1, 1, 1, '2023-06-17'),
(1, 11, 11, '2023-07-01'),
(2, 12, 12, '2023-06-28'),
(3, 3, 3, '2023-06-19'),
(3, 13, 13, '2023-07-15'),
(4, 14, 14, '2023-07-03'),
(5, 15, 15, '2023-07-18'),
(6, 6, 6, '2023-06-22'),
(6, 16, 16, '2023-07-01'),
(7, 17, 17, '2023-07-21'),
(8, 18, 18, '2023-07-12'),
(9, 9, 9, '2023-06-25'),
(9, 19, 19, '2023-07-29'),
(11, 20, 20, '2023-07-17'),
(11, 34, 34, '2023-09-01');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `donante`
--

CREATE TABLE IF NOT EXISTS `donante` (
  `id_donante` int(11) NOT NULL AUTO_INCREMENT,
  `tipo_sangre` tinytext NOT NULL,
  `nombre` tinytext NOT NULL,
  `apellidos` tinytext NOT NULL,
  `anio_nacimiento` date NOT NULL,
  `donacion_realizada` tinyint(4) DEFAULT '0',
  `id_usuario` int(11) NOT NULL,
  PRIMARY KEY (`id_donante`,`id_usuario`),
  KEY `fk_donante_usuario_idx` (`id_usuario`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=35 ;

--
-- Volcado de datos para la tabla `donante`
--

INSERT INTO `donante` (`id_donante`, `tipo_sangre`, `nombre`, `apellidos`, `anio_nacimiento`, `donacion_realizada`, `id_usuario`) VALUES
(1, 'O+', 'Donante 1', 'Apellido 1', '1980-01-10', 1, 1),
(2, 'A+', 'Donante 2', 'Apellido 2', '1975-03-15', 0, 2),
(3, 'B-', 'Donante 3', 'Apellido 3', '1988-07-22', 1, 3),
(4, 'AB+', 'Donante 4', 'Apellido 4', '1992-05-05', 0, 4),
(5, 'O-', 'Donante 5', 'Apellido 5', '1985-11-18', 0, 5),
(6, 'A-', 'Donante 6', 'Apellido 6', '1998-02-28', 1, 6),
(7, 'B+', 'Donante 7', 'Apellido 7', '1992-09-03', 0, 7),
(8, 'AB-', 'Donante 8', 'Apellido 8', '1993-06-23', 0, 8),
(9, 'O+', 'Donante 9', 'Apellido 9', '1990-04-05', 0, 9),
(10, 'A+', 'Donante 10', 'Apellido 10', '1987-08-12', 0, 10),
(11, 'A+', 'Juan', 'García López', '1990-05-12', 1, 11),
(12, 'B+', 'María', 'Hernández Rodríguez', '1985-09-28', 1, 12),
(13, 'O+', 'Carlos', 'Martínez Pérez', '1992-02-15', 1, 13),
(14, 'AB+', 'Laura', 'González Gómez', '1998-11-03', 1, 14),
(15, 'A-', 'Luis', 'Rodríguez García', '1995-07-19', 1, 15),
(16, 'B-', 'Ana', 'López Martínez', '1993-03-25', 1, 16),
(17, 'O-', 'Pedro', 'Sánchez González', '1988-08-09', 1, 17),
(18, 'AB-', 'Sofía', 'Pérez Hernández', '1991-06-06', 1, 18),
(19, 'A+', 'Diego', 'Gómez Rodríguez', '1997-04-17', 1, 19),
(20, 'O+', 'Lucía', 'García Pérez', '1994-12-30', 1, 20),
(21, 'A-', 'Alejandro', 'Hernández López', '1989-10-22', 0, 21),
(22, 'O-', 'Valentina', 'Martínez García', '1996-01-07', 0, 22),
(23, 'B+', 'Javier', 'González Sánchez', '1999-09-14', 0, 23),
(24, 'AB+', 'Mariana', 'Rodríguez Pérez', '1992-07-31', 0, 24),
(25, 'B-', 'Fernando', 'López Gómez', '1987-03-18', 0, 25),
(26, 'AB-', 'Camila', 'Martínez Rodríguez', '1993-06-02', 0, 26),
(27, 'A+', 'Emilio', 'García Martínez', '1998-04-24', 0, 27),
(28, 'O+', 'Isabella', 'Hernández González', '1991-12-11', 0, 28),
(29, 'A-', 'Andrés', 'Pérez López', '1986-08-27', 0, 29),
(30, 'O-', 'Natalia', 'Gómez Martínez', '1994-02-02', 0, 30),
(31, 'O+', 'Juan', 'Garcia Lopez', '1999-06-22', 0, 31),
(32, 'O+', 'José Alberto ', 'Méndez Jimenez', '1993-06-27', 0, 32),
(33, 'O+', 'María Andrea', 'Martínez López', '1984-08-21', 0, 33),
(34, 'O-', 'Jose Luis', 'Cervantes García', '1986-02-06', 1, 34);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `hospitales`
--

CREATE TABLE IF NOT EXISTS `hospitales` (
  `id_hospitales` int(11) NOT NULL AUTO_INCREMENT,
  `nombre_hospital` tinytext NOT NULL,
  `direccion_hospital` tinytext NOT NULL,
  PRIMARY KEY (`id_hospitales`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=14 ;

--
-- Volcado de datos para la tabla `hospitales`
--

INSERT INTO `hospitales` (`id_hospitales`, `nombre_hospital`, `direccion_hospital`) VALUES
(1, 'Hospital General', 'Calle Principal 123, Ciudad'),
(2, 'Hospital Metropolitano', 'Avenida Central 456, Ciudad'),
(3, 'Hospital Universitario', 'Avenida Principal 789, Ciudad'),
(4, 'Hospital San Lucas', 'Calle Secundaria 321, Ciudad'),
(5, 'Hospital Santa María', 'Avenida Secundaria 654, Ciudad'),
(6, 'Hospital San José', 'Calle Principal 987, Ciudad'),
(7, 'Hospital Nacional', 'Avenida Central 654, Ciudad'),
(8, 'Hospital A', 'Dirección A'),
(9, 'Hospital B', 'Dirección B'),
(10, 'Hospital C', 'Dirección C'),
(11, 'hospital  la raza', 'Calz. Vallejo 8, La Raza, Azcapotzalco, 02990 Ciudad de México, CDMX'),
(12, 'IMSS Hospital General de Zona 24', 'magdalena de las Salinas, Gustavo A. Madero,'),
(13, 'hospital m', '');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE IF NOT EXISTS `usuario` (
  `id_usuario` int(11) NOT NULL AUTO_INCREMENT,
  `curp` varchar(18) NOT NULL,
  `contrasena` tinytext NOT NULL,
  `rol` tinytext NOT NULL,
  PRIMARY KEY (`id_usuario`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=35 ;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`id_usuario`, `curp`, `contrasena`, `rol`) VALUES
(1, 'LOXJ900428HDFSMR05', 'pass10', 'admin'),
(2, 'GARP960214HDFLNN07', 'pass9', 'usuario'),
(3, 'MOLJ850812HDFNRN01', 'pass8', 'usuario'),
(4, 'VOPA880518HDFBRN02', 'pass7', 'usuario'),
(5, 'HERC890202HDFGRN09', 'pass6', 'usuario'),
(6, 'ZAND800728HDFDRN08', 'pass5', 'usuario'),
(7, 'DORL920305HDFSMN04', 'pass4', 'usuario'),
(8, 'COSP930623HDFLSN03', 'pass3', 'usuario'),
(9, 'JUAR940515HDFDRN06', 'pass2', 'admin'),
(10, 'ALVM950303HDFNRN10', 'pass1', 'admin'),
(11, 'GAJL900512HMCRRS00', 'contraseña11', 'usuario'),
(12, 'HERM850928MMCRDR03', 'contraseña12', 'usuario'),
(13, 'MARP920215HMCRZQ07', 'contraseña13', 'usuario'),
(14, 'GOLJ981103HMCRZQ09', 'contraseña14', 'usuario'),
(15, 'ROGL950719HMCRRC02', 'contraseña15', 'usuario'),
(16, 'LOMA930325HMCRZQ06', 'contraseña16', 'usuario'),
(17, 'SAGP880809HMCRDR04', 'contraseña17', 'usuario'),
(18, 'PEHS910606HMCRSF02', 'contraseña18', 'usuario'),
(19, 'GODD970417HMCRQS05', 'contraseña19', 'usuario'),
(20, 'GARP941230HMCRZQ08', 'contraseña20', 'usuario'),
(21, 'HELA891022HMCRRS08', 'contraseña21', 'usuario'),
(22, 'MAGV960107HMCRRL01', 'contraseña22', 'usuario'),
(23, 'GOSJ990914HMCRGZ05', 'contraseña23', 'usuario'),
(24, 'ROPM920731HMCRCD07', 'contraseña24', 'usuario'),
(25, 'LOFG870318HMCRZQ01', 'contraseña25', 'usuario'),
(26, 'MARC930602HMCRZQ09', 'contraseña26', 'usuario'),
(27, 'GAMA980424HMCRZQ04', 'contraseña27', 'usuario'),
(28, 'HEGO911211HMCRGZ05', 'contraseña28', 'usuario'),
(29, 'PEAL860827HMCRZQ02', 'contraseña29', 'usuario'),
(30, 'GOMN940202HMCRRS08', 'contraseña30', 'usuario'),
(31, 'GOLJ990622HJCRRPS1', '1234', 'usuario'),
(32, 'JAME930627MOCGRLK4', '1234', 'usuario'),
(33, 'MALM840921MOCGRS71', '1234', 'usuario'),
(34, 'JOLC860206HOCGRN46', '1234', 'usuario');

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `almacen`
--
ALTER TABLE `almacen`
  ADD CONSTRAINT `fk_almacen_donante1` FOREIGN KEY (`id_donante`) REFERENCES `donante` (`id_donante`);

--
-- Filtros para la tabla `citas`
--
ALTER TABLE `citas`
  ADD CONSTRAINT `fk_citas_donante1` FOREIGN KEY (`id_donante`) REFERENCES `donante` (`id_donante`);

--
-- Filtros para la tabla `donacion`
--
ALTER TABLE `donacion`
  ADD CONSTRAINT `fk_hospitales_has_almacen_almacen1` FOREIGN KEY (`id_almacen`, `id_donante`) REFERENCES `almacen` (`id_almacen`, `id_donante`),
  ADD CONSTRAINT `fk_hospitales_has_almacen_hospitales1` FOREIGN KEY (`id_hospitales`) REFERENCES `hospitales` (`id_hospitales`);

--
-- Filtros para la tabla `donante`
--
ALTER TABLE `donante`
  ADD CONSTRAINT `fk_donante_usuario` FOREIGN KEY (`id_usuario`) REFERENCES `usuario` (`id_usuario`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
