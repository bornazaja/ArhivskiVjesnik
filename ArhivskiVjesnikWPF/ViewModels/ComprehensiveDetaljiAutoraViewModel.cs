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
    public class ComprehensiveDetaljiAutoraViewModel : Screen, IHandle<ComprehensiveDetaljiAutoraClickedEvent>
    {
        private IEventAggregator _eventAggregator;
        private IExtendedWindowManager _extendedWindowManager;
        private IClanakService _clanakService;
        private IKljucnaRijecService _kljucnaRijecService;
        private INaslovService _naslovService;
        private ISazetakService _sazetakService;
        private IVrstaService _vrstaService;
        private PagedList<ClanakDto> clanakPagedList;
        private PagedList<KljucnaRijecDto> kljucnaRijecPagedList;
        private PagedList<NaslovDto> naslovPagedList;
        private PagedList<SazetakDto> sazetakPagedList;
        private PagedList<VrstaDto> vrstaPagedList;

        public ComprehensiveDetaljiAutoraViewModel(IEventAggregator eventAggregator, IExtendedWindowManager extendedWindowManager, IClanakService clanakService, IKljucnaRijecService kljucnaRijecService,
                                                    INaslovService naslovService, ISazetakService sazetakService, IVrstaService vrstaService)
        {
            _eventAggregator = eventAggregator;
            _extendedWindowManager = extendedWindowManager;
            _clanakService = clanakService;
            _kljucnaRijecService = kljucnaRijecService;
            _naslovService = naslovService;
            _sazetakService = sazetakService;
            _vrstaService = vrstaService;

            _eventAggregator.Subscribe(this);
        }

        public BasicDetaljiAutoraViewModel BasicDetaljiAutoraViewModel { get; set; }
        public PageableDataGridViewModel<ClanakDto> ClanciPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<KljucnaRijecDto> KljucneRijeciPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<NaslovDto> NasloviPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<SazetakDto> SazetciPageableDataGridViewModel { get; set; }
        public PageableDataGridViewModel<VrstaDto> VrstePageableDataGridViewModel { get; set; }

        public void NatragNaAutore()
        {
            _eventAggregator.PublishOnUIThread(new NatragNaAutoreClickedEvent());
        }

        public async void Handle(ComprehensiveDetaljiAutoraClickedEvent message)
        {
            AutorDto autorDto = message.Autor;
            int idAutor = autorDto.IDAutor;

            InitClanciPageableDataGridViewModel(idAutor);
            InitKljucneRijeciPageableDataGridViewModel(idAutor);
            InitNasloviPageableDataGridViewModel(idAutor);
            InitSazetciPageableDataGridViewModel(idAutor);
            InitVrstePageableDataGridViewModel(idAutor);

            InitAndPopulateBasicDetaljiAutoraViewModel(autorDto);
            await PopulateDataAsync(idAutor);
        }

        private void InitAndPopulateBasicDetaljiAutoraViewModel(AutorDto autorDto)
        {
            BasicDetaljiAutoraViewModel = IoC.Get<BasicDetaljiAutoraViewModel>();
            BasicDetaljiAutoraViewModel.InitDto(autorDto);
        }

        private void InitClanciPageableDataGridViewModel(int idAutor)
        {
            ClanciPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<ClanakDto>>();
            ClanciPageableDataGridViewModel.Title = "Članci";
            ClanciPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<ClanciDataGridViewModel>();
            ClanciPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<ClanakDto>
            {
                Name = "Osnovni detalji",
                Action = (clanakDto) => _extendedWindowManager.ShowDialog<BasicDetaljiClankaViewModel, ClanakDto>(clanakDto)
            });
            ClanciPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateClanci(idAutor), "Dohvaćanje članaka...", "Desila se greška prilikom dohvaćanja članaka.");
        }

        private void InitKljucneRijeciPageableDataGridViewModel(int idAutor)
        {
            KljucneRijeciPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<KljucnaRijecDto>>();
            KljucneRijeciPageableDataGridViewModel.Title = "Ključne riječi";
            KljucneRijeciPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<KljucneRijeciDataGridViewModel>();
            KljucneRijeciPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<KljucnaRijecDto>
            {
                Name = "Osnovni detalji",
                Action = (kljucnaRijecDto) => _extendedWindowManager.ShowDialog<BasicDetaljiKljucneRijeciViewModel, KljucnaRijecDto>(kljucnaRijecDto)
            });
            KljucneRijeciPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateKljucneRijeci(idAutor), "Dohvaćanje ključnih rijeći...", "Desila se greška prilikom dohvaćanja ključnih riječi.");
        }

        private void InitNasloviPageableDataGridViewModel(int idAutor)
        {
            NasloviPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<NaslovDto>>();
            NasloviPageableDataGridViewModel.Title = "Naslovi";
            NasloviPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<NasloviDataGridViewModel>();
            NasloviPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<NaslovDto>
            {
                Name = "Osnovni detalji",
                Action = (naslovDto) => _extendedWindowManager.ShowDialog<BasicDetaljiNaslovaViewModel, NaslovDto>(naslovDto)
            });
            NasloviPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateNaslovi(idAutor), "Dohvaćanje naslova...", "Desila se greška prilikom dohvaćanja naslova.");
        }

        private void InitSazetciPageableDataGridViewModel(int idAutor)
        {
            SazetciPageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<SazetakDto>>();
            SazetciPageableDataGridViewModel.Title = "Sažetci";
            SazetciPageableDataGridViewModel.DataGridViewModelBase = IoC.Get<SazetciDataGridViewModel>();
            SazetciPageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<SazetakDto>
            {
                Name = "Osnovni detalji",
                Action = (sazetakDto) => _extendedWindowManager.ShowDialog<BasicDetaljiSazetkaViewModel, SazetakDto>(sazetakDto)
            });
            SazetciPageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateSazetci(idAutor), "Dohvaćanje sažetaka...", "Desila se greška prilikom dohvaćanja sažetaka.");
        }

        private void InitVrstePageableDataGridViewModel(int idAutor)
        {
            VrstePageableDataGridViewModel = IoC.Get<PageableDataGridViewModel<VrstaDto>>();
            VrstePageableDataGridViewModel.Title = "Vrste";
            VrstePageableDataGridViewModel.DataGridViewModelBase = IoC.Get<VrsteDataGridViewModel>();
            VrstePageableDataGridViewModel.DataGridViewModelBase.ActionItems.Add(new ActionItemModel<VrstaDto>
            {
                Name = "Osnovni detalji",
                Action = (vrstaDto) => _extendedWindowManager.ShowDialog<BasicDetaljiVrsteViewModel, VrstaDto>(vrstaDto)
            });
            VrstePageableDataGridViewModel.PaginationViewModel.SetActionForNavigations(() => PopulateVrste(idAutor), "Dohvaćanje vrsta...", "Desila se greška prilikom dohvaćanja vrsta.");
        }

        private async Task PopulateDataAsync(int idAutor)
        {
            await _extendedWindowManager.ShowLoadingDialogAsync(() =>
            {
                PopulateClanci(idAutor);
                PopulateKljucneRijeci(idAutor);
                PopulateNaslovi(idAutor);
                PopulateSazetci(idAutor);
                PopulateVrste(idAutor);
            }, "Dohvaćanje podataka...", "Desila se greška prilikom dohvaćanja podataka.");
        }

        private void PopulateClanci(int idAutor)
        {
            clanakPagedList = _clanakService.GetAllByAutorID(idAutor, new PageCriteria { Page = ClanciPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            ClanciPageableDataGridViewModel.Refresh(clanakPagedList);
        }

        private void PopulateKljucneRijeci(int idAutor)
        {
            kljucnaRijecPagedList = _kljucnaRijecService.GetAllByAutorID(idAutor, new PageCriteria { Page = KljucneRijeciPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            KljucneRijeciPageableDataGridViewModel.Refresh(kljucnaRijecPagedList);
        }

        private void PopulateNaslovi(int idAutor)
        {
            naslovPagedList = _naslovService.GetAllByAutorID(idAutor, new PageCriteria { Page = NasloviPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            NasloviPageableDataGridViewModel.Refresh(naslovPagedList);
        }

        private void PopulateSazetci(int idAutor)
        {
            sazetakPagedList = _sazetakService.GetAllByAutorID(idAutor, new PageCriteria { Page = SazetciPageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            SazetciPageableDataGridViewModel.Refresh(sazetakPagedList);
        }

        private void PopulateVrste(int idAutor)
        {
            vrstaPagedList = _vrstaService.GetAllByAutorID(idAutor, new PageCriteria { Page = VrstePageableDataGridViewModel.PaginationViewModel.CurrentPage, Size = 10 });
            VrstePageableDataGridViewModel.Refresh(vrstaPagedList);
        }
    }
}
