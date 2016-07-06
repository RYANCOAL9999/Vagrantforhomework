CREATE DATABASE ajax; 
USE ajax;
CREATE TABLE users 
(
user_id INT UNSIGNED NOT NULL auto_increment,
user_name varchar(32) NOT NULL,
PRIMARY KEY (user_id)
);

INSERT INTO users (user_name) VALUES ('bogdan');
INSERT INTO users (user_name) VALUES ('audra');
INSERT INTO users (user_name) VALUES ('cristian');
