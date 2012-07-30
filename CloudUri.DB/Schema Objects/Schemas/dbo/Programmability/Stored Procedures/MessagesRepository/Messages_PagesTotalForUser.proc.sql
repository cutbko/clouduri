CREATE PROCEDURE [dbo].[Messages_PagesTotalForUser]
	@UserName NVARCHAR(100), 
	@ItemsPerPage INT
AS
	SELECT
	(CASE 
		WHEN COUNT(M.[Id]) % @ItemsPerPage > 0 
			THEN COUNT(M.[Id]) / @ItemsPerPage + 1
			ELSE COUNT(M.[Id]) / @ItemsPerPage 
		END) AS PagesTotal
	FROM [dbo].[Messages] M
	INNER JOIN Devices D ON D.Id = M.FromId 
	INNER JOIN Users U ON D.OwnerId = U.Id
	WHERE U.Username = @UserName