CREATE PROCEDURE [dbo].[DeviceTypes_Delete]
	@id int
AS
DELETE FROM [dbo].[DeviceTypes] 
WHERE [Id] = @id
