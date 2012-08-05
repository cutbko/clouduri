CREATE PROCEDURE [dbo].[Messages_ForUser]
	@UserName NVARCHAR(100), 
	@ItemsPerPage INT,
	@Page INT,
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
	INNER JOIN Users U ON D.OwnerId = U.Id
	WHERE U.Username = @UserName

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
		INNER JOIN Users U ON D.OwnerId = U.Id
		WHERE U.Username = @UserName
	) SEQ
	
	WHERE rownum BETWEEN @ItemsPerPage * (@Page - 1) + @Offset AND @ItemsPerPage * @Page

