CREATE PROCEDURE [dbo].[Roles_GetForUser]
	@UserName NVARCHAR(100)
AS
SELECT R.id, R.Name, R.[Description] FROM Roles R
INNER JOIN UsersToRoles UTR ON R.Id = UTR.RoleId
INNER JOIN Users U ON U.Id = UTR.UserId
WHERE U.Username = @UserName
