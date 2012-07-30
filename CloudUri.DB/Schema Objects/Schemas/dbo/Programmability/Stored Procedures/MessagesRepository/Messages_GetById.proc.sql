CREATE PROCEDURE [dbo].[Messages_GetById]
	@id int
AS
	SELECT [Id], [MessageText], [FromId], [ToId], [CreatedOn] FROM [dbo].[Messages]
	WHERE [Id] = @id
