CREATE PROCEDURE [dbo].[Users_Delete]
    @Id int
AS
	DELETE FROM Users
	WHERE Id = @Id