if not exists(select * from sys.databases where name = 'Test1DB')
    create database Test1DB
GO
use Test1DB;
GO
CREATE TABLE Users (
    ID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Username varchar(255) NOT NULL,
    Password varchar(255) NOT NULL
);
GO
CREATE TABLE Roles (
    ID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Description varchar(255) NOT NULL,
	UserId int NOT NULL,
	CONSTRAINT FK_UserRole FOREIGN KEY (UserId)
    REFERENCES Users(ID) on delete cascade
);
GO
INSERT INTO Users(Username,Password) values ('user1','5BAA61E4C9B93F3F0682250B6CF8331B7EE68FD8');  -- this is the hashed passsword, the original password is:password
INSERT INTO Users(Username,Password) values ('user2','2AA60A8FF7FCD473D321E0146AFD9E26DF395147'); -- this is the hashed passsword, the original password is:password2

INSERT INTO ROLES(Description,UserId) values ('Admin',1);
INSERT INTO ROLES(Description, UserId) values ('Teacher',2);
INSERT INTO ROLES(Description, UserId) values ('Student',1);

GO
CREATE PROCEDURE dbo.spInsertUser 
       @UserName NVARCHAR(255) = NULL, 
       @Password NVARCHAR(255) = NULL 
AS 
BEGIN 
     INSERT INTO Users(Username,Password) 
		VALUES(@UserName,@Password) 
END 
GO
CREATE PROCEDURE dbo.spUpdateUser 
	   @Id int = NULL,
       @UserName NVARCHAR(255) = NULL, 
       @Password NVARCHAR(255) = NULL 
AS 
BEGIN 
     UPDATE Users SET Username = @UserName,
					  Password = @Password 
					WHERE Id = @Id 
END 
GO
CREATE PROCEDURE dbo.spDeleteUser 
	   @Id INT = NULL
AS 
BEGIN 
     DELETE FROM Users WHERE Id = @Id 
END 
GO
CREATE PROCEDURE dbo.spGetUser 
	   @Id INT = NULL
AS 
BEGIN 
     SELECT Username, Password FROM Users WHERE Id = @Id 
END 
GO
CREATE PROCEDURE dbo.spGetUserByUsername 
	   @Username NVARCHAR(255) = NULL
AS 
BEGIN 
     SELECT Username, Password FROM Users WHERE UserName = @Username 
END 
GO
CREATE PROCEDURE dbo.spInsertRole 
       @Description NVARCHAR(255) = NULL, 
       @UserId INT = NULL 
AS 
BEGIN 
     INSERT INTO Roles(Description,UserId) 
		VALUES(@Description,@UserId) 
END 
GO
CREATE PROCEDURE dbo.spUpdateRole 
	   @Id int = NULL,
       @Description NVARCHAR(255) = NULL, 
       @UserId INT = NULL 
AS 
BEGIN 
     UPDATE Roles SET Description = @Description,
					  UserId = @UserId 
					WHERE Id = @Id 
END 
GO
CREATE PROCEDURE dbo.spDeleteRole 
	   @Id INT = NULL
AS 
BEGIN 
     DELETE FROM Roles WHERE Id = @Id 
END 
GO
CREATE PROCEDURE dbo.spGetRole 
	   @Id INT = NULL
AS 
BEGIN 
     SELECT Description, UserID FROM Roles WHERE Id = @Id 
END 
GO

