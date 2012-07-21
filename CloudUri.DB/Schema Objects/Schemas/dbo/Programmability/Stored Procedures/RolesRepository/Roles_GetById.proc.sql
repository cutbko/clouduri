CREATE PROCEDURE [dbo].[Roles_GetById]
	@id int
AS
SELECT [Id], [Name], [Description] FROM [dbo].[Roles] 
WHERE [Id] = @id
