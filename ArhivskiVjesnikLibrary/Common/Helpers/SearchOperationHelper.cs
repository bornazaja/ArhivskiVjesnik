using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using System;
using System.Collections.Generic;

namespace ArhivskiVjesnikLibrary.Common.Helpers
{
    public static class SearchOperationHelper
    {
        private static IEnumerable<SearchOperation?> stringSearchOperations = new List<SearchOperation?> { SearchOperation.Equal, SearchOperation.StartsWith, SearchOperation.Contains, SearchOperation.EndsWith };
        private static IEnumerable<SearchOperation?> numericAndDateTimeSearchOperations = new List<SearchOperation?> { SearchOperation.LessThen, SearchOperation.LessThenOrEqualTo, SearchOperation.Equal, SearchOperation.GreaterThan, SearchOperation.GreaterThanOrEqualTo };

        public static IEnumerable<SearchOperation?> GetSearchOperationsByPropertyName(string propertyName)
        {
            if (ModelHelper.IsPropertyTextType(propertyName))
            {
                return stringSearchOperations;
            }
            else if (ModelHelper.IsPropertyNumericType(propertyName) || ModelHelper.IsPropertyDateTimeType(propertyName))
            {
                return numericAndDateTimeSearchOperations;
            }
            else
            {
                throw new Exception("This type of search operations does not exists.");
            }
        }
    }
}
