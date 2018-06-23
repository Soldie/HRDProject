 
 



 

IF EXISTS(SELECT * FROM sys.objects WHERE type = 'FN' AND name = 'terbilang_Eng')
DROP function terbilang_Eng
GO

 
 -- Convert numbers to text - Scalar-valued user-defined function - UDF
CREATE FUNCTION terbilang_Eng(@Money AS money, @nl int)
    RETURNS VARCHAR(1024)
AS
BEGIN
            DECLARE @Number as BIGINT
      SET @Number = FLOOR(@Money)
      DECLARE @Below20 TABLE (ID int identity(0,1), Word varchar(32))
      DECLARE @Below100 TABLE (ID int identity(2,1), Word varchar(32))

      INSERT @Below20 (Word) VALUES
                        ( 'Zero'), ('One'),( 'Two' ), ( 'Three'),
                        ( 'Four' ), ( 'Five' ), ( 'Six' ), ( 'Seven' ),
                        ( 'Eight'), ( 'Nine'), ( 'Ten'), ( 'Eleven' ),
                        ( 'Twelve' ), ( 'Thirteen' ), ( 'Fourteen'),
                        ( 'Fifteen' ), ('Sixteen' ), ( 'Seventeen'),
                        ('Eighteen' ), ( 'Nineteen' )
       INSERT @Below100 VALUES ('Twenty'), ('Thirty'),('Forty'), ('Fifty'),
                               ('Sixty'), ('Seventy'), ('Eighty'), ('Ninety')

DECLARE @English varchar(1024) =
(
  SELECT Case
    WHEN @Number = 0 THEN  ''
    WHEN @Number BETWEEN 1 AND 19
      THEN (SELECT Word FROM @Below20 WHERE ID=@Number)
    WHEN @Number BETWEEN 20 AND 99
    -- SQL Server recursive function  
     THEN  (SELECT Word FROM @Below100 WHERE ID=@Number/10)+ '-' +
           dbo.terbilang_Eng( @Number % 10, @nl)
       WHEN @Number BETWEEN 100 AND 999 
     THEN  (dbo.terbilang_Eng( @Number / 100, @nl))+' Hundred '+
         dbo.terbilang_Eng( @Number % 100, @nl)
       WHEN @Number BETWEEN 1000 AND 999999 
     THEN  (dbo.terbilang_Eng( @Number / 1000, @nl))+' Thousand '+
         dbo.terbilang_Eng( @Number % 1000, @nl)
    WHEN @Number BETWEEN 1000000 AND 999999999 
     THEN  (dbo.terbilang_Eng( @Number / 1000000, @nl))+' Million '+
         dbo.terbilang_Eng( @Number % 1000000, @nl)
       ELSE ' INVALID INPUT' END
)
if (@Money - @Number)=''
begin
    select @English = RTRIM(@English)
      select @English = RTRIM(LEFT(@English,len(@English)-1)) 
       where RIGHT(@English,1)='-'
end
else if (@@NestLevel - @nl) = 1
begin
    select @English = @English+' Dollars and '
    select @English = @English+
    convert(varchar,convert(int,100*(@Money - @Number))) +' Cents' 
end
RETURN (@English)
END
GO




IF EXISTS(SELECT * FROM sys.objects WHERE type = 'FN' AND name = 'terbilang')
DROP function terbilang
GO


CREATE FUNCTION [dbo].[Terbilang](@number NUMERIC(19,6))
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE 
    @position INT, 
    @length INT, 
    @words NVARCHAR(MAX), 
    @ends NVARCHAR(MAX), 
    @numStr NVARCHAR(MAX), 
    @foreStr NVARCHAR(MAX), 
    @backStr NVARCHAR(MAX), 
    @char NVARCHAR(1),
    @charafter NVARCHAR(1),
    @charprev NVARCHAR(1),
    @charprev2 NVARCHAR(1)

