ALTER TABLE [dbo].[Devices]
    ADD CONSTRAINT [FK_Devices_DeviceTypes] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[DeviceTypes] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

