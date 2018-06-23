

IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'INS_JadwalOffKerja')
DROP PROCEDURE INS_JadwalOffKerja
GO




/*
	Created By 	:	Chandra Arifin
	Date		:	
	Stored Proc	:	INS_JadwalOffKerja
	Description	:	
*/
create procedure INS_JadwalOffKerja
	@nik				int,
	@tgl				date
WITH ENCRYPTION
as
	set nocount on

    insert into 
		MS_JadwalOffKerja
		(
			nik,			
			tgl
		)
	values
	(
		@nik,			
		@tgl
	)

GO




IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Del_JadwalOffKerja')
DROP PROCEDURE Del_JadwalOffKerja
GO




/*
	Created By 	:	Chandra Arifin
	Date		:	
	Stored Proc	:	Del_JadwalOffKerja
	Description	:	
*/
create procedure Del_JadwalOffKerja
	@nik				int,
	@PeriodeAwal		date,
	@PeriodeAkhir		date
WITH ENCRYPTION
as
set nocount on

delete from 
	MS_JadwalOffKerja
where
	nik = @nik and tgl between @PeriodeAwal and @PeriodeAkhir
	
GO

