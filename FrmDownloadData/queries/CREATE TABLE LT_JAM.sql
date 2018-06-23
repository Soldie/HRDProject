

/****** Object:  Table [dbo].[lt_jam]    Script Date: 01/01/2014 16:00:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[lt_jam](
	[idjam] [smallint] NOT NULL,
	[jammasuk] [time](7) NOT NULL,
	[jamkeluar] [time](7) NOT NULL,
	[toleransimasuk] [time](7) NOT NULL,
 CONSTRAINT [PK_lt_jam] PRIMARY KEY CLUSTERED 
(
	[idjam] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


