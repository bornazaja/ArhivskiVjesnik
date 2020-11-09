using ArhivskiVjesnikLibrary.BLL.DTO;
using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Models;

namespace ArhivskiVjesnikLibrary.BLL.Services.Interfaces
{
    public interface IKljucnaRijecService : IGenericService<KljucnaRijec, KljucnaRijecDto>
    {
        PagedList<KljucnaRijecDto> GetAllByAutorID(int autorId, PageCriteria pageCriteria);
        PagedList<KljucnaRijecDto> GetAllByClanakID(int clanakId, PageCriteria pageCriteria);
        PagedList<KljucnaRijecDto> GetAllByNaslovID(int naslovId, PageCriteria pageCriteria);
        PagedList<KljucnaRijecDto> GetAllBySazetakID(int sazetakId, PageCriteria pageCriteria);
        PagedList<KljucnaRijecDto> GetAllByVrstaID(int vrstaId, PageCriteria pageCriteria);
    }
}
