using ArhivskiVjesnikLibrary.Common.Extensions;
using ArhivskiVjesnikLibrary.Common.Helpers;
using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikWPF.Enums;
using ArhivskiVjesnikWPF.Events;
using ArhivskiVjesnikWPF.Extensions;
using Caliburn.Micro;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ArhivskiVjesnikWPF.ViewModels
{
    public class NaprednaPretragaViewModel : Screen, IHandle<SearchCriteriaDeleteClickedEvent>, IDialogResultViewModel
    {
        private const int MaxNumberOfSearchCriterias = 10;

        private IEventAggregator _eventAggregator;
        private QueryCriteria queryCriteria;

        public NaprednaPretragaViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _eventAggregator.Subscribe(this);

            SearchEntryViewModelList = new ObservableCollection<SearchEntryViewModel>();

            PopulateOperators();
            PopulateSortDirections();
            PopulateSizes();

            queryCriteria = new QueryCriteria
            {
                SearchCriterias = new List<SearchCriteria>(),
                Operator = Operator.And,
                SortCriteria = new SortCriteria { ColumnName = string.Empty, SortDirection = SortDirection.Ascending },
                PageCriteria = new PageCriteria { Page = 1, Size = SelectedPageSize }
            };
            DialogResult = DialogResult.Cancel;
        }

        private void PopulateOperators()
        {
            IEnumerable<KeyValuePair<Operator, string>> operatorList = ListHelper.ToEnumValuesAndDescriptions<Operator>();
            Operators = new BindableCollection<KeyValuePair<Operator, string>>(operatorList);
            SelectedOperator = Operators[0];
        }

        private void PopulateSortDirections()
        {
            IEnumerable<KeyValuePair<SortDirection, string>> sortDirectionList = ListHelper.ToEnumValuesAndDescriptions<SortDirection>();
            SortDirections = new BindableCollection<KeyValuePair<SortDirection, string>>(sortDirectionList);
            SelectedSortDirection = SortDirections[0];
        }

        private void PopulateSizes()
        {
            PageSizes = new BindableCollection<int>(new List<int> { 10, 20, 50 });
            SelectedPageSize = PageSizes[0];
        }

        public ObservableCollection<SearchEntryViewModel> SearchEntryViewModelList { get; set; }

        public BindableCollection<KeyValuePair<Operator, string>> Operators { get; set; }

        private KeyValuePair<Operator, string> _selectedOperator;

        public KeyValuePair<Operator, string> SelectedOperator
        {
            get { return _selectedOperator; }
            set
            {
                _selectedOperator = value;
                NotifyOfPropertyChange(() => SelectedOperator);
            }
        }

        public BindableCollection<KeyValuePair<string, string>> StupciZaSortiranje { get; set; }

        private KeyValuePair<string, string> _selectedStupacZaSortiranje;

        public KeyValuePair<string, string> SelectedStupacZaSortiranje
        {
            get { return _selectedStupacZaSortiranje; }
            set
            {
                _selectedStupacZaSortiranje = value;
                NotifyOfPropertyChange(() => SelectedStupacZaSortiranje);
            }
        }

        public BindableCollection<KeyValuePair<SortDirection, string>> SortDirections { get; set; }

        private KeyValuePair<SortDirection, string> _selectedSortDirection;

        public KeyValuePair<SortDirection, string> SelectedSortDirection
        {
            get { return _selectedSortDirection; }
            set
            {
                _selectedSortDirection = value;
                NotifyOfPropertyChange(() => SelectedSortDirection);
            }
        }

        public BindableCollection<int> PageSizes { get; set; }

        private int _selectedPageSize;

        public int SelectedPageSize
        {
            get { return _selectedPageSize; }
            set
            {
                _selectedPageSize = value;
                NotifyOfPropertyChange(() => SelectedPageSize);
            }
        }

        private int _currentNumberOfSearchCriterias;

        public int CurrentNumberOfSearchCriterias
        {
            get { return _currentNumberOfSearchCriterias; }
            set
            {
                _currentNumberOfSearchCriterias = value;
                NotifyOfPropertyChange(() => CurrentNumberOfSearchCriterias);
                NotifyOfPropertyChange(() => CurrentAndMaxNumberOfSearchCriterias);
            }
        }

        public string CurrentAndMaxNumberOfSearchCriterias
        {
            get
            {
                return $"{CurrentNumberOfSearchCriterias}/{MaxNumberOfSearchCriterias}";
            }
        }

        public DialogResult DialogResult { get; private set; }

        public void Handle(SearchCriteriaDeleteClickedEvent message)
        {
            SearchEntryViewModelList.Remove(message.SearchEntryVM);
            CurrentNumberOfSearchCriterias = SearchEntryViewModelList.Count;
        }

        public void DodajSearchCriteria(int currentNumberOfSearchCriterias)
        {
            SearchEntryViewModelList.Add(IoC.Get<SearchEntryViewModel>());
            CurrentNumberOfSearchCriterias = SearchEntryViewModelList.Count;
        }


        public bool CanDodajSearchCriteria(int currentNumberOfSearchCriterias)
        {
            if (currentNumberOfSearchCriterias < MaxNumberOfSearchCriterias)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void IzbrisiSveSearchCriterias(int currentNumberOfSearchCriterias)
        {
            SearchEntryViewModelList.Clear();
            CurrentNumberOfSearchCriterias = SearchEntryViewModelList.Count;
        }

        public bool CanIzbrisiSveSearchCriterias(int currentNumberOfSearchCriterias)
        {
            if (currentNumberOfSearchCriterias > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Primjeni()
        {
            queryCriteria = new QueryCriteria
            {
                Operator = SelectedOperator.Key,
                SearchCriterias = PrepareSearchCriterias(),
                SortCriteria = PrepareSortCriteria(),
                PageCriteria = PreparePageCriteria()
            };
            DialogResult = DialogResult.OK;
            TryClose();
        }

        public void Resetiraj()
        {
            SelectedOperator = Operators[0];
            IzbrisiSveSearchCriterias(CurrentNumberOfSearchCriterias);
            SelectedStupacZaSortiranje = StupciZaSortiranje[0];
            SelectedSortDirection = SortDirections[0];
            SelectedPageSize = PageSizes[0];
        }

        private List<SearchCriteria> PrepareSearchCriterias()
        {
            return SearchEntryViewModelList.Select(x => x.ToSearchCriteria()).Where(x => x.IsValid()).ToList();
        }

        private SortCriteria PrepareSortCriteria()
        {
            return new SortCriteria
            {
                ColumnName = SelectedStupacZaSortiranje.Key,
                SortDirection = SelectedSortDirection.Key
            };
        }

        private PageCriteria PreparePageCriteria()
        {
            return new PageCriteria
            {
                Page = 1,
                Size = SelectedPageSize
            };
        }

        public QueryCriteria GetQueryCriteria()
        {
            return queryCriteria;
        }

        public void InitStupciZaSortiranje(IEnumerable<KeyValuePair<string, string>> stupciZaSortiranje)
        {
            if (CanStupciZaSortiranjeBeSetUp(stupciZaSortiranje))
            {
                StupciZaSortiranje = new BindableCollection<KeyValuePair<string, string>>(stupciZaSortiranje);
                SelectedStupacZaSortiranje = StupciZaSortiranje[0];
            }
        }

        private bool CanStupciZaSortiranjeBeSetUp(IEnumerable<KeyValuePair<string, string>> data)
        {
            bool condition = false;

            if (StupciZaSortiranje == null)
            {
                condition = true;
            }

            if (StupciZaSortiranje != null && !StupciZaSortiranje.SequenceEqual(data))
            {
                condition = true;
            }

            return condition;
        }
    }
}
