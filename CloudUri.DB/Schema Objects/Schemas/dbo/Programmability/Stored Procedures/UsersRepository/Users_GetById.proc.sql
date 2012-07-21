CREATE PROCEDURE [dbo].[Users_GetById]
    @Id int
AS
	SELECT [Users].Id, [Users].Username, [Users].Email, [Users].PasswordHash, [Users].Salt  FROM [Users]
	WHERE Id = @Id