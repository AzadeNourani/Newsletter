USE [NewsletterDb]
GO

/****** Object:  Table [dbo].[SendNewsletterLog]    Script Date: 11/30/2023 11:24:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SendNewsletterLog2](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NewsletterId] [int] NULL,
	[SendTime] [datetime] NULL,
	[ReceiveTime] [datetime] NULL,
	[ViewStatus] [bit] NULL,
	[PersonnelId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SendNewsletterLog2]  WITH CHECK ADD FOREIGN KEY([NewsletterId])
REFERENCES [dbo].[Newsletter2] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SendNewsletterLog2]  WITH CHECK ADD FOREIGN KEY([PersonnelId])
REFERENCES [dbo].[Personnels2] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO


