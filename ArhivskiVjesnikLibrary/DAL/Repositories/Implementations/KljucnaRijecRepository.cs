using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Extensions;
using ArhivskiVjesnikLibrary.DAL.Models;
using ArhivskiVjesnikLibrary.DAL.Repositories.Interfaces;
using System.Data;
using System.Data.SQLite;

namespace ArhivskiVjesnikLibrary.DAL.Repositories.Implementations
{
    public class KljucnaRijecRepository : GenericRepository<KljucnaRijec>, IKljucnaRijecRepository
    {
        public PagedList<KljucnaRijec> GetAllByAutorID(int autorId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT KljucnaRijec.IDKljucnaRijec, KljucnaRijec.* FROM KljucnaRijec
                                INNER JOIN ClanakKljucnaRijec
                                ON KljucnaRijec.IDKljucnaRijec = ClanakKljucnaRijec.KljucnaRijecID
                                INNER JOIN ClanakAutor
                                ON ClanakKljucnaRijec.ClanakID = ClanakAutor.ClanakID
                                WHERE ClanakAutor.AutorID = @AutorID";

                var parameters = new { AutorID = autorId };
                return conn.QueryWithPagination<KljucnaRijec>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<KljucnaRijec> GetAllByClanakID(int clanakId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT KljucnaRijec.* FROM KljucnaRijec
                                INNER JOIN ClanakKljucnaRijec
                                ON KljucnaRijec.IDKljucnaRijec = ClanakKljucnaRijec.KljucnaRijecID
                                WHERE ClanakKljucnaRijec.ClanakID = @ClanakID";

                var parameters = new { ClanakID = clanakId };
                return conn.QueryWithPagination<KljucnaRijec>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<KljucnaRijec> GetAllByNaslovID(int naslovId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT KljucnaRijec.IDKljucnaRijec, KljucnaRijec.* FROM KljucnaRijec
                                INNER JOIN ClanakKljucnaRijec
                                ON KljucnaRijec.IDKljucnaRijec = ClanakKljucnaRijec.KljucnaRijecID
                                INNER JOIN ClanakNaslov
                                ON ClanakKljucnaRijec.ClanakID = ClanakNaslov.ClanakID
                                WHERE ClanakNaslov.NaslovID = @NaslovID";

                var parameters = new { NaslovID = naslovId };
                return conn.QueryWithPagination<KljucnaRijec>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<KljucnaRijec> GetAllBySazetakID(int sazetakId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT KljucnaRijec.* FROM KljucnaRijec
                                INNER JOIN ClanakKljucnaRijec
                                ON KljucnaRijec.IDKljucnaRijec = ClanakKljucnaRijec.KljucnaRijecID
                                INNER JOIN ClanakSazetak
                                ON ClanakKljucnaRijec.ClanakID = ClanakSazetak.ClanakID
                                WHERE ClanakSazetak.SazetakID = @SazetakID";

                var parameters = new { SazetakID = sazetakId };
                return conn.QueryWithPagination<KljucnaRijec>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<KljucnaRijec> GetAllByVrstaID(int vrstaId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT KljucnaRijec.IDKljucnaRijec, KljucnaRijec.* FROM KljucnaRijec
                                INNER JOIN ClanakKljucnaRijec
                                ON KljucnaRijec.IDKljucnaRijec = ClanakKljucnaRijec.KljucnaRijecID
                                INNER JOIN ClanakVrsta
                                ON ClanakKljucnaRijec.ClanakID = ClanakVrsta.ClanakID
                                WHERE ClanakVrsta.VrstaID = @VrstaID";

                var parameters = new { VrstaID = vrstaId };
                return conn.QueryWithPagination<KljucnaRijec>(sql, parameters, pageCriteria);
            }
        }
    }
}
