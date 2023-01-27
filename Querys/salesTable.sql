CREATE TABLE sales (
    id INT NOT NULL PRIMARY KEY IDENTITY,
    training VARCHAR (100) NOT NULL,
    customer VARCHAR (100) NOT NULL,
    dogName VARCHAR (100) NOT NULL,
    trainer VARCHAR (100) NOT NULL,
    salePrice DECIMAL (6,2) NOT NULL,
    saleDate VARCHAR (100) NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO sales (training, customer, dogName, trainer, salePrice, saleDate)
VALUES
('Sit and Stay', 'Randolph Beltran', 'Commander', 'Althea Tuttle', 299.99, '2020-08-04'),
('Fetch', 'Marion Fee', 'Spot', 'Althea Tuttle', 199.99, '2022-03-20'),
('Handshake','Katie Calderon', 'Cupcake', 'John Ruffner', 99.99, '2022-07-03'),
('Cattle Dog Special', 'Katie Calderon', 'BigMah', 'Rebecca Mancuso', 599.99, '2022-01-29'),
('Intruder Protection', 'Isabel Scott', 'Overlord', 'Mark Offutt', 1499.99, '2021-12-20');