CREATE TABLE trainer (
    id INT NOT NULL PRIMARY KEY IDENTITY,
    firstName VARCHAR (100) NOT NULL UNIQUE,
    lastName VARCHAR (100) NOT NULL UNIQUE,
    startDate VARCHAR (100) NOT NULL,
    terminationDate VARCHAR (100) NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO trainer (firstName, lastName, startDate, terminationDate)
VALUES
('Mark', 'Offutt', '2020-05-12', '2021-12-29'),
('Althea', 'Tuttle', '2020-07-04', 'N/A'),
('Rebecca', 'Mancuso', '2021-12-29', 'N/A'),
('John', 'Ruffner', '2022-06-03', 'N/A');