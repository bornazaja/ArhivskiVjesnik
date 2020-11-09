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
    public class ComprehensiveDetaljiVrsteViewModel : Screen, IHandle<ComprehensiveDetaljiVrsteClickedEvent>
    {
        private IEventAggregator _eventAggregator;
        private IExtendedWindowManager _extendedWindowManager;
        private IAutorService _autorService;
        private IClanakService _clanakService;
        private IKljucnaRijecService _kljucnaRijecService;
        private INaslovService _naslovService;
        private ISazetakService _sazetakService;
        private PagedList<AutorDto> autorPagedList;
        private PagedList<ClanakDto> clanakPagedList;
        private PagedList<KljucnaRijecDto> kljucnaRjecPagedList;
        private PagedList<NaslovDto> naslovPagedList;
        private PagedList<SazetakDto> sazetakPagedList;

        public ComprehensiveDetaljiVrsteViewModel(IEventAggregator eventAggregator, IExtendedWindowManager extendedWindowManager, IAutorService autorService, IClanakService clanakService,
                                                    IKljucnaRijecService kljucnaRijecService, INaslovService naslovService, ISazetakService sazetakService)
        {
            _eventAggregator = eventAggregator;
            _extendedWindowManager = extendedWindowManager;
            _autorService = autorService;
            _clanakService = clanakService;
            _kljucnaRijecService = kljucnaRijecService;
            _naslovService = naslovService;
            _sazetakService = sazetakService;

            _eventAggregator.Subscribe(this);
        }

        public BasicDetaljiVrsteViewModel BasicDetaljiVrsteViewModel { get; set; }
        public PageableDataGridViewModel<AutorDto> AutoriPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<ClanakDto> ClanciPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<KljucnaRijecDto> KljucneRijeciPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<NaslovDto> NasloviPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<SazetakDto> SazetciPageableDataGridViewModel { get; set; }

        public void NatragNaVrste()
        {
            _eventAggregator.PublishOnUIThread(new NatragNaVrsteClickedEvent());
        }

        public async void Handle(ComprehensiveDetaljiVrsteClickedEvent message)
        {
            VrstaDto vrstaDto = message.Vrsta;
            int idVrsta = vrstaDto.IDVrsta;

            InitAndPopuleBasicDetaljiVrsteViewModel(vrstaDto);
            InitAutoriPageableDataGridViewModel(idVrsta);
            InitClanciPageableDataGridViewModel(idVrsta);
            InitKljucneRijeciPageableDataGridViewModel(idVrsta);
            InitNasloviPageableDataGridViewModel(idVrsta);
            InitSazetciPageableDataGridViewModel(idVrsta);

            await PopulateDataAsync(idVrsta);
        }

        private void InitAndPopuleBasicDetaljiVrsteViewModel(VrstaDto vrstaDto)
        {
            BasicDetaljiVrsteViewModel = IoC.Get<BasicDetaljiVrsteViewModel>();
            BasicDetaljiVrsteViewModel.InitDto(vrstaDto);
        }

        private void InitAutoriPageableDataGridViewModel(int idVrsta)
        {
            AutoriPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<AutorDto>>();
            AutoriPageableDataGridViewModel.Title = "Autori";
            AutoriPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<AutoriDataGridViewModel>();
            AutoriPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<AutorDto>
            {
                Name = "Osnovni detalji",
                Action = (autorDto) => _extendedWindowManager.ShowDialog<BasicDetaljiAutoraViewModel, AutorDto>(autorDto)
            });
            AutoriPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateAutori(idVrsta), "Dohvaćanje autora...", "Desila se greška prilikom dohvaćanja autora.");
        }

        private void InitClanciPageableDataGridViewModel(int idVrsta)
        {
            ClanciPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<ClanakDto>>();
            ClanciPageableDataGridViewModel.Title = "Članci";
            ClanciPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<ClanciDataGridViewModel>();
            ClanciPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<ClanakDto>
            {
                Name = "Osnovni detalji",
                Action = (clanakDto) => _extendedWindowManager.ShowDialog<BasicDetaljiClankaViewModel, ClanakDto>(clanakDto)
            });
            ClanciPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateClanci(idVrsta), "Dohvaćanje članaka...", "Desila se greška prilikom dohvaćanja članaka.");
        }

        private void InitKljucneRijeciPageableDataGridViewModel(int idVrsta)
        {
            KljucneRijeciPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<KljucnaRijecDto>>();
            KljucneRijeciPageableDataGridViewModel.Title = "Ključne riječi";
            KljucneRijeciPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<KljucneRijeciDataGridViewModel>();
            KljucneRijeciPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<KljucnaRijecDto>
            {
                Name = "Osnovni detalji",
                Action = (kljucnaRijecDto) => _extendedWindowManager.ShowDialog<BasicDetaljiKljucneRijeciViewModel, KljucnaRijecDto>(kljucnaRijecDto)
            });
            KljucneRijeciPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateKljucneRijeci(idVrsta), "Dohvaćanje ključnih riječi...", "Desila se greška prilikom dohvaćanja ključnih riječi.");
        }

        private void InitNasloviPageableDataGridViewModel(int idVrsta)
        {
            NasloviPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<NaslovDto>>();
            NasloviPageableDataGridViewModel.Title = "Naslovi";
            NasloviPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<NasloviDataGridViewModel>();
            NasloviPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<NaslovDto>
            {
                Name = "Osnovni detalji",
                Action = (naslovDto) => _extendedWindowManager.ShowDialog<BasicDetaljiNaslovaViewModel, NaslovDto>(naslovDto)
            });
            NasloviPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateNaslovi(idVrsta), "Dohvaćanje naslova...", "Desila se greška prilikom dohvaćanja naslova.");
        }

        private void InitSazetciPageableDataGridViewModel(int idVrsta)
        {
            SazetciPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<SazetakDto>>();
            SazetciPageableDataGridViewModel.Title = "Sažetci";
            SazetciPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<SazetciDataGridViewModel>();
            SazetciPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<SazetakDto>
            {
                Name = "Osnovni detalji",
                Action = (sazetakDto) => _extendedWindowManager.ShowDialog<BasicDetaljiSazetkaViewModel, SazetakDto>(sazetakDto)
            });
            SazetciPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateSazetci(idVrsta), "Dohvaćanje sažetaka...", "Desila se greška prilikom dohvaćanja sažetaka.");
        }

        private async Task PopulateDataAsync(int idVrsta)
        {
            await _extendedWindowManager.ShowLoadingDialogAsync(() =>
            {
                PopulateAutori(idVrsta);
                PopulateClanci(idVrsta);
                PopulateKljucneRijeci(idVrsta);
                PopulateNaslovi(idVrsta);
                PopulateSazetci(idVrsta);
            }, "Dohvaćanje podataka...", "Desila se greška prilikom dohvaćanja podataka.");
        }

        private void PopulateAutori(int idVrsta)
        {
            autorPagedList = _autorService.GetAllByVrstaID(idVrsta, new PageCriteria { Page = AutoriPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            AutoriPageableDataGridViewModel.Refresh(autorPagedList);
        }

        private void PopulateClanci(int idVrsta)
        {
            clanakPagedList = _clanakService.GetAllByVrstaID(idVrsta, new PageCriteria { Page = ClanciPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            ClanciPageableDataGridViewModel.Refresh(clanakPagedList);
        }

        private void PopulateKljucneRijeci(int idVrsta)
        {
            kljucnaRjecPagedList = _kljucnaRijecService.GetAllByVrstaID(idVrsta, new PageCriteria { Page = KljucneRijeciPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            KljucneRijeciPageableDataGridViewModel.Refresh(kljucnaRjecPagedList);
        }

        private void PopulateNaslovi(int idVrsta)
        {
            naslovPagedList = _naslovService.GetAllByVrstaID(idVrsta, new PageCriteria { Page = NasloviPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            NasloviPageableDataGridViewModel.Refresh(naslovPagedList);
        }

        private void PopulateSazetci(int idVrsta)
        {
            sazetakPagedList = _sazetakService.GetAllByVrstaID(idVrsta, new PageCriteria { Page = SazetciPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            SazetciPageableDataGridViewModel.Refresh(sazetakPagedList);
        }
    }
}
