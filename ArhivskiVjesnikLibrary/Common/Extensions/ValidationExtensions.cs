using ArhivskiVjesnikLibrary.Common.QueryCriterias;

namespace ArhivskiVjesnikLibrary.Common.Extensions
{
    public static class ValidationExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNull(this object value)
        {
            return value == null;
        }

        public static bool IsValid(this SearchCriteria searchCriteria)
        {
            return searchCriteria.SearchCriteriaType.HasValue
                    && !searchCriteria.ColumnName.IsNullOrEmpty()
                    && searchCriteria.SearchOperation.HasValue
                    && !searchCriteria.Term.IsNullOrEmpty();
        }
    }
}
