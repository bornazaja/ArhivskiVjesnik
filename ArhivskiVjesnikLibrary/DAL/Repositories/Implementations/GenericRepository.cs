using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Extensions;
using ArhivskiVjesnikLibrary.DAL.Repositories.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace ArhivskiVjesnikLibrary.DAL.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly string connectionString;

        public GenericRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        }

        public IEnumerable<T> GetAll()
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                return conn.Query<T>($"SELECT * FROM {typeof(T).Name}");
            }
        }

        public IEnumerable<string> GetAll(string columnName, string term, int n = 10)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                SearchCriteria searchCriteria = new SearchCriteria
                {
                    SearchCriteriaType = SearchCriteriaType.Include,
                    ColumnName = columnName,
                    SearchOperation = SearchOperation.Contains,
                    Term = term
                };

                string sqlSelectPart = columnName.Contains("+") ? $"{columnName.Replace("+", "|| ' ' ||")}" : $"DISTINCT {columnName} AS NVARCHAR";

                var sqlBuilder = new SqlBuilder();
                var template = sqlBuilder.AddTemplate($"SELECT {sqlSelectPart} FROM {typeof(T).Name} /**where**/ /**orderby**/ LIMIT @Limit");
                sqlBuilder.AddParameters(new { Limit = n });
                sqlBuilder.Where(new List<SearchCriteria> { searchCriteria }, Operator.And);
                sqlBuilder.OrderBy(new SortCriteria { ColumnName = columnName, SortDirection = SortDirection.Ascending });

                return conn.Query<string>(template.RawSql, template.Parameters);
            }
        }

        public PagedList<T> GetAll(QueryCriteria queryCriteria)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                string sql = $@"SELECT DISTINCT {typeof(T).Name}.* FROM Clanak
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
                                /**where**/
                                /**orderby**/";

                SqlBuilder sqlBuilder = new SqlBuilder();
                sqlBuilder.Where($"{typeof(T).Name}.ID{typeof(T).Name} IS NOT NULL");
                sqlBuilder.Where(queryCriteria.SearchCriterias, queryCriteria.Operator);
                sqlBuilder.OrderBy(queryCriteria?.SortCriteria);

                return conn.QueryWithPagination<T>(sql, sqlBuilder, queryCriteria.PageCriteria);
            }
        }

        public T GetByID(int id)
        {
            using (IDbConnection conn = new SQLiteConnection(connectionString))
            {
                return conn.QueryFirst<T>($"SELECT * FROM {typeof(T).Name} WHERE ID{typeof(T).Name} = @ID", new { ID = id });
            }
        }
    }
}
