CREATE DATABASE IF NOT EXISTS FastContainerShippingLine;

USE FastContainerShippingLine;

CREATE TABLE Transatlantic (
    Voyage int(5),
    Vessel varchar(255),
    DepPort varchar(255),
    Departure Timestamp,
    ArrPort varchar(255),
    Arrival Timestamp
);

INSERT INTO Transatlantic(Voyage, Vessel, DepPort, Departure, ArrPort, Arrival) VALUES
(6566, 'KAM MEXICO', 'Houston', '2016-05-11 15:00:00', 'Rotterdam', '2016-05-28 19:00:00'),
(6772, 'Norton Express','Virginia', '2016-05-28 11:00:00', 'Southampton', '2016-05-31 00:00:00'),
(6561, 'ACS VIRGINIA','Savannah', '2016-05-18 15:00:00', 'Rotterdam', '2016-06-04 19:00:00'),
(6770, 'YORK STAR', 'Houston', '2016-05-19 13:00:00', 'Rotterdam', '2016-06-07 23:00:00'),
(6566, 'Altar MXCICO','Houston', '2016-05-11 15:00:00', 'Hamburg', '2016-05-28 19:00:00');

CREATE TABLE Transpacific (
    Voyage int(5),
    Vessel varchar(255),
    DepPort varchar(255),
    Departure Timestamp,
    ArrPort varchar(255),
    Arrival Timestamp
);

INSERT INTO Transpacific(Voyage, Vessel, DepPort, Departure, ArrPort, Arrival) VALUES
(7166, 'Oriental Express', 'Hong Kong', '2016-05-09 10:00:00', 'Rotterdam', '2016-05-28 19:00:00'),
(7729, 'TransGlobe', 'Long Beach', '2016-05-18 12:00:00', 'Singapore', '2016-06-06 21:00:00'),
(5761, 'Venus', 'Tokyo', '2016-05-10 15:30:00', 'Seattle', '2016-05-30 19:00:00'),
(7700, 'Pacific STAR', 'Nagoya', '2016-05-12 13:00:00','Long Beach', '2016-06-02 23:00:00'),
(5775, 'La Spezia', 'Singapore','2016-05-12 13:00:00', 'Los Angelas', '2016-06-06 23:00:00');

