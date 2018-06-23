

rpt_rekapabsensi '2013-11-24', '2013-11-30'
rpt_ambiljamabsensi2 '2013-12-29', '2014-01-03'






/*

--insert into trx_absensi(nik, clock_date, clock_time)
--select userid, "DATE", "time" from a 

--selincah
--insert into trx_absensi(nik, clock_date, clock_time) 
--select nik, cast(tgl as DATE), cast(jam as time) from a 


--insert into ms_karyawan(nik, nama, jammasuk, jamkeluar)
--select distinct userid, username, '08:00', '17:00' from a where UserID is not null



declare @a int 

select 
a.nik,
b.dated,
min(a.clock_time),
max(a.clock_time)
 from 
trx_absensi a
cross apply
(
select DATEADD(day, datediff(day, 0, clock_date), 0) as dated
) as b
group by 
a.nik, b.dated
order by a.nik

*/











/*

insert into trx_absensi(nik, clock_date, clock_time)
select userid, "DATE", "time" from a


--insert into ms_karyawan(nik, jammasuk, jamkeluar)
--select distinct nik, '08:00', '17:00' from trx_absensi



WITH Dates AS (
        SELECT
         [Date] = CONVERT(date,DATEADD(dd,-(DAY(GETDATE())-1), GETDATE()),101)
        UNION ALL SELECT
         [Date] = DATEADD(DAY, 1, [Date])
        FROM
         Dates
        WHERE
         Date < CONVERT(date,DATEADD(dd,-(DAY(DATEADD(mm, 1, GETDATE()))),DATEADD(mm,1, GETDATE())),101)--'01/31/2014'
) 
SELECT
 [Date]
FROM
 Dates
 OPTION (MAXRECURSION 0)
 
 
 
 

declare @a int 

select 
a.nik,
b.dated,
min(a.clock_time),
max(a.clock_time)
 from 
trx_absensi a
cross apply
(
select DATEADD(day, datediff(day, 0, clock_date), 0) as dated
) as b
group by 
a.nik, b.dated
order by a.nik

*/






