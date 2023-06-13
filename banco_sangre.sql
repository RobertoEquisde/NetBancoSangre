-- phpMyAdmin SQL Dump
-- version 4.0.4.2
-- http://www.phpmyadmin.net
--
-- Servidor: localhost
-- Tiempo de generación: 13-06-2023 a las 16:47:10
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
-- Estructura de tabla para la tabla `almacenamiento`
--

CREATE TABLE IF NOT EXISTS `almacenamiento` (
  `almacenamiento_id` int(11) NOT NULL AUTO_INCREMENT,
  `tipo_sangre` varchar(10) NOT NULL,
  `cantidad` int(11) NOT NULL,
  `fecha_expiracion` date NOT NULL,
  PRIMARY KEY (`almacenamiento_id`),
  KEY `tipo_sangre` (`tipo_sangre`),
  KEY `tipo_sangre_2` (`tipo_sangre`),
  KEY `tipo_sangre_3` (`tipo_sangre`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=106 ;

--
-- Volcado de datos para la tabla `almacenamiento`
--

INSERT INTO `almacenamiento` (`almacenamiento_id`, `tipo_sangre`, `cantidad`, `fecha_expiracion`) VALUES
(26, 'A+', 80, '2023-12-31'),
(27, 'B-', 40, '2023-11-30'),
(28, 'O+', 65, '2023-10-31'),
(29, 'AB+', 20, '2024-01-31'),
(30, 'A-', 50, '2023-09-30'),
(31, 'O-', 30, '2023-08-31'),
(32, 'B+', 60, '2023-07-31'),
(33, 'AB-', 15, '2023-12-31'),
(34, 'A+', 70, '2023-11-30'),
(35, 'O-', 25, '2023-10-31'),
(36, 'B+', 55, '2024-01-31'),
(37, 'AB-', 10, '2023-09-30'),
(38, 'O+', 45, '2023-08-31'),
(39, 'B-', 35, '2023-07-31'),
(40, 'A-', 60, '2023-12-31'),
(41, 'AB+', 18, '2023-11-30'),
(42, 'O-', 28, '2023-10-31'),
(43, 'A+', 75, '2024-01-31'),
(44, 'B-', 30, '2023-09-30'),
(45, 'O+', 50, '2023-08-31'),
(46, 'A+', 80, '2023-12-31'),
(47, 'B-', 40, '2023-11-30'),
(48, 'O+', 65, '2023-10-31'),
(49, 'AB+', 20, '2024-01-31'),
(50, 'A-', 50, '2023-09-30'),
(51, 'O-', 30, '2023-08-31'),
(52, 'B+', 60, '2023-07-31'),
(53, 'AB-', 15, '2023-12-31'),
(54, 'A+', 70, '2023-11-30'),
(55, 'O-', 25, '2023-10-31'),
(56, 'B+', 55, '2024-01-31'),
(57, 'AB-', 10, '2023-09-30'),
(58, 'O+', 45, '2023-08-31'),
(59, 'B-', 35, '2023-07-31'),
(60, 'A-', 60, '2023-12-31'),
(61, 'AB+', 18, '2023-11-30'),
(62, 'O-', 28, '2023-10-31'),
(63, 'A+', 75, '2024-01-31'),
(64, 'B-', 30, '2023-09-30'),
(65, 'O+', 50, '2023-08-31'),
(66, 'A+', 80, '2023-12-31'),
(67, 'B-', 40, '2023-11-30'),
(68, 'O+', 65, '2023-10-31'),
(69, 'AB+', 20, '2024-01-31'),
(70, 'A-', 50, '2023-09-30'),
(71, 'O-', 30, '2023-08-31'),
(72, 'B+', 60, '2023-07-31'),
(73, 'AB-', 15, '2023-12-31'),
(74, 'A+', 70, '2023-11-30'),
(75, 'O-', 25, '2023-10-31'),
(76, 'B+', 55, '2024-01-31'),
(77, 'AB-', 10, '2023-09-30'),
(78, 'O+', 45, '2023-08-31'),
(79, 'B-', 35, '2023-07-31'),
(80, 'A-', 60, '2023-12-31'),
(81, 'AB+', 18, '2023-11-30'),
(82, 'O-', 28, '2023-10-31'),
(83, 'A+', 75, '2024-01-31'),
(84, 'B-', 30, '2023-09-30'),
(85, 'O+', 50, '2023-08-31'),
(86, 'A+', 80, '2023-12-31'),
(87, 'B-', 40, '2023-11-30'),
(88, 'O+', 65, '2023-10-31'),
(89, 'AB+', 20, '2024-01-31'),
(90, 'A-', 50, '2023-09-30'),
(91, 'O-', 30, '2023-08-31'),
(92, 'B+', 60, '2023-07-31'),
(93, 'AB-', 15, '2023-12-31'),
(94, 'A+', 70, '2023-11-30'),
(95, 'O-', 25, '2023-10-31'),
(96, 'B+', 55, '2024-01-31'),
(97, 'AB-', 10, '2023-09-30'),
(98, 'O+', 45, '2023-08-31'),
(99, 'B-', 35, '2023-07-31'),
(100, 'A-', 60, '2023-12-31'),
(101, 'AB+', 18, '2023-11-30'),
(102, 'O-', 28, '2023-10-31'),
(103, 'A+', 75, '2024-01-31'),
(104, 'B-', 30, '2023-09-30'),
(105, 'O+', 50, '2023-08-31');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `donacion`
--

CREATE TABLE IF NOT EXISTS `donacion` (
  `donacion_id` int(11) NOT NULL AUTO_INCREMENT,
  `donante_id` int(11) NOT NULL,
  `tipo_sangre` varchar(10) NOT NULL,
  `fecha` date NOT NULL,
  `hora` time NOT NULL,
  `cantidad` int(11) NOT NULL,
  `ubicacion` varchar(255) NOT NULL,
  PRIMARY KEY (`donacion_id`),
  KEY `donante_id` (`donante_id`),
  KEY `tipo_sangre` (`tipo_sangre`),
  KEY `donante_id_2` (`donante_id`),
  KEY `tipo_sangre_2` (`tipo_sangre`),
  KEY `donante_id_3` (`donante_id`),
  KEY `tipo_sangre_3` (`tipo_sangre`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=241 ;

--
-- Volcado de datos para la tabla `donacion`
--

INSERT INTO `donacion` (`donacion_id`, `donante_id`, `tipo_sangre`, `fecha`, `hora`, `cantidad`, `ubicacion`) VALUES
(221, 26, 'A+', '2023-06-06', '10:30:00', 1, 'Hospital ABC'),
(222, 27, 'B-', '2023-06-07', '14:45:00', 2, 'Centro Médico XYZ'),
(223, 28, 'O+', '2023-06-08', '09:00:00', 1, 'Hospital General'),
(224, 29, 'AB+', '2023-06-09', '11:15:00', 2, 'Clínica Defensa'),
(225, 30, 'A-', '2023-06-10', '16:30:00', 1, 'Hospital Central'),
(226, 31, 'A+', '2023-06-11', '10:30:00', 1, 'Hospital ABC'),
(227, 32, 'B-', '2023-06-12', '14:45:00', 2, 'Centro Médico XYZ'),
(228, 33, 'O+', '2023-06-13', '09:00:00', 1, 'Hospital General'),
(229, 34, 'AB+', '2023-06-14', '11:15:00', 2, 'Clínica Defensa'),
(230, 35, 'A-', '2023-06-15', '16:30:00', 1, 'Hospital Central'),
(231, 36, 'A+', '2023-06-16', '10:30:00', 1, 'Hospital ABC'),
(232, 37, 'B-', '2023-06-17', '14:45:00', 2, 'Centro Médico XYZ'),
(233, 38, 'O+', '2023-06-18', '09:00:00', 1, 'Hospital General'),
(234, 39, 'AB+', '2023-06-19', '11:15:00', 2, 'Clínica Defensa'),
(235, 40, 'A-', '2023-06-20', '16:30:00', 1, 'Hospital Central'),
(236, 41, 'A+', '2023-06-21', '10:30:00', 1, 'Hospital ABC'),
(237, 42, 'B-', '2023-06-22', '14:45:00', 2, 'Centro Médico XYZ'),
(238, 43, 'O+', '2023-06-23', '09:00:00', 1, 'Hospital General'),
(239, 44, 'AB+', '2023-06-24', '11:15:00', 2, 'Clínica Defensa'),
(240, 45, 'A-', '2023-06-25', '16:30:00', 1, 'Hospital Central');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `donante`
--

CREATE TABLE IF NOT EXISTS `donante` (
  `donante_id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(255) NOT NULL,
  `edad` int(11) NOT NULL,
  `genero` varchar(10) NOT NULL,
  `tipo_sangre` varchar(10) NOT NULL,
  `numero_contacto` varchar(10) NOT NULL,
  `direccion` varchar(255) NOT NULL,
  PRIMARY KEY (`donante_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=106 ;

--
-- Volcado de datos para la tabla `donante`
--

INSERT INTO `donante` (`donante_id`, `nombre`, `edad`, `genero`, `tipo_sangre`, `numero_contacto`, `direccion`) VALUES
(26, 'Patricia Sánchez', 42, 'Femenino', 'O-', '1234567891', 'Calle Principal 456'),
(27, 'Ricardo Martínez', 33, 'Masculino', 'B+', '9876543211', 'Avenida Central 789'),
(28, 'Carolina García', 29, 'Femenino', 'A-', '5678901235', 'Carrera 7 891'),
(29, 'Jorge Torres', 36, 'Masculino', 'AB-', '4321098766', 'Calle Secundaria 322'),
(30, 'Carmen Ramírez', 41, 'Femenino', 'A+', '8765432108', 'Avenida Norte 790'),
(31, 'Fernando Gómez', 28, 'Masculino', 'O+', '1234567892', 'Calle Principal 457'),
(32, 'Ana Torres', 30, 'Femenino', 'AB+', '9876543212', 'Avenida Central 790'),
(33, 'Roberto García', 45, 'Masculino', 'B-', '5678901236', 'Carrera 7 892'),
(34, 'Laura Sánchez', 39, 'Femenino', 'O+', '4321098767', 'Calle Secundaria 323'),
(35, 'Andrés Martínez', 27, 'Masculino', 'A-', '8765432107', 'Avenida Norte 791'),
(36, 'Valentina García', 31, 'Femenino', 'B+', '1234567893', 'Calle Principal 458'),
(37, 'Carlos Ramírez', 38, 'Masculino', 'O-', '9876543213', 'Avenida Central 791'),
(38, 'María Gómez', 34, 'Femenino', 'AB-', '5678901237', 'Carrera 7 893'),
(39, 'Juan Torres', 40, 'Masculino', 'A+', '4321098768', 'Calle Secundaria 324'),
(40, 'Sofía Sánchez', 32, 'Femenino', 'B-', '8765432106', 'Avenida Norte 792'),
(41, 'Pedro Martínez', 37, 'Masculino', 'O+', '1234567894', 'Calle Principal 459'),
(42, 'Luisa García', 43, 'Femenino', 'AB+', '9876543214', 'Avenida Central 792'),
(43, 'Mario Ramírez', 26, 'Masculino', 'A-', '5678901238', 'Carrera 7 894'),
(44, 'Carolina Gómez', 35, 'Femenino', 'B+', '4321098769', 'Calle Secundaria 325'),
(45, 'Roberto Torres', 44, 'Masculino', 'O-', '8765432105', 'Avenida Norte 793'),
(46, 'Patricia Sánchez', 42, 'Femenino', 'O-', '1234567891', 'Calle Principal 456'),
(47, 'Ricardo Martínez', 33, 'Masculino', 'B+', '9876543211', 'Avenida Central 789'),
(48, 'Carolina García', 29, 'Femenino', 'A-', '5678901235', 'Carrera 7 891'),
(49, 'Jorge Torres', 36, 'Masculino', 'AB-', '4321098766', 'Calle Secundaria 322'),
(50, 'Carmen Ramírez', 41, 'Femenino', 'A+', '8765432108', 'Avenida Norte 790'),
(51, 'Fernando Gómez', 28, 'Masculino', 'O+', '1234567892', 'Calle Principal 457'),
(53, 'Roberto García', 45, 'Masculino', 'B-', '5678901236', 'Carrera 7 892'),
(54, 'Laura Sánchez', 39, 'Femenino', 'O+', '4321098767', 'Calle Secundaria 323'),
(55, 'Andrés Martínez', 27, 'Masculino', 'A-', '8765432107', 'Avenida Norte 791'),
(56, 'Valentina García', 31, 'Femenino', 'B+', '1234567893', 'Calle Principal 458'),
(57, 'Carlos Ramírez', 38, 'Masculino', 'O-', '9876543213', 'Avenida Central 791'),
(58, 'María Gómez', 34, 'Femenino', 'AB-', '5678901237', 'Carrera 7 893'),
(59, 'Juan Torres', 40, 'Masculino', 'A+', '4321098768', 'Calle Secundaria 324'),
(60, 'Sofía Sánchez', 32, 'Femenino', 'B-', '8765432106', 'Avenida Norte 792'),
(61, 'Pedro Martínez', 37, 'Masculino', 'O+', '1234567894', 'Calle Principal 459'),
(62, 'Luisa García', 43, 'Femenino', 'AB+', '9876543214', 'Avenida Central 792'),
(63, 'Mario Ramírez', 26, 'Masculino', 'A-', '5678901238', 'Carrera 7 894'),
(64, 'Carolina Gómez', 35, 'Femenino', 'B+', '4321098769', 'Calle Secundaria 325'),
(65, 'Roberto Torres', 44, 'Masculino', 'O-', '8765432105', 'Avenida Norte 793'),
(66, 'Patricia Sánchez', 42, 'Femenino', 'O-', '1234567891', 'Calle Principal 456'),
(67, 'Ricardo Martínez', 33, 'Masculino', 'B+', '9876543211', 'Avenida Central 789'),
(68, 'Carolina García', 29, 'Femenino', 'A-', '5678901235', 'Carrera 7 891'),
(69, 'Jorge Torres', 36, 'Masculino', 'AB-', '4321098766', 'Calle Secundaria 322'),
(70, 'Carmen Ramírez', 41, 'Femenino', 'A+', '8765432108', 'Avenida Norte 790'),
(71, 'Fernando Gómez', 28, 'Masculino', 'O+', '1234567892', 'Calle Principal 457'),
(73, 'Roberto García', 45, 'Masculino', 'B-', '5678901236', 'Carrera 7 892'),
(74, 'Laura Sánchez', 39, 'Femenino', 'O+', '4321098767', 'Calle Secundaria 323'),
(75, 'Andrés Martínez', 27, 'Masculino', 'A-', '8765432107', 'Avenida Norte 791'),
(76, 'Valentina García', 31, 'Femenino', 'B+', '1234567893', 'Calle Principal 458'),
(77, 'Carlos Ramírez', 38, 'Masculino', 'O-', '9876543213', 'Avenida Central 791'),
(78, 'María Gómez', 34, 'Femenino', 'AB-', '5678901237', 'Carrera 7 893'),
(79, 'Juan Torres', 40, 'Masculino', 'A+', '4321098768', 'Calle Secundaria 324'),
(80, 'Sofía Sánchez', 32, 'Femenino', 'B-', '8765432106', 'Avenida Norte 792'),
(81, 'Pedro Martínez', 37, 'Masculino', 'O+', '1234567894', 'Calle Principal 459'),
(82, 'Luisa García', 43, 'Femenino', 'AB+', '9876543214', 'Avenida Central 792'),
(83, 'Mario Ramírez', 26, 'Masculino', 'A-', '5678901238', 'Carrera 7 894'),
(84, 'Carolina Gómez', 35, 'Femenino', 'B+', '4321098769', 'Calle Secundaria 325'),
(85, 'Roberto Torres', 44, 'Masculino', 'O-', '8765432105', 'Avenida Norte 793'),
(86, 'Patricia Sánchez', 42, 'Femenino', 'O-', '1234567891', 'Calle Principal 456'),
(87, 'Ricardo Martínez', 33, 'Masculino', 'B+', '9876543211', 'Avenida Central 789'),
(88, 'Carolina García', 29, 'Femenino', 'A-', '5678901235', 'Carrera 7 891'),
(89, 'Jorge Torres', 36, 'Masculino', 'AB-', '4321098766', 'Calle Secundaria 322'),
(90, 'Carmen Ramírez', 41, 'Femenino', 'A+', '8765432108', 'Avenida Norte 790'),
(91, 'Fernando Gómez', 28, 'Masculino', 'O+', '1234567892', 'Calle Principal 457'),
(93, 'Roberto García', 45, 'Masculino', 'B-', '5678901236', 'Carrera 7 892'),
(94, 'Laura Sánchez', 39, 'Femenino', 'O+', '4321098767', 'Calle Secundaria 323'),
(95, 'Andrés Martínez', 27, 'Masculino', 'A-', '8765432107', 'Avenida Norte 791'),
(96, 'Valentina García', 31, 'Femenino', 'B+', '1234567893', 'Calle Principal 458'),
(97, 'Carlos Ramírez', 38, 'Masculino', 'O-', '9876543213', 'Avenida Central 791'),
(98, 'María Gómez', 34, 'Femenino', 'AB-', '5678901237', 'Carrera 7 893'),
(99, 'Juan Torres', 40, 'Masculino', 'A+', '4321098768', 'Calle Secundaria 324'),
(100, 'Sofía Sánchez', 32, 'Femenino', 'B-', '8765432106', 'Avenida Norte 792'),
(101, 'Pedro Martínez', 37, 'Masculino', 'O+', '1234567894', 'Calle Principal 459'),
(102, 'Luisa García', 43, 'Femenino', 'AB+', '9876543214', 'Avenida Central 792'),
(103, 'Mario Ramírez', 26, 'Masculino', 'A-', '5678901238', 'Carrera 7 894'),
(104, 'Carolina Gómez', 35, 'Femenino', 'B+', '4321098769', 'Calle Secundaria 325'),
(105, 'Roberto Torres', 44, 'Masculino', 'O-', '8765432105', 'Avenida Norte 793');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE IF NOT EXISTS `usuario` (
  `usuario_id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(255) NOT NULL,
  `correo_electronico` varchar(255) NOT NULL,
  `contrasena` varchar(255) NOT NULL,
  `rol` enum('admin','usuario') NOT NULL,
  PRIMARY KEY (`usuario_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=81 ;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`usuario_id`, `nombre`, `correo_electronico`, `contrasena`, `rol`) VALUES
(1, 'Luisa López', 'luisa@example.com', 'qwerty123', 'usuario'),
(2, 'Mario González', 'mario@example.com', 'password123', 'admin'),
(3, 'Carolina Ramírez', 'carolina@example.com', 'abc123', 'usuario'),
(4, 'Juan Carlos Rodríguez', 'juancarlos@example.com', 'pass1234', 'usuario'),
(5, 'Ana María Pérez', 'anamaria@example.com', 'test123', 'usuario'),
(6, 'Carlos Sánchez', 'carlos@example.com', 'password', 'usuario'),
(7, 'Laura Martínez', 'laura@example.com', 'abc12345', 'usuario'),
(8, 'Roberto Gómez', 'roberto@example.com', 'password12345', 'admin'),
(9, 'María Torres', 'maria@example.com', 'test1234', 'usuario'),
(10, 'Pedro Ramírez', 'pedro@example.com', 'abc123456', 'usuario'),
(11, 'Sofía García', 'sofia@example.com', 'password123456', 'usuario'),
(12, 'Fernando López', 'fernando@example.com', 'test12345', 'usuario'),
(13, 'Marta González', 'marta@example.com', 'abc1234567', 'usuario'),
(14, 'Javier Sánchez', 'javier@example.com', 'password1234567', 'admin'),
(15, 'Camila Martínez', 'camila@example.com', 'test123456', 'usuario'),
(16, 'David Gómez', 'david@example.com', 'abc12345678', 'usuario'),
(17, 'Isabella Torres', 'isabella@example.com', 'password12345678', 'usuario'),
(18, 'Manuel Ramírez', 'manuel@example.com', 'test1234567', 'usuario'),
(19, 'Valentina García', 'valentina@example.com', 'abc123456789', 'usuario'),
(20, 'Andrés López', 'andres@example.com', 'password123456789', 'admin'),
(21, 'Luisa López', 'luisa@example.com', 'qwerty123', 'usuario'),
(22, 'Mario González', 'mario@example.com', 'password123', 'admin'),
(23, 'Carolina Ramírez', 'carolina@example.com', 'abc123', 'usuario'),
(24, 'Juan Carlos Rodríguez', 'juancarlos@example.com', 'pass1234', 'usuario'),
(25, 'Ana María Pérez', 'anamaria@example.com', 'test123', 'usuario'),
(26, 'Carlos Sánchez', 'carlos@example.com', 'password', 'usuario'),
(27, 'Laura Martínez', 'laura@example.com', 'abc12345', 'usuario'),
(28, 'Roberto Gómez', 'roberto@example.com', 'password12345', 'admin'),
(29, 'María Torres', 'maria@example.com', 'test1234', 'usuario'),
(30, 'Pedro Ramírez', 'pedro@example.com', 'abc123456', 'usuario'),
(31, 'Sofía García', 'sofia@example.com', 'password123456', 'usuario'),
(32, 'Fernando López', 'fernando@example.com', 'test12345', 'usuario'),
(33, 'Marta González', 'marta@example.com', 'abc1234567', 'usuario'),
(34, 'Javier Sánchez', 'javier@example.com', 'password1234567', 'admin'),
(35, 'Camila Martínez', 'camila@example.com', 'test123456', 'usuario'),
(36, 'David Gómez', 'david@example.com', 'abc12345678', 'usuario'),
(37, 'Isabella Torres', 'isabella@example.com', 'password12345678', 'usuario'),
(38, 'Manuel Ramírez', 'manuel@example.com', 'test1234567', 'usuario'),
(39, 'Valentina García', 'valentina@example.com', 'abc123456789', 'usuario'),
(40, 'Andrés López', 'andres@example.com', 'password123456789', 'admin'),
(41, 'Luisa López', 'luisa@example.com', 'qwerty123', 'usuario'),
(42, 'Mario González', 'mario@example.com', 'password123', 'admin'),
(43, 'Carolina Ramírez', 'carolina@example.com', 'abc123', 'usuario'),
(44, 'Juan Carlos Rodríguez', 'juancarlos@example.com', 'pass1234', 'usuario'),
(45, 'Ana María Pérez', 'anamaria@example.com', 'test123', 'usuario'),
(46, 'Carlos Sánchez', 'carlos@example.com', 'password', 'usuario'),
(47, 'Laura Martínez', 'laura@example.com', 'abc12345', 'usuario'),
(48, 'Roberto Gómez', 'roberto@example.com', 'password12345', 'admin'),
(49, 'María Torres', 'maria@example.com', 'test1234', 'usuario'),
(50, 'Pedro Ramírez', 'pedro@example.com', 'abc123456', 'usuario'),
(51, 'Sofía García', 'sofia@example.com', 'password123456', 'usuario'),
(52, 'Fernando López', 'fernando@example.com', 'test12345', 'usuario'),
(53, 'Marta González', 'marta@example.com', 'abc1234567', 'usuario'),
(54, 'Javier Sánchez', 'javier@example.com', 'password1234567', 'admin'),
(55, 'Camila Martínez', 'camila@example.com', 'test123456', 'usuario'),
(56, 'David Gómez', 'david@example.com', 'abc12345678', 'usuario'),
(57, 'Isabella Torres', 'isabella@example.com', 'password12345678', 'usuario'),
(58, 'Manuel Ramírez', 'manuel@example.com', 'test1234567', 'usuario'),
(59, 'Valentina García', 'valentina@example.com', 'abc123456789', 'usuario'),
(60, 'Andrés López', 'andres@example.com', 'password123456789', 'admin'),
(61, 'Luisa López', 'luisa@example.com', 'qwerty123', 'usuario'),
(62, 'Mario González', 'mario@example.com', 'password123', 'admin'),
(63, 'Carolina Ramírez', 'carolina@example.com', 'abc123', 'usuario'),
(64, 'Juan Carlos Rodríguez', 'juancarlos@example.com', 'pass1234', 'usuario'),
(65, 'Ana María Pérez', 'anamaria@example.com', 'test123', 'usuario'),
(66, 'Carlos Sánchez', 'carlos@example.com', 'password', 'usuario'),
(67, 'Laura Martínez', 'laura@example.com', 'abc12345', 'usuario'),
(68, 'Roberto Gómez', 'roberto@example.com', 'password12345', 'admin'),
(69, 'María Torres', 'maria@example.com', 'test1234', 'usuario'),
(70, 'Pedro Ramírez', 'pedro@example.com', 'abc123456', 'usuario'),
(71, 'Sofía García', 'sofia@example.com', 'password123456', 'usuario'),
(72, 'Fernando López', 'fernando@example.com', 'test12345', 'usuario'),
(73, 'Marta González', 'marta@example.com', 'abc1234567', 'usuario'),
(74, 'Javier Sánchez', 'javier@example.com', 'password1234567', 'admin'),
(75, 'Camila Martínez', 'camila@example.com', 'test123456', 'usuario'),
(76, 'David Gómez', 'david@example.com', 'abc12345678', 'usuario'),
(77, 'Isabella Torres', 'isabella@example.com', 'password12345678', 'usuario'),
(78, 'Manuel Ramírez', 'manuel@example.com', 'test1234567', 'usuario'),
(79, 'Valentina García', 'valentina@example.com', 'abc123456789', 'usuario'),
(80, 'Andrés López', 'andres@example.com', 'password123456789', 'admin');

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `donacion`
--
ALTER TABLE `donacion`
  ADD CONSTRAINT `donacion_ibfk_1` FOREIGN KEY (`donante_id`) REFERENCES `donante` (`donante_id`),
  ADD CONSTRAINT `donacion_ibfk_2` FOREIGN KEY (`donante_id`) REFERENCES `donante` (`donante_id`),
  ADD CONSTRAINT `donacion_ibfk_3` FOREIGN KEY (`tipo_sangre`) REFERENCES `almacenamiento` (`tipo_sangre`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
