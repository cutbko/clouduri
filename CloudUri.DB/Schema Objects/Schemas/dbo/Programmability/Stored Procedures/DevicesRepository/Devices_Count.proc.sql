
CREATE PROCEDURE [dbo].[Devices_Count]
AS
	SELECT COUNT(*) FROM [Devices] WHERE Deleted = 0 