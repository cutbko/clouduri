ALTER TABLE [dbo].[Messages]
    ADD CONSTRAINT [FK_Messages_Devices] FOREIGN KEY ([FromId]) REFERENCES [dbo].[Devices] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

