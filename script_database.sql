CREATE DATABASE [Abstra];
GO
USE [Abstra]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 02/06/2024 20:01:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Account]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Account](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[AccountNumber] [bigint] NOT NULL,
	[AccountType] [nchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ClientId] [int] NOT NULL,
	[InitialBalance] [decimal](18, 2) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Client]    Script Date: 02/06/2024 20:01:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Client]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Client](
	[ClientId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Name] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Gender] [nchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Birthdate] [date] NOT NULL,
	[Address] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Phone] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Password] [nvarchar](250) COLLATE Latin1_General_100_CS_AS NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 02/06/2024 20:01:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Transaction]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Transaction](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[EventDate] [datetime2](7) NOT NULL,
	[TransactionType] [nchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Account] ON 
GO
INSERT [dbo].[Account] ([AccountId], [AccountNumber], [AccountType], [ClientId], [InitialBalance], [Status]) VALUES (1, 76325324532, N'C', 1, CAST(2000.00 AS Decimal(18, 2)), 1)
GO
INSERT [dbo].[Account] ([AccountId], [AccountNumber], [AccountType], [ClientId], [InitialBalance], [Status]) VALUES (2, 4687453487, N'A', 2, CAST(0.00 AS Decimal(18, 2)), 1)
GO
INSERT [dbo].[Account] ([AccountId], [AccountNumber], [AccountType], [ClientId], [InitialBalance], [Status]) VALUES (3, 8084844, N'C', 2, CAST(1000.00 AS Decimal(18, 2)), 1)
GO
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Client] ON 
GO
INSERT [dbo].[Client] ([ClientId], [UserName], [Name], [Gender], [Birthdate], [Address], [Phone], [Password], [Status]) VALUES (1, N'rriveros', N'Ronald Riveros', N'M', CAST(N'1986-12-29' AS Date), N'Chaco 3245', N'+95185713', N'903f9729efc5cdd8822e18f6ee806b451cc6027c2cc4a8e5d5b96c18b2e4d8f864ec20bca55040f66e16113f5337345b0d5a1e1ea50f0e90da2b222bb99ff8bb', 1)
GO
INSERT [dbo].[Client] ([ClientId], [UserName], [Name], [Gender], [Birthdate], [Address], [Phone], [Password], [Status]) VALUES (2, N'rfretes', N'Ramona Fretes', N'F', CAST(N'1983-12-29' AS Date), N'Brasilia 423', N'+951857131234', N'903f9729efc5cdd8822e18f6ee806b451cc6027c2cc4a8e5d5b96c18b2e4d8f864ec20bca55040f66e16113f5337345b0d5a1e1ea50f0e90da2b222bb99ff8bb', 1)
GO
INSERT [dbo].[Client] ([ClientId], [UserName], [Name], [Gender], [Birthdate], [Address], [Phone], [Password], [Status]) VALUES (3, N'mayala', N'Miguel Ayala', N'M', CAST(N'1985-04-21' AS Date), N'Mongelos 353', N'09907837123', N'622e28c014ec40ec199ff096df2a8898046dbcf877ce8824b4c857a287f7630cb25301d2987e91d12c44ef72d2f9c594f5605f06162b3229b649d2d149855c5d', 0)
GO
SET IDENTITY_INSERT [dbo].[Client] OFF
GO
SET IDENTITY_INSERT [dbo].[Transaction] ON 
GO
INSERT [dbo].[Transaction] ([TransactionId], [AccountId], [EventDate], [TransactionType], [Amount]) VALUES (1, 1, CAST(N'2008-12-12T00:00:00.0000000' AS DateTime2), N'D', CAST(-100.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Transaction] ([TransactionId], [AccountId], [EventDate], [TransactionType], [Amount]) VALUES (2, 1, CAST(N'2009-01-13T00:00:00.0000000' AS DateTime2), N'C', CAST(500.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Transaction] ([TransactionId], [AccountId], [EventDate], [TransactionType], [Amount]) VALUES (3, 1, CAST(N'2022-11-20T00:00:00.0000000' AS DateTime2), N'D', CAST(-500.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Transaction] ([TransactionId], [AccountId], [EventDate], [TransactionType], [Amount]) VALUES (4, 2, CAST(N'2022-11-20T00:00:00.0000000' AS DateTime2), N'C', CAST(500.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Transaction] ([TransactionId], [AccountId], [EventDate], [TransactionType], [Amount]) VALUES (5, 2, CAST(N'2022-11-20T00:00:00.0000000' AS DateTime2), N'C', CAST(600.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Transaction] ([TransactionId], [AccountId], [EventDate], [TransactionType], [Amount]) VALUES (6, 2, CAST(N'2022-11-20T00:00:00.0000000' AS DateTime2), N'D', CAST(-80000.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Transaction] ([TransactionId], [AccountId], [EventDate], [TransactionType], [Amount]) VALUES (7, 2, CAST(N'2022-11-20T00:00:00.0000000' AS DateTime2), N'C', CAST(1000.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Transaction] ([TransactionId], [AccountId], [EventDate], [TransactionType], [Amount]) VALUES (9, 2, CAST(N'2022-11-20T00:00:00.0000000' AS DateTime2), N'D', CAST(-100.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Transaction] ([TransactionId], [AccountId], [EventDate], [TransactionType], [Amount]) VALUES (10, 2, CAST(N'2022-11-20T00:00:00.0000000' AS DateTime2), N'D', CAST(-100.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Transaction] ([TransactionId], [AccountId], [EventDate], [TransactionType], [Amount]) VALUES (11, 3, CAST(N'2022-11-20T00:00:00.0000000' AS DateTime2), N'D', CAST(-350.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Transaction] ([TransactionId], [AccountId], [EventDate], [TransactionType], [Amount]) VALUES (12, 3, CAST(N'2022-11-20T00:00:00.0000000' AS DateTime2), N'C', CAST(1200.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Transaction] ([TransactionId], [AccountId], [EventDate], [TransactionType], [Amount]) VALUES (13, 3, CAST(N'2022-11-20T00:00:00.0000000' AS DateTime2), N'D', CAST(-500.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Transaction] ([TransactionId], [AccountId], [EventDate], [TransactionType], [Amount]) VALUES (14, 1, CAST(N'2024-06-02T02:35:57.7942482' AS DateTime2), N'D', CAST(-200.00 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[Transaction] OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Account_InitialBalance]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF_Account_InitialBalance]  DEFAULT ((0)) FOR [InitialBalance]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Account_Status]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF_Account_Status]  DEFAULT ((1)) FOR [Status]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Client_Status]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Client] ADD  CONSTRAINT [DF_Client_Status]  DEFAULT ((1)) FOR [Status]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Transaction_EventDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Transaction] ADD  CONSTRAINT [DF_Transaction_EventDate]  DEFAULT (sysdatetime()) FOR [EventDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Account_Client]') AND parent_object_id = OBJECT_ID(N'[dbo].[Account]'))
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Client] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([ClientId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Account_Client]') AND parent_object_id = OBJECT_ID(N'[dbo].[Account]'))
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Client]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Transaction_Account]') AND parent_object_id = OBJECT_ID(N'[dbo].[Transaction]'))
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Account] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Transaction_Account]') AND parent_object_id = OBJECT_ID(N'[dbo].[Transaction]'))
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Account]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_Table_Type]') AND parent_object_id = OBJECT_ID(N'[dbo].[Account]'))
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [CK_Table_Type] CHECK  (([AccountType]='A' OR [AccountType]='C'))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_Table_Type]') AND parent_object_id = OBJECT_ID(N'[dbo].[Account]'))
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [CK_Table_Type]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_Transaction_Type]') AND parent_object_id = OBJECT_ID(N'[dbo].[Transaction]'))
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [CK_Transaction_Type] CHECK  (([TransactionType]=N'C' OR [TransactionType]=N'D'))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_Transaction_Type]') AND parent_object_id = OBJECT_ID(N'[dbo].[Transaction]'))
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [CK_Transaction_Type]
GO