SELECT @numStr = STR(@number, 19, 2)
SELECT @numStr = LTRIM(RTRIM(@numStr))
SELECT @foreStr = SUBSTRING(@numStr, 0, (SELECT CHARINDEX('.', @numStr, 1)))
SELECT @backStr = SUBSTRING(@numStr, (SELECT CHARINDEX('.', @numStr, 1)+1), LEN(@numStr))
SELECT @length = LEN(@foreStr)
SELECT @position = @length
SELECT @words =''
    --Memproses "angka di depan koma" 
    WHILE(@position > 0)
    BEGIN    
            SELECT @char = SUBSTRING(@numStr, @length+1 - @position, 1)
            SELECT @charafter = SUBSTRING(@numStr, @length+2 - @position, 1)
            SELECT @charprev = SUBSTRING(@numStr, @length - @position, 1)
            SELECT @charprev2 = SUBSTRING(@numStr, @length - @position - 1, 1)

            IF ((@char = '1') AND ((SELECT(@position-1)/3.0) = 1) AND 
                (@charafter != '' ) AND ((SELECT CAST(@charprev as INT)) = 0)) 
                SELECT @words = @words + 'se' 
            ELSE
            IF ((@char = '1') AND ((SELECT @position % 3) = 1)) 
                SELECT @words = @words + 'satu ' 
            ELSE
            IF ((@char = '1') AND ((SELECT CAST(@charafter as INT)) > 1) AND ((SELECT @position % 3) = 2))
                BEGIN
                    IF (@charafter = '1') SELECT @words = @words + 'se' ELSE
                    IF (@charafter = '2') SELECT @words = @words + 'dua ' ELSE
                    IF (@charafter = '3') SELECT @words = @words + 'tiga ' ELSE    
                    IF (@charafter = '4') SELECT @words = @words + 'empat ' ELSE    
                    IF (@charafter = '5') SELECT @words = @words + 'lima ' ELSE    
                    IF (@charafter = '6') SELECT @words = @words + 'enam ' ELSE    
                    IF (@charafter = '7') SELECT @words = @words + 'tujuh ' ELSE    
                    IF (@charafter = '8') SELECT @words = @words + 'delapan ' ELSE    
                    IF (@charafter = '9') SELECT @words = @words + 'sembilan '
                END
            ELSE
            IF (@char = '1') SELECT @words = @words + 'se' ELSE
            IF (@char = '2') SELECT @words = @words + 'dua ' ELSE
            IF (@char = '3') SELECT @words = @words + 'tiga ' ELSE    
            IF (@char = '4') SELECT @words = @words + 'empat ' ELSE    
            IF (@char = '5') SELECT @words = @words + 'lima ' ELSE    
            IF (@char = '6') SELECT @words = @words + 'enam ' ELSE    
            IF (@char = '7') SELECT @words = @words + 'tujuh ' ELSE    
            IF (@char = '8') SELECT @words = @words + 'delapan ' ELSE    
            IF (@char = '9') SELECT @words = @words + 'sembilan ' ELSE
            IF ((@char = '0') AND ((SELECT CAST(@charprev as INT)) > 1) AND ((SELECT @position % 3) = 1))
                SELECT @words = @words 
            ELSE
            IF ((@char = '0') AND ((SELECT @charprev) = '0') AND ((SELECT CAST(@charprev2 as INT)) > 0) AND ((SELECT @position % 3) = 1))
                SELECT @words = @words 
            ELSE
            IF (@char = '0') 
            BEGIN
                SELECT @position = @position - 1
                CONTINUE
            END     

            IF ((SELECT @position % 3) = 0) SELECT @words = @words + 'ratus ' ELSE    
            IF (((SELECT @position % 3) = 2) AND ((SELECT CAST(@char as INT)) > 1)) 
                SELECT @words = @words + 'puluh ' 
            ELSE    
            IF (((SELECT @position % 3) = 2) AND ((SELECT CAST(@char as INT)) = 1)
                AND ((SELECT CAST(@charafter as INT)) > 0))
            BEGIN 
                SELECT @words = @words + 'belas '
                SELECT @position = @position - 1
            END
            ELSE
            IF (((SELECT @position % 3) = 2) AND ((SELECT CAST(@char as INT)) = 1)
                AND ((SELECT CAST(@charafter as INT)) = 0))
            BEGIN 
                SELECT @words = @words + 'puluh '
                SELECT @position = @position - 1
            END
            
            IF ((SELECT (@position-1)/3.0) = 1) SELECT @words = @words +'ribu ' ELSE
            IF ((SELECT (@position-1)/3.0) = 2) SELECT @words = @words +'juta ' ELSE
            IF ((SELECT (@position-1)/3.0) = 3) SELECT @words = @words +'milyar ' ELSE
            IF ((SELECT (@position-1)/3.0) = 4) SELECT @words = @words +'triliun '
 
        SELECT @position = @position - 1
    END
    --Memproses "koma" dan "angka di belakang koma"
    IF((SELECT CAST(@backStr AS INT)) > 0)
    BEGIN
        --Menambahkan "koma" pada terbilang
        SELECT @words = @words + 'koma '
        --Menambahkan "Angka di belakang koma" pada terbilang
        SELECT @length = LEN(@backStr)
        SELECT @position = @length
        
        WHILE( @position > 0)
        BEGIN
            SELECT @char = SUBSTRING(@backStr, @length+1 - @position, 1)
            SELECT @words = @words +
            (CASE @char
                WHEN '0'THEN 'nol '
                WHEN '1'THEN 'satu '
                WHEN '2'THEN 'dua '
                WHEN '3'THEN 'tiga '
                WHEN '4'THEN 'empat '
                WHEN '5'THEN 'lima '
                WHEN '6'THEN 'enam '
                WHEN '7'THEN 'tujuh '
                WHEN '8'THEN 'delapan '
                WHEN '9'THEN 'sembilan '
                ELSE ''
             END
            )    
            SELECT @position = @position - 1
        END 
    END

    SELECT @words = LTRIM(RTRIM(@words))
    -- Huruf pertama huruf besar
    IF LEN(@words) > 0 
    BEGIN
        SET @words = UPPER(left(@words,1)) + RIGHT(@words, LEN(@words)-1)
    END
    /* FINAL RETURN */
	
	RETURN (SELECT @words)
