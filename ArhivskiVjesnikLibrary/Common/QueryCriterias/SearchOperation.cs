using System.ComponentModel;

namespace ArhivskiVjesnikLibrary.Common.QueryCriterias
{
    public enum SearchOperation
    {
        [Description("Manji")]
        LessThen,

        [Description("Manji ili jednak")]
        LessThenOrEqualTo,

        [Description("Jednak")]
        Equal,

        [Description("Počinje s")]
        StartsWith,

        [Description("Sadržava")]
        Contains,

        [Description("Završava s")]
        EndsWith,

        [Description("Veći")]
        GreaterThan,

        [Description("Veći ili jednak")]
        GreaterThanOrEqualTo
    }
}
