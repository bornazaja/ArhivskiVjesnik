using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace ArhivskiVjesnikLibrary.DAL.Extensions
{
    public static class IDbConnectionExtensions
    {
        private const string SqlPagination = "LIMIT @Size OFFSET (@Page - 1) * @Size";

        public static PagedList<T> QueryWithPagination<T>(this IDbConnection dbConnection, string sql, object parameters, PageCriteria pageCriteria)
        {
            string newSql = CreatePaginationQuery(sql);
            SqlBuilder sqlBuilder = new SqlBuilder();
            SqlBuilder.Template template = sqlBuilder.AddTemplate(newSql, new { Size = pageCriteria.Size, Page = pageCriteria.Page });
            sqlBuilder.AddParameters(parameters);
            return GetPagedList<T>(dbConnection, pageCriteria, template);
        }

        public static PagedList<T> QueryWithPagination<T>(this IDbConnection dbConnection, string sql, SqlBuilder sqlBuilder, PageCriteria pageCriteria)
        {
            string newSql = CreatePaginationQuery(sql);
            SqlBuilder.Template template = sqlBuilder.AddTemplate(newSql, new { Size = pageCriteria.Size, Page = pageCriteria.Page });
            return GetPagedList<T>(dbConnection, pageCriteria, template);
        }

        private static PagedList<T> GetPagedList<T>(IDbConnection dbConnection, PageCriteria pageCriteria, SqlBuilder.Template template)
        {
            IEnumerable<T> subset = dbConnection.Query<T>(template.RawSql, template.Parameters);
            string sqlCountRows = CreateSqlCountQuery<T>(template.RawSql).Replace(SqlPagination, string.Empty);
            int count = dbConnection.ExecuteScalar<int>(sqlCountRows, template.Parameters);

            PagedList<T> pagedList = new PagedList<T>();
            pagedList.Subset = subset;
            pagedList.TotalPages = (int)Math.Ceiling((decimal)count / pageCriteria.Size);
            pagedList.TotalCount = count;
            pagedList.HasPrevious = pageCriteria.Page > 1 ? true : false;
            pagedList.HasNext = pageCriteria.Page < pagedList.TotalPages;
            pagedList.PageCriteria = pageCriteria;
            return pagedList;
        }

        private static string CreatePaginationQuery(string sql)
        {
            return $"{sql}{Environment.NewLine}{SqlPagination}";
        }

        private static string CreateSqlCountQuery<T>(string sql)
        {
            string regex = @"(?<=SELECT )(.*?)(?= FROM)";
            string replacement = sql.ToLower().Contains("distinct") ? $"COUNT(DISTINCT {typeof(T).Name}.ID{typeof(T).Name})" : "COUNT(*)";
            string sqlCountRows = Regex.Replace(sql.Replace(Environment.NewLine, " "), regex, replacement, RegexOptions.IgnoreCase);
            return sqlCountRows;
        }
    }
}
