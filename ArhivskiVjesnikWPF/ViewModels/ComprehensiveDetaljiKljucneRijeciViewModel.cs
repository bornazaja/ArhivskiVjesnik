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
    public class ComprehensiveDetaljiKljucneRijeciViewModel : Screen, IHandle<ComprehensiveDetaljiKljucneRijeciClickedEvent>
    {
        private IEventAggregator _eventAggregator;
        private IExtendedWindowManager _extendedWindowManager;
        private IAutorService _autorService;
        private IClanakService _clanakService;
        private INaslovService _naslovService;
        private ISazetakService _sazetakService;
        private IVrstaService _vrstaService;
        private PagedList<AutorDto> autorPagedList;
        private PagedList<ClanakDto> clanakPagedList;
        private PagedList<NaslovDto> naslovPagedList;
        private PagedList<SazetakDto> sazetakPagedList;
        private PagedList<VrstaDto> vrstaPagedList;

        public ComprehensiveDetaljiKljucneRijeciViewModel(IEventAggregator eventAggregator, IExtendedWindowManager extendedWindowManager, IAutorService autorService, IClanakService clanakService,
                                                            INaslovService naslovService, ISazetakService sazetakService, IVrstaService vrstaService)
        {
            _eventAggregator = eventAggregator;
            _extendedWindowManager = extendedWindowManager;
            _autorService = autorService;
            _clanakService = clanakService;
            _naslovService = naslovService;
            _sazetakService = sazetakService;
            _vrstaService = vrstaService;

            _eventAggregator.Subscribe(this);
        }

        public BasicDetaljiKljucneRijeciViewModel BasicDetaljiKljucneRijeciViewModel { get; set; }
        public PageableDataGridViewModel<AutorDto> AutoriPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<ClanakDto> ClanciPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<NaslovDto> NasloviPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<SazetakDto> SazetciPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<VrstaDto> VrstePageableDataGridViewModel { get; set; }

        public void NatragNaKljucneRijeci()
        {
            _eventAggregator.PublishOnUIThread(new NatragNaKljucneRijeciClickedEvent());
        }

        public async void Handle(ComprehensiveDetaljiKljucneRijeciClickedEvent message)
        {
            KljucnaRijecDto kljucnaRijecDto = message.KljucnaRijec;
            int idKljucnaRijec = kljucnaRijecDto.IDKljucnaRijec;

            InitAndPopulateBasicDetaljiKljucneRijeciViewModel(kljucnaRijecDto);
            InitAutoriPageableDataGridViewModel(idKljucnaRijec);
            InitClanciPageableDataGridViewModel(idKljucnaRijec);
            InitNasloviPageableDataGridViewModel(idKljucnaRijec);
            InitSazetciPageableDataGridViewModel(idKljucnaRijec);
            InitVrstePageableDataGridViewModel(idKljucnaRijec);

            await PopulateDataAsync(idKljucnaRijec);
        }

        private void InitAndPopulateBasicDetaljiKljucneRijeciViewModel(KljucnaRijecDto kljucnaRijecDto)
        {
            BasicDetaljiKljucneRijeciViewModel = IoC.Get<BasicDetaljiKljucneRijeciViewModel>();
            BasicDetaljiKljucneRijeciViewModel.InitDto(kljucnaRijecDto);
        }

        private void InitAutoriPageableDataGridViewModel(int idKljucnaRijec)
        {
            AutoriPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<AutorDto>>();
            AutoriPageableDataGridViewModel.Title = "Autori";
            AutoriPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<AutoriDataGridViewModel>();
            AutoriPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<AutorDto>
            {
                Name = "Osnovni detalji",
                Action = (autorDto) => _extendedWindowManager.ShowDialog<BasicDetaljiAutoraViewModel, AutorDto>(autorDto)
            });
            AutoriPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateAutori(idKljucnaRijec), "Dohvaćanje autora...", "Desila se greška prilikom dohvaćanja autora.");
        }

        private void InitClanciPageableDataGridViewModel(int idKljucnaRijec)
        {
            ClanciPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<ClanakDto>>();
            ClanciPageableDataGridViewModel.Title = "Članci";
            ClanciPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<ClanciDataGridViewModel>();
            ClanciPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<ClanakDto>
            {
                Name = "Osnovni detalji",
                Action = (clanakDto) => _extendedWindowManager.ShowDialog<BasicDetaljiClankaViewModel, ClanakDto>(clanakDto)
            });
            ClanciPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateClanci(idKljucnaRijec), "Dohvaćanje članaka...", "Desila se greška prilikom dohvaćanja članaka.");
        }

        private void InitNasloviPageableDataGridViewModel(int idKljucnaRijec)
        {
            NasloviPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<NaslovDto>>();
            NasloviPageableDataGridViewModel.Title = "Naslovi";
            NasloviPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<NasloviDataGridViewModel>();
            NasloviPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<NaslovDto>
            {
                Name = "Osnovni detalji",
                Action = (naslovDto) => _extendedWindowManager.ShowDialog<BasicDetaljiNaslovaViewModel, NaslovDto>(naslovDto)
            });
            NasloviPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateNaslovi(idKljucnaRijec), "Dohvaćanje naslova...", "Desila se greška prilikom dohvaćanja naslova.");
        }

        private void InitSazetciPageableDataGridViewModel(int idKljucnaRijec)
        {
            SazetciPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<SazetakDto>>();
            SazetciPageableDataGridViewModel.Title = "Sažetci";
            SazetciPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<SazetciDataGridViewModel>();
            SazetciPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<SazetakDto>
            {
                Name = "Osnovni detalji",
                Action = (sazetakDto) => _extendedWindowManager.ShowDialog<BasicDetaljiSazetkaViewModel, SazetakDto>(sazetakDto)
            });
            SazetciPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateSazetci(idKljucnaRijec), "Dohvaćanje sažetaka...", "Desila se greška prilikom dohvaćanja sažetaka.");
        }

        private void InitVrstePageableDataGridViewModel(int idKljucnaRijec)
        {
            VrstePageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<VrstaDto>>();
            VrstePageableDataGridViewModel.Title = "Vrste";
            VrstePageableDataGridViewModel.DataGridViewModelBase = IoC.Get<VrsteDataGridViewModel>();
            VrstePageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<VrstaDto>
            {
                Name = "Osnovni detalji",
                Action = (vrstaDto) => _extendedWindowManager.ShowDialog<BasicDetaljiVrsteViewModel, VrstaDto>(vrstaDto)
            });
            VrstePageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateVrste(idKljucnaRijec), "Dohvaćanje vrsta...", "Desila se greška prilikom dohvaćanja vrsta.");
        }

        private async Task PopulateDataAsync(int idKljucnaRijec)
        {
            await _extendedWindowManager.ShowLoadingDialogAsync(() =>
            {
                PopulateAutori(idKljucnaRijec);
                PopulateClanci(idKljucnaRijec);
                PopulateNaslovi(idKljucnaRijec);
                PopulateSazetci(idKljucnaRijec);
                PopulateVrste(idKljucnaRijec);
            }, "Dohvaćanje podataka...", "Desila se greška prilikom dohvaćanja podataka.");
        }

        private void PopulateAutori(int idKljucnaRijec)
        {
            autorPagedList = _autorService.GetAllByKljucnaRijecID(idKljucnaRijec, new PageCriteria { Page = AutoriPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            AutoriPageableDataGridViewModel.Refresh(autorPagedList);
        }

        private void PopulateClanci(int idKljucnaRijec)
        {
            clanakPagedList = _clanakService.GetAllByKljucnaRijecID(idKljucnaRijec, new PageCriteria { Page = ClanciPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            ClanciPageableDataGridViewModel.Refresh(clanakPagedList);
        }

        private void PopulateNaslovi(int idKljucnaRijec)
        {
            naslovPagedList = _naslovService.GetAllByKljucnaRijecID(idKljucnaRijec, new PageCriteria { Page = NasloviPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            NasloviPageableDataGridViewModel.Refresh(naslovPagedList);
        }

        private void PopulateSazetci(int idKljucnaRijec)
        {
            sazetakPagedList = _sazetakService.GetAllByKljucnaRijecID(idKljucnaRijec, new PageCriteria { Page = SazetciPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            SazetciPageableDataGridViewModel.Refresh(sazetakPagedList);
        }

        private void PopulateVrste(int idKljucnaRijec)
        {
            vrstaPagedList = _vrstaService.GetAllByKljucnaRijecID(idKljucnaRijec, new PageCriteria { Page = VrstePageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            VrstePageableDataGridViewModel.Refresh(vrstaPagedList);
        }
    }
}
