CREATE TABLE [dbo].[Item] (
    [ItemId]       INT             IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (MAX)  NULL,
    [Description]  NVARCHAR (MAX)  NULL,
    [Price]        DECIMAL (18, 2) NOT NULL,
    [Cost]         DECIMAL (18, 2) NOT NULL,
    [ItemImageUrl] NVARCHAR (MAX)  NULL,
    [Disable]      BIT             NOT NULL,
    CONSTRAINT [PK_dbo.Item] PRIMARY KEY CLUSTERED ([ItemId] ASC)
);

