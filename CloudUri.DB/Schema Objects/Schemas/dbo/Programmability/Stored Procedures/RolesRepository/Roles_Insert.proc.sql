CREATE PROCEDURE [dbo].[Roles_Insert]
	@Name nvarchar(50), 
	@Description nvarchar(200)
AS
BEGIN
	INSERT INTO [Roles] ([Name], [Description]) VALUES (@Name, @Description)

	SELECT NEWID = CAST(SCOPE_IDENTITY() AS int)
END