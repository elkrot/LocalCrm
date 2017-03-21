
CREATE TABLE [dbo].[City](
	[CityId] [int] IDENTITY(1,1) NOT NULL,
	[CityName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Person](
	[PersonId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nchar](50) NOT NULL,
	[MiddleName] [nchar](50) NOT NULL,
	[LastName] [nchar](50) NOT NULL,
	[AdditionalContactInfo] [nvarchar](max) NULL,
	[ModifiedDate] [datetime2](7) NULL CONSTRAINT [DF_Person_ModifiedDate]  DEFAULT (getdate()),
	[Discriminator] [nvarchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[PersonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [dbo].[TransportCompany](
	[TransportCompanyId] [int] IDENTITY(1,1) NOT NULL,
	[TransportCompanyName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TransportCompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



CREATE TABLE [dbo].[SalesOrderHeader](
	[SalesOrderId] [int] IDENTITY(1,1) NOT NULL,
	[OrderNo] [nvarchar](50) NOT NULL,
	[OrderDate] [datetime2](7) NOT NULL,
	[OrderTotal] [money] NULL,
	[SalesPersonId] [int] NULL,
	[CustomerId] [int] NULL,
	[OrderPrice] [money] NULL,
	[Prepayment] [money] NULL,
	[ShipDate] [datetime2](7) NULL,
	[CityId] [int] NULL,
	[TransportCompanyId] [int] NULL,
	[ShipingCost] [money] NULL,
	[ReceiptDate] [datetime2](7) NULL,
	[ReceivedForDelivery] [money] NULL,
	[ReceivedForOrder] [money] NULL,
	[Status] [int] NULL,
	[Comment] [nvarchar](max) NULL,
	[ModifiedDate] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[SalesOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[SalesOrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_City_Order] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([CityId])
GO

ALTER TABLE [dbo].[SalesOrderHeader] CHECK CONSTRAINT [FK_City_Order]
GO

ALTER TABLE [dbo].[SalesOrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Order] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Person] ([PersonId])
GO

ALTER TABLE [dbo].[SalesOrderHeader] CHECK CONSTRAINT [FK_Customer_Order]
GO

ALTER TABLE [dbo].[SalesOrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_SalesPerson_Order] FOREIGN KEY([SalesPersonId])
REFERENCES [dbo].[Person] ([PersonId])
GO

ALTER TABLE [dbo].[SalesOrderHeader] CHECK CONSTRAINT [FK_SalesPerson_Order]
GO

ALTER TABLE [dbo].[SalesOrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_TransportCo_Order] FOREIGN KEY([TransportCompanyId])
REFERENCES [dbo].[TransportCompany] ([TransportCompanyId])
GO

ALTER TABLE [dbo].[SalesOrderHeader] CHECK CONSTRAINT [FK_TransportCo_Order]
GO