END





--CREATE FUNCTION fnMoneyToEnglishNL(@Money AS money, @nl int)
--    RETURNS VARCHAR(1024)
--AS
--BEGIN
--      DECLARE @Number as BIGINT
--      SET @Number = FLOOR(@Money)
--      DECLARE @Below20 TABLE (ID int identity(0,1), Word varchar(32))
--      DECLARE @Below100 TABLE (ID int identity(2,1), Word varchar(32))

--      INSERT @Below20 (Word) VALUES
--                        ( 'Zero'), ('One'),( 'Two' ), ( 'Three'),
--                        ( 'Four' ), ( 'Five' ), ( 'Six' ), ( 'Seven' ),
--                        ( 'Eight'), ( 'Nine'), ( 'Ten'), ( 'Eleven' ),
--                        ( 'Twelve' ), ( 'Thirteen' ), ( 'Fourteen'),
--                        ( 'Fifteen' ), ('Sixteen' ), ( 'Seventeen'),
--                        ('Eighteen' ), ( 'Nineteen' )
--       INSERT @Below100 VALUES ('Twenty'), ('Thirty'),('Forty'), ('Fifty'),
--                               ('Sixty'), ('Seventy'), ('Eighty'), ('Ninety')

--DECLARE @English varchar(1024) =
--(
--  SELECT Case
--    WHEN @Number = 0 THEN  ''
--    WHEN @Number BETWEEN 1 AND 19
--      THEN (SELECT Word FROM @Below20 WHERE ID=@Number)
--   WHEN @Number BETWEEN 20 AND 99
---- SQL Server recursive function   
--     THEN  (SELECT Word FROM @Below100 WHERE ID=@Number/10)+ '-' +
--           dbo.fnMoneyToEnglishNL( @Number % 10, @nl)
--   WHEN @Number BETWEEN 100 AND 999  
--     THEN  (dbo.fnMoneyToEnglishNL( @Number / 100, @nl))+' Hundred '+
--         dbo.fnMoneyToEnglishNL( @Number % 100, @nl)
--   WHEN @Number BETWEEN 1000 AND 999999  
--     THEN  (dbo.fnMoneyToEnglishNL( @Number / 1000, @nl))+' Thousand '+
--         dbo.fnMoneyToEnglishNL( @Number % 1000, @nl) 
--   WHEN @Number BETWEEN 1000000 AND 999999999  
--     THEN  (dbo.fnMoneyToEnglishNL( @Number / 1000000, @nl))+' Million '+
--         dbo.fnMoneyToEnglishNL( @Number % 1000000, @nl)
--   ELSE ' INVALID INPUT' END
--)
--SELECT @English = RTRIM(@English)
--SELECT @English = RTRIM(LEFT(@English,len(@English)-1))
--                 WHERE RIGHT(@English,1)='-'
--IF (@@NestLevel - @nl) = 1
--BEGIN
--      SELECT @English = @English+' Dollars and '
--      SELECT @English = @English+
--      convert(varchar,convert(int,100*(@Money - @Number))) +' Cents'
--END
--RETURN (@English)
--END
--GO



