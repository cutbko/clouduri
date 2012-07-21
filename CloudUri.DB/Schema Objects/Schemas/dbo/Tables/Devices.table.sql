CREATE TABLE [dbo].[Devices] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [TypeId]  INT            NOT NULL,
    [Name]    NVARCHAR (MAX) NOT NULL,
    [OwnerId] INT            NULL
);

