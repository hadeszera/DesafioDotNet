if not exists (select * from sysobjects where name='Product' and xtype='U')

CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](80) NOT NULL,
	[Price] [decimal](18,2) NOT NULL,
	[Brand] [nvarchar](50) NULL,
	[UpdatedAt] [date] NULL,
	[CreatedAt] [date] NOT NULL
)
