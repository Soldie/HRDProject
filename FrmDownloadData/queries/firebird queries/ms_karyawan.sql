


IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'get_karyawan')
DROP PROCEDURE get_karyawan
GO



/*
	Created By 	:	Chandra Arifin
	Date		:	Tuesday; 31 Dec 2013; 20:09 PM
	Stored Proc	:	get_karyawan
	Description	:	Mendapatkan Info Ttg karywan
*/
create procedure get_karyawan
	@stsrc char(1) = null,
	@nik int = null
WITH ENCRYPTION
as
set nocount on

select
	stsrc,
	nik,
	nama,
	idjam
from
	ms_karyawan
where
	stsrc = coalesce(@stsrc, stsrc) and nik = coalesce(@nik, nik)
order by
	nama
GO




IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'del_karyawan')
DROP PROCEDURE del_karyawan
GO


/*
	Created By 	:	Chandra Arifin
	Date		:	Tuesday; 31 Dec 2013; 20:09 PM
	Stored Proc	:	del_karyawan
	Description	:	Delete karyawan
*/
create procedure del_karyawan
	@nik int,
	@userid nvarchar(10)
WITH ENCRYPTION
as
set nocount on

update
	ms_karyawan
set
	stsrc = 'D',
	UserUpdated = @UserID,
	DateUpdated = getdate()
where
	nik = @nik
GO





IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'del_karyawanpermanent')
DROP PROCEDURE del_karyawanpermanent
GO

/*
	Created By 	:	Chandra Arifin
	Date		:	Tuesday; 31 Dec 2013; 20:09 PM
	Stored Proc	:	del_karyawanpermanent
	Description	:	Delete karyawan permanent
*/
create procedure del_karyawanpermanent
	@nik int,
	@userid nvarchar(10)
WITH ENCRYPTION
as
set nocount on

DELETE from
	ms_karyawan
where
	nik = @nik
GO





IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ins_karyawan')
DROP PROCEDURE ins_karyawan
GO


/*
	Created By 	:	Chandra Arifin
	Date		:	Tuesday; 31 Dec 2013; 20:09 PM
	Stored Proc	:	ins_karyawan
	Description	:	Insert karyawan
*/
create procedure ins_karyawan
	@nik int,
	@nama nvarchar(255),
	@idjam smallint = 1,
	@userid nvarchar(10)
WITH ENCRYPTION
as
set nocount on

insert into
	ms_karyawan
	(
		nik,
		stsrc,
		nama,
		idjam,
		DateCreated,
		UserCreated,
		DateUpdated,
		UserUpdated
	)
values
	(
		@nik, 'A', @nama, @idjam, 
		getdate(), @UserID, getdate(), @UserID
	)
GO





IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'upd_karyawan')
DROP PROCEDURE upd_karyawan
GO


/*
	Created By 	:	Chandra Arifin
	Date		:	Tuesday; 31 Dec 2013; 20:09 PM
	Stored Proc	:	upd_karyawan
	Description	:	Update karyawan
*/
create procedure upd_karyawan
	@nik int,
	@stsrc char(1) = null,
	@nama nvarchar(255),
	@idjam smallint = 1,
	@userid nvarchar(10)
WITH ENCRYPTION
as
set nocount on

update
	ms_karyawan
set
	stsrc = coalesce(@StsRc, stsrc),
	nama = @nama,
	idjam = @idjam,
	UserUpdated = @UserID,
	DateUpdated = getdate()
where
	nik = @nik
GO



