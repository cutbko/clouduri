CREATE PROCEDURE [dbo].[Devices_GetById]
	@id int
AS
	SELECT [Id], [Name], [TypeId], [OwnerId] FROM [dbo].[Devices]
	WHERE [Id] = @id
