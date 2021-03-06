


IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'rpt_historyabsensi')
DROP PROCEDURE rpt_historyabsensi
GO


/*
	Created By 	:	Chandra Arifin
	Date		:	
	Stored Proc	:	rpt_historyabsensi
	Description	:	
*/
create procedure rpt_historyabsensi
	@nik int = null,
	@periodeawal date,
	@periodeakhir date,
	@kdcabang varchar(5) = null,
	@kdwarehouse varchar(3) = null,
	@divisi smallint,
	@viewNickName bit = 1
WITH ENCRYPTION
as
	set nocount on;

	select 
		distinct a.nik, 
			case 
				when b.nickname is null then b.nama
				when rtrim(b.nickname) = '' then b.nama
				else b.nickname
			end as nama, 
		b.idjam, a.clock_date,
		a.clock_time,
		a.devid 
	from 
		ms_karyawan as b
	inner join
		trx_absensi_filter as a
		on a.nik = b.nik
	where 
		b.stsrc <> 'D' and b.kdcabang = coalesce(@kdcabang, b.kdcabang) and
		b.kdwarehouse = coalesce(@kdwarehouse, b.kdwarehouse) and
		b.Divisi = @divisi and 
		a.clock_date between @periodeawal and @periodeakhir and
		b.nik = coalesce(@nik, b.nik)
	order by 
		a.nik, a.clock_date, a.clock_time

GO






IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'rpt_ambiljamabsensibyjadwal')
DROP PROCEDURE rpt_ambiljamabsensibyjadwal
GO


/*
	Created By 	:	Chandra Arifin
	Date		:	
	Stored Proc	:	rpt_ambiljamabsensibyjadwal
	Description	:	
*/
create procedure rpt_ambiljamabsensibyjadwal
	@nik int = null,
	@periodeawal date,
	@periodeakhir date,
	@kdcabang varchar(5) = null,
	@kdwarehouse varchar(3) = null,
	@divisi smallint,
	@viewNickName bit = 1
