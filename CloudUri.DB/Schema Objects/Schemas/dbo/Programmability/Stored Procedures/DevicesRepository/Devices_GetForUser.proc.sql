CREATE PROCEDURE [dbo].[Devices_GetForUser]
	@UserName NVARCHAR(100)
AS
SELECT d.[Id], d.[Name], d.[TypeId], d.[OwnerId] FROM [dbo].[Devices] d
INNER JOIN Users u On d.OwnerId = u.Id
WHERE u.Username = @UserName