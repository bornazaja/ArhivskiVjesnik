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
    public class ClanciViewModel : Screen
    {
        private IEventAggregator _eventAggregator;
        private IExtendedWindowManager _extendedWindowManager;
        private IClanakService _clanakService;
        private QueryCriteria queryCriteria;
        private PagedList<ClanakDto> clanciPagedList;
        private IEnumerable<KeyValuePair<string, string>> stupciZaSortiranje;

        public ClanciViewModel(IEventAggregator eventAggregator, IExtendedWindowManager extendedWindowManager, IClanakService clanakService)
        {
            _eventAggregator = eventAggregator;
            _extendedWindowManager = extendedWindowManager;
            _clanakService = clanakService;

            InitPageableDataGridViewModel();
            InitQueryCriteria();
            PopulateStupciZaSortiranje();
        }

        protected override async void OnInitialize()
        {
            await PopulateClanciAsync();
        }

        private void InitPageableDataGridViewModel()
        {
            PageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<ClanakDto>>();
            PageableDataGridViewModel.Title = "Članci";
            PageableDataGridViewModel.MenuItems.Add(new MenuItemModel { Name = "Napredna pretraga", Action = () => OpenNaprednaPretraga() });
            PageableDataGridViewModel.DataGridViewModelBase = IoC.Get<ClanciDataGridViewModel>();
            PageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<ClanakDto>
            {
                Name = "Opsežni detalji",
                Action = (clanakDto) => _eventAggregator.BeginPublishOnUIThread(new ComprehensiveDetaljiClankaClickedEvent { Clanak = clanakDto })
            });
            PageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(async () => await PopulateClanciAsync());
        }

        private void InitQueryCriteria()
        {
            queryCriteria = new QueryCriteria
            {
                SearchCriterias = new List<SearchCriteria>(),
                Operator = Operator.And,
                SortCriteria = new SortCriteria { ColumnName = "Clanak.IDClanak", SortDirection = SortDirection.Ascending },
                PageCriteria = new PageCriteria { Page = 1, Size = 10 },
            };
        }

        private void PopulateStupciZaSortiranje()
        {
            List<string> keys = new List<string> { "Clanak.IDClanak", "Clanak.Naziv", "Clanak.Godiste", "Clanak.Volumen", "Clanak.Broj", "Clanak.DatumIzdavanja", "Clanak.DatumObjave",
                                                    "Clanak.URL"};
            List<string> values = new List<string> { "ID Članak", "Naziv", "Godište", "Volumen", "Broj", "Datum izdavanja", "Datum objave",
                                                        "URL"};

            stupciZaSortiranje = ListHelper.ToKeyValuePairs(keys, values);
        }

        private async Task PopulateClanciAsync()
        {
            await _extendedWindowManager.ShowLoadingDialogAsync(() =>
            {
                queryCriteria.PageCriteria = new PageCriteria { Page = PageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = queryCriteria.PageCriteria.Size };
                clanciPagedList = _clanakService.GetAll(queryCriteria);
                PageableDataGridViewModel.Refresh(clanciPagedList);
            }, "Dohvaćanje članaka...", "Desila se greška prilikom dohvaćanja članaka.");
        }

        private void OpenNaprednaPretraga()
        {
            _extendedWindowManager.ShowNaprednaPretragaDialog(stupciZaSortiranje, async (qc) =>
            {
                queryCriteria = qc;
                await PopulateClanciAsync();
            });
        }

        public PageableDataGridViewModel<ClanakDto> PageableDataGridViewModel { get; set; }
    }
}