WITH ENCRYPTION
as
	set nocount on;
	
	if(@viewNickName = 1)
	begin
		;with cte as
		(
				select 
					distinct a.nik, 
						case 
							when b.nickname is null then b.nama
							when rtrim(b.nickname) = '' then b.nama
							else b.nickname
						end as nama, b.idjam, a.clock_date,
					a.clock_time,
					coalesce(c.jammasuk, '08:00') jammasuk, coalesce(c.jamkeluar, '17:00') jamkeluar,
					a.devid 
				from 
					ms_karyawan as b
				inner join
					trx_absensi_filter as a
					on a.nik = b.nik
				inner join
					MS_JadwalKerja as c
					on a.nik = c.nik and a.clock_date = c.tgl
				where 
					b.stsrc <> 'D' and b.kdcabang = coalesce(@kdcabang, b.kdcabang) and
					b.kdwarehouse = coalesce(@kdwarehouse, b.kdwarehouse) and
					b.ishitungum = 1 and b.Divisi = @divisi and
					b.nik = coalesce(@nik, b.nik)
		),
		agama as
		(
			select 
				a.nik, b.toleransimasuk
			from
				ms_karyawan as a
			left join
				lt_agama as b
				on a.agamaid = b.agamaid
		),
		result as
		(
			select 
				distinct
				b.nik,
				b.nama,
				b.clock_date,
				time_in,
				--coalesce(time_in, '00:00:00') as time_in,
				time_out,
				c.jammasuk,
				c.jamkeluar,
				case datepart(dw, b.clock_date)
					when 1 then coalesce(d.toleransimasuk, c.toleransimasuk)  --kalau hari minggu
					else c.toleransimasuk
				end as toleransimasuk,
					--coalesce(d.toleransimasuk, c.toleransimasuk) as toleransimasuk,
					--convert(varchar, DATEDIFF(second, time_in, time_out) / (60 * 60 * 24)) + ':' + 
				
				case 
					when time_in is null or time_out is null then null 
					else 
					
					right('0' + cast(datediff(minute, time_in, time_out) / 60 as varchar), 2) + ':' +
					right('0' + cast(datediff(minute, time_in, time_out) % 60 as varchar), 2) 

					--convert(varchar, dateadd(s, DATEDIFF(minute, time_in, time_out), convert(datetime2, '0001-01-01')), 108)
				end as jamkerja
			from
			(
				select
					nik,
					nama,
					clock_date,
					idjam,
					case when time_in = time_out and duration_in >= duration_out then null else time_in end time_in,
					case when time_in = time_out and duration_in <= duration_out then null else time_out end time_out
				from
				(
					select 
						nik,
						nama,
						clock_date,
						time_in,
						time_out,
						idjam,
						cast(datediff(second, jammasuk, time_in) as int) as duration_in,
						cast(datediff(second, time_out, jamkeluar) as int) as duration_out
					from
					(
						select 
							distinct 
							nik,
							nama,
							clock_date,
							jammasuk,
							jamkeluar,
							idjam,
							MIN(clock_time) over(partition by nik, clock_date) as time_in,
							MAX(clock_time) over(partition by nik, clock_date) as time_out
						from
							cte
					) cte1
				) a
			) b
			left join 
				MS_JadwalKerja as c
				on b.nik = c.nik and b.clock_date = c.tgl
			left join
				agama d
				on b.nik = d.nik
			where 
				b.clock_date between @periodeawal and @periodeakhir
		)
		
		
		select 
			a.nik,         
			a.nama,                                                                                                                                                                                                                                                            
			a.clock_date, 
			a.time_in,          
			a.time_out,         
			a.jammasuk,        
			a.jamkeluar,        
			a.toleransimasuk,   
			a.jamkerja, 
			b.alias as devid_masuk,
			c.alias as devid_keluar
		from
		(
			select 
				a.nik,         
				a.nama,                                                                                                                                                                                                                                                            
				a.clock_date, 
				a.time_in,          
				a.time_out,         
				a.jammasuk,        
				a.jamkeluar,        
				a.toleransimasuk,   
				a.jamkerja, 
				(select top 1 cte.devID from cte where cte.nik = a.nik and cte.clock_date = a.clock_date and cte.clock_time = a.time_in) as devid_masuk,
				(select top 1 cte.devID from cte where cte.nik = a.nik and cte.clock_date = a.clock_date and cte.clock_time = a.time_out) as devid_keluar
			from 
				result as a
		) as a
		left join
			MS_Warehouse as b
			on a.devid_masuk = b.devid
		left join
			MS_Warehouse as c
			on a.devid_keluar = c.devid			
		order by 
			a.nik, a.clock_date
	
	end
	else
	begin
		;with cte as
		(
				select 
					distinct a.nik, b.nama, b.idjam, a.clock_date,
					a.clock_time,
					coalesce(c.jammasuk, '08:00') jammasuk, coalesce(c.jamkeluar, '17:00') jamkeluar,
					a.devid 
				from 
					ms_karyawan as b
				inner join
					trx_absensi_filter as a
					on a.nik = b.nik
				inner join
					MS_JadwalKerja as c
					on a.nik = c.nik and a.clock_date = c.tgl
				where 
					b.stsrc <> 'D' and b.kdcabang = coalesce(@kdcabang, b.kdcabang) and
					b.kdwarehouse = coalesce(@kdwarehouse, b.kdwarehouse) and
					b.ishitungum = 1 and b.Divisi = @divisi
		),
		agama as
		(
			select 
				a.nik, b.toleransimasuk
			from
				ms_karyawan as a
			left join
				lt_agama as b
				on a.agamaid = b.agamaid
		),
		result as
		(		
			select 
				distinct
				b.nik,
				nama,
				clock_date,
				time_in,
				--coalesce(time_in, '00:00:00') as time_in,
				time_out,
				c.jammasuk,
				c.jamkeluar,
				case datepart(dw, clock_date)
					when 1 then coalesce(d.toleransimasuk, c.toleransimasuk)  --kalau hari minggu
					else c.toleransimasuk
				end as toleransimasuk,
					--coalesce(d.toleransimasuk, c.toleransimasuk) as toleransimasuk,
					--convert(varchar, DATEDIFF(second, time_in, time_out) / (60 * 60 * 24)) + ':' + 
				
				case 
					when time_in is null or time_out is null then null 
					else 
					
					right('0' + cast(datediff(minute, time_in, time_out) / 60 as varchar), 2) + ':' +
					right('0' + cast(datediff(minute, time_in, time_out) % 60 as varchar), 2) 

					--convert(varchar, dateadd(s, DATEDIFF(minute, time_in, time_out), convert(datetime2, '0001-01-01')), 108)
				end as jamkerja
			from
			(
				select
					nik,
					nama,
					clock_date,
					idjam,
					case when time_in = time_out and duration_in >= duration_out then null else time_in end time_in,
					case when time_in = time_out and duration_in <= duration_out then null else time_out end time_out
				from
				(
					select 
						nik,
						nama,
						clock_date,
						time_in,
						time_out,
						idjam,
						cast(datediff(second, jammasuk, time_in) as int) as duration_in,
						cast(datediff(second, time_out, jamkeluar) as int) as duration_out
					from
					(
						select 
							distinct 
							nik,
							nama,
							clock_date,
							jammasuk,
							jamkeluar,
							idjam,
							MIN(clock_time) over(partition by nik, clock_date) as time_in,
							MAX(clock_time) over(partition by nik, clock_date) as time_out
						from
							cte
					) cte1
				) a
			) b
			left join 
				MS_JadwalKerja as c
				on b.nik = c.nik and b.clock_date = c.tgl
			left join
				agama d
				on b.nik = d.nik
			where 
				b.clock_date between @periodeawal and @periodeakhir
		)
		
		select 
			a.nik,         
			a.nama,                                                                                                                                                                                                                                                            
			a.clock_date, 
			a.time_in,          
			a.time_out,         
			a.jammasuk,        
			a.jamkeluar,        
			a.toleransimasuk,   
			a.jamkerja, 
			b.alias as devid_masuk,
			c.alias as devid_keluar
		from
		(
			select 
				a.nik,         
				a.nama,                                                                                                                                                                                                                                                            
				a.clock_date, 
				a.time_in,          
				a.time_out,         
				a.jammasuk,        
				a.jamkeluar,        
				a.toleransimasuk,   
				a.jamkerja, 
				(select top 1 cte.devID from cte where cte.nik = a.nik and cte.clock_date = a.clock_date and cte.clock_time = a.time_in) as devid_masuk,
				(select top 1 cte.devID from cte where cte.nik = a.nik and cte.clock_date = a.clock_date and cte.clock_time = a.time_out) as devid_keluar
			from 
				result as a
		) as a
		left join
			MS_Warehouse as b
			on a.devid_masuk = b.devid
		left join
			MS_Warehouse as c
			on a.devid_keluar = c.devid			
		order by 
			a.nik, a.clock_date
	end


GO




IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'rpt_rekapabsensibyjadwal_telat')
DROP PROCEDURE rpt_rekapabsensibyjadwal_telat
GO


/*
	Created By 	:	Chandra Arifin
	Date		:	
	Stored Proc	:	rpt_rekapabsensibyjadwal_telat
	Description	:	
*/
create procedure rpt_rekapabsensibyjadwal_telat
	@periodeawal date,
	@periodeakhir date,
	@kdcabang varchar(5) = null,
	@kdwarehouse varchar(3) = null,
	@divisi smallint
WITH ENCRYPTION
as
	set nocount on

	SET @PeriodeAwal = convert(date, convert(varchar(10), @periodeawal, 105), 105)
	SET @PeriodeAkhir = convert(date, convert(varchar(10), @PeriodeAkhir, 105), 105)

	;with cte as
	(
		select 
			distinct a.nik, a.clock_date, a.clock_time, c.toleransimasuk
		from 
			trx_absensi_filter as a
		inner join
			ms_karyawan as b
			on a.nik = b.nik and b.stsrc <> 'D'
		inner join
			MS_JadwalKerja as c
			on a.nik = c.nik and a.clock_date = c.tgl
		where 
			b.KdCabang = @kdcabang and b.kdwarehouse = @kdwarehouse and 
			b.Divisi = @divisi and
			a.clock_date between @periodeawal and @periodeakhir
			
		--order by a.nik, a.clock_date
	),
	agama as
	(
		select 
			a.nik, b.toleransimasuk
		from
			ms_karyawan as a
		left join
			lt_agama as b
			on a.agamaid = b.agamaid
	),
	cte1 as
	(
		select 
			distinct 
			a.nik,
			clock_date,
			cast(MIN(clock_time) over(partition by a.nik, clock_date) as varchar(5)) as time_in,
			case datepart(dw, clock_date)
				when 1 then cast(coalesce(b.toleransimasuk, a.toleransimasuk) as varchar(5)) --kalau hari minggu
				else cast(a.toleransimasuk as varchar(5))
			end as toleransimasuk
			--,
			--cast(toleransimasuk as varchar(5)) as toleransimasuk
		from
			cte as a
		left join
			agama as b
			on a.nik = b.nik
	)
	
	select nik, clock_date from 
		cte1
	where
		time_in > toleransimasuk
	order by nik, clock_date
