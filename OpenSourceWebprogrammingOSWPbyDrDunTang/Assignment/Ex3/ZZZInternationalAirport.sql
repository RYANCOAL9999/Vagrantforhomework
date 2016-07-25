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
('2016-08-05', '20:20:00', 'CX369', 'Shanghai/PVG', 'Cathay Pacific', '', 'Est at 21:33'),
('2016-08-05','', 'KA5369', '', 'Dragonair', '', ''),
('2016-08-05','20:20:00', 'HX158', 'Haikou', 'Hong Kong Airlines', '', 'Cancelled'),
('2016-08-05','20:20:00', 'KA221', 'Da Nang', 'Dragonair', '', 'Est at 20:23'),
('2016-08-05','', 'CX5221', '', 'Cathay Pacific', '', ''),
('2016-08-05','20:20:00', 'GK063', 'Osaka/Kansai', 'Jetstar Japan', '', 'Est at 20:42'),
('2016-08-05','20:25:00', 'CX776', 'Jakarta', 'Cathay Pacific', 'A', 'At gate 19:58'),
('2016-08-05','', 'AA8912', '', 'American Airlines', '', ''),
('2016-08-05','', 'BA4574', '', 'British Airways', ' ', 'Est at 21:33'),
('2016-08-05','20:25:00', 'Cx807', 'Chicago', 'Cathay Pacific', 'A', 'At gate 19:56'),
('2016-08-05','', 'AA8922', '', 'American Airlines', ' ', ''),
('2016-08-05','20:30:00', 'CI937', 'Kaohsiung', 'China Airlines', '', 'Est at 21:20'),
('2016-08-05','20:35:00', 'FD524', 'Phuket', 'Thai AirAsia', 'C', 'At gate 20:10'),
('2016-08-05','20:35:00', 'AK130', 'Kuala Lumpur', 'AirPacific', '', 'Est at 20:32'),

INSERT INTO Arrival (`Date`,`Time`, `Flight`, `Origin`, `Airline`, `Hall`, `Status`) VALUES 
(NOW(), '20:20:00', 'CX369', 'Shanghai/PVG', 'Cathay Pacific', '', 'Est at 21:33'),
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
('2016-08-07', '20:20:00', 'CX369', 'Shanghai/PVG', 'Cathay Pacific', '', 'Est at 21:33'),
('2016-08-07','', 'KA5369', '', 'Dragonair', '', ''),
('2016-08-07','20:20:00', 'HX158', 'Haikou', 'Hong Kong Airlines', '', 'Cancelled'),
('2016-08-07','20:20:00', 'KA221', 'Da Nang', 'Dragonair', '', 'Est at 20:23'),
('2016-08-07','', 'CX5221', '', 'Cathay Pacific', '', ''),
('2016-08-07','20:20:00', 'GK063', 'Osaka/Kansai', 'Jetstar Japan', '', 'Est at 20:42'),
('2016-08-07','20:25:00', 'CX776', 'Jakarta', 'Cathay Pacific', 'A', 'At gate 19:58'),
('2016-08-07','', 'AA8912', '', 'American Airlines', '', ''),
('2016-08-07','', 'BA4574', '', 'British Airways', ' ', 'Est at 21:33'),

INSERT INTO Arrival (`Date`,`Time`, `Flight`, `Origin`, `Airline`, `Hall`, `Status`) VALUES 
('2016-08-08', '20:20:00', 'CX369', 'Shanghai/PVG', 'Cathay Pacific', '', 'Est at 21:33'),
('2016-08-08','', 'KA5369', '', 'Dragonair', '', ''),
('2016-08-08','20:20:00', 'HX158', 'Haikou', 'Hong Kong Airlines', '', 'Cancelled'),
('2016-08-08','20:20:00', 'KA221', 'Da Nang', 'Dragonair', '', 'Est at 20:23'),
('2016-08-08','', 'CX5221', '', 'Cathay Pacific', '', ''),
('2016-08-08','20:20:00', 'GK063', 'Osaka/Kansai', 'Jetstar Japan', '', 'Est at 20:42'),
('2016-08-08','20:25:00', 'CX776', 'Jakarta', 'Cathay Pacific', 'A', 'At gate 19:58'),
('2016-08-08','', 'AA8912', '', 'American Airlines', '', ''),
('2016-08-08','', 'BA4574', '', 'British Airways', ' ', 'Est at 21:33'),

INSERT INTO Departure (`Date`, `Time`, `Flight`, `Destination`, `Terminal`,`Airline`, `Gate`, `Status`) VALUES
('2016-08-05','20:30:00', 'MH 433', 'Kuala Lumpur', 'T1 ', 'Dragonair', 28, 'Gate Closed'),
('2016-08-05','20:30:00', 'QF 030', 'Melboume', 'T1 ', 'Cathay Pacific', 25, 'Gate Closed'),
('2016-08-05','20:45:00', 'MF 8016', 'Xiamen', 'T1 ', 'American Airlines', 41, 'Est 21:00'),
('2016-08-05','20:45:00', 'TG 607', 'BangKok', 'T2 ', 'American Airlines', 50, 'BoardingSoon'),
('2016-08-05','20:50:00', 'HX 452', 'Chengdu', 'T1 ', 'British Airlines', 206, 'BoardingSoon'),
('2016-08-05','20:50:00', 'UO 1226', 'Ningbo', 'T2 ', 'American Airlines', 53, 'Est 21:45'),
('2016-08-05','20:50:00', 'QF 118', 'Sydney', 'T1 ', 'Jetsatar Japan', 24, 'BoardingSoon'),
('2016-08-05','20:55:00', 'HX 496', 'Chongqing', 'T1 ', 'Thai Airlines', 219, 'BoardingSoon'),

