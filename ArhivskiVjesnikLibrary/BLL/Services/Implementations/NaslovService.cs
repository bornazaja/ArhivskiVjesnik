using ArhivskiVjesnikLibrary.BLL.DTO;
using ArhivskiVjesnikLibrary.BLL.Services.Interfaces;
using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Models;
using ArhivskiVjesnikLibrary.DAL.Repositories.Interfaces;
using AutoMapper;

namespace ArhivskiVjesnikLibrary.BLL.Services.Implementations
{
    public class NaslovService : GenericService<Naslov, NaslovDto>, INaslovService
    {
        private IMapper _mapper;
        private INaslovRepository _naslovRepository;

        public NaslovService(IGenericRepository<Naslov> genericRepository, IMapper mapper, INaslovRepository naslovRepository) : base(genericRepository, mapper)
        {
            _mapper = mapper;
            _naslovRepository = naslovRepository;
        }

        public PagedList<NaslovDto> GetAllByAutorID(int autorId, PageCriteria pageCriteria)
        {
            PagedList<Naslov> naslovPagedList = _naslovRepository.GetAllByAutorID(autorId, pageCriteria);
            return _mapper.Map<PagedList<Naslov>, PagedList<NaslovDto>>(naslovPagedList);
        }

        public PagedList<NaslovDto> GetAllByClanakID(int clanakId, PageCriteria pageCriteria)
        {
            PagedList<Naslov> naslovPagedList = _naslovRepository.GetAllByClanakID(clanakId, pageCriteria);
            return _mapper.Map<PagedList<Naslov>, PagedList<NaslovDto>>(naslovPagedList);
        }

        public PagedList<NaslovDto> GetAllByKljucnaRijecID(int kljucnaRijecId, PageCriteria pageCriteria)
        {
            PagedList<Naslov> naslovPagedList = _naslovRepository.GetAllByKljucnaRijecID(kljucnaRijecId, pageCriteria);
            return _mapper.Map<PagedList<Naslov>, PagedList<NaslovDto>>(naslovPagedList);
        }

        public PagedList<NaslovDto> GetAllBySazetakID(int sazetakId, PageCriteria pageCriteria)
        {
            PagedList<Naslov> naslovPagedList = _naslovRepository.GetAllBySazetakID(sazetakId, pageCriteria);
            return _mapper.Map<PagedList<Naslov>, PagedList<NaslovDto>>(naslovPagedList);
        }

        public PagedList<NaslovDto> GetAllByVrstaID(int vrstaId, PageCriteria pageCriteria)
        {
            PagedList<Naslov> naslovPagedList = _naslovRepository.GetAllByVrstaID(vrstaId, pageCriteria);
            return _mapper.Map<PagedList<Naslov>, PagedList<NaslovDto>>(naslovPagedList);
        }
    }
}
