﻿ALTER TABLE [dbo].[Devices]
    ADD CONSTRAINT [FK_Devices_Users] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