-- create function dbo.terbilang (@nilai bigint)
--RETURNS varchar(500)
--AS
--BEGIN
--DECLARE @hasil varchar(300),@x bigint
--DECLARE @angka TABLE (
--kode int, ket varchar(20))
--INSERT INTO @angka VALUES(0,'')
--INSERT INTO @angka VALUES(1,'Satu')
--INSERT INTO @angka VALUES(2,'Dua')
--INSERT INTO @angka VALUES(3,'Tiga')
--INSERT INTO @angka VALUES(4,'Empat')
--INSERT INTO @angka VALUES(5,'Lima')
--INSERT INTO @angka VALUES(6,'Enam')
--INSERT INTO @angka VALUES(7,'Tujuh')
--INSERT INTO @angka VALUES(8,'Delapan')
--INSERT INTO @angka VALUES(9,'Sembilan')
--INSERT INTO @angka VALUES(10,'Sepuluh')
--INSERT INTO @angka VALUES(11,'Sebelas')
--IF @nilai <12
--SET @hasil = (SELECT ket FROM @angka WHERE kode=@nilai)
--ELSE IF @nilai <20
--BEGIN
--SET @x = @nilai - 10
--SET @hasil = (SELECT ket FROM @angka WHERE kode=@x) + ' Belas'
--END
--ELSE IF @nilai <100
--BEGIN
--SET @x = @nilai / 10
--SET @hasil = (SELECT ket FROM @angka WHERE kode=@x) + ' Puluh '
--SET @x = @nilai % 10
--SET @hasil = @hasil + (SELECT ket FROM @angka WHERE kode=@x)
--END
--ELSE IF @nilai <200
--BEGIN
--SET @x = @nilai - 100
--SET @hasil = 'Seratus ' + dbo.terbilang(@x)
--END
--ELSE IF @nilai <1000
--BEGIN
--SET @x = @nilai / 100
--SET @hasil = dbo.terbilang(@x) + ' Ratus '
--SET @x = @nilai % 100
--SET @hasil = @hasil + dbo.terbilang(@x)
--END
--ELSE IF @nilai <2000
--BEGIN
--SET @x = @nilai - 1000
--SET @hasil = 'Seribu ' + dbo.terbilang(@x)
--END
--ELSE IF @nilai <1000000
--BEGIN
--SET @x = @nilai / 1000
--SET @hasil = dbo.terbilang(@x) + ' Ribu '
--SET @x = @nilai % 1000
--SET @hasil = @hasil + dbo.terbilang(@x)
--END
--ELSE IF @nilai <1000000000
--BEGIN
--SET @x = @nilai / 1000000
--SET @hasil = dbo.terbilang(@x) + ' Juta '
--SET @x = @nilai % 1000000
--SET @hasil = @hasil + dbo.terbilang(@x)
--END
--ELSE IF @nilai <1000000000000
--BEGIN
--SET @x = @nilai / 1000000000
--SET @hasil = dbo.terbilang(@x) + ' Milyar '
--SET @x = @nilai % 1000000000
--SET @hasil = @hasil + dbo.terbilang(@x)
--END
--ELSE IF @nilai <1000000000000000
--BEGIN
--SET @x = @nilai / 1000000000000
--SET @hasil = dbo.terbilang(@x) + ' Trilyun '
--SET @x = @nilai % 1000000000000
--SET @hasil = @hasil + dbo.terbilang(@x)
--END
--ELSE
--SET @hasil = 'N/A'
--RETURN @hasil
--END



--create function FN_SATUAN
--(
--	@ANGKA INT, 
--	@currency varchar(3)
--) 
--RETURNS VARCHAR(50)
--BEGIN
--	DECLARE 
--		@strtmp VARCHAR(50),
--		@satbesar VARCHAR(50), 
--		@strangka varchar(3), 
--		@lenangka int, 
--		@i int, 
--		@ch int
--		--, 
--		--@ANGKA INT,
--		--@currency varchar(3)
--	--baca di belakang koma

