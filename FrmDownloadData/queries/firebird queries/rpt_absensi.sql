



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
	@kdwarehouse varchar(3) = null
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

	select @cte =
	'
	with 
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
			nama is not null
	) ';


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

	exec (@query)

	select 
		a.*, c.uangmakan as "Uang Makan", coalesce(b.offhari, -1) as offhari
	from 
		tmp_cek_harian as a
	left join
		ms_karyawan as c
		on a.nik = c.nik and c.stsrc <> 'D'
	left join
		ms_offkerja as b
		on a.nik = b.nik

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
	@kdwarehouse varchar(3) = null
WITH ENCRYPTION
as
	set nocount on;
	
	with cte as
	(
		select 
			distinct a.nik, b.nama, b.idjam, a.clock_date,
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
			b.kdwarehouse = coalesce(@kdwarehouse, b.kdwarehouse)
	)

	select 
		nik,
		nama,
		clock_date,
		time_in,
		time_out,
		c.jammasuk,
		c.jamkeluar,
		c.toleransimasuk,
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
	where b.clock_date between @periodeawal and @periodeakhir
	order by nik, clock_date

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
--	@nik int,
	@periodeawal date,
	@periodeakhir date,
	@kdcabang varchar(5) = null,
	@kdwarehouse varchar(3) = null
WITH ENCRYPTION
as
	set nocount on;
	
	with cte as
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
					b.kdwarehouse = coalesce(@kdwarehouse, b.kdwarehouse)
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
GO




