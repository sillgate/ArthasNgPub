CREATE TABLE [dbo].[CartItem] (
    [CartItemId] INT             IDENTITY (1, 1) NOT NULL,
    [OrderId]    INT             NULL,
    [ItemId]     INT             NOT NULL,
    [Quantity]   INT             NOT NULL,
    [Price]      DECIMAL (18, 2) NOT NULL,
    [Cancel]     BIT             NOT NULL,
    [OrderDate]  DATETIME        NOT NULL,
    [UserId]     NVARCHAR (128)  NULL,
    CONSTRAINT [PK_dbo.CartItem] PRIMARY KEY CLUSTERED ([CartItemId] ASC),
    CONSTRAINT [FK_dbo.CartItem_dbo.Item_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Item] ([ItemId]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.CartItem_dbo.Order_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([OrderId]),
    CONSTRAINT [FK_dbo.CartItem_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_OrderId]
    ON [dbo].[CartItem]([OrderId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ItemId]
    ON [dbo].[CartItem]([ItemId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[CartItem]([UserId] ASC);