--	set @strangka = CONVERT( varchar(3), @ANGKA )
--	set @lenangka = len( @strangka)
--	set @strtmp =''
--	set @i=1
--	while ((@lenangka = 2 and @i=2) or (@lenangka = 2 and @i=1) ) and @ch > 0
--	begin
--		if UPPER(@currency) = 'USD' 
--			set @strtmp=@strtmp+'ty'
--		else 
--			set @strtmp=@strtmp+' puluh'
		
--		if @lenangka > 2 and @i=1
--		begin
--			if UPPER(@currency) = 'USD' 
--				set @strtmp=@strtmp+' hundred'
--			else 
--				set @strtmp=@strtmp+' ratus'
--		end

--		set @i=@i+1
--	end
--	--replace untu gantiin string output dengan yang baru(output yang benar)
--	set @strtmp = REPLACE(@strtmp, 'satu puluh','sepuluh')
--	set @strtmp = REPLACE(@strtmp, 'sepuluh satu','sebelas')
--	set @strtmp = REPLACE(@strtmp, 'sepuluh dua','dua belas')
--	set @strtmp = REPLACE(@strtmp, 'sepuluh tiga','tiga belas')
--	set @strtmp = REPLACE(@strtmp, 'sepuluh empat','empat belas')
--	set @strtmp = REPLACE(@strtmp, 'sepuluh lima','lima belas')
--	set @strtmp = REPLACE(@strtmp, 'sepuluh enam','enam belas')
--	set @strtmp = REPLACE(@strtmp, 'sepuluh tujuh','tujuh belas')
--	set @strtmp = REPLACE(@strtmp, 'sepuluh delapan','delapan belas')
--	set @strtmp = REPLACE(@strtmp, 'sepuluh sembilan','sembilan belas')

--	set @strtmp = REPLACE(@strtmp, 'satu ratus','seratus')
--	set @strtmp = REPLACE(@strtmp, 'onety', 'ten')
--	set @strtmp = REPLACE(@strtmp, 'twoty', 'twenty')
--	set @strtmp = REPLACE(@strtmp, 'ten one','eleven')
--	set @strtmp = REPLACE(@strtmp, 'ten two','twelve')
--	set @strtmp = REPLACE(@strtmp, 'ten three','thirdten')
--	set @strtmp = REPLACE(@strtmp, 'ten four','fourten')
--	set @strtmp = REPLACE(@strtmp, 'ten five','fiveten')
--	set @strtmp = REPLACE(@strtmp, 'ten six','sixten')
--	set @strtmp = REPLACE(@strtmp, 'ten seven','seventen')
--	set @strtmp = REPLACE(@strtmp, 'ten eight','eightten')
--	set @strtmp = REPLACE(@strtmp, 'ten nine','nineten')
--	set @strtmp = REPLACE(@strtmp, 'eightty', 'eighty')
	
--	RETURN @strtmp
--END

 

--Ngerti kan, ngerti ? Pokoknya lanjut dulu aja yaa !!!

--2. FN_TERBILANG

--Nah.. di function ini ni kita baru maen di ratusan, ribuan, jutaan, dan kawan-kawan.. Disini juga kita bermain dengan koma. Mau tau gimana caranya ? Liat ke bawah yuuk !!!

 

--CREATE FUNCTION [dbo].[CORE_FN_CORE_TERBILANG](
--@input varchar(1024),
--@currency varchar(1024)
--)
--RETURNS varchar(1024)
--BEGIN
--declare @angka bigint, @koma int

--declare @strAngka varchar(1024)
--declare @i int, @j int, @ch int, @lenAngka int
--declare @satbesar varchar(1024), @strtmp varchar(1024)
--declare @strBelakangKoma varchar(255), @belakangKoma int, @angkaKoma int, @jumlahNol int

--set @angka = convert(float,(@input))

--set @strAngka = convert(varchar(100),@input,0)
--set @koma = isnull(CHARINDEX('.',@strAngka),0)
--if @koma=null set @koma=0
--set @strBelakangKoma = RIGHT(@strAngka, LEN(@strAngka)-@koma)
--if @koma > 0
--set @strAngka = substring(@strAngka,1, @koma-1)

--set @lenAngka = LEN(@strAngka)
--set @i=0

----cek angka yang di input 0 atau null
--if @angka != 0 or @angka != null
--begin
----set @satbesar=convert(varchar(25),@angka,0)
--set @strtmp = ''

