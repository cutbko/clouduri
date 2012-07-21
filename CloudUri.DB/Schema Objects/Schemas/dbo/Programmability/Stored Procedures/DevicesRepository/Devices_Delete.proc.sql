CREATE PROCEDURE [dbo].[Devices_Delete]
	@Key int
AS
	DELETE FROM [Devices] WHERE [Devices].[Id] = @Key 