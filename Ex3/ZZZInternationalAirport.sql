CREATE DATABASE ZZZInternationalAirport;

CREATE TABLE Arrival (
  Time varchar(5),
  Flight varchar(6),
  Origin varchar(255),
  Airline varchar(255),
  Hall varchar(1),
  Status varchar(255)
);

INSERT INTO Arrival (Time, Flight, Origin, Airline, Hall, Status) VALUES
('20:20', 'CX369', 'Shanghai/PVG', 'Cathay Pacific', '', 'Est at 21:33'),
('', 'KA5369', '', 'Dragonair', '', ''),
('20:20', 'HX158', 'Haikou', 'Hong Kong Airlines', '', 'Cancelled'),
('20:20', 'KA221', 'Da Nang', 'Dragonair', '', 'Est at 20:23'),
('', 'CX5221', ' ', 'Cathay Pacific', '', ''),
('20:20', 'GK063', 'Osaka/Kansai', 'Jetstar Japan', '', 'Est at 20:42'),
('20:25', 'CX776', 'Jakarta', 'Cathay Pacific', 'A', 'At gate 19:58'),
('', 'AA8912', '', 'American Airlines', '', ''),
('', 'BA4574', '', 'British Airways', ' ', 'Est at 21:33'),
('20:25', 'Cx807', 'Chicago', 'Cathay Pacific', 'A', 'At gate 19:56'),
('', 'AA8922', '', 'American Airlines', ' ', ''),
('20:30', 'CI937', 'Kaohsiung', 'China Airlines', '', 'Est at 21:20'),
('20:35', 'FD524', 'Phuket', 'Thai AirAsia', 'C', 'At gate 20:10'),
('20:35', 'AK130', 'Kuala Lumpur', 'AirPacific', '', 'Est at 20:32'),
('20:40', 'CX521', 'Tokyo/NRT', 'Cathay Pacific', '', 'Est at 21:45'),
('', 'QR5807', '', 'Qatar Airways', '', ''),
('', 'JL7047', '', 'Japan Airlines', '', ''),
('20:40', 'CX581', 'Sapporo', 'Cathay Pacific', ' ', 'Est at 21:14'),
('', 'JL7061', '', 'Japan Airlines', '', ''),
('20:45', 'HX693', 'Sapporo', 'Hong Kong Airlines', ' ', 'Est at 21:18');

CREATE TABLE Departure (
  Time varchar(5),
  Flight varchar(6),
  Destination varchar(255),
  Terminal varchar(2),
  Gate int(3),
  Status varchar(255)
);



INSERT INTO Departure (Time, Flight, Destination, Terminal, Gate, Status) VALUES
('20:30', 'MH 433', 'Kuala Lumpur', 'T1 ', 28, 'Gate Closed'),
('20:30', 'QF 030', 'Melboume', 'T1 ', 25, 'Gate Closed'),
('20:45', 'MF 8016', 'Xiamen', 'T1 ', 41, 'Est 21:00'),
('20:45', 'TG 607', 'BangKok', 'T2 ', 50, 'BoardingSoon'),
('20:50', 'HX 452', 'Chengdu', 'T1 ', 206, 'BoardingSoon'),
('20:50', 'UO 1226', 'Ningbo', 'T2 ', 53, 'Est 21:45'),
('20:50', 'QF 118', 'Sydney', 'T1 ', 24, 'BoardingSoon'),
('20:55', 'HX 496', 'Chongqing', 'T1 ', 219, 'BoardingSoon'),
('20:55', 'BR 858', 'Taipei', 'T1 ', 32, 'BoardingSoon'),
('21:00', '5J 115', 'Manila', 'T1 ', 201, 'BoardingSoon'),
('21:05', 'HX 128', 'Hangzhou', 'T1 ', 217, 'Est 21:35'),
('21:10', 'FD 525', 'Phunket', 'T2 ', 203, 'BoardingSoon'),
('21:10', 'KQ861', 'BangKok', 'T1 ', 27, 'Est 21:00');
