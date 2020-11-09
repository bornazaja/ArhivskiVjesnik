using ArhivskiVjesnikLibrary.BLL.DTO;
using ArhivskiVjesnikLibrary.BLL.Services.Interfaces;
using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Models;
using ArhivskiVjesnikLibrary.DAL.Repositories.Interfaces;
using AutoMapper;
using System.Collections.Generic;

namespace ArhivskiVjesnikLibrary.BLL.Services.Implementations
{
    public class ClanakService : GenericService<Clanak, ClanakDto>, IClanakService
    {
        private IMapper _mapper;
        private IClanakRepository _clanakRepository;

        public ClanakService(IGenericRepository<Clanak> genericRepository, IMapper mapper, IClanakRepository clanakRepository) : base(genericRepository, mapper)
        {
            _mapper = mapper;
            _clanakRepository = clanakRepository;
        }

        public PagedList<ClanakDto> GetAllByAutorID(int autorId, PageCriteria pageCriteria)
        {
            PagedList<Clanak> clanakPagedList = _clanakRepository.GetAllByAutorID(autorId, pageCriteria);
            return _mapper.Map<PagedList<Clanak>, PagedList<ClanakDto>>(clanakPagedList);
        }

        public PagedList<ClanakDto> GetAllByKljucnaRijecID(int kljucnaRijecId, PageCriteria pageCriteria)
        {
            PagedList<Clanak> clanakPagedList = _clanakRepository.GetAllByKljucnaRijecID(kljucnaRijecId, pageCriteria);
            return _mapper.Map<PagedList<Clanak>, PagedList<ClanakDto>>(clanakPagedList);
        }

        public PagedList<ClanakDto> GetAllByNaslovID(int naslovId, PageCriteria pageCriteria)
        {
            PagedList<Clanak> clanakPagedList = _clanakRepository.GetAllByNaslovID(naslovId, pageCriteria);
            return _mapper.Map<PagedList<Clanak>, PagedList<ClanakDto>>(clanakPagedList);
        }

        public PagedList<ClanakDto> GetAllBySazetakID(int sazetakId, PageCriteria pageCriteria)
        {
            PagedList<Clanak> clanakPagedList = _clanakRepository.GetAllBySazetakID(sazetakId, pageCriteria);
            return _mapper.Map<PagedList<Clanak>, PagedList<ClanakDto>>(clanakPagedList);
        }

        public PagedList<ClanakDto> GetAllByVrstaID(int vrstaId, PageCriteria pageCriteria)
        {
            PagedList<Clanak> clanakPagedList = _clanakRepository.GetAllByVrstaID(vrstaId, pageCriteria);
            return _mapper.Map<PagedList<Clanak>, PagedList<ClanakDto>>(clanakPagedList);
        }
    }
}
