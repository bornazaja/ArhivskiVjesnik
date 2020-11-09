using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Extensions;
using ArhivskiVjesnikLibrary.DAL.Models;
using ArhivskiVjesnikLibrary.DAL.Repositories.Interfaces;
using System.Data;
using System.Data.SQLite;

namespace ArhivskiVjesnikLibrary.DAL.Repositories.Implementations
{
    public class VrstaRepository : GenericRepository<Vrsta>, IVrstaRepository
    {
        public PagedList<Vrsta> GetAllByAutorID(int autorId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT Vrsta.IDVrsta, Vrsta.* FROM Vrsta
                                INNER JOIN ClanakVrsta
                                ON Vrsta.IDVrsta = ClanakVrsta.VrstaID
                                INNER JOIN ClanakAutor
                                ON ClanakVrsta.ClanakID = ClanakAutor.ClanakID
                                WHERE ClanakAutor.AutorID = @AutorID";

                var parameters = new { AutorID = autorId };
                return conn.QueryWithPagination<Vrsta>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<Vrsta> GetAllByClanakID(int clanakId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT Vrsta.* FROM Vrsta
                                INNER JOIN ClanakVrsta
                                ON Vrsta.IDVrsta = ClanakVrsta.VrstaID
                                WHERE ClanakVrsta.ClanakID = @ClanakID";

                var parameters = new { ClanakID = clanakId };
                return conn.QueryWithPagination<Vrsta>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<Vrsta> GetAllByKljucnaRijecID(int kljucnaRijecId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT Vrsta.IDVrsta, Vrsta.* FROM Vrsta
                                INNER JOIN ClanakVrsta
                                ON Vrsta.IDVrsta = ClanakVrsta.VrstaID
                                INNER JOIN ClanakKljucnaRijec
                                ON ClanakVrsta.ClanakID = ClanakKljucnaRijec.ClanakID
                                WHERE ClanakKljucnaRijec.KljucnaRijecID = @KljucnaRijecID";

                var parameters = new { KljucnaRijecID = kljucnaRijecId };
                return conn.QueryWithPagination<Vrsta>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<Vrsta> GetAllByNaslovID(int naslovId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT Vrsta.IDVrsta, Vrsta.* FROM Vrsta
                                INNER JOIN ClanakVrsta
                                ON Vrsta.IDVrsta = ClanakVrsta.VrstaID
                                INNER JOIN ClanakNaslov
                                ON ClanakVrsta.ClanakID = ClanakNaslov.ClanakID
                                WHERE ClanakNaslov.NaslovID = @NaslovID";

                var parameters = new { NaslovID = naslovId };
                return conn.QueryWithPagination<Vrsta>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<Vrsta> GetAllBySazetakID(int sazetakId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT Vrsta.IDVrsta, Vrsta.* FROM Vrsta
                                INNER JOIN ClanakVrsta
                                ON Vrsta.IDVrsta = ClanakVrsta.VrstaID
                                INNER JOIN ClanakSazetak
                                ON ClanakVrsta.ClanakID = ClanakSazetak.ClanakID
                                WHERE ClanakSazetak.SazetakID = @SazetakID";

                var parameters = new { SazetakID = sazetakId };
                return conn.QueryWithPagination<Vrsta>(sql, parameters, pageCriteria);
            }
        }
    }
}