--while @i 3 and LEN(@strAngka) 0
--set @satbesar = @satbesar +' Ribu'
--set @strAngka = substring(@strAngka,LEN(@strAngka)-3+1, 3)
----set @strtmp=@strtmp+' ' +@satbesar
--end
--else if LEN(@strAngka) > 6 and LEN(@strAngka) 0 set @satbesar = @satbesar +' Juta'
--set @strAngka = substring(@strAngka,LEN(@strAngka)-6+1, 6)
----set @strtmp=@strtmp+' ' +@satbesar
--end
--else if LEN(@strAngka) > 9 and LEN(@strAngka) 0 set @satbesar = @satbesar +' Milyar'
--set @strAngka = substring(@strAngka,LEN(@strAngka)-9+1, 9)
----set @strtmp=@strtmp+' ' +@satbesar
--end
--else if LEN(@strAngka) > 12 and LEN(@strAngka) 0 set @satbesar = @satbesar +' Trilyun'
--set @strAngka = substring(@strAngka,LEN(@strAngka)-12+1, 12)
----set @strtmp=@strtmp+' ' +@satbesar
--end
--else if LEN(@strAngka) > 15 and LEN(@strAngka) 0 set @satbesar = @satbesar +' Bilyun'
--set @strAngka = substring(@strAngka,LEN(@strAngka)-15+1, 15)
----set @strtmp=@strtmp+' ' +@satbesar
--end
--else if LEN(@strAngka) 3 and LEN(@strAngka) 0
--set @satbesar = @satbesar +' Thousand'
--set @strAngka = substring(@strAngka,LEN(@strAngka)-3+1, 3)
----set @strtmp=@strtmp+' ' +@satbesar
--end
--else if LEN(@strAngka) > 6 and LEN(@strAngka) 0 set @satbesar = @satbesar +' Million'
--set @strAngka = substring(@strAngka,LEN(@strAngka)-6+1, 6)
----set @strtmp=@strtmp+' ' +@satbesar
--end
--else if LEN(@strAngka) > 9 and LEN(@strAngka) 0 set @satbesar = @satbesar +' Billion'
--set @strAngka = substring(@strAngka,LEN(@strAngka)-9+1, 9)
----set @strtmp=@strtmp+' ' +@satbesar
--end
--else if LEN(@strAngka) > 12 and LEN(@strAngka) 0 set @satbesar = @satbesar +' Zillion'
--set @strAngka = substring(@strAngka,LEN(@strAngka)-12+1, 12)
----set @strtmp=@strtmp+' ' +@satbesar
--end
--else if LEN(@strAngka) > 15 and LEN(@strAngka) 0 set @satbesar = @satbesar +' Billion'
--set @strAngka = substring(@strAngka,LEN(@strAngka)-15+1, 15)
----set @strtmp=@strtmp+' ' +@satbesar
--end
--else if LEN(@strAngka)<= 3
--begin
--set @satbesar = dbo.CORE_FN_SATUAN(substring(@strAngka,1, LEN(@strAngka)+3),@currency)
--set @strAngka = substring(@strAngka,LEN(@strAngka)+3-1, 3)
--end
--end

--set @strtmp=@strtmp+' ' +@satbesar
--set @i=@i+1
--end
--end

----baca di belakang koma
--if ((@angka != 0 or @angka != null) and @input like '%.%' )
--begin
--set @lenAngka = 0
--set @lenAngka = LEN(@strBelakangKoma)
--set @angkaKoma = ''

----baca banyaknya angka nol di belakang koma — jika diawali dg koma 0
--if (substring(convert(varchar(100),@input,0),@koma+1,1) = '0')
--begin
--set @jumlahNol = 1
--set @i = 0

--while @i @jumlahNol) or --jika jumlah koma(@lenAngka) lebih besar dari jumlah 0
--((substring(convert(varchar(100),@input,0),@koma+1,1) != '0'))) --jika koma tidak di awali dengan angka 0
--begin
--if UPPER(@currency) = 'USD'
--set @strtmp=@strtmp+' Point'
--else
--set @strtmp=@strtmp+' Koma'
--set @i=0
--while @i<@lenAngka --+1
--begin
--set @ch = convert(int,substring(@strBelakangKoma,@i+1,1))
--if UPPER(@currency) = 'IDR'
--begin
--select @satbesar =
--case @ch
--when 0 then ' Nol'
--when 1 then ' Satu'
--when 2 then ' Dua'
--when 3 then ' Tiga'
--when 4 then ' Empat'
--when 5 then ' Lima'
--when 6 then ' Enam'
--when 7 then ' Tujuh'
--when 8 then ' Delapan'
--when 9 then ' Sembilan'
--end
--end

