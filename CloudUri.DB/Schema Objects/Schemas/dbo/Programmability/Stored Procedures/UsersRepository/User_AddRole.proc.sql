CREATE PROCEDURE [dbo].[User_AddRole]
	@UserId int, 
	@RoleName VARCHAR(30)
AS
BEGIN
	DECLARE @RoleId INT
	SELECT @RoleId = id FROM Roles WHERE Name = @RoleName
	if NOT EXISTS (SELECT * FROM UsersToRoles WHERE @UserId = @UserId AND RoleId = @RoleId) 
		INSERT INTO UsersToRoles (UserId, RoleId) VALUES (@UserId, @RoleId)
END	
	