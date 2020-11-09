using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikWPF.ViewModels;

namespace ArhivskiVjesnikWPF.Extensions
{
    public static class CommonExtensions
    {
        public static SearchCriteria ToSearchCriteria(this SearchEntryViewModel searchEntryViewModel)
        {
            return new SearchCriteria
            {
                SearchCriteriaType = searchEntryViewModel.SelectedVrstaKriterija.Key,
                ColumnName = searchEntryViewModel.SelectedStupac.Key,
                SearchOperation = searchEntryViewModel.SelectedOperacija.Key,
                Term = searchEntryViewModel.Term
            };
        }
    }
}
