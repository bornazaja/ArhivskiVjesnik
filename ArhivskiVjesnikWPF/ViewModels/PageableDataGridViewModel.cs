using ArhivskiVjesnikLibrary.Common.Constants;
using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikWPF.Commands;
using ArhivskiVjesnikWPF.Models;
using Caliburn.Micro;
using System.Windows.Input;

namespace ArhivskiVjesnikWPF.ViewModels
{
    public class PageableDataGridViewModel<T> : Screen
    {
        public PageableDataGridViewModel()
        {
            PaginationViewModel = IoC.Get<PaginationViewModel>();
            MenuItems = new BindableCollection<MenuItemModel>();
            MenuVisibility = false;
            MenuItems.CollectionChanged += MenuItems_CollectionChanged;
        }

        private void MenuItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (MenuItems.Count == 0)
            {
                MenuVisibility = false;
            }
            else
            {
                MenuVisibility = true;
            }
        }

        public string Title { get; set; }
        public DataGridViewModelBase<T> DataGridViewModelBase { get; set; }
        public PaginationViewModel PaginationViewModel { get; private set; }
        public BindableCollection<MenuItemModel> MenuItems { get; set; }

        private string _numberOfResults;

        public string NumberOfResults
        {
            get
            {
                return _numberOfResults;
            }
            set
            {
                _numberOfResults = value;
                NotifyOfPropertyChange(() => NumberOfResults);
            }
        }

        private ICommand _menuCommand;

        public ICommand MenuCommand
        {
            get
            {
                if (_menuCommand == null)
                {
                    _menuCommand = new DelegateCommand(x => true, (parameter) =>
                     {
                         MenuItemModel menuItem = parameter as MenuItemModel;
                         menuItem.Action.Invoke();
                     });
                }
                return _menuCommand;
            }
        }

        private bool _menuVisibility;

        public bool MenuVisibility
        {
            get
            {
                return _menuVisibility;
            }
            set
            {
                _menuVisibility = value;
                NotifyOfPropertyChange(() => MenuVisibility);
            }
        }

        public void Refresh(PagedList<T> pagedList)
        {
            if (pagedList.PageCriteria.Page == PaginationConstants.FirstPage)
            {
                PaginationViewModel.CurrentPage = PaginationConstants.FirstPage;
            }

            PaginationViewModel.Refresh(pagedList.HasPrevious, pagedList.HasNext, pagedList.TotalPages);
            DataGridViewModelBase.Items = new BindableCollection<T>(pagedList.Subset);
            NumberOfResults = $"Broj rezultata: {pagedList.TotalCount}";
        }
    }
}
