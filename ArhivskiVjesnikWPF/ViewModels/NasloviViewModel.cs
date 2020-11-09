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
    public class NasloviViewModel : Screen
    {
        private IEventAggregator _eventAggregator;
        private IExtendedWindowManager _extendedWindowManager;
        private INaslovService _naslovService;
        private QueryCriteria queryCriteria;
        private PagedList<NaslovDto> naslovPagedList;
        private IEnumerable<KeyValuePair<string, string>> stupciZaSortiranje;

        public NasloviViewModel(IEventAggregator eventAggregator, IExtendedWindowManager extendedWindowManager, INaslovService naslovService)
        {
            _eventAggregator = eventAggregator;
            _extendedWindowManager = extendedWindowManager;
            _naslovService = naslovService;

            InitPageableDataGridViewModel();
            InitQueryCriteria();
            PopulateStupciZaSortiranje();
        }

        protected override async void OnInitialize()
        {
            await PopulateNasloviAsync();
        }

        private void InitPageableDataGridViewModel()
        {
            PageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<NaslovDto>>();
            PageableDataGridViewModel.Title = "Naslovi";
            PageableDataGridViewModel.MenuItems.Add(new MenuItemModel { Name = "Napredna pretraga", Action = () => OpenNaprednaPretraga() });
            PageableDataGridViewModel.DataGridViewModelBase = IoC.Get<NasloviDataGridViewModel>();
            PageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<NaslovDto>
            {
                Name = "Opsežni detalji",
                Action = (naslovDto) => _eventAggregator.PublishOnUIThread(new ComprehensiveDetaljiNaslovaClickedEvent { Naslov = naslovDto })
            });
            PageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(async () => await PopulateNasloviAsync());
        }

        private void InitQueryCriteria()
        {
            queryCriteria = new QueryCriteria
            {
                SearchCriterias = new List<SearchCriteria>(),
                Operator = Operator.And,
                SortCriteria = new SortCriteria { ColumnName = "Naslov.IDNaslov", SortDirection = SortDirection.Ascending },
                PageCriteria = new PageCriteria { Page = 1, Size = 10 }
            };
        }

        private void PopulateStupciZaSortiranje()
        {
            List<string> keys = new List<string> { "Naslov.IDNaslov", "Naslov.Naziv" };
            List<string> values = new List<string> { "ID Naslov", "Naziv" };
            stupciZaSortiranje = ListHelper.ToKeyValuePairs(keys, values);
        }

        private async Task PopulateNasloviAsync()
        {
            await _extendedWindowManager.ShowLoadingDialogAsync(() =>
            {
                queryCriteria.PageCriteria = new PageCriteria { Page = PageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = queryCriteria.PageCriteria.Size };
                naslovPagedList = _naslovService.GetAll(queryCriteria);
                PageableDataGridViewModel.Refresh(naslovPagedList);
            }, "Dohvaćanje naslova...", "Desila se greška prilikom dohvaćanja naslova.");
        }

        private void OpenNaprednaPretraga()
        {
            _extendedWindowManager.ShowNaprednaPretragaDialog(stupciZaSortiranje, async (qc) =>
            {
                queryCriteria = qc;
                await PopulateNasloviAsync();
            });
        }

        public PageableDataGridViewModel<NaslovDto> PageableDataGridViewModel { get; set; }
    }
}
