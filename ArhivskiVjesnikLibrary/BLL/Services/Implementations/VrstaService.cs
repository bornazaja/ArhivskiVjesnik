using ArhivskiVjesnikLibrary.BLL.DTO;
using ArhivskiVjesnikLibrary.BLL.Services.Interfaces;
using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Models;
using ArhivskiVjesnikLibrary.DAL.Repositories.Interfaces;
using AutoMapper;

namespace ArhivskiVjesnikLibrary.BLL.Services.Implementations
{
    public class VrstaService : GenericService<Vrsta, VrstaDto>, IVrstaService
    {
        private IMapper _mapper;
        private IVrstaRepository _vrstaRepository;

        public VrstaService(IGenericRepository<Vrsta> genericRepository, IMapper mapper, IVrstaRepository vrstaRepository) : base(genericRepository, mapper)
        {
            _mapper = mapper;
            _vrstaRepository = vrstaRepository;
        }

        public PagedList<VrstaDto> GetAllByAutorID(int autorId, PageCriteria pageCriteria)
        {
            PagedList<Vrsta> vrstaPagedList = _vrstaRepository.GetAllByAutorID(autorId, pageCriteria);
            return _mapper.Map<PagedList<Vrsta>, PagedList<VrstaDto>>(vrstaPagedList);
        }

        public PagedList<VrstaDto> GetAllByClanakID(int clanakId, PageCriteria pageCriteria)
        {
            PagedList<Vrsta> vrstaPagedList = _vrstaRepository.GetAllByClanakID(clanakId, pageCriteria);
            return _mapper.Map<PagedList<Vrsta>, PagedList<VrstaDto>>(vrstaPagedList);
        }

        public PagedList<VrstaDto> GetAllByKljucnaRijecID(int kljucnaRijecId, PageCriteria pageCriteria)
        {
            PagedList<Vrsta> vrstaPagedList = _vrstaRepository.GetAllByKljucnaRijecID(kljucnaRijecId, pageCriteria);
            return _mapper.Map<PagedList<Vrsta>, PagedList<VrstaDto>>(vrstaPagedList);
        }

        public PagedList<VrstaDto> GetAllByNaslovID(int naslovId, PageCriteria pageCriteria)
        {
            PagedList<Vrsta> vrstaPagedList = _vrstaRepository.GetAllByNaslovID(naslovId, pageCriteria);
            return _mapper.Map<PagedList<Vrsta>, PagedList<VrstaDto>>(vrstaPagedList);
        }

        public PagedList<VrstaDto> GetAllBySazetakID(int sazetakId, PageCriteria pageCriteria)
        {
            PagedList<Vrsta> vrstaPagedList = _vrstaRepository.GetAllBySazetakID(sazetakId, pageCriteria);
            return _mapper.Map<PagedList<Vrsta>, PagedList<VrstaDto>>(vrstaPagedList);
        }
    }
}
