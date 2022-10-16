CREATE TABLE CodeLookup (
    CodeId int NOT NULL PRIMARY KEY,
    CodeName varchar(200) NOT NULL,
);

INSERT INTO dbo.CodeLookup
  ( CodeId, CodeName )
VALUES
  ( 2323, 'Code1'), 
  ( 2324, 'Code2')


CREATE TABLE Customer (
    CustomerId int not null PRIMARY KEY,
    CustomerName varchar(20) not null,
    Notes varchar(200),
    CodeId int null,
    ImportDate varchar(100)	
	FOREIGN KEY (CodeId) REFERENCES CodeLookup(CodeId)
);

CREATE PROCEDURE SelectAllCustomers

SELECT c.CustomerId, c.CustomerName, c.Notes, cl.CodeName, c.ImportDate,   GETDATE() as ExportDate, c.CodeId
FROM dbo.Customer c
LEFT JOIN dbo.CodeLookup cl 
ON cl.CodeId = c.CodeId

GO


