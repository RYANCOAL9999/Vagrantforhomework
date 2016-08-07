CREATE DATABASE IF NOT EXISTS `FastContainerShippingLine`;

USE `FastContainerShippingLine`;

CREATE TABLE `Transatlantic` (
  `ID` int NOT NULL PRIMARY KEY AUTO_INCREMENT,
  `Voyage`  int(4),
  `Vessel`  varchar(255), 
  `DepPort` varchar(255),
  `DepDate` date,
  `DepTime` time,
  `ArrPort` varchar(255),
  `ArrDate` date,
  `ArrTime` time
);

CREATE TABLE `Transpacific` (
  `ID` int NOT NULL PRIMARY KEY AUTO_INCREMENT,
  `Voyage`  int(4),
  `Vessel`  varchar(255), 
  `DepPort` varchar(255),
  `DepDate` date,
  `DepTime` time,
  `ArrPort` varchar(255),
  `ArrDate` date,
  `ArrTime` time
);

INSERT INTO `Transatlantic` (`ID`, `Voyage`, `Vessel`, `DepPort`, `DepDate`, `DepTime`, `ArrPort`, `ArrDate`, `ArrTime`) VALUES
(NULL, '6566', 'KAM MEXICO', 'Houston', '2016-05-11', '15:00:00', 'Rotterdam', '2016-05-28','19:00:00'), 
(NULL, '6772', 'Norton Express', 'Virginia', '2016-05-28', '11:00:00', 'Southampton', '2016-05-31', '00:00:00'), 
(NULL, '6561', 'ACS VIRGINIA', 'Savannah', '2016-05-18', '15:00:00', 'Rotterdam', '2016-06-04', '19:00:00'),
(NULL, '6770', 'YORK STAR', 'Houston', '2016-05-19', '13:00:00', 'Rotterdam', '2016-05-07','23:00:00'),
(NULL, '6566', 'Altar MEXICO', 'Houston', '2016-05-11', '15:00:00', 'Hamburg', '2016-05-28','19:00:00'),
(NULL, '6666', 'KAM MEXICO', 'England', '2016-05-11', '14:00:00', 'Luala Lumpur', '2016-05-15','14:00:00'), 
(NULL, '6788', 'Norton Express', 'HongKong', '2016-05-21', '13:00:00', 'Ho Chi Minh', '2016-05-16', '02:00:00'), 
(NULL, '6123', 'ACS VIRGINIA', 'Tokyo', '2016-05-12', '12:00:00', 'Luzon', '2016-06-03', '17:00:00'),
(NULL, '6887', 'YORK STAR', 'Shanghai', '2016-05-17', '11:00:00', 'Taiwan', '2016-05-20','21:00:00'),
(NULL, '6123', 'Altar MEXICO', 'Beijing', '2016-05-18', '16:00:00', 'Seoul', '2016-06-28','22:00:00'),
(NULL, '6987', 'KAM MEXICO', 'Paris', '2016-05-12', '17:00:00', 'Pyongyang', '2016-07-28','12:00:00'), 
(NULL, '6283', 'Norton Express', 'Berlin', '2016-05-23', '18:00:00', 'Fukuoka', '2016-07-31', '11:00:00'), 
(NULL, '6162', 'ACS VIRGINIA', 'Parha', '2016-05-21', '19:00:00', 'Hiroshima', '2016-07-04', '17:00:00'),
(NULL, '6872', 'YORK STAR', 'Roma', '2016-05-22', '20:00:00', 'Osaka', '2016-07-07','08:00:00'),
(NULL, '6123', 'Altar MEXICO', 'Madrid', '2016-05-04', '21:00:00', 'Nagoya', '2016-06-28','07:00:00'),
(NULL, '6921', 'YORK STAR', 'Barcelona', '2016-05-21', '22:00:00','Sendai ', '2016-06-07','09:00:00'),
(NULL, '6837', 'Altar MEXICO', 'London', '2016-05-22', '23:00:00', 'Sapporo', '2016-06-14','10:00:00'),
(NULL, '6921', 'KAM MEXICO', 'Istanbul', '2016-05-23', '00:00:00', 'Perth', '2016-05-28','13:00:00'), 
(NULL, '6293', 'Norton Express', 'Actaha', '2016-05-01', '01:00:00', 'Brishban', '2016-05-16', '14:00:00'), 
(NULL, '6827', 'ACS VIRGINIA', 'SiberiaSiberia', '2016-05-02', '02:00:00', 'Sydney', '2016-05-28', '15:00:00'),
(NULL, '6938', 'YORK STAR', 'Budapest', '2016-05-03', '03:00:00', 'Newcastle', '2016-05-29','16:00:00'),
(NULL, '6023', 'YORK STAR', 'Munchen', '2016-05-04', '04:00:00', 'Melbourne', '2016-05-30','17:00:00'),
(NULL, '6291', 'Altar MEXICO', 'Milano', '2016-05-05', '05:00:00', 'TASMANIA', '2016-05-31','18:00:00'),
(NULL, '6928', 'KAM MEXICO', 'Athens', '2016-05-06', '06:00:00', 'Auckland', '2016-05-29','19:00:00'), 
(NULL, '6192', 'Norton Express', 'Antalya', '2016-05-07', '07:00:00', 'Wellington', '2016-05-27', '20:00:00'), 
(NULL, '6912', 'YORK STAR', 'New York', '2016-05-08', '08:00:00', 'Christchurch', '2016-05-28','23:00:00');

