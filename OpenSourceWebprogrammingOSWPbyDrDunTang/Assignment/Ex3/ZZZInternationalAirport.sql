CREATE DATABASE IF NOT EXISTS `ZZZInternationalAirport`;

USE `ZZZInternationalAirport`;

CREATE TABLE `Arrival` (
  `ID` int NOT NULL PRIMARY KEY AUTO_INCREMENT,
  `Date` date,
  `Time` time, 
  `Flight` varchar(6),
  `Origin` varchar(255),
  `Airline` varchar(255),
  `Hall` varchar(1),
  `Status` varchar(255)
);

CREATE TABLE Departure (
    `ID` int NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `Date` date,
    `Time` time,
    `Flight` varchar(6),
    `Destination` varchar(255),
    `Terminal` varchar(2),
    `Airline` varchar(255),
    `Gate` int(3),
    `Status` varchar(255)
);

INSERT INTO Arrival (`Date`,`Time`, `Flight`, `Origin`, `Airline`, `Hall`, `Status`) VALUES 
('2016-08-05', '10:20:00', 'CX872', 'Kaohsiung', 'Cathay Pacific', '', ''),
('2016-08-05','', 'KA812', '', 'Dragonair', '', ''),
('2016-08-05','12:30:00', 'HX168', 'Osaka/kansai', 'Hong Kong Airlines', 'C', 'At gate 01:54'),
('2016-08-05','14:20:00', 'KA123', 'Haikou', 'Dragonair', '', 'Est at 20:03'),
('2016-08-05','', 'CX839', '', 'Cathay Pacific', '', ''),
('2016-08-05','16:20:00', 'GK9882', 'Haikou', 'Jetstar Japan', '', 'Cancelled'),
('2016-08-05','18:25:00', 'CX7231', 'Shanghai/PVG', 'Cathay Pacific', '', 'Est at 00:03'),
('2016-08-05','', 'AA9921', '', 'American Airlines', '', ''),
('2016-08-05','', 'BA8212', '', 'British Airways', ' ', 'Est at 21:33'),
('2016-08-05','20:25:00', 'CX2138', 'Kuala Lumpur', 'Cathay Pacific', 'A', 'At gate 02:36'),
('2016-08-05','', 'AA1223', '', 'American Airlines', ' ', ''),
('2016-08-05','20:30:00', 'CI8273', 'Da Nang', 'China Airlines', '', 'Cancelled'),
('2016-08-05','22:35:00', 'FD1233', 'Jetstar', 'Thai AirAsia', '', ''),
('2016-08-05','23:35:00', 'AK8788', 'Osaka/Kansai', 'AirPacific', '', '');

INSERT INTO Arrival (`Date`,`Time`, `Flight`, `Origin`, `Airline`, `Hall`, `Status`) VALUES 
(NOW(), '20:20:00', 'CX369', 'Shanghai', 'Cathay Pacific', '', 'Est at 21:33'),
(NOW(),'', 'KA5369', '', 'Dragonair', '', ''),
(NOW(),'20:20:00', 'HX158', 'Haikou', 'Hong Kong Airlines', '', 'Cancelled'),
(NOW(),'20:20:00', 'KA221', 'Da Nang', 'Dragonair', '', 'Est at 20:23'),
(NOW(),'', 'CX5221', '', 'Cathay Pacific', '', ''),
(NOW(),'20:20:00', 'GK063', 'Osaka/Kansai', 'Jetstar Japan', '', 'Est at 20:42'),
(NOW(),'20:25:00', 'CX776', 'Jakarta', 'Cathay Pacific', 'A', 'At gate 19:58'),
(NOW(),'', 'AA8912', '', 'American Airlines', '', ''),
(NOW(),'', 'BA4574', '', 'British Airways', ' ', 'Est at 21:33'),
(NOW(),'20:25:00', 'Cx807', 'Chicago', 'Cathay Pacific', 'A', 'At gate 19:56'),
(NOW(),'', 'AA8922', '', 'American Airlines', ' ', ''),
(NOW(),'20:30:00', 'CI937', 'Kaohsiung', 'China Airlines', '', 'Est at 21:20'),
(NOW(),'20:35:00', 'FD524', 'Phuket', 'Thai AirAsia', 'C', 'At gate 20:10'),
(NOW(),'20:35:00', 'AK130', 'Kuala Lumpur', 'AirPacific', '', 'Est at 20:32'),
(NOW(),'20:40:00', 'CX521', 'Tokyo/NRT', 'Cathay Pacific', '', 'Est at 21:45'),
(NOW(),'', 'QR5807', '', 'Qatar Airways', '', ''),
(NOW(),'', 'JL7047', '', 'Japan Airlines', '', ''),
(NOW(),'20:40:00', 'CX581', 'Sapporo', 'Cathay Pacific', ' ', 'Est at 21:14'),
(NOW(),'', 'JL7061', '', 'Japan Airlines', '', ''),
(NOW(),'20:45:00', 'HX693', 'Sapporo', 'Hong Kong Airlines', ' ', 'Est at 21:18');

