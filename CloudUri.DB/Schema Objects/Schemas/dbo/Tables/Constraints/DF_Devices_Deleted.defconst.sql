ALTER TABLE [dbo].[Devices]
    ADD CONSTRAINT [DF_Devices_Deleted] DEFAULT ((0)) FOR [Deleted];

