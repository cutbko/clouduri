CREATE PROCEDURE [Test].[FillUserCutbko]
AS

DECLARE @WP7Id INT, @AndroidId INT, @W8Id INT, @UserId INT, @MPId INT, @WMPId INT, @TId INT

--Fill device types
INSERT INTO DeviceTypes (Name, DownloadUrl) VALUES ('Windows phone 7', 'http://windowsmarketplace.com')
SET @WP7Id = CAST(SCOPE_IDENTITY() AS int)
INSERT INTO DeviceTypes (Name, DownloadUrl) VALUES ('Android', 'http://windowsmarketplace.com')
SET @AndroidId = CAST(SCOPE_IDENTITY() AS int)
INSERT INTO DeviceTypes (Name, DownloadUrl) VALUES ('Windows 8', 'http://windowsmarketplace.com')
SET @W8Id = CAST(SCOPE_IDENTITY() AS int)

--Fill users
INSERT INTO Users ([Username], [Email], [PasswordHash], [Salt])
VALUES ('cutbko', 'cutbko@hotmail.com', N'33423342', N'')
SET @UserId = CAST(SCOPE_IDENTITY() AS int)

--Fill devices
INSERT INTO Devices (TypeId, Name, OwnerId)
VALUES (@WP7Id, 'Mobile phone', @UserId)
SET @MPId = CAST(SCOPE_IDENTITY() AS int)
INSERT INTO Devices (TypeId, Name, OwnerId)
VALUES (@AndroidId, 'Work mobile phone', @UserId)
SET @WMPId = CAST(SCOPE_IDENTITY() AS int)
INSERT INTO Devices (TypeId, Name, OwnerId)
VALUES (@W8Id, 'Tablet', @UserId)
SET @TId = CAST(SCOPE_IDENTITY() AS int)

--Fill messages with sender and receiver
DECLARE @i INT
SET @i = 0
WHILE(@i < 90)
BEGIN

	DECLARE @SenderId INT, 	@ReceiverId INT, @Sender NVARCHAR(100),	@Receiver NVARCHAR(100)

	SELECT TOP 1 @SenderId = id, @Sender = Name FROM Devices ORDER BY NEWID()
	SELECT TOP 1 @ReceiverId = id, @Receiver = Name FROM Devices ORDER BY NEWID()

	INSERT INTO [Messages] (FromId, ToId, MessageText) 
	VALUES (@SenderId, @ReceiverId, 'Sender: ' +@Sender + ' Receiver:' + @Receiver)

	SET @i = @i + 1
END

--Fill messages with sender and null receiver
SET @i = 0
WHILE(@i < 10)
BEGIN

	SELECT TOP 1 @SenderId = id, @Sender = Name FROM Devices ORDER BY NEWID()

	INSERT INTO [Messages] (FromId, ToId, MessageText) 
	VALUES (@SenderId, null, 'Sender: ' +@Sender + ' Receiver: ALL')

	SET @i = @i + 1
END