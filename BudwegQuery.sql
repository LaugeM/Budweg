-- ============================================================
-- CREATE DATABASE BudwegDB
-- ============================================================

USE BudwegDB

-- ============================================================
-- DROP TABLES
-- ============================================================
--DROP TABLE IF EXISTS dbo.STATION_EMPLOYEE;
--DROP TABLE IF EXISTS dbo.BATCH_STATION;
--DROP TABLE IF EXISTS dbo.RENOVATION;
--DROP TABLE IF EXISTS dbo.CALIPER;
--DROP TABLE IF EXISTS dbo.EMPLOYEE;
--DROP TABLE IF EXISTS dbo.STATION;
--DROP TABLE IF EXISTS dbo.BATCH;
--DROP PROCEDURE IF EXISTS dbo.GenerateStampNumber;
--DROP PROCEDURE IF EXISTS dbo.GetTimesRenovated;

-- ============================================================
-- TABLES
-- ============================================================

CREATE TABLE BATCH
(
    BatchNumber     INT             IDENTITY(1, 1)  PRIMARY KEY,
    PickDate        DATE            NOT NULL,
    Quantity        INT             NOT NULL,
    ProcessStatus   NVARCHAR(255),
    SalesDate       DATE            NOT NULL
)

CREATE TABLE CALIPER
(
    StampNumber     CHAR(8)         PRIMARY KEY,
    Manufacturer    NVARCHAR(50)    NOT NULL,
    Approval        BIT             NOT NULL,
    ModelNumber     NVARCHAR(20)    NOT NULL,
    TimesRenovated  INT             NOT NULL DEFAULT 0
)

CREATE TABLE RENOVATION
(
    RenovationId        INT     IDENTITY(1,1)   PRIMARY KEY,
    StampNumber         CHAR(8)                 NOT NULL,
    BatchNumber         INT                     NULL,
    RegistrationDate    DATE                    NOT NULL,
    FOREIGN KEY (StampNumber)   REFERENCES CALIPER(StampNumber),
    FOREIGN KEY (BatchNumber)   REFERENCES BATCH(BatchNumber)
)

CREATE TABLE STATION
(
    StationNumber   INT             PRIMARY KEY NOT NULL,
    StationName     NVARCHAR(50)
)

CREATE TABLE BATCH_STATION
(
    BatchNumber     INT             NOT NULL,
    StationNumber   INT             NOT NULL,
    StartTime       TIME            NOT NULL,
    EndTime         TIME,
    PRIMARY KEY (BatchNumber, StationNumber),
    FOREIGN KEY (BatchNumber)   REFERENCES BATCH(BatchNumber),
    FOREIGN KEY (StationNumber) REFERENCES STATION(StationNumber)
)

CREATE TABLE EMPLOYEE
(
    EmployeeId      INT             IDENTITY(1, 1) PRIMARY KEY,
    Initials        NVARCHAR(5)     NOT NULL,
    Department      NVARCHAR(50)    NOT NULL
)

CREATE TABLE STATION_EMPLOYEE
(
    StationNumber   INT             NOT NULL,
    EmployeeId      INT             NOT NULL,
    PRIMARY KEY (StationNumber, EmployeeId),
    FOREIGN KEY (StationNumber) REFERENCES STATION(StationNumber),
    FOREIGN KEY (EmployeeId)    REFERENCES EMPLOYEE(EmployeeId)
)

-- ============================================================
-- STORED PROCEDURE: GenerateStampNumber
-- Generates a unique 8-character alphanumeric stamp number
-- NOTE: Calipers that already have a StampNumber should NOT call this.
-- ============================================================

GO

CREATE PROCEDURE dbo.GenerateStampNumber
    @Manufacturer   NVARCHAR(50),
    @Approval       BIT,
    @ModelNumber    NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @chars      VARCHAR(36) = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
    DECLARE @result     VARCHAR(8)  = '';
    DECLARE @i          INT;
    DECLARE @attempts   INT         = 0;

    WHILE @result = '' OR EXISTS (
        SELECT 1 FROM dbo.CALIPER WHERE StampNumber = @result
    )
    BEGIN
        SET @result = '';
        SET @i = 1;
        WHILE @i <= 8
        BEGIN
            SET @result = @result +
                SUBSTRING(@chars, (CAST(CRYPT_GEN_RANDOM(1) AS INT) % 36) + 1, 1);
            SET @i = @i + 1;
        END

        SET @attempts = @attempts + 1;
        IF @attempts > 100
            THROW 50001, 'Could not generate a unique StampNumber after 100 attempts.', 1;
    END

    INSERT INTO dbo.CALIPER (StampNumber, Manufacturer, Approval, ModelNumber)
    VALUES (@result, @Manufacturer, @Approval, @ModelNumber);

    SELECT @result;
END

-- ============================================================
-- STORED PROCEDURE: GetTimesRenovated
-- Retrieves the newest TimesRenovated of a StampNumber
-- ============================================================

GO

CREATE PROCEDURE dbo.GetTimesRenovated
    @StampNumber CHAR(8)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TimesRenovated
    FROM dbo.CALIPER
    WHERE StampNumber = @StampNumber;
END

-- ============================================================
-- USEFUL QUERIES
-- ============================================================

--SELECT * FROM dbo.CALIPER;
--SELECT * FROM dbo.RENOVATION;
--SELECT * FROM dbo.BATCH;
--SELECT * FROM dbo.CALIPER WHERE Approval = 1;
--SELECT * FROM dbo.CALIPER WHERE StampNumber IS NULL;   -- calipers awaiting stamping
--SELECT * FROM dbo.CALIPER WHERE StampNumber IS NOT NULL; -- calipers already stamped
