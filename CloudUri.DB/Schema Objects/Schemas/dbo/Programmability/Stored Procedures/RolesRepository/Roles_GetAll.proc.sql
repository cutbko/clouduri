CREATE PROCEDURE [dbo].[Roles_GetAll]
AS
SELECT [Id], [Name], [Description] FROM [dbo].[Roles]