GO


IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'rpt_rekapabsensibyjadwal_off')
DROP PROCEDURE rpt_rekapabsensibyjadwal_off
GO


/*
	Created By 	:	Chandra Arifin
	Date		:	
	Stored Proc	:	rpt_rekapabsensibyjadwal_off
	Description	:	
*/
create procedure rpt_rekapabsensibyjadwal_off
	@periodeawal date,
	@periodeakhir date,
	@kdcabang varchar(5) = null,
	@kdwarehouse varchar(3) = null,
	@divisi smallint
WITH ENCRYPTION
as
	set nocount on

	SET @PeriodeAwal = convert(date, convert(varchar(10), @periodeawal, 105), 105)
	SET @PeriodeAkhir = convert(date, convert(varchar(10), @PeriodeAkhir, 105), 105)

	select 
		a.nik, b.tgl as clock_date
	from 
		ms_karyawan as a
	inner join
		MS_JadwalOffKerja as b
		on a.nik = b.nik
	where 
		a.KdCabang = @kdcabang and a.kdwarehouse = @kdwarehouse and 
		a.stsrc <> 'D' and
		a.Divisi = @divisi and
		b.tgl between @periodeawal and @periodeakhir		
	order by a.nik, b.tgl

GO



IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'rpt_rekapabsensibyjadwal')
DROP PROCEDURE rpt_rekapabsensibyjadwal
GO


/*
	Created By 	:	Chandra Arifin
	Date		:	
	Stored Proc	:	rpt_rekapabsensibyjadwal
	Description	:	
*/
create procedure rpt_rekapabsensibyjadwal
	@periodeawal date,
	@periodeakhir date,
	@kdcabang varchar(5) = null,
	@kdwarehouse varchar(3) = null,
	@divisi smallint,
	@viewNickName bit = 1
WITH ENCRYPTION
as
	set nocount on

	DECLARE 
		@query  AS VARCHAR(MAX),
		@cte as varchar(max),
		@tgl as varchar(max),
		@tmp as varchar(5);


	set @tmp = @kdwarehouse;
	
	if (@tmp is not null)
	begin
		set @tmp = '''' + @tmp + ''''
	end
	else
	begin
		set @tmp = 'null'
	end;
	
	with Dates AS (
			SELECT
			 [Date] = @periodeawal--CONVERT(date,DATEADD(dd,-(DAY(GETDATE())-1), GETDATE()),101)
			UNION ALL SELECT
			 [Date] = DATEADD(DAY, 1, [Date])
			FROM
			 Dates
			WHERE
			 Date < @periodeakhir--CONVERT(date,DATEADD(dd,-(DAY(DATEADD(mm, 1, GETDATE()))),DATEADD(mm,1, GETDATE())),101)
	)

	select @tgl = STUFF((SELECT ', [' + cast(date as varchar) + ']' from dates for xml path('')), 1, 1, '')

	IF EXISTS(SELECT * FROM sys.objects WHERE type = 'U' AND name = 'tmp_cek_harian')
	begin
		DROP table tmp_cek_harian
	end;

	if(@viewNickName = 1)
	begin
		select @cte =
		'
		;with 
		cek_harian as
		(
			select 
				distinct a.nik, 
					case 
						when b.nickname is null then b.nama
						when len(b.nickname) = 0 then b.nama
						else b.nickname
					end
				as nama, 
				a.clock_date
			from 
				trx_absensi_filter as a
			left join
				ms_karyawan as b
			on a.nik = b.nik and b.stsrc <> ''D''
			where 
				b.stsrc <> ''D'' and b.kdcabang = coalesce(''' + @kdcabang + ''', b.kdcabang) and
				b.kdwarehouse = coalesce(' + @tmp + ', b.kdwarehouse) and
				b.ishitungum = 1 and b.divisi = ' + cast(@divisi as varchar) + ' and 
				nama is not null
		) ';
	end
	else
	begin
		select @cte =
		'
		;with 
		cek_harian as
		(
			select 
				distinct a.nik, b.nama, a.clock_date
			from 
				trx_absensi_filter as a
			left join
				ms_karyawan as b
			on a.nik = b.nik and b.stsrc <> ''D''
			where 
				b.stsrc <> ''D'' and b.kdcabang = coalesce(''' + @kdcabang + ''', b.kdcabang) and
				b.kdwarehouse = coalesce(' + @tmp + ', b.kdwarehouse) and
				b.ishitungum = 1 and b.divisi = ' + cast(@divisi as varchar) + ' and 
				nama is not null
		) ';
	end
	
	select @query =

	@cte + 
	'select * into tmp_cek_harian
	from cek_harian 
	pivot
	(
	count(clock_date) for
	clock_date in
	(' +

	@tgl

	+ ')) f';

	exec (@query);

	select 
		distinct a.*, c.uangmakan as "Uang Makan", b.offhari
	from 
		tmp_cek_harian as a
	left join
		ms_karyawan as c
		on a.nik = c.nik and c.stsrc <> 'D'
	left join
		(
			select 
				nik, max(tgl) as offhari 
			from 
			ms_jadwaloffkerja
			where
			tgl between @periodeawal and @periodeakhir
			group by nik
		)
		as b
		on c.nik = b.nik
		order by b.offhari, a.nama


	
	
GO





IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'rpt_rekapabsensi_ijin_sakit')
DROP PROCEDURE rpt_rekapabsensi_ijin_sakit
GO


/*
	Created By 	:	Chandra Arifin
	Date		:	
	Stored Proc	:	rpt_rekapabsensi_ijin_sakit
	Description	:	
*/
create procedure rpt_rekapabsensi_ijin_sakit
--	@nik int,
	@periodeawal date,
	@periodeakhir date,
	@kdcabang varchar(5) = null,
	@kdwarehouse varchar(3) = null,
	@divisi smallint
WITH ENCRYPTION
as
	set nocount on

	SET @PeriodeAwal = convert(date, convert(varchar(10), @periodeawal, 105), 105)
	SET @PeriodeAkhir = convert(date, convert(varchar(10), @PeriodeAkhir, 105), 105)

	select 
		a.nik, b.clock_date
	from 
		ms_karyawan as a
	inner join
		trx_absensi_spesial as b
		on a.nik = b.nik
	where 
		a.KdCabang = @kdcabang and a.kdwarehouse = @kdwarehouse and 
		a.stsrc <> 'D' and
		a.Divisi = @divisi and
		b.clock_date between @periodeawal and @periodeakhir		and
		b.id_absensi_spesial <> 'L'
	order by a.nik, b.clock_date

GO





IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'rpt_rekapabsensi_ijin_lain2')
DROP PROCEDURE rpt_rekapabsensi_ijin_lain2
GO


