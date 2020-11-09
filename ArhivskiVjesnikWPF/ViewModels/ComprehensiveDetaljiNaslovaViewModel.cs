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
    public class ComprehensiveDetaljiNaslovaViewModel : Screen, IHandle<ComprehensiveDetaljiNaslovaClickedEvent>
    {
        private IEventAggregator _eventAggregator;
        private IExtendedWindowManager _extendedWindowManager;
        private IAutorService _autorService;
        private IClanakService _clanakService;
        private IKljucnaRijecService _kljucnaRijecService;
        private ISazetakService _sazetakService;
        private IVrstaService _vrstaService;
        private PagedList<AutorDto> autorPagedList;
        private PagedList<ClanakDto> clanakPagedList;
        private PagedList<KljucnaRijecDto> kljucnaRijecPagedList;
        private PagedList<SazetakDto> sazetakPagedList;
        private PagedList<VrstaDto> vrstaPagedList;

        public ComprehensiveDetaljiNaslovaViewModel(IEventAggregator eventAggregator, IExtendedWindowManager extendedWindowManager, IAutorService autorService, IClanakService clanakService,
                                                        IKljucnaRijecService kljucnaRijecService, ISazetakService sazetakService, IVrstaService vrstaService)
        {
            _eventAggregator = eventAggregator;
            _extendedWindowManager = extendedWindowManager;
            _autorService = autorService;
            _clanakService = clanakService;
            _kljucnaRijecService = kljucnaRijecService;
            _sazetakService = sazetakService;
            _vrstaService = vrstaService;

            _eventAggregator.Subscribe(this);
        }

        public BasicDetaljiNaslovaViewModel BasicDetaljiNaslovaViewModel { get; set; }
        public PageableDataGridViewModel<AutorDto> AutoriPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<ClanakDto> ClanciPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<KljucnaRijecDto> KljucneRijeciPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<SazetakDto> SazetciPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<VrstaDto> VrstePageableDataGridViewModel { get; set; }

        public void NatragNaNaslove()
        {
            _eventAggregator.PublishOnUIThread(new NatragNaNasloveClickedEvent());
        }

        public async void Handle(ComprehensiveDetaljiNaslovaClickedEvent message)
        {
            NaslovDto naslovDto = message.Naslov;
            int idNaslov = naslovDto.IDNaslov;

            InitAndPopulateBasicDetaljiNaslovaViewModel(naslovDto);
            InitAutoriPageableDataGridViewModel(idNaslov);
            InitClanciPageableDataGridViewModel(idNaslov);
            InitKljucneRijeciPageableDataGridViewModel(idNaslov);
            InitSazetciPageableDataGridViewModel(idNaslov);
            InitVrstePageableDataGridViewModel(idNaslov);

            await PopulateDataAsync(idNaslov);
        }

        private void InitAndPopulateBasicDetaljiNaslovaViewModel(NaslovDto naslovDto)
        {
            BasicDetaljiNaslovaViewModel = IoC.Get<BasicDetaljiNaslovaViewModel>();
            BasicDetaljiNaslovaViewModel.InitDto(naslovDto);
        }

        private void InitAutoriPageableDataGridViewModel(int idNaslov)
        {
            AutoriPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<AutorDto>>();
            AutoriPageableDataGridViewModel.Title = "Autori";
            AutoriPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<AutoriDataGridViewModel>();
            AutoriPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<AutorDto>
            {
                Name = "Osnovni detalji",
                Action = (autorDto) => _extendedWindowManager.ShowDialog<BasicDetaljiAutoraViewModel, AutorDto>(autorDto)
            });
            AutoriPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateAutori(idNaslov), "Dohvaćanje autora...", "Desila se greška prilikom dohvaćanja autora.");
        }

        private void InitClanciPageableDataGridViewModel(int idNaslov)
        {
            ClanciPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<ClanakDto>>();
            ClanciPageableDataGridViewModel.Title = "Članci";
            ClanciPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<ClanciDataGridViewModel>();
            ClanciPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<ClanakDto>
            {
                Name = "Osnovni detalji",
                Action = (clanakDto) => _extendedWindowManager.ShowDialog<BasicDetaljiClankaViewModel, ClanakDto>(clanakDto)
            });
            ClanciPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateClanci(idNaslov), "Dohvaćanje članaka...", "Desila se greška prilikom dohvaćanja članaka.");
        }

        private void InitKljucneRijeciPageableDataGridViewModel(int idNaslov)
        {
            KljucneRijeciPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<KljucnaRijecDto>>();
            KljucneRijeciPageableDataGridViewModel.Title = "Ključne riječi";
            KljucneRijeciPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<KljucneRijeciDataGridViewModel>();
            KljucneRijeciPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<KljucnaRijecDto>
            {
                Name = "Osnovni detalji",
                Action = (kljucneRijeciDto) => _extendedWindowManager.ShowDialog<BasicDetaljiKljucneRijeciViewModel, KljucnaRijecDto>(kljucneRijeciDto)
            });
            KljucneRijeciPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateKljucneRijeci(idNaslov), "Dohvaćanje ključnih riječi...", "Desila se greška prilikom dohvaćanja ključnih riječi.");
        }

        private void InitSazetciPageableDataGridViewModel(int idNaslov)
        {
            SazetciPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<SazetakDto>>();
            SazetciPageableDataGridViewModel.Title = "Sažetci";
            SazetciPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<SazetciDataGridViewModel>();
            SazetciPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<SazetakDto>
            {
                Name = "Osnovni detalji",
                Action = (sazetakDto) => _extendedWindowManager.ShowDialog<BasicDetaljiSazetkaViewModel, SazetakDto>(sazetakDto)
            });
            SazetciPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateSazetci(idNaslov), "Dohvaćanje sažetaka...", "Desila se greška prilikom dohvaćanja sažetaka.");
        }

        private void InitVrstePageableDataGridViewModel(int idNaslov)
        {
            VrstePageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<VrstaDto>>();
            VrstePageableDataGridViewModel.Title = "Vrste";
            VrstePageableDataGridViewModel.DataGridViewModelBase = IoC.Get<VrsteDataGridViewModel>();
            VrstePageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<VrstaDto>
            {
                Name = "Osnovni detalji",
                Action = (vrsteDto) => _extendedWindowManager.ShowDialog<BasicDetaljiVrsteViewModel, VrstaDto>(vrsteDto)
            });
            VrstePageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateVrste(idNaslov), "Dohvaćanje vrsta...", "Desila se greška prilikom dohvaćanja vrsta.");
        }

        private async Task PopulateDataAsync(int idNaslov)
        {
            await _extendedWindowManager.ShowLoadingDialogAsync(() =>
            {
                PopulateAutori(idNaslov);
                PopulateClanci(idNaslov);
                PopulateKljucneRijeci(idNaslov);
                PopulateSazetci(idNaslov);
                PopulateVrste(idNaslov);
            }, "Dohvaćanje podataka...", "Desila se greška prilikom dohvaćanja podataka.");
        }

        private void PopulateAutori(int idNaslov)
        {
            autorPagedList = _autorService.GetAllByNaslovID(idNaslov, new PageCriteria { Page = AutoriPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            AutoriPageableDataGridViewModel.Refresh(autorPagedList);
        }

        private void PopulateClanci(int idNaslov)
        {
            clanakPagedList = _clanakService.GetAllByNaslovID(idNaslov, new PageCriteria { Page = ClanciPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            ClanciPageableDataGridViewModel.Refresh(clanakPagedList);
        }

        private void PopulateKljucneRijeci(int idNaslov)
        {
            kljucnaRijecPagedList = _kljucnaRijecService.GetAllByNaslovID(idNaslov, new PageCriteria { Page = KljucneRijeciPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            KljucneRijeciPageableDataGridViewModel.Refresh(kljucnaRijecPagedList);
        }

        private void PopulateSazetci(int idNaslov)
        {
            sazetakPagedList = _sazetakService.GetAllByNaslovID(idNaslov, new PageCriteria { Page = SazetciPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            SazetciPageableDataGridViewModel.Refresh(sazetakPagedList);
        }

        private void PopulateVrste(int idNaslov)
        {
            vrstaPagedList = _vrstaService.GetAllByNaslovID(idNaslov, new PageCriteria { Page = VrstePageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            VrstePageableDataGridViewModel.Refresh(vrstaPagedList);
        }
    }
}
