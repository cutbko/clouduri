CREATE TABLE [dbo].[Users] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [Username]     NVARCHAR (128) NOT NULL,
    [Email]        NVARCHAR (128) NOT NULL,
    [PasswordHash] NVARCHAR (128) NOT NULL,
    [Salt]         NVARCHAR (128) NOT NULL
);