/*
	Created By 	:	Chandra Arifin
	Date		:	
	Stored Proc	:	rpt_rekapabsensi_ijin_lain2
	Description	:	
*/
create procedure rpt_rekapabsensi_ijin_lain2
--	@nik int,
	@periodeawal date,
	@periodeakhir date,
	@kdcabang varchar(5) = null,
	@kdwarehouse varchar(3) = null,
	@divisi smallint
WITH ENCRYPTION
as
	set nocount on

	SET @PeriodeAwal = convert(date, convert(varchar(10), @periodeawal, 105), 105)
	SET @PeriodeAkhir = convert(date, convert(varchar(10), @PeriodeAkhir, 105), 105)

	select 
		a.nik, b.clock_date
	from 
		ms_karyawan as a
	inner join
		trx_absensi_spesial as b
		on a.nik = b.nik
	where 
		a.KdCabang = @kdcabang and a.kdwarehouse = @kdwarehouse and 
		a.stsrc <> 'D' and
		a.Divisi = @divisi and
		b.clock_date between @periodeawal and @periodeakhir and
		b.id_absensi_spesial = 'L'
	order by a.nik, b.clock_date

GO



IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'rpt_rekapabsensi_off')
DROP PROCEDURE rpt_rekapabsensi_off
GO


/*
	Created By 	:	Chandra Arifin
	Date		:	
	Stored Proc	:	rpt_rekapabsensi_off
	Description	:	
*/
create procedure rpt_rekapabsensi_off
--	@nik int,
	@periodeawal date,
	@periodeakhir date,
	@kdcabang varchar(5) = null,
	@kdwarehouse varchar(3) = null,
	@divisi smallint
WITH ENCRYPTION
as
	set nocount on

	SET @PeriodeAwal = convert(date, convert(varchar(10), @periodeawal, 105), 105)
	SET @PeriodeAkhir = convert(date, convert(varchar(10), @PeriodeAkhir, 105), 105)

	select 
		a.nik, b.tgloff as clock_date
	from 
		ms_karyawan as a
	inner join
		ms_offkerja as b
		on a.nik = b.nik
	where 
		a.KdCabang = @kdcabang and a.kdwarehouse = @kdwarehouse and 
		a.stsrc <> 'D' and
		a.Divisi = @divisi and
		b.tgloff between @periodeawal and @periodeakhir		
	order by a.nik, b.tgloff

GO







IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'rpt_rekapabsensi_pulangcepat')
DROP PROCEDURE rpt_rekapabsensi_pulangcepat
GO


/*
	Created By 	:	Chandra Arifin
	Date		:	
	Stored Proc	:	rpt_rekapabsensi_pulangcepat
	Description	:	
*/
create procedure rpt_rekapabsensi_pulangcepat
--	@nik int,
	@periodeawal date,
	@periodeakhir date,
	@kdcabang varchar(5) = null,
	@kdwarehouse varchar(3) = null,
	@divisi smallint
WITH ENCRYPTION
as
	set nocount on

	SET @PeriodeAwal = convert(date, convert(varchar(10), @periodeawal, 105), 105)
	SET @PeriodeAkhir = convert(date, convert(varchar(10), @PeriodeAkhir, 105), 105)

	;with cte as
	(
		select 
			distinct a.nik, a.clock_date, a.clock_time, 
			coalesce(c.jamkeluar, '17:00') as toleransikeluar
		from 
			ms_karyawan as b
		inner join
			trx_absensi_filter as a
			on a.nik = b.nik
		inner join
			MS_JadwalKerja as c
			on a.nik = c.nik and a.clock_date = c.tgl
		where 
			b.stsrc <> 'D' and b.kdcabang = coalesce(@kdcabang, b.kdcabang) and
			b.kdwarehouse = coalesce(@kdwarehouse, b.kdwarehouse) and
			b.Divisi = @divisi and a.clock_date between @periodeawal and @periodeakhir
	),
	cte1 as
	(
		select 
			distinct 
			nik,
			clock_date,
			cast(Max(clock_time) over(partition by nik, clock_date) as varchar(5)) as time_out,
			cast(toleransikeluar as varchar(5)) as toleransikeluar
		from
			cte
	)
	
	select nik, clock_date from 
		cte1
	where
		time_out < toleransikeluar
	order by nik, clock_date
GO




IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'rpt_rekapabsensi_pulangcepat_OLD')
DROP PROCEDURE rpt_rekapabsensi_pulangcepat_OLD
GO


/*
	Created By 	:	Chandra Arifin
	Date		:	
	Stored Proc	:	rpt_rekapabsensi_pulangcepat_OLD
	Description	:	
*/
create procedure rpt_rekapabsensi_pulangcepat_OLD
--	@nik int,
	@periodeawal date,
	@periodeakhir date,
	@kdcabang varchar(5) = null,
	@kdwarehouse varchar(3) = null,
	@divisi smallint
