using ArhivskiVjesnikWPF.Commands;
using ArhivskiVjesnikWPF.Models;
using Caliburn.Micro;
using System.Windows.Input;

namespace ArhivskiVjesnikWPF.ViewModels
{
    public abstract class DataGridViewModelBase<T> : Screen
    {
        public DataGridViewModelBase()
        {
            ActionItems = new BindableCollection<ActionItemModel<T>>();
            MenuVisibility = false;
            ActionItems.CollectionChanged += ActionItems_CollectionChanged;
        }

        private void ActionItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (ActionItems.Count == 0)
            {
                MenuVisibility = false;
            }
            else
            {
                MenuVisibility = true;
            }
        }

        private BindableCollection<T> _items;

        public BindableCollection<T> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                NotifyOfPropertyChange(() => Items);
            }
        }

        private T _selectedItem;

        public T SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                NotifyOfPropertyChange(() => SelectedItem);
            }
        }

        public BindableCollection<ActionItemModel<T>> ActionItems { get; set; }

        private ICommand _actionCommand;

        public ICommand ActionCommand
        {
            get
            {
                if (_actionCommand == null)
                {
                    _actionCommand = new DelegateCommand(x => true, (parameter) =>
                    {
                        ActionItemModel<T> actionItem = parameter as ActionItemModel<T>;
                        actionItem.Action.Invoke(SelectedItem);
                    });
                }
                return _actionCommand;
            }
        }

        private bool _menuVisibility;

        public bool MenuVisibility
        {
            get { return _menuVisibility; }
            set
            {
                _menuVisibility = value;
                NotifyOfPropertyChange(() => MenuVisibility);
            }
        }
    }
}
