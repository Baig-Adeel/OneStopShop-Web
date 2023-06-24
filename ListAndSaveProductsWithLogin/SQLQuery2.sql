 Use[Test]
Go
CREATE TABLE [dbo].[CartItem](
    
    [CartItemId] BIGINT  IDENTITY (1, 1) NOT NULL,

    [CartId]      BIGINT NOT NULL,
    [ProductId] INT NOT NULL,
    [Price] DECIMAL(18,2)  NOT NULL,
    [Quantity] INT NOT NULL,
    CONSTRAINT[PK_CartItem_CartItemId] PRIMARY KEY CLUSTERED([CartItemId] ASC),
    CONSTRAINT[FK_Cart_CartId] FOREIGN KEY([CartId]) REFERENCES[dbo].[Cart] ([CartId]) ON DELETE CASCADE,
    CONSTRAINT[FK_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id])
);

GO
CREATE NONCLUSTERED INDEX[IX_CartItem_CartId]
    ON[dbo].[CartItem] ([CartId] ASC);

