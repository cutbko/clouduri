CREATE PROCEDURE [dbo].[DeviceTypes_Update]
	@Id int,
	@Name NVARCHAR(50),
	@DownloadUri NVARCHAR(100)
AS
	UPDATE [DeviceTypes] 
	SET [Name] = @Name,
	    [DownloadUrl] = @DownloadUri
	WHERE [Id] = @Id
