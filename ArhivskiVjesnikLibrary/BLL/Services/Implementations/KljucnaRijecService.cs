using ArhivskiVjesnikLibrary.BLL.DTO;
using ArhivskiVjesnikLibrary.BLL.Services.Interfaces;
using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Models;
using ArhivskiVjesnikLibrary.DAL.Repositories.Interfaces;
using AutoMapper;

namespace ArhivskiVjesnikLibrary.BLL.Services.Implementations
{
    public class KljucnaRijecService : GenericService<KljucnaRijec, KljucnaRijecDto>, IKljucnaRijecService
    {
        private IMapper _mapper;
        private IKljucnaRijecRepository _kljucnaRijecRepository;

        public KljucnaRijecService(IGenericRepository<KljucnaRijec> genericRepository, IMapper mapper, IKljucnaRijecRepository kljucnaRijecRepository) : base(genericRepository, mapper)
        {
            _mapper = mapper;
            _kljucnaRijecRepository = kljucnaRijecRepository;
        }

        public PagedList<KljucnaRijecDto> GetAllByAutorID(int autorId, PageCriteria pageCriteria)
        {
            PagedList<KljucnaRijec> kljucnaRijecPagedList = _kljucnaRijecRepository.GetAllByAutorID(autorId, pageCriteria);
            return _mapper.Map<PagedList<KljucnaRijec>, PagedList<KljucnaRijecDto>>(kljucnaRijecPagedList);
        }

        public PagedList<KljucnaRijecDto> GetAllByClanakID(int clanakId, PageCriteria pageCriteria)
        {
            PagedList<KljucnaRijec> kljucnaRijecPagedList = _kljucnaRijecRepository.GetAllByClanakID(clanakId, pageCriteria);
            return _mapper.Map<PagedList<KljucnaRijec>, PagedList<KljucnaRijecDto>>(kljucnaRijecPagedList);
        }

        public PagedList<KljucnaRijecDto> GetAllByNaslovID(int naslovId, PageCriteria pageCriteria)
        {
            PagedList<KljucnaRijec> kljucnaRijecPagedList = _kljucnaRijecRepository.GetAllByNaslovID(naslovId, pageCriteria);
            return _mapper.Map<PagedList<KljucnaRijec>, PagedList<KljucnaRijecDto>>(kljucnaRijecPagedList);
        }

        public PagedList<KljucnaRijecDto> GetAllBySazetakID(int sazetakId, PageCriteria pageCriteria)
        {
            PagedList<KljucnaRijec> kljucnaRijecPagedList = _kljucnaRijecRepository.GetAllBySazetakID(sazetakId, pageCriteria);
            return _mapper.Map<PagedList<KljucnaRijec>, PagedList<KljucnaRijecDto>>(kljucnaRijecPagedList);
        }

        public PagedList<KljucnaRijecDto> GetAllByVrstaID(int vrstaId, PageCriteria pageCriteria)
        {
            PagedList<KljucnaRijec> kljucnaRijecPagedList = _kljucnaRijecRepository.GetAllByVrstaID(vrstaId, pageCriteria);
            return _mapper.Map<PagedList<KljucnaRijec>, PagedList<KljucnaRijecDto>>(kljucnaRijecPagedList);
        }
    }
}