WITH ENCRYPTION
as
	set nocount on

	SET @PeriodeAwal = convert(date, convert(varchar(10), @periodeawal, 105), 105)
	SET @PeriodeAkhir = convert(date, convert(varchar(10), @PeriodeAkhir, 105), 105)

	;with cte as
	(
		select 
			distinct a.nik, a.clock_date, a.clock_time, c.toleransikeluar
		from 
			trx_absensi_filter as a
		inner join
			ms_karyawan as b
			on a.nik = b.nik and b.stsrc <> 'D'
		inner join
			lt_jam as c
			on b.idjam = c.idjam
		where 
			b.KdCabang = @kdcabang and b.kdwarehouse = @kdwarehouse and 
			b.Divisi = @divisi and
			a.clock_date between @periodeawal and @periodeakhir			
	),
	cte1 as
	(
		select 
			distinct 
			nik,
			clock_date,
			cast(Max(clock_time) over(partition by nik, clock_date) as varchar(5)) as time_out,
			cast(toleransikeluar as varchar(5)) as toleransikeluar
		from
			cte
	)
	
	select nik, clock_date from 
		cte1
	where
		time_out < toleransikeluar
	order by nik, clock_date
GO






IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'rpt_rekapabsensi_telat')
DROP PROCEDURE rpt_rekapabsensi_telat
GO


/*
	Created By 	:	Chandra Arifin
	Date		:	
	Stored Proc	:	rpt_rekapabsensi_telat
	Description	:	
*/
create procedure rpt_rekapabsensi_telat
--	@nik int,
	@periodeawal date,
	@periodeakhir date,
	@kdcabang varchar(5) = null,
	@kdwarehouse varchar(3) = null,
	@divisi smallint
WITH ENCRYPTION
as
	set nocount on

	SET @PeriodeAwal = convert(date, convert(varchar(10), @periodeawal, 105), 105)
	SET @PeriodeAkhir = convert(date, convert(varchar(10), @PeriodeAkhir, 105), 105)

	;with cte as
	(
		select 
			distinct a.nik, a.clock_date, a.clock_time, c.toleransimasuk
		from 
			trx_absensi_filter as a
		inner join
			ms_karyawan as b
			on a.nik = b.nik and b.stsrc <> 'D'
		inner join
			lt_jam as c
			on b.idjam = c.idjam
		where 
			b.KdCabang = @kdcabang and b.kdwarehouse = @kdwarehouse and 
			b.Divisi = @divisi and
			a.clock_date between @periodeawal and @periodeakhir
			
		--order by a.nik, a.clock_date
	),
	agama as
	(
		select 
			a.nik, b.toleransimasuk
		from
			ms_karyawan as a
		left join
			lt_agama as b
			on a.agamaid = b.agamaid
	),
	cte1 as
	(
		select 
			distinct 
			a.nik,
			clock_date,
			cast(MIN(clock_time) over(partition by a.nik, clock_date) as varchar(5)) as time_in,
			case datepart(dw, clock_date)
				when 1 then cast(coalesce(b.toleransimasuk, a.toleransimasuk) as varchar(5)) --kalau hari minggu
				else cast(a.toleransimasuk as varchar(5))
			end as toleransimasuk
			--,
			--cast(toleransimasuk as varchar(5)) as toleransimasuk
		from
			cte as a
		left join
			agama as b
			on a.nik = b.nik
	)
	
	select nik, clock_date from 
		cte1
	where
		time_in > toleransimasuk
	order by nik, clock_date
GO




IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'rpt_rekapabsensi_onefingerprint')
DROP PROCEDURE rpt_rekapabsensi_onefingerprint
GO


/*
	Created By 	:	Chandra Arifin
	Date		:	
	Stored Proc	:	rpt_rekapabsensi_onefingerprint
	Description	:	
*/
create procedure rpt_rekapabsensi_onefingerprint
--	@nik int,
	@periodeawal date,
	@periodeakhir date,
	@kdcabang varchar(5) = null,
	@kdwarehouse varchar(3) = null,
	@divisi smallint
WITH ENCRYPTION
as
	set nocount on

	SET @PeriodeAwal = convert(date, convert(varchar(10), @periodeawal, 105), 105)
	SET @PeriodeAkhir = convert(date, convert(varchar(10), @PeriodeAkhir, 105), 105)

	select 
		distinct a.nik, a.clock_date
	from 
		trx_absensi_filter as a
	inner join
		ms_karyawan as b
		on a.nik = b.nik and b.stsrc <> 'D'
	where 
		b.KdCabang = @kdcabang and b.kdwarehouse = @kdwarehouse and 
		b.Divisi = @divisi and
		a.clock_date between @periodeawal and @periodeakhir
	group by a.nik, a.clock_date
	having COUNT(1) = 1
	order by a.nik, a.clock_date
	
GO







IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'rpt_rekapabsensi')
DROP PROCEDURE rpt_rekapabsensi
GO


/*
	Created By 	:	Chandra Arifin
	Date		:	Tuesday; 31 Dec 2013; 20:09 PM
	Stored Proc	:	rpt_rekapabsensi
	Description	:	Rekap absensi karyawan
*/
create procedure rpt_rekapabsensi
--	@nik int,
	@periodeawal date,
	@periodeakhir date,
	@kdcabang varchar(5) = null,
	@kdwarehouse varchar(3) = null,
	@divisi smallint,
	@viewNickName bit = 1
