
CREATE PROCEDURE [dbo].[Devices_GetAll]
AS
	SELECT [Id], [Name], [TypeId], [OwnerId] FROM [dbo].[Devices]
	WHERE Deleted = 0
