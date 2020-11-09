using System.ComponentModel;

namespace ArhivskiVjesnikLibrary.Common.QueryCriterias
{
    public enum SearchCriteriaType
    {
        [Description("Uključiv")]
        Include,

        [Description("Nije uključiv")]
        Exclude
    }
}
