USE ArhivskiVjesnik

GO

ALTER TABLE Podaci$
ADD id_clanak INT PRIMARY KEY IDENTITY NOT NULL

GO

INSERT INTO Clanak(Naziv, Godiste, Volumen, Broj, DatumIzdavanja, DatumObjave, URL)
SELECT naziv_hr AS Naziv, godiste AS Godiste, volumen AS Volumen, broj AS Broj, datum_izdavanja AS DatumIzdavanja,
		objava_hrcak AS DatumObjave, URL AS URL 
FROM Podaci$

INSERT INTO Vrsta(Naziv)
SELECT DISTINCT value Naziv FROM Podaci$
CROSS APPLY string_split(REPLACE(vrsta, ', ', ','), ',')

INSERT INTO Naslov(Naziv)
SELECT DISTINCT value AS Naziv FROM Podaci$
CROSS APPLY string_split(REPLACE(naslovi, '; ', ';'), ';')

INSERT INTO Sazetak(Opis)
SELECT DISTINCT value AS Opis FROM Podaci$
CROSS APPLY string_split(REPLACE(sazeci, '; ', ';'), ';')

INSERT INTO Autor(Ime, Prezime)
SELECT DISTINCT
		REPLACE(SUBSTRING(value, CHARINDEX(',',value), LEN(value)), ', ', '') AS Ime,
		REPLACE(SUBSTRING(value, 1, CHARINDEX(',',value)), ',', '') AS Prezime
FROM Podaci$
CROSS APPLY string_split(REPLACE(autori, '; ', ';'), ';')

INSERT INTO KljucnaRijec(Vrijednost)
SELECT DISTINCT value Vrijednost FROM Podaci$
CROSS APPLY string_split(REPLACE(kljucne, '; ', ';'), ';')

DECLARE @Counter INT, @PodaciCount INT
SET @Counter = 1
SELECT @PodaciCount = COUNT(*) FROM Podaci$

WHILE @Counter <= @PodaciCount
BEGIN
	INSERT INTO ClanakVrsta(ClanakID, VrstaID)
	SELECT @Counter AS ClanakID, v.IDVrsta  AS VrstaID FROM Podaci$ AS p
	OUTER APPLY string_split(REPLACE(p.vrsta, ', ', ','),',')
	INNER JOIN Vrsta AS v
	ON v.Naziv = value
	WHERE p.id_clanak = @Counter
	GROUP BY v.IDVrsta

	INSERT INTO ClanakNaslov(ClanakID, NaslovID)
	SELECT @Counter AS ClanakID, n.IDNaslov AS NaslovID FROM Podaci$ AS p
	OUTER APPLY string_split(REPLACE(p.naslovi, '; ', ';'), ';')
	INNER JOIN Naslov AS n
	ON n.Naziv = value
	WHERE p.id_clanak = @Counter
	GROUP BY n.IDNaslov

	INSERT INTO ClanakSazetak(ClanakID, SazetakID)
	SELECT @Counter AS ClanakID, s.IDSazetak AS SazetakID FROM Podaci$ p
	OUTER APPLY string_split(REPLACE(p.sazeci, '; ', ';'), ';')
	INNER JOIN Sazetak AS s
	ON s.Opis = value
	WHERE p.id_clanak = @Counter
	GROUP BY s.IDSazetak

	INSERT INTO ClanakAutor(ClanakID, AutorID)
	SELECT @Counter AS ClanakID, a.IDAutor AS AutorID FROM Podaci$ AS p
	OUTER APPLY string_split(REPLACE(p.autori, '; ', ';'), ';')
	INNER JOIN Autor AS a
	ON  a.Ime = REPLACE(SUBSTRING(value, CHARINDEX(',',value), LEN(value)), ', ', '')
	AND a.Prezime = REPLACE(SUBSTRING(value, 1, CHARINDEX(',',value)), ',', '')
	WHERE p.id_clanak = @Counter
	GROUP BY a.IDAutor

	INSERT INTO ClanakKljucnaRijec(ClanakID, KljucnaRijecID)
	SELECT @Counter AS ClanakID, kr.IDKljucnaRijec AS KljucnaRijecID FROM Podaci$ AS p
	OUTER APPLY string_split(REPLACE(p.kljucne, '; ', ';'), ';')
	INNER JOIN KljucnaRijec AS kr
	ON kr.Vrijednost = value
	WHERE p.id_clanak = @Counter
	GROUP BY kr.IDKljucnaRijec

	SET @Counter +=1		
END