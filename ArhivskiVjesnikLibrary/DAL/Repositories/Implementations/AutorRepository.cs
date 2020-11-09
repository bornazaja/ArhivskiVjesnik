using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Extensions;
using ArhivskiVjesnikLibrary.DAL.Models;
using ArhivskiVjesnikLibrary.DAL.Repositories.Interfaces;
using System.Data;
using System.Data.SQLite;

namespace ArhivskiVjesnikLibrary.DAL.Repositories.Implementations
{
    public class AutorRepository : GenericRepository<Autor>, IAutorRepository
    {
        public PagedList<Autor> GetAllByClanakID(int clanakId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT Autor.* FROM Autor
                                INNER JOIN ClanakAutor
                                ON Autor.IDAutor = ClanakAutor.AutorID
                                WHERE ClanakAutor.ClanakID = @ClanakID";

                var parameters = new { ClanakID = clanakId };
                return conn.QueryWithPagination<Autor>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<Autor> GetAllByKljucnaRijecID(int kljucnaRijecId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT Autor.IDAutor, Autor.* FROM Autor
                                INNER JOIN ClanakAutor
                                ON Autor.IDAutor = ClanakAutor.AutorID
                                INNER JOIN ClanakKljucnaRijec
                                ON ClanakAutor.ClanakID = ClanakKljucnaRijec.ClanakID
                                WHERE ClanakKljucnaRijec.KljucnaRijecID = @KljucnaRijecID";

                var parameters = new { KljucnaRijecID = kljucnaRijecId };
                return conn.QueryWithPagination<Autor>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<Autor> GetAllByNaslovID(int naslovId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT Autor.IDAutor, Autor.* FROM Autor
                                INNER JOIN ClanakAutor
                                ON Autor.IDAutor = ClanakAutor.AutorID
                                INNER JOIN ClanakNaslov
                                ON ClanakAutor.ClanakID = ClanakNaslov.ClanakID
                                WHERE ClanakNaslov.NaslovID = @NaslovID";

                var parameters = new { NaslovID = naslovId };
                return conn.QueryWithPagination<Autor>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<Autor> GetAllBySazetakID(int sazetakId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT Autor.IDAutor, Autor.* FROM Autor
                                INNER JOIN ClanakAutor
                                ON Autor.IDAutor = ClanakAutor.AutorID
                                INNER JOIN ClanakSazetak
                                ON ClanakAutor.ClanakID = ClanakSazetak.ClanakID
                                WHERE ClanakSazetak.SazetakID = @SazetakID";

                var parameters = new { SazetakID = sazetakId };
                return conn.QueryWithPagination<Autor>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<Autor> GetAllByVrstaID(int vrstaId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT Autor.IDAutor, Autor.* FROM Autor
                                INNER JOIN ClanakAutor
                                ON Autor.IDAutor = ClanakAutor.AutorID
                                INNER JOIN ClanakVrsta
                                ON ClanakAutor.ClanakID = ClanakVrsta.ClanakID
                                WHERE ClanakVrsta.VrstaID = @VrstaID";

                var parameters = new { VrstaID = vrstaId };
                return conn.QueryWithPagination<Autor>(sql, parameters, pageCriteria);
            }
        }
    }
}
