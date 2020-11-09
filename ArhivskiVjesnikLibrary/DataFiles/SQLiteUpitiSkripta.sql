SELECT DISTINCT Clanak.IDClanak, Clanak.* FROM Clanak
LEFT JOIN ClanakVrsta
ON Clanak.IDClanak = ClanakVrsta.ClanakID
LEFT JOIN Vrsta
ON ClanakVrsta.VrstaID = Vrsta.IDVrsta
LEFT JOIN ClanakNaslov
ON Clanak.IDClanak = ClanakNaslov.ClanakID
LEFT JOIN Naslov
ON ClanakNaslov.NaslovID = Naslov.IDNaslov
LEFT JOIN ClanakSazetak
ON Clanak.IDClanak = ClanakSazetak.ClanakID
LEFT JOIN Sazetak
ON ClanakSazetak.SazetakID = Sazetak.IDSazetak
LEFT JOIN ClanakKljucnaRijec
ON Clanak.IDClanak = ClanakKljucnaRijec.ClanakID
LEFT JOIN KljucnaRijec
ON ClanakKljucnaRijec.KljucnaRijecID = KljucnaRijec.IDKljucnaRijec
LEFT JOIN ClanakAutor
ON Clanak.IDClanak = ClanakAutor.ClanakID
LEFT JOIN Autor
ON ClanakAutor.AutorID = Autor.IDAutor
WHERE Clanak.Godiste >= 2003 OR Autor.Prezime = 'lajnert' OR Autor.Prezime = 'janjatović'


SELECT * FROM Autor
LIMIT 5 OFFSET 0

SELECT * FROM ClanakAutor
WHERE AutorID = 478