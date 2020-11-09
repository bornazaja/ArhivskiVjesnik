using ArhivskiVjesnikLibrary.BLL.DTO;
using ArhivskiVjesnikLibrary.BLL.Services.Interfaces;
using ArhivskiVjesnikLibrary.Common.Helpers;
using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikWPF.Events;
using ArhivskiVjesnikWPF.Managers.Interfaces;
using ArhivskiVjesnikWPF.Models;
using Caliburn.Micro;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArhivskiVjesnikWPF.ViewModels
{
    public class SazetciViewModel : Screen
    {
        private IEventAggregator _eventAggregator;
        private IExtendedWindowManager _extendedWindowManager;
        private ISazetakService _sazetakService;
        private QueryCriteria queryCriteria;
        private PagedList<SazetakDto> sazetakPagedList;
        private IEnumerable<KeyValuePair<string, string>> stupciZaSortiranje;

        public SazetciViewModel(IEventAggregator eventAggregator, IExtendedWindowManager extendedWindowManager, ISazetakService sazetakService)
        {
            _eventAggregator = eventAggregator;
            _extendedWindowManager = extendedWindowManager;
            _sazetakService = sazetakService;

            InitPageableDataGridViewModel();
            InitQueryCriteria();
            PopulateStupciZaSortiranje();
        }

        protected override async void OnInitialize()
        {
            await PopulateSazetciAsync();
        }

        private void InitPageableDataGridViewModel()
        {
            PageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<SazetakDto>>();
            PageableDataGridViewModel.Title = "Sažetci";
            PageableDataGridViewModel.MenuItems.Add(new MenuItemModel { Name = "Napredna pretraga", Action = () => OpenNaprednaPretraga() });
            PageableDataGridViewModel.DataGridViewModelBase = IoC.Get<SazetciDataGridViewModel>();
            PageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<SazetakDto>
            {
                Name = "Opsežni detalji",
                Action = (sazetakDto) => _eventAggregator.PublishOnUIThread(new ComprehensiveDetaljiSazetkaClickedEvent { Sazetak = sazetakDto })
            });
            PageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(async () => await PopulateSazetciAsync());
        }

        private void InitQueryCriteria()
        {
            queryCriteria = new QueryCriteria
            {
                SearchCriterias = new List<SearchCriteria>(),
                Operator = Operator.And,
                SortCriteria = new SortCriteria { ColumnName = "Sazetak.IDSazetak", SortDirection = SortDirection.Ascending },
                PageCriteria = new PageCriteria { Page = 1, Size = 10 }
            };
        }

        private void PopulateStupciZaSortiranje()
        {
            List<string> keys = new List<string> { "Sazetak.IDSazetak", "Sazetak.Opis" };
            List<string> values = new List<string> { "ID Sažetak", "Opis" };
            stupciZaSortiranje = ListHelper.ToKeyValuePairs(keys, values);
        }

        private async Task PopulateSazetciAsync()
        {
            await _extendedWindowManager.ShowLoadingDialogAsync(() =>
            {
                queryCriteria.PageCriteria = new PageCriteria { Page = PageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = queryCriteria.PageCriteria.Size };
                sazetakPagedList = _sazetakService.GetAll(queryCriteria);
                PageableDataGridViewModel.Refresh(sazetakPagedList);
            }, "Dohvaćanje sažetaka...", "Desila se greška prilikom dohvaćanja sažetaka.");
        }

        private void OpenNaprednaPretraga()
        {
            _extendedWindowManager.ShowNaprednaPretragaDialog(stupciZaSortiranje, async (qc) =>
            {
                queryCriteria = qc;
                await PopulateSazetciAsync();
            });
        }

        public PageableDataGridViewModel<SazetakDto> PageableDataGridViewModel { get; set; }
    }
}
