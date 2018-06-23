

IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GET_KodeAbsenSpesial')
DROP PROCEDURE GET_KodeAbsenSpesial
GO




/*
	Created By 	:	Chandra Arifin
	Date		:	Thursday, 10 July 2014, 11:30 AM
	Stored Proc	:	GET_KodeAbsenSpesial
	Description	:	
*/
create procedure GET_KodeAbsenSpesial
WITH ENCRYPTION
as
	set nocount on

    select
        id,
        absen_spesial
    from
        lt_kodeabsenspesial
    order by
        absen_spesial

GO



