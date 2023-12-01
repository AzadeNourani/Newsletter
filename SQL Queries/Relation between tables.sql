-- Your existing Newsletter and Personnels tables...

CREATE TABLE [dbo].[SendNewsletterLog4](
	[StatusID] [int] NOT NULL,
	[NewsletterID] [int] NULL,
	[SendTime] [datetime] NULL,
	[ReceiveTime] [datetime] NULL,
	[ViewStatus] [bit] NULL,
	[PersonnelId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Establishing the one-to-many relationship between Personnels and SendNewsletterLog
ALTER TABLE [dbo].[SendNewsletterLog4]  
WITH CHECK ADD FOREIGN KEY([PersonnelId])
REFERENCES [dbo].Personnels([PersonnelId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SendNewsletterLog4]  
WITH CHECK ADD FOREIGN KEY([NewsletterId])
REFERENCES [dbo].Newsletter ([NewsletterId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

