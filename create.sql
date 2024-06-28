CREATE TABLE Customers
(
    CustomerID  INT PRIMARY KEY IDENTITY(1,1),
    FirstName   NVARCHAR(50) NOT NULL,
    LastName    NVARCHAR(50) NOT NULL,
    Address     NVARCHAR(100) NOT NULL,
    Email       NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,
    PESEL       CHAR(11) NOT NULL UNIQUE,
    IsDeleted   BIT      NOT NULL DEFAULT 0
);

CREATE TABLE Companies
(
    CompanyID   INT PRIMARY KEY IDENTITY(1,1),
    CompanyName NVARCHAR(100) NOT NULL,
    Address     NVARCHAR(100) NOT NULL,
    Email       NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,
    KRS         CHAR(10) NOT NULL UNIQUE
);

CREATE TABLE Software
(
    SoftwareID     INT PRIMARY KEY IDENTITY(1,1),
    Name           NVARCHAR(100) NOT NULL,
    Description    NVARCHAR(MAX),
    CurrentVersion NVARCHAR(20),
    Category       NVARCHAR(50)
);


CREATE TABLE Discounts
(
    DiscountID         INT PRIMARY KEY IDENTITY(1,1),
    Name               NVARCHAR(100) NOT NULL,
    Description        NVARCHAR(MAX),
    DiscountPercentage DECIMAL(5, 2) NOT NULL,
    StartDate          DATE          NOT NULL,
    EndDate            DATE          NOT NULL
);


CREATE TABLE Contracts
(
    ContractID  INT PRIMARY KEY IDENTITY(1,1),
    CustomerID  INT            NOT NULL,
    SoftwareID  INT            NOT NULL,
    StartDate   DATE           NOT NULL,
    EndDate     DATE           NOT NULL,
    TotalAmount DECIMAL(18, 2) NOT NULL,
    Payed       DECIMAL(18, 2) NOT NULL DEFAULT 0,
    DiscountID  INT,
    IsSigned    BIT            NOT NULL DEFAULT 0,
    IsActive    BIT            NOT NULL DEFAULT 1,
    FOREIGN KEY (CustomerID) REFERENCES Customers (CustomerID),
    FOREIGN KEY (SoftwareID) REFERENCES Software (SoftwareID),
    FOREIGN KEY (DiscountID) REFERENCES Discounts (DiscountID)
);


CREATE TABLE ContractsCompanies
(
    ContractID  INT PRIMARY KEY IDENTITY(1,1),
    CompanyID   INT            NOT NULL,
    SoftwareID  INT            NOT NULL,
    StartDate   DATE           NOT NULL,
    EndDate     DATE           NOT NULL,
    TotalAmount DECIMAL(18, 2) NOT NULL,
    Payed       DECIMAL(18, 2) NOT NULL DEFAULT 0,
    DiscountID  INT,
    IsSigned    BIT            NOT NULL DEFAULT 0,
    IsActive    BIT            NOT NULL DEFAULT 1,
    FOREIGN KEY (CompanyID) REFERENCES Companies (CompanyID),
    FOREIGN KEY (SoftwareID) REFERENCES Software (SoftwareID),
    FOREIGN KEY (DiscountID) REFERENCES Discounts (DiscountID)
);


CREATE TABLE Payments
(
    PaymentID   INT PRIMARY KEY IDENTITY(1,1),
    ContractID  INT            NOT NULL,
    PaymentDate DATE           NOT NULL,
    Amount      DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (ContractID) REFERENCES Contracts (ContractID)
);


CREATE TABLE PaymentsCompanies
(
    PaymentID   INT PRIMARY KEY IDENTITY(1,1),
    ContractID  INT            NOT NULL,
    PaymentDate DATE           NOT NULL,
    Amount      DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (ContractID) REFERENCES ContractsCompanies (ContractID)
);

CREATE TABLE AppUsers
(
    AppUserID        int IDENTITY(1,1) NOT NULL,
    Login            varchar(100) NOT NULL,
    Email            varchar(100) NOT NULL,
    Role             varchar(50)  NOT NULL,
    Password         nvarchar(max) NOT NULL,
    Salt             nvarchar(max) NOT NULL,
    RefreshToken     nvarchar(max) NOT NULL,
    RefreshTockenExp datetime     NOT NULL,
    CONSTRAINT AppUsers_pk PRIMARY KEY (AppUserID)
);
