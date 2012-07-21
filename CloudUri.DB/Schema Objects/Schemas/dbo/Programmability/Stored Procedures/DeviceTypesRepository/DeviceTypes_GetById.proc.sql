CREATE PROCEDURE [dbo].[DeviceTypes_GetById]
	@id int
AS
SELECT [Id], [Name], [DownloadUrl] FROM [dbo].[DeviceTypes] 
WHERE [Id] = @id
