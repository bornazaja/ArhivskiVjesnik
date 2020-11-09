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
    public class AutoriViewModel : Screen
    {
        private IEventAggregator _eventAggregator;
        private IExtendedWindowManager _extendedWindowManager;
        private IAutorService _autorService;
        private QueryCriteria queryCriteria;
        private PagedList<AutorDto> autorPagedList;
        private IEnumerable<KeyValuePair<string, string>> stupciZaSortiranje;

        public AutoriViewModel(IEventAggregator eventAggregator, IExtendedWindowManager extendedWindowManager, IAutorService autorService)
        {
            _eventAggregator = eventAggregator;
            _extendedWindowManager = extendedWindowManager;
            _autorService = autorService;

            InitPageableDataGridViewModel();
            InitQueryCriteria();
            PopulateStupciZaSortiranje();
        }

        protected override async void OnInitialize()
        {
            await PopulateAutoriAsync();
        }

        private void InitPageableDataGridViewModel()
        {
            PageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<AutorDto>>();
            PageableDataGridViewModel.Title = "Autori";
            PageableDataGridViewModel.MenuItems.Add(new MenuItemModel { Name = "Napredna pretraga", Action = () => OpenNaprednaPretraga() });
            PageableDataGridViewModel.DataGridViewModelBase = IoC.Get<AutoriDataGridViewModel>();
            PageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<AutorDto>
            {
                Name = "Opsežni detalji",
                Action = (autorDto) => _eventAggregator.PublishOnUIThread(new ComprehensiveDetaljiAutoraClickedEvent { Autor = autorDto })
            });
            PageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(async () => await PopulateAutoriAsync());
        }

        private void InitQueryCriteria()
        {
            queryCriteria = new QueryCriteria
            {
                SearchCriterias = new List<SearchCriteria>(),
                Operator = Operator.And,
                SortCriteria = new SortCriteria { ColumnName = "Autor.IDAutor", SortDirection = SortDirection.Ascending },
                PageCriteria = new PageCriteria { Page = 1, Size = 10 }
            };
        }

        private void PopulateStupciZaSortiranje()
        {
            List<string> keys = new List<string> { "Autor.IDAutor", "Autor.Ime", "Autor.Prezime" };
            List<string> values = new List<string> { "ID Autor", "Ime", "Prezime" };
            stupciZaSortiranje = ListHelper.ToKeyValuePairs(keys, values);
        }

        private async Task PopulateAutoriAsync()
        {
            await _extendedWindowManager.ShowLoadingDialogAsync(() =>
            {
                queryCriteria.PageCriteria = new PageCriteria { Page = PageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = queryCriteria.PageCriteria.Size };
                autorPagedList = _autorService.GetAll(queryCriteria);
                PageableDataGridViewModel.Refresh(autorPagedList);
            }, "Dohvaćanje autora...", "Desila se greška prilikom dohvaćanja autora.");
        }

        private void OpenNaprednaPretraga()
        {
            _extendedWindowManager.ShowNaprednaPretragaDialog(stupciZaSortiranje, async (qc) =>
            {
                queryCriteria = qc;
                await PopulateAutoriAsync();
            });
        }

        public PageableDataGridViewModel<AutorDto> PageableDataGridViewModel { get; set; }
    }
}
