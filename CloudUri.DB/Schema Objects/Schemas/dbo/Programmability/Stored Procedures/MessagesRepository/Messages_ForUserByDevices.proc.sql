CREATE PROCEDURE [dbo].[Messages_ForUserByDevices]
	@UserName NVARCHAR(100), 
	@ItemsPerPage INT,
	@Page INT,
	@SendingDevice NVARCHAR(200),
	@ReceivingDevice NVARCHAR(200),
	@PagesTotal INT OUTPUT
AS
	SELECT @PagesTotal =
	(CASE 
		WHEN COUNT(M.[Id]) % @ItemsPerPage > 0 
			THEN COUNT(M.[Id]) / @ItemsPerPage + 1
			ELSE COUNT(M.[Id]) / @ItemsPerPage 
		END)
	FROM [dbo].[Messages] M
	INNER JOIN Devices D ON D.Id = M.FromId
	INNER JOIN Devices DTo ON DTo.Id = M.ToId 
	INNER JOIN Users U ON D.OwnerId = U.Id
	WHERE U.Username = @UserName AND DTo.Name = @ReceivingDevice AND D.Name = @SendingDevice

	DECLARE @Offset INT = 0
	
	IF (@Page <> 1) 
		SET @Offset = 1	

	SELECT [Id], [MessageText], [FromId], [ToId], [CreatedOn]
	FROM
	(
		SELECT 	M.[Id], 	[MessageText],	[FromId],	[ToId], 	[CreatedOn],  
		ROW_NUMBER() OVER (ORDER BY M.[Id]) rownum 
		FROM [dbo].[Messages] M
		INNER JOIN Devices D ON D.Id = M.FromId 
		INNER JOIN Devices DTo ON DTo.Id = M.ToId 
		INNER JOIN Users U ON D.OwnerId = U.Id
		WHERE U.Username = @UserName AND DTo.Name = @ReceivingDevice AND D.Name = @SendingDevice
	) SEQ
	
	WHERE rownum BETWEEN @ItemsPerPage * (@Page - 1) + @Offset AND @ItemsPerPage * @Page