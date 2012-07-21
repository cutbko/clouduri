CREATE PROCEDURE [dbo].[Users_Update]
    @Id int,
    @Username nvarchar(max),
    @Email nvarchar(max),
    @PasswordHash nvarchar(max),
    @Salt nvarchar(max)
AS
	UPDATE Users
	SET Username = @Username,
		Email = @Email,
		PasswordHash = @PasswordHash,
		Salt = @Salt
	WHERE Id = @Id