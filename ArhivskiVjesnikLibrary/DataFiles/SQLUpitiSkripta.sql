SELECT c.IDClanak, c.* FROM Clanak AS c
FULL JOIN ClanakAutor AS ca
ON ca.ClanakID = c.IDClanak
FULL JOIN Autor AS a
ON ca.AutorID = a.IDAutor
FULL JOIN ClanakKljucnaRijec AS ckr
ON c.IDClanak = ckr.ClanakID
FULL JOIN KljucnaRijec AS kr
ON ckr.KljucnaRijecID = kr.IDKljucnaRijec
FULL JOIN ClanakVrsta AS cv
ON cv.ClanakID = c.IDClanak
FULL JOIN Vrsta AS v
ON cv.VrstaID = v.IDVrsta
FULL JOIN ClanakNaslov AS cn
ON cn.ClanakID = c.IDClanak
FULL JOIN Naslov AS n
ON cn.NaslovID = n.IDNaslov
FULL JOIN ClanakSazetak AS cs
ON cs.ClanakID = c.IDClanak
FULL JOIN Sazetak AS s
ON cs.SazetakID = s.IDSazetak
WHERE c.Naziv LIKE '%arhiv%' AND a.Prezime = 'lajnert' AND kr.Vrijednost LIKE '%josip matasović%'
ORDER BY c.IDClanak ASC
OFFSET 2000 * (1 - 1) ROWS FETCH NEXT 200 ROWS ONLY;

SELECT * FROM KljucnaRijec
WHERE Vrijednost LIKE '%matasović%'

SELECT * FROM Podaci$
ORDER BY id_clanak


SELECT c.* FROM Clanak AS c
INNER JOIN ClanakAutor AS ca
ON ca.ClanakID = c.IDClanak
INNER JOIN Autor AS a
ON ca.AutorID = a.IDAutor
WHERE ca.ClanakID = 1