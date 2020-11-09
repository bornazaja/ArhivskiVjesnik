using System.Collections.Generic;

namespace ArhivskiVjesnikLibrary.Common.QueryCriterias
{
    public class QueryCriteria
    {
        public Operator Operator { get; set; }
        public List<SearchCriteria> SearchCriterias { get; set; }
        public SortCriteria SortCriteria { get; set; }
        public PageCriteria PageCriteria { get; set; }
    }
}
