CREATE TABLE [dbo].[Devices] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [TypeId]  INT            NOT NULL,
    [Name]    NVARCHAR (450) NULL,
    [OwnerId] INT            NULL,
    [Deleted] BIT            NOT NULL
);