--if UPPER(@currency) = 'USD'
--begin
--select @satbesar =
--case @ch
--when 0 then ' Zero'
--when 1 then ' One'
--when 2 then ' Two'
--when 3 then ' Three'
--when 4 then ' Four'
--when 5 then ' Five'
--when 6 then ' Six'
--when 7 then ' Seven'
--when 8 then ' Eight'
--when 9 then ' Nine'
--end
--end
--set @strtmp=@strtmp+@satbesar
--set @i=@i+1
--end
--end
--end

--set @strtmp = REPLACE(@strtmp, ' ', ' ')
--set @strtmp = REPLACE(@strtmp, 'Satu Ribu','Seribu')

--if UPPER(@currency)='IDR'
--begin
--set @strtmp = @strtmp +' Rupiah '
--end

--if UPPER(@currency)='USD'
--begin
--set @strtmp = @strtmp +' Dollar '
--end

--set @strtmp = REPLACE(@strtmp, ' ',' ')
--set @strtmp = REPLACE(@strtmp, ' ',' ')

--return @strtmp
--END

--GO




--IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND name = 'IsKaryawanExists')
--DROP PROCEDURE IsKaryawanExists
--GO

--/*
--	Created By 	:	Chandra Arifin
--	Date		:	
--	Stored Proc	:	IsKaryawanExists
--	Description	:	
--*/


--CREATE FUNCTION terbilang(@the_amount money)
--RETURNS varchar(250) AS
--BEGIN

--DECLARE
--    @divisor    bigint,
--    @large_amount    money,
--    @tiny_amount    money,
--    @the_word    varchar(250),
--    @dividen    money,
--    @dummy        money,
--    @weight    varchar(100),
--    @unit        varchar(30),
--    @follower    varchar(50),
--    @prefix    varchar(10),
--    @sufix        varchar(10)

----SET NOCOUNT ON
--SET @the_word = ''
--SET @large_amount = FLOOR(ABS(@the_amount) )
--SET @tiny_amount = ROUND((ABS(@the_amount) - @large_amount ) * 100.00,0)
--SET @divisor = 1000000000000.00

--IF @large_amount > @divisor * 1000.00
--    RETURN 'OUT OF RANGE'
   
--WHILE @divisor >= 1
--BEGIN       
--    SET @dividen = FLOOR(@large_amount / @divisor)
--    SET @large_amount = CONVERT(bigint,@large_amount) % @divisor
   
--    SET @unit = ''
--    IF @dividen > 0.00
--        SET @unit=(CASE @divisor
--            WHEN 1000000000000.00 THEN 'TRILYUN '
--            WHEN 1000000000.00 THEN 'MILYAR '           
--            WHEN 1000000.00 THEN 'JUTA '               
--            WHEN 1000.00 THEN 'RIBU '
--            ELSE @unit
--        END )

--    SET @weight = ''   
--    SET @dummy = @dividen
--    IF @dummy >= 100.00
--        SET @weight = (CASE FLOOR(@dummy / 100.00)
--            WHEN 1 THEN 'SE'
--            WHEN 2 THEN 'DUA '
--            WHEN 3 THEN 'TIGA '
--            WHEN 4 THEN 'EMPAT '
--            WHEN 5 THEN 'LIMA '
--            WHEN 6 THEN 'ENAM '
--            WHEN 7 THEN 'TUJUH '
--            WHEN 8 THEN 'DELAPAN '
--            ELSE 'SEMBILAN ' END ) + 'RATUS '

--    SET @dummy = CONVERT(bigint,@dividen) % 100

