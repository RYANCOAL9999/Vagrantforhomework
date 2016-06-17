CREATE TABLE users (
id int AUTO_INCREMENT,
username varchar(50),
email varchar(50),
password int(8),
CustomerID int(8),
PRIMARY KEY (id)
);

INSERT INTO users (username,email,password,CustomerID)
VALUES ('Cardinal', 'Stavanger@gmail.com', '23212321','12345678');


