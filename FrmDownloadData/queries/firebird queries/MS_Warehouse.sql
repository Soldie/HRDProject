


IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Get_Warehouse')
DROP PROCEDURE Get_Warehouse
GO

/*
	Created By 	:	Chandra Arifin
	Date		:	Wednesday; 21 May 2008; 22:09 PM
	Stored Proc	:	Get_Warehouse
	Description	:	Mendapatkan Info Ttg Warehouse
*/
create Procedure [dbo].[Get_Warehouse]
	@KdCabang varchar(5) = null,
	@KdWarehouse varchar(3) = null
WITH ENCRYPTION
as
set nocount on

select
	KdCabang,
	KdWarehouse,
	NmWarehouse
from
	MS_Warehouse
where
	KdCabang = coalesce(@KdCabang, KdCabang) and 
	KdWarehouse = coalesce(@KdWarehouse, KdWarehouse)
order by
	NmWarehouse
GO

