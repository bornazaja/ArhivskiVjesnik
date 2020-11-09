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
    public class VrsteViewModel : Screen
    {
        private IEventAggregator _eventAggregator;
        private IExtendedWindowManager _extendedWindowManager;
        private IVrstaService _vrstaService;
        private QueryCriteria queryCriteria;
        private PagedList<VrstaDto> vrstaPagedList;
        private IEnumerable<KeyValuePair<string, string>> stupciZaSortiranje;

        public VrsteViewModel(IEventAggregator eventAggregator, IExtendedWindowManager extendedWindowManager, IVrstaService vrstaService)
        {
            _eventAggregator = eventAggregator;
            _extendedWindowManager = extendedWindowManager;
            _vrstaService = vrstaService;

            InitPageableDataGridViewModel();
            InitQueryCriteria();
            PopulateStupciZaSortiranje();
        }

        protected override async void OnInitialize()
        {
            await PopulateVrsteAsync();
        }

        private void InitPageableDataGridViewModel()
        {
            PageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<VrstaDto>>();
            PageableDataGridViewModel.Title = "Vrste";
            PageableDataGridViewModel.MenuItems.Add(new MenuItemModel { Name = "Napredna pretraga", Action = () => OpenNaprednaPretraga() });
            PageableDataGridViewModel.DataGridViewModelBase = IoC.Get<VrsteDataGridViewModel>();
            PageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<VrstaDto>
            {
                Name = "Opsežni detalji",
                Action = (vrstaDto) => _eventAggregator.PublishOnUIThread(new ComprehensiveDetaljiVrsteClickedEvent { Vrsta = vrstaDto })
            });
            PageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(async () => await PopulateVrsteAsync());
        }

        private void InitQueryCriteria()
        {
            queryCriteria = new QueryCriteria
            {
                SearchCriterias = new List<SearchCriteria>(),
                Operator = Operator.And,
                SortCriteria = new SortCriteria { ColumnName = "Vrsta.IDVrsta", SortDirection = SortDirection.Ascending },
                PageCriteria = new PageCriteria { Page = 1, Size = 10 }
            };
        }

        private void PopulateStupciZaSortiranje()
        {
            List<string> keys = new List<string> { "Vrsta.IDVrsta", "Vrsta.Naziv" };
            List<string> values = new List<string> { "ID Vrsta", "Naziv" };
            stupciZaSortiranje = ListHelper.ToKeyValuePairs(keys, values);
        }

        private async Task PopulateVrsteAsync()
        {
            await _extendedWindowManager.ShowLoadingDialogAsync(() =>
            {
                queryCriteria.PageCriteria = new PageCriteria { Page = PageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = queryCriteria.PageCriteria.Size };
                vrstaPagedList = _vrstaService.GetAll(queryCriteria);
                PageableDataGridViewModel.Refresh(vrstaPagedList);
            }, "Dohvaćanje vrsta...", "Desila se greška prilikom dohvaćanja vrsta.");
        }

        private void OpenNaprednaPretraga()
        {
            _extendedWindowManager.ShowNaprednaPretragaDialog(stupciZaSortiranje, async (qc) =>
            {
                queryCriteria = qc;
                await PopulateVrsteAsync();
            });
        }

        public PageableDataGridViewModel<VrstaDto> PageableDataGridViewModel { get; set; }
    }
}
