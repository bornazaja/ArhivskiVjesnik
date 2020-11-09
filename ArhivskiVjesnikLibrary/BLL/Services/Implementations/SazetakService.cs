using ArhivskiVjesnikLibrary.BLL.DTO;
using ArhivskiVjesnikLibrary.BLL.Services.Interfaces;
using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Models;
using ArhivskiVjesnikLibrary.DAL.Repositories.Interfaces;
using AutoMapper;

namespace ArhivskiVjesnikLibrary.BLL.Services.Implementations
{
    public class SazetakService : GenericService<Sazetak, SazetakDto>, ISazetakService
    {
        private IMapper _mapper;
        private ISazetakRepository _sazetakRepository;

        public SazetakService(IGenericRepository<Sazetak> genericRepository, IMapper mapper, ISazetakRepository sazetakRepository) : base(genericRepository, mapper)
        {
            _mapper = mapper;
            _sazetakRepository = sazetakRepository;
        }

        public PagedList<SazetakDto> GetAllByAutorID(int autorId, PageCriteria pageCriteria)
        {
            PagedList<Sazetak> sazetakPagedList = _sazetakRepository.GetAllByAutorID(autorId, pageCriteria);
            return _mapper.Map<PagedList<Sazetak>, PagedList<SazetakDto>>(sazetakPagedList);
        }

        public PagedList<SazetakDto> GetAllByClanakID(int clanakId, PageCriteria pageCriteria)
        {
            PagedList<Sazetak> sazetakPagedList = _sazetakRepository.GetAllByClanakID(clanakId, pageCriteria);
            return _mapper.Map<PagedList<Sazetak>, PagedList<SazetakDto>>(sazetakPagedList);
        }

        public PagedList<SazetakDto> GetAllByKljucnaRijecID(int kljucnaRijecId, PageCriteria pageCriteria)
        {
            PagedList<Sazetak> sazetakPagedList = _sazetakRepository.GetAllByKljucnaRijecID(kljucnaRijecId, pageCriteria);
            return _mapper.Map<PagedList<Sazetak>, PagedList<SazetakDto>>(sazetakPagedList);
        }

        public PagedList<SazetakDto> GetAllByNaslovID(int naslovId, PageCriteria pageCriteria)
        {
            PagedList<Sazetak> sazetakPagedList = _sazetakRepository.GetAllByNaslovID(naslovId, pageCriteria);
            return _mapper.Map<PagedList<Sazetak>, PagedList<SazetakDto>>(sazetakPagedList);
        }

        public PagedList<SazetakDto> GetAllByVrstaID(int vrstaId, PageCriteria pageCriteria)
        {
            PagedList<Sazetak> sazetakPagedList = _sazetakRepository.GetAllByVrstaID(vrstaId, pageCriteria);
            return _mapper.Map<PagedList<Sazetak>, PagedList<SazetakDto>>(sazetakPagedList);
        }
    }
}
