using ArhivskiVjesnikLibrary.BLL.DTO;
using ArhivskiVjesnikLibrary.BLL.Services.Interfaces;
using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikWPF.Events;
using ArhivskiVjesnikWPF.Managers.Interfaces;
using ArhivskiVjesnikWPF.Models;
using Caliburn.Micro;
using System.Threading.Tasks;

namespace ArhivskiVjesnikWPF.ViewModels
{
    public class ComprehensiveDetaljiSazetkaViewModel : Screen, IHandle<ComprehensiveDetaljiSazetkaClickedEvent>
    {
        private IEventAggregator _eventAggregator;
        private IExtendedWindowManager _extendedWindowManager;
        private IAutorService _autorService;
        private IClanakService _clanakService;
        private IKljucnaRijecService _kljucnaRijecService;
        private INaslovService _naslovService;
        private IVrstaService _vrstaService;
        private PagedList<AutorDto> autorPagedList;
        private PagedList<ClanakDto> clanakPagedList;
        private PagedList<KljucnaRijecDto> kljucnaRjecPagedList;
        private PagedList<NaslovDto> naslovPagedList;
        private PagedList<VrstaDto> vrstaPagedList;

        public ComprehensiveDetaljiSazetkaViewModel(IEventAggregator eventAggregator, IExtendedWindowManager extendedWindowManager, IAutorService autorService, IClanakService clanakService,
                                                        IKljucnaRijecService kljucnaRijecService, INaslovService naslovService, IVrstaService vrstaService)
        {
            _eventAggregator = eventAggregator;
            _extendedWindowManager = extendedWindowManager;
            _autorService = autorService;
            _clanakService = clanakService;
            _kljucnaRijecService = kljucnaRijecService;
            _naslovService = naslovService;
            _vrstaService = vrstaService;

            _eventAggregator.Subscribe(this);
        }

        public BasicDetaljiSazetkaViewModel BasicDetaljiSazetkaViewModel { get; set; }
        public PageableDataGridViewModel<AutorDto> AutoriPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<ClanakDto> ClanciPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<KljucnaRijecDto> KljucneRijeciPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<NaslovDto> NasloviPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<VrstaDto> VrstePageableDataGridViewModel { get; set; }

        public void NatragNaSazetke()
        {
            _eventAggregator.PublishOnUIThread(new NatragNaSazetkeClickedEvent());
        }

        public async void Handle(ComprehensiveDetaljiSazetkaClickedEvent message)
        {
            SazetakDto sazetakDto = message.Sazetak;
            int idSazetak = sazetakDto.IDSazetak;

            InitAndPopulateBasicDetaljiSazetkaViewModel(sazetakDto);
            InitAutoriPageableDataGridViewModel(idSazetak);
            InitClanciPageableDataGridViewModel(idSazetak);
            InitKljucneRijeciPageableDataGridViewModel(idSazetak);
            InitNasloviPageableDataGridViewModel(idSazetak);
            InitVrstePageableDataGridViewModel(idSazetak);

            await PopulateDataAsync(idSazetak);
        }

        private void InitAndPopulateBasicDetaljiSazetkaViewModel(SazetakDto sazetakDto)
        {
            BasicDetaljiSazetkaViewModel = IoC.Get<BasicDetaljiSazetkaViewModel>();
            BasicDetaljiSazetkaViewModel.InitDto(sazetakDto);
        }

