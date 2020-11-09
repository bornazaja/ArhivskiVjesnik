using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Extensions;
using ArhivskiVjesnikLibrary.DAL.Models;
using ArhivskiVjesnikLibrary.DAL.Repositories.Interfaces;
using System.Data;
using System.Data.SQLite;

namespace ArhivskiVjesnikLibrary.DAL.Repositories.Implementations
{
    public class SazetakRepository : GenericRepository<Sazetak>, ISazetakRepository
    {
        public PagedList<Sazetak> GetAllByAutorID(int autorId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT Sazetak.IDSazetak, Sazetak.* FROM Sazetak
                                INNER JOIN ClanakSazetak
                                ON Sazetak.IDSazetak = ClanakSazetak.SazetakID
                                INNER JOIN ClanakAutor
                                ON ClanakSazetak.ClanakID = ClanakAutor.ClanakID
                                WHERE ClanakAutor.AutorID = @AutorID";

                var parameters = new { AutorID = autorId };
                return conn.QueryWithPagination<Sazetak>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<Sazetak> GetAllByClanakID(int clanakId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT Sazetak.* FROM Sazetak
                                INNER JOIN ClanakSazetak
                                ON Sazetak.IDSazetak = ClanakSazetak.SazetakID
                                WHERE ClanakSazetak.ClanakID = @ClanakID";

                var parameters = new { ClanakID = clanakId };
                return conn.QueryWithPagination<Sazetak>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<Sazetak> GetAllByKljucnaRijecID(int kljucnaRijecId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT Sazetak.IDSazetak, Sazetak.* FROM Sazetak
                                INNER JOIN ClanakSazetak
                                ON Sazetak.IDSazetak = ClanakSazetak.SazetakID
                                INNER JOIN ClanakKljucnaRijec
                                ON ClanakSazetak.ClanakID = ClanakKljucnaRijec.ClanakID
                                WHERE ClanakKljucnaRijec.KljucnaRijecID = @KljucnaRijecID";

                var parameters = new { KljucnaRijecID = kljucnaRijecId };
                return conn.QueryWithPagination<Sazetak>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<Sazetak> GetAllByNaslovID(int naslovId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT Sazetak.IDSazetak, Sazetak.* FROM Sazetak
                                INNER JOIN ClanakSazetak
                                ON Sazetak.IDSazetak = ClanakSazetak.SazetakID
                                INNER JOIN ClanakNaslov
                                ON ClanakSazetak.ClanakID = ClanakNaslov.ClanakID
                                WHERE ClanakNaslov.NaslovID = @NaslovID";

                var parameters = new { NaslovID = naslovId };
                return conn.QueryWithPagination<Sazetak>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<Sazetak> GetAllByVrstaID(int vrstaId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT Sazetak.IDSazetak, Sazetak.* FROM Sazetak
                                INNER JOIN ClanakSazetak
                                ON Sazetak.IDSazetak = ClanakSazetak.SazetakID
                                INNER JOIN ClanakVrsta
                                ON ClanakSazetak.ClanakID = ClanakVrsta.ClanakID
                                WHERE ClanakVrsta.VrstaID = @VrstaID";

                var parameters = new { VrstaID = vrstaId };
                return conn.QueryWithPagination<Sazetak>(sql, parameters, pageCriteria);
            }
        }
    }
}
