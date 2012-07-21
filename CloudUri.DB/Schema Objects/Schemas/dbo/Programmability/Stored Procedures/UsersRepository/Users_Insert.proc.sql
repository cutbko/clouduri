CREATE PROCEDURE [dbo].[Users_Insert]
    @Username nvarchar(max),
    @Email nvarchar(max),
    @PasswordHash nvarchar(max),
    @Salt nvarchar(max)
AS
BEGIN
	INSERT INTO Users(Username,Email,PasswordHash,Salt)
	VALUES(@Username,@Email,@PasswordHash,@Salt)
	SELECT NEWID = CAST(SCOPE_IDENTITY() AS int)
END