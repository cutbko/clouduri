ALTER TABLE [dbo].[Messages]
    ADD CONSTRAINT [FK_Messages_Devices1] FOREIGN KEY ([ToId]) REFERENCES [dbo].[Devices] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