INSERT INTO Arrival (`Date`,`Time`, `Flight`, `Origin`, `Airline`, `Hall`, `Status`) VALUES 
('2016-08-07', '09:20:00', 'CX1238', 'Shanghai/PVG', 'Cathay Pacific', '', 'Est at 15:56'),
('2016-08-07','', 'KA1234', '', 'Dragonair', '', ''),
('2016-08-07','10:20:00', 'HX898', '', 'Hong Kong Airlines', '', 'Cancelled'),
('2016-08-07','12:20:00', '', '', 'Dragonair', '', 'Cancelled'),
('2016-08-07','', 'CX8723', 'Shanghai/PVG', 'Cathay Pacific', '', ''),
('2016-08-07','13:20:00', 'GK8273', 'Osaka/Kansai', 'Jetstar Japan', '', 'Est at 04:47'),
('2016-08-07','15:25:00', 'CX9823', 'Jakarta', 'Cathay Pacific', '', 'Est at 04:47'),
('2016-08-07','', 'AA8921', '', 'American Airlines', 'C', 'At gate 21:10'),
('2016-08-07','', 'BA821', '', 'British Airways', 'D', 'At gate 21:54');

INSERT INTO Arrival (`Date`,`Time`, `Flight`, `Origin`, `Airline`, `Hall`, `Status`) VALUES 
('2016-08-08', '10:20:00', 'CX558', 'Phuket', 'Cathay Pacific', '', 'Est at 14:31'),
('2016-08-08','', 'KA213', '', 'Haikou', '', ''),
('2016-08-08','12:20:00', 'HX988', 'Kaohsiung', 'Hong Kong Airlines', 'C', 'At gate 03:12'),
('2016-08-08','14:20:00', 'KA123', 'Osaka/kansai', 'Dragonair', '', 'Cancelled'),
('2016-08-08','', 'CX982', '', 'Cathay Pacific', '', ''),
('2016-08-08','16:20:00', 'GK989', 'Osaka/Kansai', 'Jetstar Japan', '', 'Est at 20:42'),
('2016-08-08','18:25:00', 'CX9823', '', 'Cathay Pacific', 'A', 'At gate 19:58'),
('2016-08-08','18:55:00', 'AA7688', 'Tokyo/NRT', 'American Airlines', '', 'Est at 23:55'),
('2016-08-08','', 'BA9898', '', 'British Airways', ' ', '');

INSERT INTO Departure (`Date`, `Time`, `Flight`, `Destination`, `Terminal`,`Airline`, `Gate`, `Status`) VALUES
('2016-08-05','10:30:00', 'MH 3192', 'Ningbo', 'T2', 'Dragonair', 28, 'BoardingSoon'),
('2016-08-05','12:30:00', 'QF 8878', 'Xiamen', 'T2', 'Cathay Pacific', 25, 'Gate Closed'),
('2016-08-05','14:45:00', 'MF 0990', 'Melbounce', 'T1', 'American Airlines', 41, 'Est 14:39'),
('2016-08-05','16:45:00', 'TG 878', 'Kuala Lumpur', 'T2', 'American Airlines', 50, 'Gate Closed'),
('2016-08-05','18:50:00', 'HX 8787', 'Manila', 'T4', 'British Airlines', 206, 'Gate Closed'),
('2016-08-05','20:50:00', 'UO 7263', 'Kuala Lumpur', 'T2', 'American Airlines', 53, 'Est 21:45'),
('2016-08-05','21:50:00', 'QF 8726', 'Sydney', 'T1', 'Jetsatar Japan', 24, 'BoardingSoon'),
('2016-08-05','22:55:00', 'HX 7627', 'Chongqing', 'T3', 'Thai Airlines', 219, 'Est 23:33'),