INSERT INTO `Transpacific` (`ID`, `Voyage`, `Vessel`, `DepPort`, `DepDate`, `DepTime`, `ArrPort`, `ArrDate`, `ArrTime`) VALUES 
(NULL, '7166', 'Oriental Express', 'Hong Kong', '2016-05-09', '10:00:00', 'Long Beach', '2016-05-28', '19:00:00'),
(NULL, '7729', 'TransGlobe', 'Long Beach', '2016-05-18', '12:00:00', 'Singapore', '2016-06-06','21:00:00'),
(NULL, '5761', 'Venus', 'Tokyo', '2016-05-10', '15:30:00', 'Seattle', '2016-05-30','19:00:00'),
(NULL, '7700', 'Pacific STAR', 'Nagoya', '2016-05-12', '13:00:00', 'Long Beach', '2016-06-02','23:00:00'),
(NULL, '5775', 'La Spezia', 'Singapore', '2016-05-12', '13:00:00', 'Los Angeles', '2016-06-06','23:00:00'),
(NULL, '8172', 'Oriental Express', 'Ottawa', '2016-05-10', '10:00:00', 'Lima District', '2016-05-28', '22:00:00'),
(NULL, '2837', 'TransGlobe', 'Montreal', '2016-05-11', '11:00:00', 'Santiago', '2016-05-28','20:00:00'),
(NULL, '0912', 'Venus', 'Chicago', '2016-05-13', '10:30:00', 'Braslia', '2016-06-30','18:00:00'),
(NULL, '7263', 'Pacific STAR', 'San Diego', '2016-05-14', '09:00:00', 'Sao Paul', '2016-06-07','17:00:00'),
(NULL, '2817', 'La Spezia', 'Los Angeles', '2016-05-15', '08:00:00', 'Asuncion', '2016-06-08','16:00:00'),
(NULL, '9128', 'Oriental Express', 'Las Vegas', '2016-05-16', '07:00:00', 'French Guiana', '2016-06-28', '15:00:00'),
(NULL, '1298', 'TransGlobe', 'San Francisco', '2016-05-17', '06:00:00', 'Guyana', '2016-06-28','14:00:00'),
(NULL, '8123', 'Venus', 'Kansas City', '2016-05-18', '04:30:00', 'Buenos Aires', '2016-06-28','13:00:00'),
(NULL, '9128', 'Pacific STAR', 'Denver', '2016-05-19', '03:00:00', 'Fortaleza', '2016-06-17','12:00:00'),
(NULL, '2913', 'La Spezia', 'Regina', '2016-05-20', '02:00:00', 'Salvador', '2016-06-20','11:00:00'),
(NULL, '9128', 'Oriental Express', 'Saskatoon', '2016-05-21', '01:00:00', 'Recife', '2016-06-28', '10:00:00'),
(NULL, '2193', 'TransGlobe', 'Calgary', '2016-05-22', '00:00:00', 'Port_au_Prince', '2016-06-22','09:00:00'),
(NULL, '0129', 'Venus', 'Edmonton', '2016-05-23', '23:30:00', 'Punta Cana', '2016-06-30','08:00:00'),
(NULL, '1928', 'Pacific STAR', 'Winnipeg', '2016-05-24', '22:00:00', 'Sn Juan', '2016-07-02','07:00:00'),
(NULL, '1298', 'La Spezia', 'Minneapolis', '2016-05-25', '21:00:00', 'La Ceiba', '2016-07-06','06:00:00'),
(NULL, '8127', 'Oriental Express', 'Quito', '2016-05-26', '20:00:00', 'Tegucigalpa', '2016-07-28', '05:00:00'),
(NULL, '3274', 'TransGlobe', 'Lima', '2016-05-27', '19:00:00', 'Managua', '2016-07-06','04:00:00'),
(NULL, '4937', 'Venus','Cordoba', '2016-05-28', '18:30:00', 'Campeche', '2016-08-30','03:00:00'),
(NULL, '9283', 'Pacific STAR', 'Asuncion', '2016-05-29', '17:00:00', 'Tuxtla Gutierrez', '2016-08-02','02:00:00');