
/****** Object:  Table [dbo].[trx_absensi]    Script Date: 01/01/2014 16:01:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[trx_absensi](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nik] [int] NULL,
	[clock_date] [date] NULL,
	[clock_time] [time](7) NULL,
 CONSTRAINT [PK_trx_absensi] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


