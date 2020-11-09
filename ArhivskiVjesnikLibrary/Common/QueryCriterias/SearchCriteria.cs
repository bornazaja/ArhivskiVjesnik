namespace ArhivskiVjesnikLibrary.Common.QueryCriterias
{
    public class SearchCriteria
    {
        public SearchCriteriaType? SearchCriteriaType { get; set; }
        public string ColumnName { get; set; }
        public SearchOperation? SearchOperation { get; set; }
        public string Term { get; set; }
    }
}
