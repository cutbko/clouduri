CREATE TABLE [dbo].[Messages] (
    [Id]          INT  IDENTITY (1, 1) NOT NULL,
    [FromId]      INT  NOT NULL,
    [ToId]        INT  NULL,
    [MessageText] TEXT NOT NULL
);

