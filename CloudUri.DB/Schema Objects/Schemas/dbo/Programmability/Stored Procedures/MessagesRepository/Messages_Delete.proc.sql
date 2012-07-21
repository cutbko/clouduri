CREATE PROCEDURE [dbo].[Messages_Delete]
	@Key int
AS
	DELETE FROM [Messages] WHERE [Messages].[Id] = @Key 