WITH ENCRYPTION
as
	set nocount on

	DECLARE 
		@query  AS VARCHAR(MAX),
		@cte as varchar(max),
		@tgl as varchar(max),
		@tmp as varchar(5);


	set @tmp = @kdwarehouse;
	
	if (@tmp is not null)
	begin
		set @tmp = '''' + @tmp + ''''
	end
	else
	begin
		set @tmp = 'null'
	end;
	
	with Dates AS (
			SELECT
			 [Date] = @periodeawal--CONVERT(date,DATEADD(dd,-(DAY(GETDATE())-1), GETDATE()),101)
			UNION ALL SELECT
			 [Date] = DATEADD(DAY, 1, [Date])
			FROM
			 Dates
			WHERE
			 Date < @periodeakhir--CONVERT(date,DATEADD(dd,-(DAY(DATEADD(mm, 1, GETDATE()))),DATEADD(mm,1, GETDATE())),101)
	)

	select @tgl = STUFF((SELECT ', [' + cast(date as varchar) + ']' from dates for xml path('')), 1, 1, '')

	IF EXISTS(SELECT * FROM sys.objects WHERE type = 'U' AND name = 'tmp_cek_harian')
	begin
		DROP table tmp_cek_harian
	end;

	if(@viewNickName = 1)
	begin
		select @cte =
		'
		;with 
		cek_harian as
		(
			select 
				distinct a.nik, 
					case 
						when b.nickname is null then b.nama
						when len(b.nickname) = 0 then b.nama
						else b.nickname
					end
				as nama, 
				a.clock_date
			from 
				trx_absensi_filter as a
			left join
				ms_karyawan as b
			on a.nik = b.nik and b.stsrc <> ''D''
			where 
				b.stsrc <> ''D'' and b.kdcabang = coalesce(''' + @kdcabang + ''', b.kdcabang) and
				b.kdwarehouse = coalesce(' + @tmp + ', b.kdwarehouse) and
				b.ishitungum = 1 and b.divisi = ' + cast(@divisi as varchar) + ' and 
				nama is not null
		) ';
	end
	else
	begin
		select @cte =
		'
		;with 
		cek_harian as
		(
			select 
				distinct a.nik, b.nama, a.clock_date
			from 
				trx_absensi_filter as a
			left join
				ms_karyawan as b
			on a.nik = b.nik and b.stsrc <> ''D''
			where 
				b.stsrc <> ''D'' and b.kdcabang = coalesce(''' + @kdcabang + ''', b.kdcabang) and
				b.kdwarehouse = coalesce(' + @tmp + ', b.kdwarehouse) and
				b.ishitungum = 1 and b.divisi = ' + cast(@divisi as varchar) + ' and 
				nama is not null
		) ';
	end
	
	select @query =

	@cte + 
	'select * into tmp_cek_harian
	from cek_harian 
	pivot
	(
	count(clock_date) for
	clock_date in
	(' +

	@tgl

	+ ')) f';

	exec (@query);

	--DECLARE @tmpList VARCHAR(MAX)

	--;with offkerja as (
	--	SELECT nik, STUFF(( SELECT ', ' + cast(offhari as varchar)
 --           FROM ms_offkerja as b
	--		where b.nik = a.nik
 --         FOR
 --           XML PATH('')
 --         ), 1, 1, '') as offhari
 --         from ms_offkerja as a
 --         group by nik
 --   )

	select 
		distinct a.*, c.uangmakan as "Uang Makan", b.offhari
	from 
		tmp_cek_harian as a
	left join
		ms_karyawan as c
		on a.nik = c.nik and c.stsrc <> 'D'
	left join
		(
			select nik, max(offhari) as offhari from 
			ms_offkerja
			where
			tgloff between @periodeawal and @periodeakhir
			group by nik
		)
		as b
		on c.nik = b.nik
	order by b.offhari, a.nama


	
	
GO




IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'rpt_ambiljamabsensi')
DROP PROCEDURE rpt_ambiljamabsensi
GO


/*
	Created By 	:	Chandra Arifin
	Date		:	Tuesday; 31 Dec 2013; 20:09 PM
	Stored Proc	:	rpt_ambiljamabsensi
	Description	:	Rekap absensi karyawan
*/
create procedure rpt_ambiljamabsensi
--	@nik int,
	@periodeawal date,
	@periodeakhir date,
	@kdcabang varchar(5) = null,
	@kdwarehouse varchar(3) = null,
	@divisi smallint,
	@viewNickName bit = 1
WITH ENCRYPTION
as
	set nocount on;
	
	if(@viewNickName = 1)
	begin
		;with cte as
		(
				select 
					distinct a.nik, 
						case 
							when b.nickname is null then b.nama
							when rtrim(b.nickname) = '' then b.nama
							else b.nickname
						end as nama, b.idjam, a.clock_date,
					a.clock_time,
					coalesce(c.jammasuk, '08:00') jammasuk, coalesce(c.jamkeluar, '17:00') jamkeluar,
					a.devid 
				from 
					ms_karyawan as b
				inner join
					trx_absensi_filter as a
					on a.nik = b.nik
				inner join
					lt_jam as c
					on c.idjam = b.idjam
				where 
					b.stsrc <> 'D' and b.kdcabang = coalesce(@kdcabang, b.kdcabang) and
					b.kdwarehouse = coalesce(@kdwarehouse, b.kdwarehouse) and
					b.ishitungum = 1 and b.Divisi = @divisi
		),
		agama as
		(
			select 
				a.nik, b.toleransimasuk
			from
				ms_karyawan as a
			left join
				lt_agama as b
				on a.agamaid = b.agamaid
		),
		result as
		(
			select 
				distinct
				b.nik,
				b.nama,
				b.clock_date,
				--time_in,
				coalesce(time_in, '00:00:00') as time_in,
				time_out,
				c.jammasuk,
				c.jamkeluar,
				case datepart(dw, b.clock_date)
					when 1 then coalesce(d.toleransimasuk, c.toleransimasuk)  --kalau hari minggu
					else c.toleransimasuk
				end as toleransimasuk,
					--coalesce(d.toleransimasuk, c.toleransimasuk) as toleransimasuk,
					--convert(varchar, DATEDIFF(second, time_in, time_out) / (60 * 60 * 24)) + ':' + 
				
				case 
					when time_in is null or time_out is null then null 
					else 
					
					right('0' + cast(datediff(minute, time_in, time_out) / 60 as varchar), 2) + ':' +
					right('0' + cast(datediff(minute, time_in, time_out) % 60 as varchar), 2) 

					--convert(varchar, dateadd(s, DATEDIFF(minute, time_in, time_out), convert(datetime2, '0001-01-01')), 108)
				end as jamkerja
			from
			(
				select
					nik,
					nama,
					clock_date,
					idjam,
					case when time_in = time_out and duration_in >= duration_out then null else time_in end time_in,
					case when time_in = time_out and duration_in <= duration_out then null else time_out end time_out
				from
				(
					select 
						nik,
						nama,
						clock_date,
						time_in,
						time_out,
						idjam,
						cast(datediff(second, jammasuk, time_in) as int) as duration_in,
						cast(datediff(second, time_out, jamkeluar) as int) as duration_out
					from
					(
						select 
							distinct 
							nik,
							nama,
							clock_date,
							jammasuk,
							jamkeluar,
							idjam,
							MIN(clock_time) over(partition by nik, clock_date) as time_in,
							MAX(clock_time) over(partition by nik, clock_date) as time_out
						from
							cte
					) cte1
				) a
			) b
			left join 
				lt_jam c
				on c.idjam = b.idjam
			left join
				agama d
				on b.nik = d.nik
			where 
				b.clock_date between @periodeawal and @periodeakhir
		)
		
		
		select 
			a.nik,         
			a.nama,                                                                                                                                                                                                                                                            
			a.clock_date, 
			a.time_in,          
			a.time_out,         
			a.jammasuk,        
			a.jamkeluar,        
			a.toleransimasuk,   
			a.jamkerja, 
			b.alias as devid_masuk,
			c.alias as devid_keluar
		from
		(
			select 
				a.nik,         
				a.nama,                                                                                                                                                                                                                                                            
				a.clock_date, 
				a.time_in,          
				a.time_out,         
				a.jammasuk,        
				a.jamkeluar,        
				a.toleransimasuk,   
				a.jamkerja, 
				(select top 1 cte.devID from cte where cte.nik = a.nik and cte.clock_date = a.clock_date and cte.clock_time = a.time_in) as devid_masuk,
				(select top 1 cte.devID from cte where cte.nik = a.nik and cte.clock_date = a.clock_date and cte.clock_time = a.time_out) as devid_keluar
			from 
				result as a
		) as a
		left join
			MS_Warehouse as b
			on a.devid_masuk = b.devid
		left join
			MS_Warehouse as c
			on a.devid_keluar = c.devid			
		order by 
			a.nik, a.clock_date
	
	end
	else
	begin
		;with cte as
		(
				select 
					distinct a.nik, b.nama, b.idjam, a.clock_date,
					a.clock_time,
					coalesce(c.jammasuk, '08:00') jammasuk, coalesce(c.jamkeluar, '17:00') jamkeluar,
					a.devid 
				from 
					ms_karyawan as b
				inner join
					trx_absensi_filter as a
					on a.nik = b.nik
				inner join
					lt_jam as c
					on c.idjam = b.idjam
				where 
					b.stsrc <> 'D' and b.kdcabang = coalesce(@kdcabang, b.kdcabang) and
					b.kdwarehouse = coalesce(@kdwarehouse, b.kdwarehouse) and
					b.ishitungum = 1 and b.Divisi = @divisi
		),
		agama as
		(
			select 
				a.nik, b.toleransimasuk
			from
				ms_karyawan as a
			left join
				lt_agama as b
				on a.agamaid = b.agamaid
		),
		result as
		(		
			select 
				distinct
				b.nik,
				nama,
				clock_date,
				time_in,
				--coalesce(time_in, '00:00:00') as time_in,
				time_out,
				c.jammasuk,
				c.jamkeluar,
				case datepart(dw, clock_date)
					when 1 then coalesce(d.toleransimasuk, c.toleransimasuk)  --kalau hari minggu
					else c.toleransimasuk
				end as toleransimasuk,
					--coalesce(d.toleransimasuk, c.toleransimasuk) as toleransimasuk,
					--convert(varchar, DATEDIFF(second, time_in, time_out) / (60 * 60 * 24)) + ':' + 
				
				case 
					when time_in is null or time_out is null then null 
					else 
					
					right('0' + cast(datediff(minute, time_in, time_out) / 60 as varchar), 2) + ':' +
					right('0' + cast(datediff(minute, time_in, time_out) % 60 as varchar), 2) 

					--convert(varchar, dateadd(s, DATEDIFF(minute, time_in, time_out), convert(datetime2, '0001-01-01')), 108)
				end as jamkerja
			from
			(
				select
					nik,
					nama,
					clock_date,
					idjam,
					case when time_in = time_out and duration_in >= duration_out then null else time_in end time_in,
					case when time_in = time_out and duration_in <= duration_out then null else time_out end time_out
				from
				(
					select 
						nik,
						nama,
						clock_date,
						time_in,
						time_out,
						idjam,
						cast(datediff(second, jammasuk, time_in) as int) as duration_in,
						cast(datediff(second, time_out, jamkeluar) as int) as duration_out
					from
					(
						select 
							distinct 
							nik,
							nama,
							clock_date,
							jammasuk,
							jamkeluar,
							idjam,
							MIN(clock_time) over(partition by nik, clock_date) as time_in,
							MAX(clock_time) over(partition by nik, clock_date) as time_out
						from
							cte
					) cte1
				) a
			) b
			left join 
				lt_jam c
				on c.idjam = b.idjam
			left join
				agama d
				on b.nik = d.nik
			where 
				b.clock_date between @periodeawal and @periodeakhir
		)
		
		select 
			a.nik,         
			a.nama,                                                                                                                                                                                                                                                            
			a.clock_date, 
			a.time_in,          
			a.time_out,         
			a.jammasuk,        
			a.jamkeluar,        
			a.toleransimasuk,   
			a.jamkerja, 
			b.alias as devid_masuk,
			c.alias as devid_keluar
		from
		(
			select 
				a.nik,         
				a.nama,                                                                                                                                                                                                                                                            
				a.clock_date, 
				a.time_in,          
				a.time_out,         
				a.jammasuk,        
				a.jamkeluar,        
				a.toleransimasuk,   
				a.jamkerja, 
				(select top 1 cte.devID from cte where cte.nik = a.nik and cte.clock_date = a.clock_date and cte.clock_time = a.time_in) as devid_masuk,
				(select top 1 cte.devID from cte where cte.nik = a.nik and cte.clock_date = a.clock_date and cte.clock_time = a.time_out) as devid_keluar
			from 
				result as a
		) as a
		left join
			MS_Warehouse as b
			on a.devid_masuk = b.devid
		left join
			MS_Warehouse as c
			on a.devid_keluar = c.devid			
		order by 
			a.nik, a.clock_date
	end


GO






IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'rpt_ambiljamabsensi2')
DROP PROCEDURE rpt_ambiljamabsensi2
GO


