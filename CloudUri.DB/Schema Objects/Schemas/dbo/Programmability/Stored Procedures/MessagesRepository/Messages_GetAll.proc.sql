﻿CREATE PROCEDURE [dbo].[Messages_GetAll]
AS
	SELECT [Id], [MessageText], [FromId], [ToId], [CreatedOn] FROM [dbo].[Messages]
