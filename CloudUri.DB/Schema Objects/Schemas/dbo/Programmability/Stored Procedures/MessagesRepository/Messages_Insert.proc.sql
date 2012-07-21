CREATE PROCEDURE [dbo].[Messages_Insert]
	@MessageText varchar(1000), 
	@FromId int,
	@ToId int = NULL
AS
BEGIN
	INSERT INTO [Messages] ([MessageText], [FromId], [ToId]) VALUES (@MessageText, @FromId, @ToId)

	SELECT NEWID = CAST(SCOPE_IDENTITY() AS int)
END