CREATE PROCEDURE [dbo].[Users_GetByName]
	@UserName NVARCHAR(100)
AS
SELECT [Users].Id, [Users].Username, [Users].Email, [Users].PasswordHash, [Users].Salt FROM Users
WHERE Username = @UserName