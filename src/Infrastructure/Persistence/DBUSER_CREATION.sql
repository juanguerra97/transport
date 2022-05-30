DROP DATABASE IF EXISTS seminarioumg;

CREATE DATABASE seminarioumg;

USE seminarioumg;

DROP USER IF EXISTS umgadmin;

CREATE USER 'umgadmin'@'%' IDENTIFIED BY 'UmgAdmin$123';

GRANT ALL PRIVILEGES ON rentasgt.* TO 'umgadmin'@'%';