CREATE PROCEDURE [dbo].[DeviceTypes_Insert]
	@Name nvarchar(50), 
	@DownloadUrl nvarchar(200)
AS
BEGIN
	INSERT INTO [DeviceTypes] ([Name], [DownloadUrl]) VALUES (@Name, @DownloadUrl)

	SELECT NEWID = CAST(SCOPE_IDENTITY() AS int)
END