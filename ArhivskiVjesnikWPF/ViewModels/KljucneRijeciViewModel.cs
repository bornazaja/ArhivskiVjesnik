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
    public class KljucneRijeciViewModel : Screen
    {
        private IEventAggregator _eventAggregator;
        private IExtendedWindowManager _extendedWindowManager;
        private IKljucnaRijecService _kljucnaRijecService;
        private QueryCriteria queryCriteria;
        private PagedList<KljucnaRijecDto> kljucnaRjecPagedList;
        private IEnumerable<KeyValuePair<string, string>> stupciZaSortiranje;

        public KljucneRijeciViewModel(IEventAggregator eventAggregator, IExtendedWindowManager extendedWindowManager, IKljucnaRijecService kljucnaRijecService)
        {
            _eventAggregator = eventAggregator;
            _extendedWindowManager = extendedWindowManager;
            _kljucnaRijecService = kljucnaRijecService;

            InitPageableDataGridViewModel();
            InitQueryCriteria();
            PopulateStupciZaSortiranje();
        }

        protected override async void OnInitialize()
        {
            await PopulateKljucneRijeciAsync();
        }

        private void InitPageableDataGridViewModel()
        {
            PageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<KljucnaRijecDto>>();
            PageableDataGridViewModel.Title = "Ključne riječi";
            PageableDataGridViewModel.MenuItems.Add(new MenuItemModel { Name = "Napredna pretraga", Action = () => OpenNaprednaPretraga() });
            PageableDataGridViewModel.DataGridViewModelBase = IoC.Get<KljucneRijeciDataGridViewModel>();
            PageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<KljucnaRijecDto>
            {
                Name = "Opsežni detalji",
                Action = (kljucnaRijecDto) => _eventAggregator.PublishOnUIThread(new ComprehensiveDetaljiKljucneRijeciClickedEvent { KljucnaRijec = kljucnaRijecDto })
            });
            PageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(async () => await PopulateKljucneRijeciAsync());
        }


        private void InitQueryCriteria()
        {
            queryCriteria = new QueryCriteria
            {
                SearchCriterias = new List<SearchCriteria>(),
                Operator = Operator.And,
                SortCriteria = new SortCriteria { ColumnName = "KljucnaRijec.IDKljucnaRijec", SortDirection = SortDirection.Ascending },
                PageCriteria = new PageCriteria { Page = 1, Size = 10 }
            };
        }

        private void PopulateStupciZaSortiranje()
        {
            List<string> keys = new List<string> { "KljucnaRijec.IDKljucnaRijec", "KljucnaRijec.Vrijednost" };
            List<string> values = new List<string> { "ID Ključna riječ", "Vrijednost" };
            stupciZaSortiranje = ListHelper.ToKeyValuePairs(keys, values);
        }

        private async Task PopulateKljucneRijeciAsync()
        {
            await _extendedWindowManager.ShowLoadingDialogAsync(() =>
            {
                queryCriteria.PageCriteria = new PageCriteria { Page = PageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = queryCriteria.PageCriteria.Size };
                kljucnaRjecPagedList = _kljucnaRijecService.GetAll(queryCriteria);
                PageableDataGridViewModel.Refresh(kljucnaRjecPagedList);
            }, "Dohvaćanje ključnih riječi...", "Desila se greška prilikom dohvaćanja ključnih riječi.");
        }

        private void OpenNaprednaPretraga()
        {
            _extendedWindowManager.ShowNaprednaPretragaDialog(stupciZaSortiranje, async (qc) =>
            {
                queryCriteria = qc;
                await PopulateKljucneRijeciAsync();
            });
        }

        public PageableDataGridViewModel<KljucnaRijecDto> PageableDataGridViewModel { get; set; }
    }
}
