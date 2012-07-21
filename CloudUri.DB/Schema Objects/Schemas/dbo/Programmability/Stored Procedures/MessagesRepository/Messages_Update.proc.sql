CREATE PROCEDURE [dbo].[Messages_Update]
	@Id int,
	@MessageText varchar(1000), 
	@FromId int,
	@ToId int = NULL
AS
	UPDATE [dbo].[Messages] SET
		[MessageText] = @MessageText,
		[FromId] = @FromId, 
		[ToId] = @ToId
	WHERE [Id] = @Id
