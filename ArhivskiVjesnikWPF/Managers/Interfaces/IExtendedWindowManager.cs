using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using Caliburn.Micro;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArhivskiVjesnikWPF.Managers.Interfaces
{
    public interface IExtendedWindowManager : IWindowManager
    {
        bool? ShowDialog<TBasicDtoDetailsViewModel, TDto>(TDto dto);
        Task<bool?> ShowDialogAsync(object viewModel, object context = null, IDictionary<string, object> settings = null);
        bool? ShowStatusInfoDialog(string message);
        Task<bool?> ShowStatusInfoDialogAsync(string message);
        Task<bool?> ShowLoadingDialogAsync(System.Action action, string loadingMessage, string errorMessage);
        bool? ShowNaprednaPretragaDialog(IEnumerable<KeyValuePair<string, string>> stupciZaSortiranje, System.Action<QueryCriteria> dialogResultOKAction);
    }
}
