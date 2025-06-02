-- tables
-- Table: car_rentals
CREATE TABLE car_rentals (
    ID int  NOT NULL IDENTITY,
    ClientID int  NOT NULL,
    CarID int  NOT NULL,
    DateFrom datetime  NOT NULL,
    DateTo datetime  NOT NULL,
    TotalPrice int  NOT NULL,
    Discount int  NULL,
    CONSTRAINT car_rentals_pk PRIMARY KEY  (ID)
);

-- Table: cars
CREATE TABLE cars (
    ID int  NOT NULL IDENTITY,
    VIN nvarchar(17)  NOT NULL,
    Name nvarchar(100)  NOT NULL,
    Seats int  NOT NULL,
    PricePerDay int  NOT NULL,
    ModelID int  NOT NULL,
    ColorID int  NOT NULL,
    CONSTRAINT cars_pk PRIMARY KEY  (ID)
);

-- Table: clients
CREATE TABLE clients (
    ID int  NOT NULL IDENTITY,
    FirstName nvarchar(50)  NOT NULL,
    LastName nvarchar(100)  NOT NULL,
    Address nvarchar(100)  NOT NULL,
    CONSTRAINT clients_pk PRIMARY KEY  (ID)
);

-- Table: colors
CREATE TABLE colors (
    ID int  NOT NULL IDENTITY,
    Name nvarchar(100)  NOT NULL,
    CONSTRAINT colors_pk PRIMARY KEY  (ID)
);

-- Table: models
CREATE TABLE models (
    ID int  NOT NULL IDENTITY,
    Name nvarchar(100)  NOT NULL,
    CONSTRAINT models_pk PRIMARY KEY  (ID)
);

-- foreign keys
-- Reference: car_rentals_cars (table: car_rentals)
ALTER TABLE car_rentals ADD CONSTRAINT car_rentals_cars
    FOREIGN KEY (CarID)
    REFERENCES cars (ID);

-- Reference: car_rentals_clients (table: car_rentals)
ALTER TABLE car_rentals ADD CONSTRAINT car_rentals_clients
    FOREIGN KEY (ClientID)
    REFERENCES clients (ID);

-- Reference: cars_colors (table: cars)
ALTER TABLE cars ADD CONSTRAINT cars_colors
    FOREIGN KEY (ColorID)
    REFERENCES colors (ID);

-- Reference: cars_models (table: cars)
ALTER TABLE cars ADD CONSTRAINT cars_models
    FOREIGN KEY (ModelID)
    REFERENCES models (ID);

-- End of file.


INSERT INTO clients
VALUES ('Jan', 'Kowalski', 'Koszykowa 86');
INSERT INTO clients
VALUES ('Anna', 'Nowa', 'Złota 44');

INSERT INTO colors
VALUES ('red');
INSERT INTO colors
VALUES ('black');
INSERT INTO colors
VALUES ('white');

INSERT INTO models
VALUES ('Mazda');
INSERT INTO models
VALUES ('Toyota');
INSERT INTO models
VALUES ('Skoda');
INSERT INTO models
VALUES ('Ford');

INSERT INTO cars
VALUES ('2D4HN11EX9R686008', 'Toyota Yaris', 5, 120, 2, 3);
INSERT INTO cars
VALUES ('JTDBR32E630013672', 'Skoda Fabia Estate', 5, 170, 3, 2);

INSERT INTO car_rentals
VALUES (1, 1, '2024-06-24', '2024-06-28', 480, NULL);
INSERT INTO car_rentals
VALUES (1, 1, '2024-07-01', '2024-07-05', 240, 50);
INSERT INTO car_rentals
VALUES (1, 2, '2024-08-01', '2024-08-10', 1700, NULL);
