CREATE TABLE [dbo].[Order] (
    [OrderId]    INT             IDENTITY (1, 1) NOT NULL,
    [Total]      DECIMAL (18, 2) NOT NULL,
    [CreateDate] DATETIME        NOT NULL,
    [UserId]     NVARCHAR (128)  NULL,
    CONSTRAINT [PK_dbo.Order] PRIMARY KEY CLUSTERED ([OrderId] ASC),
    CONSTRAINT [FK_dbo.Order_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[Order]([UserId] ASC);

