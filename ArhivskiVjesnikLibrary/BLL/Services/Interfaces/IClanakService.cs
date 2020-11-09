using ArhivskiVjesnikLibrary.BLL.DTO;
using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Models;
using System.Collections.Generic;

namespace ArhivskiVjesnikLibrary.BLL.Services.Interfaces
{
    public interface IClanakService : IGenericService<Clanak, ClanakDto>
    {
        PagedList<ClanakDto> GetAllByAutorID(int autorId, PageCriteria pageCriteria);
        PagedList<ClanakDto> GetAllByKljucnaRijecID(int kljucnaRijecId, PageCriteria pageCriteria);
        PagedList<ClanakDto> GetAllByNaslovID(int naslovId, PageCriteria pageCriteria);
        PagedList<ClanakDto> GetAllBySazetakID(int sazetakId, PageCriteria pageCriteria);
        PagedList<ClanakDto> GetAllByVrstaID(int vrstaId, PageCriteria pageCriteria);
    }
}
