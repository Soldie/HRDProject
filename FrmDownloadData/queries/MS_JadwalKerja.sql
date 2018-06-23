

IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'INS_JadwalKerja')
DROP PROCEDURE INS_JadwalKerja
GO




/*
	Created By 	:	Chandra Arifin
	Date		:	
	Stored Proc	:	INS_JadwalKerja
	Description	:	
*/
create procedure INS_JadwalKerja
	@nik				int,
	@tgl				date,
	@jammasuk			time(7),
	@jamkeluar			time(7),
	@toleransimasuk		time(7),
	@toleransikeluar	time(7)
WITH ENCRYPTION
as
	set nocount on

    insert into 
		MS_JadwalKerja
		(
			nik,			
			tgl,			
			jammasuk,		
			jamkeluar,		
			toleransimasuk,	
			toleransikeluar
		)
	values
	(
		@nik,			
		@tgl,			
		@jammasuk,		
		@jamkeluar,		
		@toleransimasuk,	
		@toleransikeluar
	)

GO





IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Del_JadwalKerja')
DROP PROCEDURE Del_JadwalKerja
GO




/*
	Created By 	:	Chandra Arifin
	Date		:	
	Stored Proc	:	Del_JadwalKerja
	Description	:	
*/
create procedure Del_JadwalKerja
	@nik				int,
	@PeriodeAwal		date,
	@PeriodeAkhir		date
WITH ENCRYPTION
as
set nocount on

delete from 
	MS_JadwalKerja
where
	nik = @nik and tgl between @PeriodeAwal and @PeriodeAkhir
	
GO

