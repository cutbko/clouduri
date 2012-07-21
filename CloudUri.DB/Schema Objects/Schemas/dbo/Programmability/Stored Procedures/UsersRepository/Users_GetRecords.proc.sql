CREATE PROCEDURE [dbo].[Users_GetRecords]
AS
	SELECT [Users].Id, [Users].Username, [Users].Email, [Users].PasswordHash, [Users].Salt  FROM [Users]
