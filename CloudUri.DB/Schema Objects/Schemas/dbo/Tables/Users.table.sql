CREATE TABLE [dbo].[Users] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Username]     NVARCHAR (MAX) NOT NULL,
    [Email]        NVARCHAR (MAX) NOT NULL,
    [PasswordHash] NVARCHAR (MAX) NOT NULL,
    [Salt]         NVARCHAR (MAX) NOT NULL
);

