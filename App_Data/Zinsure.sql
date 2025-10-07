CREATE TABLE Insurees
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    EmailAddress NVARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL,
    CarYear INT NOT NULL,
    CarMake NVARCHAR(50) NOT NULL,
    CarModel NVARCHAR(50) NOT NULL,
    DUI BIT NOT NULL,
    SpeedingTickets INT NOT NULL,
    FullCoverage BIT NOT NULL,
    Quote DECIMAL(18,2) NOT NULL
);
