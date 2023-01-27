CREATE TABLE customer (
    id INT NOT NULL PRIMARY KEY IDENTITY,
    firstName VARCHAR (100) NOT NULL,
    lastName VARCHAR (100) NOT NULL,
    dogBreed VARCHAR (100) NOT NULL,
    dogName VARCHAR (100) NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO customer (firstName, lastName, dogBreed, dogName)
VALUES
('Randolph', 'Beltran', 'Husky', 'Commander'),
('Marion', 'Fee', 'Beagle', 'Spot'),
('Katie', 'Calderon', 'German Shepherd', 'Cupcake'),
('Katie', 'Calderon', 'Border Collie', 'BigMa'),
('Isabel', 'Scott', 'Shih Tzu', 'Overlord');