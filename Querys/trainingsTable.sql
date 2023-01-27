CREATE TABLE trainings (
    id INT NOT NULL PRIMARY KEY IDENTITY,
    title VARCHAR (100) NOT NULL UNIQUE,
    price DECIMAL (6,2) NOT NULL,
    commission DECIMAL (2,2) NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO trainings (title, price, commission)
VALUES
('Sit and Stay', 299.99, 0.15),
('Fetch', 199.99, 0.10),
('Cattle Dog Special', 599.99, 0.30),
('Intruder Protection', 1499.99, 0.30),
('Handshake', 99.99, 0.05);