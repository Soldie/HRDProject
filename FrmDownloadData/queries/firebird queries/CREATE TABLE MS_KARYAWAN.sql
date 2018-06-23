
/****** Object:  Table [dbo].[ms_karyawan]    Script Date: 01/01/2014 16:01:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ms_karyawan](
	[nik] [int] NOT NULL,
	[stsrc] [char](1) NULL,
	[nama] [nvarchar](255) NULL,
	[idjam] [smallint] NOT NULL,
	[usercreated] [nvarchar](50) NULL,
	[datecreated] [datetime] NULL,
	[userupdated] [nvarchar](50) NULL,
	[dateupdated] [datetime] NULL,
 CONSTRAINT [PK_ms_karyawan] PRIMARY KEY CLUSTERED 
(
	[nik] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ms_karyawan] ADD  CONSTRAINT [DF_ms_karyawan_stsrc]  DEFAULT ('A') FOR [stsrc]
GO

ALTER TABLE [dbo].[ms_karyawan] ADD  CONSTRAINT [DF_ms_karyawan_idjam]  DEFAULT ((1)) FOR [idjam]
GO