INSERT INTO Departure (`Date`, `Time`, `Flight`, `Destination`, `Terminal`,`Airline`, `Gate`, `Status`) VALUES
(NOW(),'20:30:00', 'MH 433', 'Kuala Lumpur', 'T1 ', 'Dragonair', 28, 'Gate Closed'),
(NOW(),'20:30:00', 'QF 030', 'Melboume', 'T1 ', 'Cathay Pacific', 25, 'Gate Closed'),
(NOW(),'20:45:00', 'MF 8016', 'Xiamen', 'T1 ', 'American Airlines', 41, 'Est 21:00'),
(NOW(),'20:45:00', 'TG 607', 'BangKok', 'T2 ', 'American Airlines', 50, 'BoardingSoon'),
(NOW(),'20:50:00', 'HX 452', 'Chengdu', 'T1 ', 'British Airlines', 206, 'BoardingSoon'),
(NOW(),'20:50:00', 'UO 1226', 'Ningbo', 'T2 ', 'American Airlines', 53, 'Est 21:45'),
(NOW(),'20:50:00', 'QF 118', 'Sydney', 'T1 ', 'Jetsatar Japan', 24, 'BoardingSoon'),
(NOW(),'20:55:00', 'HX 496', 'Chongqing', 'T1 ', 'Thai Airlines', 219, 'BoardingSoon'),
(NOW(),'20:55:00', 'BR 858', 'Taipei', 'T1 ', 'Japan Airlines', 32, 'BoardingSoon'),
(NOW(),'21:00:00', '5J 115', 'Manila', 'T1 ', 'Dragonair', 201, 'BoardingSoon'),
(NOW(),'21:05:00', 'HX 128', 'Hangzhou', 'T1 ', 'Dragonair', 217, 'Est 21:35'),
(NOW(),'21:10:00', 'FD 525', 'Phunket', 'T2 ', 'Dragonair', 203, 'BoardingSoon'),
(NOW(),'21:10:00', 'KQ861', 'BangKok', 'T1 ', 'Cathay Pacific', 27, 'Est 21:00');

INSERT INTO Departure (`Date`, `Time`, `Flight`, `Destination`, `Terminal`,`Airline`, `Gate`, `Status`) VALUES
('2016-08-07','20:55:00', 'HX 496', 'Chongqing', 'T1 ', 'Thai Airlines', 219, 'BoardingSoon'),
('2016-08-07','20:55:00', 'BR 858', 'Taipei', 'T1 ', 'Japan Airlines', 32, 'BoardingSoon'),
('2016-08-07','21:00:00', '5J 115', 'Manila', 'T1 ', 'Dragonair', 201, 'BoardingSoon'),
('2016-08-07','21:05:00', 'HX 128', 'Hangzhou', 'T1 ', 'Dragonair', 217, 'Est 21:35'),
('2016-08-07','21:10:00', 'FD 525', 'Phunket', 'T2 ', 'Dragonair', 203, 'BoardingSoon'),
('2016-08-07','21:10:00', 'KQ861', 'BangKok', 'T1 ', 'Cathay Pacific', 27, 'Est 21:00');

INSERT INTO Departure (`Date`, `Time`, `Flight`, `Destination`, `Terminal`,`Airline`, `Gate`, `Status`) VALUES
('2016-08-08','20:45:00', 'TG 607', 'BangKok', 'T2 ', 'American Airlines', 50, 'BoardingSoon'),
('2016-08-08','20:50:00', 'HX 452', 'Chengdu', 'T1 ', 'British Airlines', 206, 'BoardingSoon'),
('2016-08-08','20:50:00', 'UO 1226', 'Ningbo', 'T2 ', 'American Airlines', 53, 'Est 21:45'),
('2016-08-08','20:50:00', 'QF 118', 'Sydney', 'T1 ', 'Jetsatar Japan', 24, 'BoardingSoon'),
('2016-08-08','20:55:00', 'HX 496', 'Chongqing', 'T1 ', 'Thai Airlines', 219, 'BoardingSoon'),
('2016-08-08','20:55:00', 'BR 858', 'Taipei', 'T1 ', 'Japan Airlines', 32, 'BoardingSoon'),
('2016-08-08','21:00:00', '5J 115', 'Manila', 'T1 ', 'Dragonair', 201, 'BoardingSoon'),
('2016-08-08','21:05:00', 'HX 128', 'Hangzhou', 'T1 ', 'Dragonair', 217, 'Est 21:35'),
('2016-08-08','21:10:00', 'FD 525', 'Phunket', 'T2 ', 'Dragonair', 203, 'BoardingSoon');