--    IF @dummy < 10.00
--    BEGIN
--        IF @dummy = 1.00 AND @unit = 'RIBU'
--        BEGIN
--            IF @dividen=@dummy
--                SET @weight = @weight + 'SE'
--            ELSE
--                SET @weight = @weight + 'SATU '
--        END
--        ELSE
--        IF @dummy > 0.00
--            SET @weight = @weight + (CASE @dummy
--                WHEN 1 THEN 'SATU '
--                WHEN 2 THEN 'DUA '
--                WHEN 3 THEN 'TIGA '
--                WHEN 4 THEN 'EMPAT '
--                WHEN 5 THEN 'LIMA '
--                WHEN 6 THEN 'ENAM '
--                WHEN 7 THEN 'TUJUH '
--                WHEN 8 THEN 'DELAPAN '
--                ELSE 'SEMBILAN ' END)
--    END
--    ELSE
--    IF @dummy BETWEEN 11 AND 19
--        SET @weight = @weight + (CASE CONVERT(bigint,@dummy) % 10
--            WHEN 1 THEN 'SE'
--            WHEN 2 THEN 'DUA '
--            WHEN 3 THEN 'TIGA '
--            WHEN 4 THEN 'EMPAT '
--            WHEN 5 THEN 'LIMA '
--            WHEN 6 THEN 'ENAM '
--            WHEN 7 THEN 'TUJUH '
--            WHEN 8 THEN 'DELAPAN '
--            ELSE 'SEMBILAN ' END ) + 'BELAS '
--    ELSE
--    BEGIN
--        SET @weight = @weight + (CASE FLOOR(@dummy / 10)
--            WHEN 1 THEN 'SE'
--            WHEN 2 THEN 'DUA '
--            WHEN 3 THEN 'TIGA '
--            WHEN 4 THEN 'EMPAT '
--            WHEN 5 THEN 'LIMA '
--            WHEN 6 THEN 'ENAM '
--            WHEN 7 THEN 'TUJUH '
--            WHEN 8 THEN 'DELAPAN '
--            ELSE 'SEMBILAN ' END ) + 'PULUH '
--        IF CONVERT(bigint,@dummy) % 10 > 0
--            SET @weight = @weight + (CASE CONVERT(bigint,@dummy) % 10
--                WHEN 1 THEN 'SATU '
--                WHEN 2 THEN 'DUA '
--                WHEN 3 THEN 'TIGA '
--                WHEN 4 THEN 'EMPAT '
--                WHEN 5 THEN 'LIMA '
--                WHEN 6 THEN 'ENAM '
--                WHEN 7 THEN 'TUJUH '
--                WHEN 8 THEN 'DELAPAN '
--                ELSE 'SEMBILAN ' END )
--    END
   
--    SET @the_word = @the_word + @weight + @unit
--    SET @divisor = @divisor / 1000.00
--END

--IF FLOOR(@the_amount) = 0.00
--    SET @the_word = 'NOL '

--SET @follower = ''
--IF @tiny_amount < 10.00
--BEGIN   
--    IF @tiny_amount > 0.00
--        SET @follower = 'KOMA NOL ' + (CASE @tiny_amount
--            WHEN 1 THEN 'SATU '
--            WHEN 2 THEN 'DUA '
--            WHEN 3 THEN 'TIGA '
--            WHEN 4 THEN 'EMPAT '
--            WHEN 5 THEN 'LIMA '
--            WHEN 6 THEN 'ENAM '
--            WHEN 7 THEN 'TUJUH '
--            WHEN 8 THEN 'DELAPAN '
--            ELSE 'SEMBILAN ' END)
--END
--ELSE
--BEGIN
--    SET @follower = 'KOMA ' + (CASE FLOOR(@tiny_amount / 10.00)
--            WHEN 1 THEN 'SATU '
--            WHEN 2 THEN 'DUA '
--            WHEN 3 THEN 'TIGA '
--            WHEN 4 THEN 'EMPAT '
--            WHEN 5 THEN 'LIMA '
--            WHEN 6 THEN 'ENAM '
--            WHEN 7 THEN 'TUJUH '
--            WHEN 8 THEN 'DELAPAN '
--            ELSE 'SEMBILAN ' END)
--    IF CONVERT(bigint,@tiny_amount) % 10 > 0
--        SET @follower = @follower + (CASE CONVERT(bigint,@tiny_amount) % 10
--            WHEN 1 THEN 'SATU '
--            WHEN 2 THEN 'DUA '
--            WHEN 3 THEN 'TIGA '
--            WHEN 4 THEN 'EMPAT '
--            WHEN 5 THEN 'LIMA '
--            WHEN 6 THEN 'ENAM '
--            WHEN 7 THEN 'TUJUH '
--            WHEN 8 THEN 'DELAPAN '
--            ELSE 'SEMBILAN ' END)
--END
   
--SET @the_word = @the_word + @follower

--IF @the_amount < 0.00
--    SET @the_word = 'MINUS ' + @the_word
   
--RETURN @the_word
--END




