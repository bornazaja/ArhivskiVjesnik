using ArhivskiVjesnikLibrary.BLL.DTO;
using ArhivskiVjesnikLibrary.BLL.Services.Interfaces;
using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Models;
using ArhivskiVjesnikLibrary.DAL.Repositories.Interfaces;
using AutoMapper;

namespace ArhivskiVjesnikLibrary.BLL.Services.Implementations
{
    public class AutorService : GenericService<Autor, AutorDto>, IAutorService
    {
        private IMapper _mapper;
        private IAutorRepository _autorRepository;

        public AutorService(IGenericRepository<Autor> genericRepository, IMapper mapper, IAutorRepository autorRepository) : base(genericRepository, mapper)
        {
            _mapper = mapper;
            _autorRepository = autorRepository;
        }

        public PagedList<AutorDto> GetAllByClanakID(int clanakId, PageCriteria pageCriteria)
        {
            PagedList<Autor> autorPagedList = _autorRepository.GetAllByClanakID(clanakId, pageCriteria);
            return _mapper.Map<PagedList<Autor>, PagedList<AutorDto>>(autorPagedList);
        }

        public PagedList<AutorDto> GetAllByKljucnaRijecID(int kljucnaRijecId, PageCriteria pageCriteria)
        {
            PagedList<Autor> autorPagedList = _autorRepository.GetAllByKljucnaRijecID(kljucnaRijecId, pageCriteria);
            return _mapper.Map<PagedList<Autor>, PagedList<AutorDto>>(autorPagedList);
        }

        public PagedList<AutorDto> GetAllByNaslovID(int naslovId, PageCriteria pageCriteria)
        {
            PagedList<Autor> autorPagedList = _autorRepository.GetAllByNaslovID(naslovId, pageCriteria);
            return _mapper.Map<PagedList<Autor>, PagedList<AutorDto>>(autorPagedList);
        }

        public PagedList<AutorDto> GetAllBySazetakID(int sazetakId, PageCriteria pageCriteria)
        {
            PagedList<Autor> autorPagedList = _autorRepository.GetAllBySazetakID(sazetakId, pageCriteria);
            return _mapper.Map<PagedList<Autor>, PagedList<AutorDto>>(autorPagedList);
        }

        public PagedList<AutorDto> GetAllByVrstaID(int vrstaId, PageCriteria pageCriteria)
        {
            PagedList<Autor> autorPagedList = _autorRepository.GetAllByVrstaID(vrstaId, pageCriteria);
            return _mapper.Map<PagedList<Autor>, PagedList<AutorDto>>(autorPagedList);
        }
    }
}
