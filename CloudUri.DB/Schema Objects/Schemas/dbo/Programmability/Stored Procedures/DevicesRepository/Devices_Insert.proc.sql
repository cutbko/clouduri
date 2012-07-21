CREATE PROCEDURE [dbo].[Devices_Insert]
	@Name varchar(30), 
	@TypeId int,
	@OwnerId int
AS
BEGIN
	INSERT INTO [Devices] ([Name], [OwnerId], [TypeId]) VALUES (@Name, @OwnerId, @TypeId)

	SELECT NEWID = CAST(SCOPE_IDENTITY() AS int)
END