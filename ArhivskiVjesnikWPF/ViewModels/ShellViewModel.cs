using ArhivskiVjesnikWPF.Events;
using ArhivskiVjesnikWPF.Helpers;
using ArhivskiVjesnikWPF.Models;
using Caliburn.Micro;
using System.Collections.Generic;

namespace ArhivskiVjesnikWPF.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<ComprehensiveDetaljiAutoraClickedEvent>, IHandle<NatragNaAutoreClickedEvent>, IHandle<ComprehensiveDetaljiClankaClickedEvent>,
                                    IHandle<NatragNaClankeClickedEvent>, IHandle<ComprehensiveDetaljiKljucneRijeciClickedEvent>, IHandle<NatragNaKljucneRijeciClickedEvent>,
                                    IHandle<ComprehensiveDetaljiNaslovaClickedEvent>, IHandle<NatragNaNasloveClickedEvent>, IHandle<ComprehensiveDetaljiSazetkaClickedEvent>,
                                    IHandle<NatragNaSazetkeClickedEvent>, IHandle<ComprehensiveDetaljiVrsteClickedEvent>, IHandle<NatragNaVrsteClickedEvent>
    {
        private IEventAggregator _eventAggregator;
        private ComprehensiveDetaljiAutoraViewModel _comprehensiveDetaljiAutoraViewModel;
        private AutoriViewModel _autoriViewModel;
        private ComprehensiveDetaljiClankaViewModel _comprehensiveDetaljiClankaViewModel;
        private ClanciViewModel _clanciViewModel;
        private ComprehensiveDetaljiKljucneRijeciViewModel _comprehensiveDetaljiKljucneRijeciViewModel;
        private KljucneRijeciViewModel _kljucneRijeciViewModel;
        private ComprehensiveDetaljiNaslovaViewModel _comprehensiveDetaljiNaslovaViewModel;
        private NasloviViewModel _nasloviViewModel;
        private ComprehensiveDetaljiSazetkaViewModel _comprehensiveDetaljiSazetkaViewModel;
        private SazetciViewModel _sazetciViewModel;
        private ComprehensiveDetaljiVrsteViewModel _comprehensiveDetaljiVrsteViewModel;
        private VrsteViewModel _vrsteViewModel;

        public ShellViewModel(IEventAggregator eventAggregator, ComprehensiveDetaljiAutoraViewModel comprehensiveDetaljiAutoraViewModel,
                                AutoriViewModel autori, ComprehensiveDetaljiClankaViewModel comprehensiveDetaljiClankaViewModel, ClanciViewModel clanciViewModel,
                                ComprehensiveDetaljiKljucneRijeciViewModel comprehensiveDetaljiKljucneRijeciViewModel, KljucneRijeciViewModel kljucneRijeciViewModel,
                                ComprehensiveDetaljiNaslovaViewModel comprehensiveDetaljiNaslovaViewModel, NasloviViewModel nasloviViewModel,
                                ComprehensiveDetaljiSazetkaViewModel comprehensiveDetaljiSazetkaViewModel, SazetciViewModel sazetciViewModel,
                                ComprehensiveDetaljiVrsteViewModel comprehensiveDetaljiVrsteViewModel, VrsteViewModel vrsteViewModel)
        {
            _eventAggregator = eventAggregator;
            _comprehensiveDetaljiAutoraViewModel = comprehensiveDetaljiAutoraViewModel;
            _autoriViewModel = autori;
            _comprehensiveDetaljiClankaViewModel = comprehensiveDetaljiClankaViewModel;
            _clanciViewModel = clanciViewModel;
            _comprehensiveDetaljiKljucneRijeciViewModel = comprehensiveDetaljiKljucneRijeciViewModel;
            _kljucneRijeciViewModel = kljucneRijeciViewModel;
            _comprehensiveDetaljiNaslovaViewModel = comprehensiveDetaljiNaslovaViewModel;
            _nasloviViewModel = nasloviViewModel;
            _comprehensiveDetaljiSazetkaViewModel = comprehensiveDetaljiSazetkaViewModel;
            _sazetciViewModel = sazetciViewModel;
            _comprehensiveDetaljiVrsteViewModel = comprehensiveDetaljiVrsteViewModel;
            _vrsteViewModel = vrsteViewModel;

            _eventAggregator.Subscribe(this);

            SetTitles();
            PopulateDrawerItems();
            ActivateItem(DrawerItems[1].ViewModel);
        }

        private void SetTitles()
        {
            string title = $"Arhivski Vjesnik v.{ApplicationHelper.GetVersionNumber()}";
            WindowTitle = title;
            MainTitle = title;
        }

        private void PopulateDrawerItems()
        {
            List<DrawerItemModel> drawerItems = new List<DrawerItemModel>
            {
                new DrawerItemModel { Name = "Autori", ViewModel = _autoriViewModel },
                new DrawerItemModel { Name = "Članci", ViewModel = _clanciViewModel },
                new DrawerItemModel { Name = "Ključne riječi", ViewModel = _kljucneRijeciViewModel },
                new DrawerItemModel { Name = "Naslovi", ViewModel = _nasloviViewModel },
                new DrawerItemModel { Name = "Sažetci", ViewModel = _sazetciViewModel },
                new DrawerItemModel { Name = "Vrste", ViewModel = _vrsteViewModel }
            };
            DrawerItems = new BindableCollection<DrawerItemModel>(drawerItems);
            SelectedDrawerItem = DrawerItems[1];
        }

        public string WindowTitle { get; set; }
        public string MainTitle { get; set; }
        public BindableCollection<DrawerItemModel> DrawerItems { get; set; }

        private DrawerItemModel _selectedDrawerItem;

        public DrawerItemModel SelectedDrawerItem
        {
            get { return _selectedDrawerItem; }
            set
            {
                _selectedDrawerItem = value;
                NotifyOfPropertyChange(() => SelectedDrawerItem);
                ActivateItem(SelectedDrawerItem.ViewModel);
            }
        }

        public void Handle(ComprehensiveDetaljiAutoraClickedEvent message)
        {
            ActivateItem(_comprehensiveDetaljiAutoraViewModel);
        }

        public void Handle(NatragNaAutoreClickedEvent message)
        {
            ActivateItem(_autoriViewModel);
        }

        public void Handle(ComprehensiveDetaljiClankaClickedEvent message)
        {
            ActivateItem(_comprehensiveDetaljiClankaViewModel);
        }

        public void Handle(NatragNaClankeClickedEvent message)
        {
            ActivateItem(_clanciViewModel);
        }

        public void Handle(ComprehensiveDetaljiKljucneRijeciClickedEvent message)
        {
            ActivateItem(_comprehensiveDetaljiKljucneRijeciViewModel);
        }

        public void Handle(NatragNaKljucneRijeciClickedEvent message)
        {
            ActivateItem(_kljucneRijeciViewModel);
        }

        public void Handle(ComprehensiveDetaljiNaslovaClickedEvent message)
        {
            ActivateItem(_comprehensiveDetaljiNaslovaViewModel);
        }

        public void Handle(NatragNaNasloveClickedEvent message)
        {
            ActivateItem(_nasloviViewModel);
        }

        public void Handle(ComprehensiveDetaljiSazetkaClickedEvent message)
        {
            ActivateItem(_comprehensiveDetaljiSazetkaViewModel);
        }

        public void Handle(NatragNaSazetkeClickedEvent message)
        {
            ActivateItem(_sazetciViewModel);
        }

        public void Handle(ComprehensiveDetaljiVrsteClickedEvent message)
        {
            ActivateItem(_comprehensiveDetaljiVrsteViewModel);
        }

        public void Handle(NatragNaVrsteClickedEvent message)
        {
            ActivateItem(_vrsteViewModel);
        }
    }
}
