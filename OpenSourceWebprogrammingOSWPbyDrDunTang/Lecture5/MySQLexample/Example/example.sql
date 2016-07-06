CREATE DATABASE IF NOT EXISTS example; 
USE example;

CREATE TABLE IF NOT EXISTS `book` (
  `Title` text,
  `Author` text,
  `ISBN` text,
  `Price` decimal(10,2) DEFAULT NULL
);

INSERT INTO `book` (`Title`, `Author`, `ISBN`,`Price`) VALUES
('Agnes Grey', 'Emily Bronte', '978-1853262166',200.00),
('Wuthering Height', 'Emily Bronte', '978-o486292564',210.00),
('Lightman', 'Mary Awjar', '978-8152361266',190.00),
('Island of The Cloud', 'Ben Hill', '978-1582326131',185.00);