INSERT INTO Departure (`Date`, `Time`, `Flight`, `Destination`, `Terminal`,`Airline`, `Gate`, `Status`) VALUES
(NOW(),'20:30:00', 'MH 433', 'Kuala Lumpur', 'T1', 'Dragonair', 28, 'Gate Closed'),
(NOW(),'20:30:00', 'QF 030', 'Melboume', 'T1', 'Cathay Pacific', 25, 'Gate Closed'),
(NOW(),'20:45:00', 'MF 8016', 'Xiamen', 'T1', 'American Airlines', 41, 'Est 21:00'),
(NOW(),'20:45:00', 'TG 607', 'BangKok', 'T2', 'American Airlines', 50, 'BoardingSoon'),
(NOW(),'20:50:00', 'HX 452', 'Chengdu', 'T1', 'British Airlines', 206, 'BoardingSoon'),
(NOW(),'20:50:00', 'UO 1226', 'Ningbo', 'T2', 'American Airlines', 53, 'Est 21:45'),
(NOW(),'20:50:00', 'QF 118', 'Sydney', 'T1', 'Jetsatar Japan', 24, 'BoardingSoon'),
(NOW(),'20:55:00', 'HX 496', 'Chongqing', 'T1', 'Thai Airlines', 219, 'BoardingSoon'),
(NOW(),'20:55:00', 'BR 858', 'Taipei', 'T1', 'Japan Airlines', 32, 'BoardingSoon'),
(NOW(),'21:00:00', 'SJ 115', 'Manila', 'T1', 'Dragonair', 201, 'BoardingSoon'),
(NOW(),'21:05:00', 'HX 128', 'Hangzhou', 'T1', 'Dragonair', 217, 'Est 21:35'),
(NOW(),'21:10:00', 'FD 525', 'Phunket', 'T2', 'Dragonair', 203, 'BoardingSoon'),
(NOW(),'21:10:00', 'KQ861', 'BangKok', 'T1', 'Cathay Pacific', 27, 'Est 21:00');

INSERT INTO Departure (`Date`, `Time`, `Flight`, `Destination`, `Terminal`,`Airline`, `Gate`, `Status`) VALUES
('2016-08-07','08:55:00', 'HX 989', 'Xiamen','T1 ', 'Thai Airlines', 219, 'Est 21:40'),
('2016-08-07','10:55:00', 'BR7672', 'Chengdu', 'T1', 'Japan Airlines', 32, 'BoardingSoon'),
('2016-08-07','12:00:00', 'SJ 988', 'Ningo', 'T1', 'Dragonair', 201, 'Gate Closed'),
('2016-08-07','16:05:00', 'HX8378', 'Kuala Lumpur', 'T1', 'Dragonair', 217, 'Est 12:12'),
('2016-08-07','20:10:00', 'FD7126', 'Chongqing', 'T2', 'Dragonair', 203, 'BoardingSoon'),
('2016-08-07','23:10:00', 'KQ8712', 'Manila', 'T1', 'Cathay Pacific', 27, 'BoardingSoon');

INSERT INTO Departure (`Date`, `Time`, `Flight`, `Destination`, `Terminal`,`Airline`, `Gate`, `Status`) VALUES
('2016-08-08','10:45:00', 'TG8273', 'Kuala Lumpur', 'T2', 'American Airlines', 50, 'BoardingSoon'),
('2016-08-08','12:50:00', 'HX7678', 'Ningbo', 'T3', 'British Airlines', 206, 'Gate Closed'),
('2016-08-08','14:50:00', 'UO2187', 'Ningbo', 'T2', 'American Airlines', 53, 'Gate Closed'),
('2016-08-08','15:50:00', 'QF8321', 'Melbounce', 'T2', 'Jetsatar Japan', 24, 'Est at 21:07'),
('2016-08-08','16:55:00', 'HX7628', 'Cheng du', 'T1', 'Thai Airlines', 219, 'BoardingSoon'),
('2016-08-08','17:55:00', 'BR2837', 'Chongqing', 'T3', 'Japan Airlines', 32, 'Gate Closed'),
('2016-08-08','18:00:00', 'SJ9892', 'Hangzhou', 'T1', 'Dragonair', 201, 'BoardingSoon'),
('2016-08-08','21:05:00', 'HX8728', 'Melbounce', 'T1', 'Dragonair', 217, 'Est 21:35'),
('2016-08-08','23:10:00', 'FD8218', 'Phunket', 'T2', 'Dragonair', 203, 'BoardingSoon');