        private void InitAutoriPageableDataGridViewModel(int idSazetak)
        {
            AutoriPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<AutorDto>>();
            AutoriPageableDataGridViewModel.Title = "Autori";
            AutoriPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<AutoriDataGridViewModel>();
            AutoriPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<AutorDto>
            {
                Name = "Osnovni detalji",
                Action = (autorDto) => _extendedWindowManager.ShowDialog<BasicDetaljiAutoraViewModel, AutorDto>(autorDto)
            });
            AutoriPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateAutori(idSazetak), "Dohvaćanje autora...", "Desila se greška prilikom dohvaćanja autora.");
        }

        private void InitClanciPageableDataGridViewModel(int idSazetak)
        {
            ClanciPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<ClanakDto>>();
            ClanciPageableDataGridViewModel.Title = "Članci";
            ClanciPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<ClanciDataGridViewModel>();
            ClanciPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<ClanakDto>
            {
                Name = "Osnovni detalji",
                Action = (clanakDto) => _extendedWindowManager.ShowDialog<BasicDetaljiClankaViewModel, ClanakDto>(clanakDto)
            });
            ClanciPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateClanci(idSazetak), "Dohvaćanje članaka...", "Desila se greška prilikom dohvaćanja članaka.");
        }

        private void InitKljucneRijeciPageableDataGridViewModel(int idSazetak)
        {
            KljucneRijeciPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<KljucnaRijecDto>>();
            KljucneRijeciPageableDataGridViewModel.Title = "Ključne riječi";
            KljucneRijeciPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<KljucneRijeciDataGridViewModel>();
            KljucneRijeciPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<KljucnaRijecDto>
            {
                Name = "Osnovni detalji",
                Action = (kljucnaRijecDto) => _extendedWindowManager.ShowDialog<BasicDetaljiKljucneRijeciViewModel, KljucnaRijecDto>(kljucnaRijecDto)
            });
            KljucneRijeciPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateKljucneRijeci(idSazetak), "Dohvaćanje ključnih riječi...", "Desila se greška prilikom dohvaćanja ključnih riječi.");
        }

        private void InitNasloviPageableDataGridViewModel(int idSazetak)
        {
            NasloviPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<NaslovDto>>();
            NasloviPageableDataGridViewModel.Title = "Naslovi";
            NasloviPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<NasloviDataGridViewModel>();
            NasloviPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<NaslovDto>
            {
                Name = "Osnovni detalji",
                Action = (naslovDto) => _extendedWindowManager.ShowDialog<BasicDetaljiNaslovaViewModel, NaslovDto>(naslovDto)
            });
            NasloviPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateNaslovi(idSazetak), "Dohvaćanje naslova...", "Desila se greška prilikom dohvaćanja naslova.");
        }

        private void InitVrstePageableDataGridViewModel(int idSazetak)
        {
            VrstePageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<VrstaDto>>();
            VrstePageableDataGridViewModel.Title = "Vrste";
            VrstePageableDataGridViewModel.DataGridViewModelBase = IoC.Get<VrsteDataGridViewModel>();
            VrstePageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<VrstaDto>
            {
                Name = "Osnovni detalji",
                Action = (vrstaDto) => _extendedWindowManager.ShowDialog<BasicDetaljiVrsteViewModel, VrstaDto>(vrstaDto)
            });
            VrstePageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateVrste(idSazetak), "Dohvaćanje vrsta...", "Desila se greška prilikom dohvaćanja vrsta.");
        }

        private async Task PopulateDataAsync(int idSazetak)
        {
            await _extendedWindowManager.ShowLoadingDialogAsync(() =>
            {
                PopulateAutori(idSazetak);
                PopulateClanci(idSazetak);
                PopulateKljucneRijeci(idSazetak);
                PopulateNaslovi(idSazetak);
                PopulateVrste(idSazetak);
            }, "Dohvaćanje podataka...", "Desila se greška prilikom dohvaćanja podataka.");
        }

        private void PopulateAutori(int idSazetak)
        {
            autorPagedList = _autorService.GetAllBySazetakID(idSazetak, new PageCriteria { Page = AutoriPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            AutoriPageableDataGridViewModel.Refresh(autorPagedList);
        }

        private void PopulateClanci(int idSazetak)
        {
            clanakPagedList = _clanakService.GetAllBySazetakID(idSazetak, new PageCriteria { Page = ClanciPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            ClanciPageableDataGridViewModel.Refresh(clanakPagedList);
        }

        private void PopulateKljucneRijeci(int idSazetak)
        {
            kljucnaRjecPagedList = _kljucnaRijecService.GetAllBySazetakID(idSazetak, new PageCriteria { Page = KljucneRijeciPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            KljucneRijeciPageableDataGridViewModel.Refresh(kljucnaRjecPagedList);
        }

        private void PopulateNaslovi(int idSazetak)
        {
            naslovPagedList = _naslovService.GetAllBySazetakID(idSazetak, new PageCriteria { Page = NasloviPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            NasloviPageableDataGridViewModel.Refresh(naslovPagedList);
        }

        private void PopulateVrste(int idSazetak)
        {
            vrstaPagedList = _vrstaService.GetAllBySazetakID(idSazetak, new PageCriteria { Page = VrstePageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            VrstePageableDataGridViewModel.Refresh(vrstaPagedList);
        }
    }
}
