CREATE PROCEDURE [dbo].[Roles_Delete]
	@id int
AS
DELETE FROM [dbo].[Roles] 
WHERE [Id] = @id
