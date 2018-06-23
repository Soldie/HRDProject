

IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GET_AGAMA')
DROP PROCEDURE GET_AGAMA
GO




/*
	Created By 	:	Chandra Arifin
	Date		:	
	Stored Proc	:	GET_AGAMA
	Description	:	
*/
create procedure GET_AGAMA
WITH ENCRYPTION
as
	set nocount on

    select
        agamaid,
        agama
    from
        lt_agama
    order by
        agama

GO



