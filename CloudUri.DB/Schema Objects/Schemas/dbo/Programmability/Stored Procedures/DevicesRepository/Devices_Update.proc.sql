CREATE PROCEDURE [dbo].[Devices_Update]
	@Id int,
	@Name NVARCHAR(50),
	@typeId int,
	@ownerId int
AS
	UPDATE [dbo].[Devices] SET
		[Name] = @Name,
		[TypeId] = @typeId, 
		[OwnerId] = @ownerId
	WHERE [Id] = @Id
