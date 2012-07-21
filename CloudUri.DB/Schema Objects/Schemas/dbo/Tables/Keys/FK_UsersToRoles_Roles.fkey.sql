ALTER TABLE [dbo].[UsersToRoles]
    ADD CONSTRAINT [FK_UsersToRoles_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION;

