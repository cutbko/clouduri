ALTER TABLE [dbo].[UsersToRoles]
    ADD CONSTRAINT [FK_UsersToRoles_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION;