/*
	Created By 	:	Chandra Arifin
	Date		:	Tuesday; 31 Dec 2013; 20:09 PM
	Stored Proc	:	rpt_ambiljamabsensi
	Description	:	Rekap absensi karyawan
*/
create procedure rpt_ambiljamabsensi2
	@nik int = null,
	@periodeawal date,
	@periodeakhir date,
	@kdcabang varchar(5) = null,
	@kdwarehouse varchar(3) = null,
	@divisi smallint,
	@viewNickName bit = 1
WITH ENCRYPTION
as
	set nocount on;

	if(@viewNickName = 1)
	begin	
		;with cte as
		(	
			select
				dense_rank() over(partition by nik, clock_date order by nik, clock_date, clock_time) as "rowno",
				nik, nama, clock_date, clock_time, jammasuk, jamkeluar, idjam
			from
			(		
					select 
						distinct 
						a.nik, case 
							when b.nickname is null then b.nama
							when rtrim(b.nickname) = '' then b.nama
							else b.nickname
						end as nama,
						b.idjam, a.clock_date,
						a.clock_time,
						coalesce(c.jammasuk, '08:00') jammasuk, coalesce(c.jamkeluar, '17:00') jamkeluar
					from 
						trx_absensi_filter as a
					left join
						ms_karyawan as b
						on a.nik = b.nik
					inner join
						lt_jam as c
						on c.idjam = b.idjam
					where 
						b.stsrc <> 'D' and b.kdcabang = coalesce(@kdcabang, b.kdcabang) and
						b.kdwarehouse = coalesce(@kdwarehouse, b.kdwarehouse) and
						b.ishitungum = 1 and b.Divisi = @divisi and 
						b.nik = coalesce(@nik, b.nik)
			) a	
		),
		cte_result as
		(
			select 
				cte.rowno, cte1.maxrowno, cte.nik, cte.nama, cte.clock_date, cte.clock_time, cte.jammasuk, cte.jamkeluar, cte.idjam
			from
				cte
			inner join
				(select max(rowno) as maxrowno, nik, clock_date from cte group by nik, clock_date) cte1
				on cte.nik = cte1.nik and cte.clock_date = cte1.clock_date
		)
			
		select
			nik,
			nama,
			clock_date,
			d.jammasuk,
			d.jamkeluar,
			d.idjam,
			coalesce(time_in, '00:00:00') as time_in,
			coalesce(time_out, '00:00:00') as time_out,
			coalesce(time_lunch_in, '00:00:00') as time_lunch_in,
			coalesce(time_lunch_out, '00:00:00') as time_lunch_out,
			c.toleransimasuk
		from
		(	
			select
				nik,
				nama,
				clock_date,
				jammasuk,
				jamkeluar,
				idjam,
				max(time_in) as time_in,
				max(time_out) as time_out,
				max(time_lunch_in) as time_lunch_in,
				max(time_lunch_out) as time_lunch_out
			from
			(
				select
				distinct 
					rowno,
					nik,
					nama,
					clock_date,
					jammasuk,
					jamkeluar,
					idjam,
					time_in,
					time_out, --case when maxrowno = 2 then time_lunch_in else time_out end as time_out,
					time_lunch_in,
					time_lunch_out
				from
				(
					select 
						distinct 
						rowno,
						nik,
						nama,
						clock_date,
						jammasuk,
						jamkeluar,
						idjam,
						maxrowno,
						case when rowno = 1 then clock_time
						end as time_in,
						case when rowno = 2 then clock_time
						end as time_lunch_in,
						case when rowno = 3 then clock_time
						end as time_lunch_out,
						case when rowno = 4 then clock_time
						end as time_out
					from
						cte_result
				) a
			) b
			group by
				nik,
				nama,
				clock_date,
				jammasuk,
				jamkeluar,
				idjam
		) d
		left join 
			lt_jam c
			on c.idjam = d.idjam
		where d.clock_date between @periodeawal and @periodeakhir
		order by nik, clock_date	
	end
	else
	begin
		;with cte as
		(	
			select
				dense_rank() over(partition by nik, clock_date order by nik, clock_date, clock_time) as "rowno",
				nik, nama, clock_date, clock_time, jammasuk, jamkeluar, idjam
			from
			(		
					select 
						distinct 
						a.nik, b.nama, b.idjam, a.clock_date,
						a.clock_time,
						coalesce(c.jammasuk, '08:00') jammasuk, coalesce(c.jamkeluar, '17:00') jamkeluar
					from 
						trx_absensi_filter as a
					left join
						ms_karyawan as b
						on a.nik = b.nik
					inner join
						lt_jam as c
						on c.idjam = b.idjam
					where 
						b.stsrc <> 'D' and b.kdcabang = coalesce(@kdcabang, b.kdcabang) and
						b.kdwarehouse = coalesce(@kdwarehouse, b.kdwarehouse) and
						b.ishitungum = 1 and b.Divisi = @divisi
			) a	
		),
		cte_result as
		(
			select 
				cte.rowno, cte1.maxrowno, cte.nik, cte.nama, cte.clock_date, cte.clock_time, cte.jammasuk, cte.jamkeluar, cte.idjam
			from
				cte
			inner join
				(select max(rowno) as maxrowno, nik, clock_date from cte group by nik, clock_date) cte1
				on cte.nik = cte1.nik and cte.clock_date = cte1.clock_date
		)
			
		select
			nik,
			nama,
			clock_date,
			d.jammasuk,
			d.jamkeluar,
			d.idjam,
			coalesce(time_in, '00:00:00') as time_in,
			coalesce(time_out, '00:00:00') as time_out,
			coalesce(time_lunch_in, '00:00:00') as time_lunch_in,
			coalesce(time_lunch_out, '00:00:00') as time_lunch_out,
			c.toleransimasuk
		from
		(	
			select
				nik,
				nama,
				clock_date,
				jammasuk,
				jamkeluar,
				idjam,
				max(time_in) as time_in,
				max(time_out) as time_out,
				max(time_lunch_in) as time_lunch_in,
				max(time_lunch_out) as time_lunch_out
			from
			(
				select
				distinct 
					rowno,
					nik,
					nama,
					clock_date,
					jammasuk,
					jamkeluar,
					idjam,
					time_in,
					time_out, --case when maxrowno = 2 then time_lunch_in else time_out end as time_out,
					time_lunch_in,
					time_lunch_out
				from
				(
					select 
						distinct 
						rowno,
						nik,
						nama,
						clock_date,
						jammasuk,
						jamkeluar,
						idjam,
						maxrowno,
						case when rowno = 1 then clock_time
						end as time_in,
						case when rowno = 2 then clock_time
						end as time_lunch_in,
						case when rowno = 3 then clock_time
						end as time_lunch_out,
						case when rowno = 4 then clock_time
						end as time_out
					from
						cte_result
				) a
			) b
			group by
				nik,
				nama,
				clock_date,
				jammasuk,
				jamkeluar,
				idjam
		) d
		left join 
			lt_jam c
			on c.idjam = d.idjam
		where d.clock_date between @periodeawal and @periodeakhir
		order by nik, clock_date	
	end
GO




