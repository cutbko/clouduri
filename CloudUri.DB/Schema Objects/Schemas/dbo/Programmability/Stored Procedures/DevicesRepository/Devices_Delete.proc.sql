
CREATE PROCEDURE [dbo].[Devices_Delete]
	@Id int
AS
	UPDATE [dbo].[Devices] SET
	[Deleted] = 1
	WHERE [Id] = @Id 