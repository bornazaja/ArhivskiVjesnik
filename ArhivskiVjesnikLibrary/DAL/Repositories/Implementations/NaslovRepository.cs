using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Extensions;
using ArhivskiVjesnikLibrary.DAL.Models;
using ArhivskiVjesnikLibrary.DAL.Repositories.Interfaces;
using System.Data;
using System.Data.SQLite;

namespace ArhivskiVjesnikLibrary.DAL.Repositories.Implementations
{
    public class NaslovRepository : GenericRepository<Naslov>, INaslovRepository
    {
        public PagedList<Naslov> GetAllByAutorID(int autorId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT Naslov.IDNaslov, Naslov.* FROM Naslov
                                INNER JOIN ClanakNaslov
                                ON Naslov.IDNaslov = ClanakNaslov.NaslovID
                                INNER JOIN ClanakAutor
                                ON ClanakNaslov.ClanakID = ClanakAutor.ClanakID
                                WHERE ClanakAutor.AutorID = @AutorID";

                var parameters = new { AutorID = autorId };
                return conn.QueryWithPagination<Naslov>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<Naslov> GetAllByClanakID(int clanakId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT Naslov.* FROM Naslov
                                INNER JOIN ClanakNaslov
                                ON Naslov.IDNaslov = ClanakNaslov.NaslovID
                                WHERE ClanakNaslov.ClanakID = @ClanakID";

                var parameters = new { ClanakID = clanakId };
                return conn.QueryWithPagination<Naslov>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<Naslov> GetAllByKljucnaRijecID(int kljucnaRijecId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT Naslov.IDNaslov, Naslov.* FROM Naslov
                                INNER JOIN ClanakNaslov
                                ON Naslov.IDNaslov = ClanakNaslov.NaslovID
                                INNER JOIN ClanakKljucnaRijec
                                ON ClanakNaslov.ClanakID = ClanakKljucnaRijec.ClanakID
                                WHERE ClanakKljucnaRijec.KljucnaRijecID = @KljucnaRijecID";

                var parameters = new { KljucnaRijecID = kljucnaRijecId };
                return conn.QueryWithPagination<Naslov>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<Naslov> GetAllBySazetakID(int sazetakId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT Naslov.IDNaslov, Naslov.* FROM Naslov
                                INNER JOIN ClanakNaslov
                                ON Naslov.IDNaslov = ClanakNaslov.NaslovID
                                INNER JOIN ClanakSazetak
                                ON ClanakNaslov.ClanakID = ClanakSazetak.ClanakID
                                WHERE ClanakSazetak.SazetakID = @SazetakID";

                var parameters = new { SazetakID = sazetakId };
                return conn.QueryWithPagination<Naslov>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<Naslov> GetAllByVrstaID(int vrstaId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT Naslov.IDNaslov, Naslov.* FROM Naslov
                                INNER JOIN ClanakNaslov
                                ON Naslov.IDNaslov = ClanakNaslov.NaslovID
                                INNER JOIN ClanakVrsta
                                ON ClanakNaslov.ClanakID = ClanakVrsta.ClanakID
                                WHERE ClanakVrsta.VrstaID = @VrstaID";

                var parameters = new { VrstaID = vrstaId };
                return conn.QueryWithPagination<Naslov>(sql, parameters, pageCriteria);
            }
        }
    }
}
