CREATE PROCEDURE [dbo].[User_EmailExists]
	@Email NVARCHAR(100)
AS
SELECT COUNT(*) FROM Users
WHERE Email=@Email