

IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GET_JAM')
DROP PROCEDURE GET_JAM
GO




/*
	Created By 	:	Chandra Arifin
	Date		:	
	Stored Proc	:	GET_JAM
	Description	:	Mendapatkan Info Ttg jam
*/
create procedure GET_JAM
WITH ENCRYPTION
as
	set nocount on

    select
        idjam,
        'Jam ' + cast(jammasuk as varchar(8)) + ' - ' + cast(jamkeluar as varchar(8)) as Jam
    from
        lt_jam
    order by
        jammasuk

GO



