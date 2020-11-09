using ArhivskiVjesnikLibrary.BLL.DTO;
using ArhivskiVjesnikLibrary.BLL.Services.Interfaces;
using ArhivskiVjesnikLibrary.Common.Extensions;
using ArhivskiVjesnikLibrary.Common.Helpers;
using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Models;
using ArhivskiVjesnikWPF.Commands;
using ArhivskiVjesnikWPF.Events;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace ArhivskiVjesnikWPF.ViewModels
{
    public class SearchEntryViewModel : Screen
    {
        private IEventAggregator _eventAggregator;
        private Dictionary<Type, System.Action> dictTypes;

        public SearchEntryViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            PopulateVrsteKriterija();
            PopulateStupci();
            PopulateDictTypes();
        }

        private void PopulateVrsteKriterija()
        {
            IEnumerable<KeyValuePair<SearchCriteriaType?, string>> searchCriteriaTypes = ListHelper.ToNullableEnumValuesAndDescriptions<SearchCriteriaType>();
            VrsteKriterija = new BindableCollection<KeyValuePair<SearchCriteriaType?, string>>(searchCriteriaTypes);
        }

        private void PopulateStupci()
        {
            List<string> keys = new List<string> { "Clanak.IDClanak", "Clanak.Naziv", "Clanak.Godiste", "Clanak.Broj", "Clanak.Volumen", "Clanak.DatumIzdavanja", "Clanak.DatumObjave", "Clanak.URL",
                                                    "Autor.Ime+Autor.Prezime", "KljucnaRijec.Vrijednost", "Naslov.Naziv", "Sazetak.Opis", "Vrsta.Naziv" };
            List<string> values = new List<string> { "ID Članak", "Naziv članka", "Godište članka", "Broj članka", "Volumen članka", "Datum izdavanja članka", "Datum objave članka", "URL članka", "Autor",
                                                        "Ključna riječ", "Naslov članka", "Sažetak članka", "Vrsta članka" };

            IEnumerable<KeyValuePair<string, string>> stupci = ListHelper.ToKeyValuePairs(keys, values);
            Stupci = new BindableCollection<KeyValuePair<string, string>>(stupci);
        }

        private void PopulateDictTypes()
        {
            dictTypes = new Dictionary<Type, System.Action>();
            dictTypes.Add(typeof(Autor), PopulateItems<Autor, AutorDto>);
            dictTypes.Add(typeof(Clanak), PopulateItems<Clanak, ClanakDto>);
            dictTypes.Add(typeof(KljucnaRijec), PopulateItems<KljucnaRijec, KljucnaRijecDto>);
            dictTypes.Add(typeof(Naslov), PopulateItems<Naslov, NaslovDto>);
            dictTypes.Add(typeof(Sazetak), PopulateItems<Sazetak, SazetakDto>);
            dictTypes.Add(typeof(Vrsta), PopulateItems<Vrsta, VrstaDto>);
        }

        public BindableCollection<KeyValuePair<SearchCriteriaType?, string>> VrsteKriterija { get; set; }

        private KeyValuePair<SearchCriteriaType?, string> _selectedVrstaKriterija;

        public KeyValuePair<SearchCriteriaType?, string> SelectedVrstaKriterija
        {
            get { return _selectedVrstaKriterija; }
            set
            {
                _selectedVrstaKriterija = value;
                NotifyOfPropertyChange(() => SelectedVrstaKriterija);
            }
        }

        public BindableCollection<KeyValuePair<string, string>> Stupci { get; set; }

        private KeyValuePair<string, string> _selectedStupac;

        public KeyValuePair<string, string> SelectedStupac
        {
            get { return _selectedStupac; }
            set
            {
                _selectedStupac = value;
                NotifyOfPropertyChange(() => SelectedStupac);

                Operacije = new BindableCollection<KeyValuePair<SearchOperation?, string>>(SearchOperationHelper.GetSearchOperationsByPropertyName(SelectedStupac.Key).ToEnumKeyValuePairs());
                NotifyOfPropertyChange(() => Operacije);
                NotifyOfPropertyChange(() => SelectedOperacija);


                if (Items != null)
                {
                    Items.Clear();
                    NotifyOfPropertyChange(() => Items);
                }

                Term = string.Empty;
                NotifyOfPropertyChange(() => Term);
            }
        }

        private BindableCollection<KeyValuePair<SearchOperation?, string>> _operacije;

        public BindableCollection<KeyValuePair<SearchOperation?, string>> Operacije
        {
            get { return _operacije; }
            set
            {
                _operacije = value;
                NotifyOfPropertyChange(() => Operacije);
            }
        }

        private KeyValuePair<SearchOperation?, string> _selectedOperacija;

        public KeyValuePair<SearchOperation?, string> SelectedOperacija
        {
            get { return _selectedOperacija; }
            set
            {
                _selectedOperacija = value;
                NotifyOfPropertyChange(() => SelectedOperacija);
            }
        }

        private BindableCollection<string> _items;

        public BindableCollection<string> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                NotifyOfPropertyChange(() => Items);
            }
        }

        private string _selectedItem;

        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                NotifyOfPropertyChange(() => SelectedItem);

                Term = SelectedItem;
                NotifyOfPropertyChange(() => Term);
            }
        }

        private bool _isOpen;

        public bool IsOpen
        {
            get { return _isOpen; }
            set
            {
                _isOpen = value;
                NotifyOfPropertyChange(() => IsOpen);
            }
        }

        private string _term;

        public string Term
        {
            get { return _term; }
            set
            {
                _term = value;
                NotifyOfPropertyChange(() => Term);
            }
        }

        private ICommand _removeCommand;

        public ICommand RemoveCommand
        {
            get
            {
                if (_removeCommand == null)
                {
                    _removeCommand = new DelegateCommand(x => true, (parameter) =>
                    {
                        _eventAggregator.PublishOnUIThread(new SearchCriteriaDeleteClickedEvent() { SearchEntryVM = this });
                    });
                }
                return _removeCommand;
            }
        }

        public void ExecuteFilteringItems(ActionExecutionContext actionExecutionContext)
        {
            var keyArgs = actionExecutionContext.EventArgs as KeyEventArgs;

            if (!SelectedStupac.Key.IsNullOrEmpty())
            {
                if (keyArgs.Key != Key.Down || keyArgs.Key != Key.Up || keyArgs.Key != Key.Left || keyArgs.Key != Key.Right || keyArgs.Key != Key.Enter)
                {
                    IsOpen = true;
                    if (SelectedItem != null)
                    {
                        SelectedItem = null;
                    }

                    string assemblyName = nameof(ArhivskiVjesnikLibrary);
                    string typeName = $"ArhivskiVjesnikLibrary.DAL.Models.{SelectedStupac.Key.Split('.').FirstOrDefault()}";
                    Type type = Type.GetType(Assembly.CreateQualifiedName(assemblyName, typeName));
                    dictTypes[type].Invoke();
                }
            }
        }

        private void PopulateItems<TModel, TDto>() where TModel : class where TDto : class
        {
            IGenericService<TModel, TDto> genericService = IoC.Get<IGenericService<TModel, TDto>>();
            IEnumerable<string> list = genericService.GetAll(SelectedStupac.Key, Term);
            Items = new BindableCollection<string>(list);
        }
    }
}
