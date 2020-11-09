using ArhivskiVjesnikLibrary.BLL.DTO;
using ArhivskiVjesnikLibrary.BLL.Services.Interfaces;
using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikWPF.Events;
using ArhivskiVjesnikWPF.Managers.Interfaces;
using ArhivskiVjesnikWPF.Models;
using Caliburn.Micro;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ArhivskiVjesnikWPF.ViewModels
{
    public class ComprehensiveDetaljiClankaViewModel : Screen, IHandle<ComprehensiveDetaljiClankaClickedEvent>
    {
        private IEventAggregator _eventAggregator;
        private IExtendedWindowManager _extendedWindowManager;
        private IAutorService _autorService;
        private IKljucnaRijecService _kljucnaRijecService;
        private INaslovService _naslovService;
        private ISazetakService _satetakService;
        private IVrstaService _vrstaService;
        private PagedList<AutorDto> autorPagedList;
        private PagedList<KljucnaRijecDto> kljucnaRijecPagedList;
        private PagedList<NaslovDto> naslovPagedList;
        private PagedList<SazetakDto> sazetakPagedList;
        private PagedList<VrstaDto> vrstaPagedList;

        public ComprehensiveDetaljiClankaViewModel(IEventAggregator eventAggregator, IExtendedWindowManager extendedWindowManager, IAutorService autorService, IKljucnaRijecService kljucnaRijecService,
                                        INaslovService naslovService, ISazetakService sazetakService, IVrstaService vrstaService, CommandHandlerViewModel commandHandlerViewModel)
        {
            _eventAggregator = eventAggregator;
            _extendedWindowManager = extendedWindowManager;
            _autorService = autorService;
            _kljucnaRijecService = kljucnaRijecService;
            _naslovService = naslovService;
            _satetakService = sazetakService;
            _vrstaService = vrstaService;
            URLCommand = commandHandlerViewModel.URLCommand;

            _eventAggregator.Subscribe(this);
        }

        public BasicDetaljiClankaViewModel BasicDetaljiClankaViewModel { get; set; }
        public PageableDataGridViewModel<AutorDto> AutoriPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<KljucnaRijecDto> KljucneRijeciPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<NaslovDto> NasloviPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<SazetakDto> SazetciPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<VrstaDto> VrstePageableDataGridViewModel { get; set; }
        public ICommand URLCommand { get; set; }

        public void NatragNaClanke()
        {
            _eventAggregator.PublishOnUIThread(new NatragNaClankeClickedEvent());
        }

        public async void Handle(ComprehensiveDetaljiClankaClickedEvent message)
        {
            ClanakDto clanakDto = message.Clanak;
            int idClanak = message.Clanak.IDClanak;

            InitAndPopulateBasicDetaljiClankaViewModel(clanakDto);
            InitAutoriPageableDataGridViewModel(idClanak);
            InitKljucneRijeciPageableDataGridViewModel(idClanak);
            InitNasloviPageableDataGridViewModel(idClanak);
            InitSazetciPageableDataGridViewModel(idClanak);
            InitVrstePageableDataGridViewModel(idClanak);

            await PopulateDataAsync(idClanak);
        }

        private void InitAndPopulateBasicDetaljiClankaViewModel(ClanakDto clanakDto)
        {
            BasicDetaljiClankaViewModel = IoC.Get<BasicDetaljiClankaViewModel>();
            BasicDetaljiClankaViewModel.InitDto(clanakDto);
        }

        private void InitAutoriPageableDataGridViewModel(int idClanak)
        {
            AutoriPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<AutorDto>>();
            AutoriPageableDataGridViewModel.Title = "Autori";
            AutoriPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<AutoriDataGridViewModel>();
            AutoriPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<AutorDto>
            {
                Name = "Osnovni detalji",
                Action = (autorDto) => _extendedWindowManager.ShowDialog<BasicDetaljiAutoraViewModel, AutorDto>(autorDto)
            });
            AutoriPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateAutori(idClanak), "Dohvaćanje autora...", "Desila se greška prilikom dohvaćanja autora.");
        }

        private void InitKljucneRijeciPageableDataGridViewModel(int idClanak)
        {
            KljucneRijeciPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<KljucnaRijecDto>>();
            KljucneRijeciPageableDataGridViewModel.Title = "Ključne riječi";
            KljucneRijeciPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<KljucneRijeciDataGridViewModel>();
            KljucneRijeciPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<KljucnaRijecDto>
            {
                Name = "Osnovni detalji",
                Action = (kljucneRijeciDto) => _extendedWindowManager.ShowDialog<BasicDetaljiKljucneRijeciViewModel, KljucnaRijecDto>(kljucneRijeciDto)
            });
            KljucneRijeciPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateKljucneRijeci(idClanak), "Dohvaćanje ključnih riječi...", "Desila se greška prilikom dohvaćanja ključinih riječi.");
        }

        private void InitNasloviPageableDataGridViewModel(int idClanak)
        {
            NasloviPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<NaslovDto>>();
            NasloviPageableDataGridViewModel.Title = "Naslovi";
            NasloviPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<NasloviDataGridViewModel>();
            NasloviPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<NaslovDto>
            {
                Name = "Osnovni detalji",
                Action = (naslovDto) => _extendedWindowManager.ShowDialog<BasicDetaljiNaslovaViewModel, NaslovDto>(naslovDto)
            });
            NasloviPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateNaslovi(idClanak), "Dohvaćanje naslova...", "Desila se greška prilikom dohvaćanja naslova.");
        }

        private void InitSazetciPageableDataGridViewModel(int idClanak)
        {
            SazetciPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<SazetakDto>>();
            SazetciPageableDataGridViewModel.Title = "Sažetci";
            SazetciPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<SazetciDataGridViewModel>();
            SazetciPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<SazetakDto>
            {
                Name = "Osnovni detalji",
                Action = (sazetakDto) => _extendedWindowManager.ShowDialog<BasicDetaljiSazetkaViewModel, SazetakDto>(sazetakDto)
            });
            SazetciPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateSazetci(idClanak), "Dohvaćanje sažetaka...", "Desila se greška prilikom dohvaćanja sažetaka.");
        }

        private void InitVrstePageableDataGridViewModel(int idClanak)
        {
            VrstePageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<VrstaDto>>();
            VrstePageableDataGridViewModel.Title = "Vrste";
            VrstePageableDataGridViewModel.DataGridViewModelBase = IoC.Get<VrsteDataGridViewModel>();
            VrstePageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<VrstaDto>
            {
                Name = "Osnovni detalji",
                Action = (vrstaDto) => _extendedWindowManager.ShowDialog<BasicDetaljiVrsteViewModel, VrstaDto>(vrstaDto)
            });
            VrstePageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateVrste(idClanak), "Dohvaćanje vrsta...", "Desila se greška prilikom dohvaćanja vrsta.");
        }

        private async Task PopulateDataAsync(int idClanak)
        {
            await _extendedWindowManager.ShowLoadingDialogAsync(() =>
            {
                PopulateAutori(idClanak);
                PopulateKljucneRijeci(idClanak);
                PopulateNaslovi(idClanak);
                PopulateSazetci(idClanak);
                PopulateVrste(idClanak);
            }, "Dohvaćanje podataka...", "Desila se greška prilikom dohvaćanja podataka.");
        }

        private void PopulateAutori(int idClanak)
        {
            autorPagedList = _autorService.GetAllByClanakID(idClanak, new PageCriteria { Page = AutoriPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            AutoriPageableDataGridViewModel.Refresh(autorPagedList);
        }

        private void PopulateKljucneRijeci(int idClanak)
        {
            kljucnaRijecPagedList = _kljucnaRijecService.GetAllByClanakID(idClanak, new PageCriteria { Page = KljucneRijeciPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            KljucneRijeciPageableDataGridViewModel.Refresh(kljucnaRijecPagedList);
        }

        private void PopulateNaslovi(int idClanak)
        {
            naslovPagedList = _naslovService.GetAllByClanakID(idClanak, new PageCriteria { Page = NasloviPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            NasloviPageableDataGridViewModel.Refresh(naslovPagedList);
        }

        private void PopulateSazetci(int idClanak)
        {
            sazetakPagedList = _satetakService.GetAllByClanakID(idClanak, new PageCriteria { Page = SazetciPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            SazetciPageableDataGridViewModel.Refresh(sazetakPagedList);
        }

        private void PopulateVrste(int idClanak)
        {
            vrstaPagedList = _vrstaService.GetAllByClanakID(idClanak, new PageCriteria { Page = VrstePageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            VrstePageableDataGridViewModel.Refresh(vrstaPagedList);
        }
    }
}
