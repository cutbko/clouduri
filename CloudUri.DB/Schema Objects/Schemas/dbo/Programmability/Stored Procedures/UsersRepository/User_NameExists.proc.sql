CREATE PROCEDURE [dbo].[User_NameExists]
	@UserName NVARCHAR(100)
AS
SELECT COUNT(*) FROM Users
WHERE Username=@UserName
