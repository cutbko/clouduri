CREATE PROCEDURE [dbo].[Roles_Update]
	@Id int,
	@Name NVARCHAR(50),
	@Description NVARCHAR(200)
AS
	UPDATE [Roles] 
	SET [Name] = @Name,
	    [Description] = @Description
	WHERE [Id] = @Id
