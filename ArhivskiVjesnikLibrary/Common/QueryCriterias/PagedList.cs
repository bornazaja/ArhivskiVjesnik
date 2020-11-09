using System.Collections.Generic;

namespace ArhivskiVjesnikLibrary.Common.QueryCriterias
{
    public class PagedList<T>
    {
        public IEnumerable<T> Subset { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
        public PageCriteria PageCriteria { get; set; }
    }
}
