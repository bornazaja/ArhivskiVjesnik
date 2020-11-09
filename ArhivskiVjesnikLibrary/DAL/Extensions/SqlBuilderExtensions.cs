using ArhivskiVjesnikLibrary.Common.Helpers;
using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using Dapper;
using System;
using System.Collections.Generic;

namespace ArhivskiVjesnikLibrary.DAL.Extensions
{
    public static class SqlBuilderExtensions
    {
        public static SqlBuilder Where(this SqlBuilder sqlBuilder, List<SearchCriteria> searchCriterias, Operator op)
        {
            if (searchCriterias != null && searchCriterias.Count > 0)
            {
                int counter = 1;
                var clauses = new Dictionary<string, DynamicParameters>();

                foreach (var searchCriteria in searchCriterias)
                {
                    if (ModelHelper.DoesPropertyExistsInModels(searchCriteria.ColumnName))
                    {
                        DoWhereBySearchOperation(searchCriteria, clauses, counter);
                        counter++;
                    }
                }

                BuildWhereClausesByOperator(sqlBuilder, op, clauses);
            }
            return sqlBuilder;
        }

        private static void DoWhereBySearchOperation(SearchCriteria searchCriteria, Dictionary<string, DynamicParameters> clauses, int counter)
        {
            switch (searchCriteria.SearchOperation)
            {
                case SearchOperation.LessThen:
                    AppendWhereClause(clauses, searchCriteria.SearchCriteriaType.Value, searchCriteria.ColumnName, "<", searchCriteria.Term, counter);
                    break;
                case SearchOperation.LessThenOrEqualTo:
                    AppendWhereClause(clauses, searchCriteria.SearchCriteriaType.Value, searchCriteria.ColumnName, "<=", searchCriteria.Term, counter);
                    break;
                case SearchOperation.Equal:
                    AppendWhereClause(clauses, searchCriteria.SearchCriteriaType.Value, searchCriteria.ColumnName, "=", searchCriteria.Term, counter);
                    break;
                case SearchOperation.StartsWith:
                    AppendWhereClause(clauses, searchCriteria.SearchCriteriaType.Value, searchCriteria.ColumnName, "LIKE", $"{searchCriteria.Term}%", counter);
                    break;
                case SearchOperation.Contains:
                    AppendWhereClause(clauses, searchCriteria.SearchCriteriaType.Value, searchCriteria.ColumnName, "LIKE", $"%{searchCriteria.Term}%", counter);
                    break;
                case SearchOperation.EndsWith:
                    AppendWhereClause(clauses, searchCriteria.SearchCriteriaType.Value, searchCriteria.ColumnName, "LIKE", $"%{searchCriteria.Term}", counter);
                    break;
                case SearchOperation.GreaterThan:
                    AppendWhereClause(clauses, searchCriteria.SearchCriteriaType.Value, searchCriteria.ColumnName, ">", searchCriteria.Term, counter);
                    break;
                case SearchOperation.GreaterThanOrEqualTo:
                    AppendWhereClause(clauses, searchCriteria.SearchCriteriaType.Value, searchCriteria.ColumnName, ">=", searchCriteria.Term, counter);
                    break;
                default:
                    throw new Exception("This search operation does not exists.");
            }
        }

        private static void AppendWhereClause(Dictionary<string, DynamicParameters> clauses, SearchCriteriaType searchCriteriaType,
                                                string columnName, string searchOperation, string searchTerm, int counter)
        {
            string searchCriteriaParameter = $"@SearchTerm{counter}";

            var dictionary = new Dictionary<string, object>
            {
                { searchCriteriaParameter, searchTerm }
            };

            if (columnName.Contains("+"))
            {
                columnName = columnName.Replace("+", " || ' ' || ");
            }


            string sql = $"{columnName} {searchOperation} CAST({searchCriteriaParameter} AS NVARCHAR)";

            if (searchCriteriaType == SearchCriteriaType.Exclude)
            {
                sql = $"NOT {sql}";
            }

            clauses.Add(sql, new DynamicParameters(dictionary));
        }

        private static void BuildWhereClausesByOperator(SqlBuilder sqlBuilder, Operator op, Dictionary<string, DynamicParameters> clauses)
        {
            foreach (var item in clauses)
            {
                switch (op)
                {
                    case Operator.And:
                        sqlBuilder.Where(item.Key, item.Value);
                        break;
                    case Operator.Or:
                        sqlBuilder.OrWhere(item.Key, item.Value);
                        break;
                    default:
                        throw new Exception("This operator does not exists.");
                }
            }
        }

        public static SqlBuilder OrderBy(this SqlBuilder sqlBuilder, SortCriteria sortCriteria)
        {
            if (sortCriteria != null && ModelHelper.DoesPropertyExistsInModels(sortCriteria.ColumnName))
            {
                string sortDirection = sortCriteria.SortDirection == SortDirection.Ascending ? "ASC" : "DESC";

                string sql = sortCriteria.ColumnName.Contains("+") ? $"{sortCriteria.ColumnName.Replace("+", $" {sortDirection}, ")}" : $"{sortCriteria.ColumnName} {sortDirection}";

                sqlBuilder.OrderBy(sql);
            }
            return sqlBuilder;
        }
    }
}
