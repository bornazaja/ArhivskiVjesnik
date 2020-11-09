using ArhivskiVjesnikLibrary.BLL.DTO;
using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Models;

namespace ArhivskiVjesnikLibrary.BLL.Services.Interfaces
{
    public interface INaslovService : IGenericService<Naslov, NaslovDto>
    {
        PagedList<NaslovDto> GetAllByAutorID(int autorId, PageCriteria pageCriteria);
        PagedList<NaslovDto> GetAllByClanakID(int clanakId, PageCriteria pageCriteria);
        PagedList<NaslovDto> GetAllByKljucnaRijecID(int kljucnaRijecId, PageCriteria pageCriteria);
        PagedList<NaslovDto> GetAllBySazetakID(int sazetakId, PageCriteria pageCriteria);
        PagedList<NaslovDto> GetAllByVrstaID(int vrstaId, PageCriteria pageCriteria);
    }
}
