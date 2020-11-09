using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Extensions;
using ArhivskiVjesnikLibrary.DAL.Models;
using ArhivskiVjesnikLibrary.DAL.Repositories.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace ArhivskiVjesnikLibrary.DAL.Repositories.Implementations
{
    public class ClanakRepository : GenericRepository<Clanak>, IClanakRepository
    {
        public PagedList<Clanak> GetAllByAutorID(int autorId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT Clanak.IDClanak, Clanak.* FROM Clanak
                                INNER JOIN ClanakAutor
                                ON Clanak.IDClanak = ClanakAutor.ClanakID
                                WHERE ClanakAutor.AutorID = @AutorID";

                var parameters = new { AutorID = autorId };
                return conn.QueryWithPagination<Clanak>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<Clanak> GetAllByKljucnaRijecID(int kljucnaRijecId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT Clanak.IDClanak, Clanak.* FROM Clanak
                                INNER JOIN ClanakKljucnaRijec
                                ON Clanak.IDClanak = ClanakKljucnaRijec.ClanakID
                                WHERE ClanakKljucnaRijec.KljucnaRijecID = @KljucnaRijecID";

                var parameters = new { KljucnaRijecID = kljucnaRijecId };
                return conn.QueryWithPagination<Clanak>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<Clanak> GetAllByNaslovID(int naslovId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT Clanak.IDClanak, Clanak.* FROM Clanak
                                INNER JOIN ClanakNaslov
                                ON Clanak.IDClanak = ClanakNaslov.ClanakID
                                WHERE ClanakNaslov.NaslovID = @NaslovID";

                var parameters = new { NaslovID = naslovId };
                return conn.QueryWithPagination<Clanak>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<Clanak> GetAllBySazetakID(int sazetakId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT Clanak.IDClanak, Clanak.* FROM Clanak
                                INNER JOIN ClanakSazetak
                                ON Clanak.IDClanak = ClanakSazetak.ClanakID
                                WHERE ClanakSazetak.SazetakID = @SazetakID";

                var parameters = new { SazetakID = sazetakId };
                return conn.QueryWithPagination<Clanak>(sql, parameters, pageCriteria);
            }
        }

        public PagedList<Clanak> GetAllByVrstaID(int vrstaId, PageCriteria pageCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = @"SELECT DISTINCT Clanak.IDClanak, Clanak.* FROM Clanak
                                INNER JOIN ClanakVrsta
                                ON Clanak.IDClanak = ClanakVrsta.ClanakID
                                WHERE ClanakVrsta.VrstaID = @VrstaID";

                var parameters = new { VrstaID = vrstaId };
                return conn.QueryWithPagination<Clanak>(sql, parameters, pageCriteria);
            }
        }
    }
}
