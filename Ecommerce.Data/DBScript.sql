USE [EcommerceDB]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 9/19/2025 12:44:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Category] [nvarchar](50) NOT NULL,
	[ProductCode] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ImagePath] [nvarchar](max) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[MinimumQuantity] [int] NOT NULL,
	[DiscountRate] [float] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/19/2025 12:44:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](450) NOT NULL,
	[LastLogin] [datetime2](7) NOT NULL,
	[RefreshToken] [nvarchar](max) NULL,
	[RefreshTokenExpiryTime] [datetime2](7) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Products] ON 
GO
INSERT [dbo].[Products] ([Id], [Category], [ProductCode], [Name], [ImagePath], [Price], [MinimumQuantity], [DiscountRate]) VALUES (1, N'string', N'string', N'productName', N'string', CAST(100.00 AS Decimal(18, 2)), 200, 10)
GO
INSERT [dbo].[Products] ([Id], [Category], [ProductCode], [Name], [ImagePath], [Price], [MinimumQuantity], [DiscountRate]) VALUES (2, N'string', N'code2', N'product2', NULL, CAST(500.00 AS Decimal(18, 2)), 50, 5)
GO
INSERT [dbo].[Products] ([Id], [Category], [ProductCode], [Name], [ImagePath], [Price], [MinimumQuantity], [DiscountRate]) VALUES (3, N'c1', N'code3', N'product3', N'/images/961363f9-f8a3-40dc-8ded-cec67e5fe969.jpeg', CAST(300.00 AS Decimal(18, 2)), 30, 30)
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([Id], [UserName], [Password], [Email], [LastLogin], [RefreshToken], [RefreshTokenExpiryTime]) VALUES (2, N'user2', N'AQAAAAIAAYagAAAAENFo4DBBU52aGqVY9/jvlDS2l5SsLzfEAvxg4A/WyNww8AmdclA0Cqas9VwL5XYS5w==', N'st@s2.com', CAST(N'2025-09-18T20:35:37.3398476' AS DateTime2), N'Js0mkjVJK0uarfJRpa5ZCw==', CAST(N'2025-09-21T20:35:37.3397261' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
