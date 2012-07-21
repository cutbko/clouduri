CREATE PROCEDURE [dbo].[DeviceTypes_GetAll]
AS
SELECT [Id], [Name], [DownloadUrl] FROM [dbo].[DeviceTypes]
