IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Hospital')
CREATE database Hospital;
GO

-- deleting tables
IF OBJECT_ID('tblPatient', 'U') IS NOT NULL DROP TABLE tblPatient;
IF OBJECT_ID('tblDoctor', 'U') IS NOT NULL DROP TABLE tblDoctor;
IF OBJECT_ID('tblRequest', 'U') IS NOT NULL DROP TABLE tblRequest;


use Hospital
CREATE TABLE tblPatient (PatientID int IDENTITY(1,1) PRIMARY KEY NOT NULL, FirstName nvarchar(50) NOT NULL, LastName nvarchar(50) NOT NULL,
 JMBG nvarchar(13) check(len(JMBG) = 13)  not null unique, EnsuranceCardNumber nvarchar(50) UNIQUE NOT NULL,
  Username nvarchar(50) UNIQUE NOT NULL, UserPassword nvarchar(50) UNIQUE NOT NULL,
   constraint checkJMBG check(JMBG not like '%[^0-9]%'));

 use Hospital
CREATE TABLE tblDoctor (DoctorID int IDENTITY(1,1) PRIMARY KEY NOT NULL, FirstName nvarchar(50) NOT NULL, LastName nvarchar(50) NOT NULL,
 JMBG nvarchar(13) check(len(JMBG) = 13)  not null unique, CurrentAccountNumber nvarchar(50) UNIQUE NOT NULL,
  Username nvarchar(50) UNIQUE NOT NULL, UserPassword nvarchar(50) UNIQUE NOT NULL,
  constraint cJMBG check(JMBG not like '%[^0-9]%'));

  use Hospital
  CREATE TABLE tblRequest(RequestID int IDENTITY(1,1) PRIMARY KEY NOT NULL, RequestDate datetime, Reason nvarchar(200), Company nvarchar(50), Urgently bit);

 use Hospital
 INSERT INTO tblPatient(FirstName, LastName, JMBG, EnsuranceCardNumber, Username, UserPassword)
 VALUES ('Marko', 'Gacinovic', '0308993800001', '1122998811', 'MarkoGacinovic', 'Gacinovic');

 use Hospital
 INSERT INTO tblDoctor(FirstName, LastName, JMBG, CurrentAccountNumber, Username, UserPassword)
 VALUES ('Milan', 'Bratic', '1102990123123', '2122998811', 'MilanBratic', 'Bratic');