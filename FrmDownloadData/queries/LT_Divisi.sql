


IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Get_Divisi')
DROP PROCEDURE Get_Divisi
GO

/*
	Created By 	:	Chandra Arifin
	Date		:	Wednesday; 21 May 2008; 22:09 PM
	Stored Proc	:	Get_Divisi
	Description	:	
*/
create Procedure [dbo].[Get_Divisi]
WITH ENCRYPTION
as
set nocount on

select
	divisi,
	[description]
from
	lt_divisi
order by
	[description]
GO

