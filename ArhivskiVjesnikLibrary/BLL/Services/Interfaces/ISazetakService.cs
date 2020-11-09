using ArhivskiVjesnikLibrary.BLL.DTO;
using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Models;

namespace ArhivskiVjesnikLibrary.BLL.Services.Interfaces
{
    public interface ISazetakService : IGenericService<Sazetak, SazetakDto>
    {
        PagedList<SazetakDto> GetAllByAutorID(int autorId, PageCriteria pageCriteria);
        PagedList<SazetakDto> GetAllByClanakID(int clanakId, PageCriteria pageCriteria);
        PagedList<SazetakDto> GetAllByKljucnaRijecID(int kljucnaRijecId, PageCriteria pageCriteria);
        PagedList<SazetakDto> GetAllByNaslovID(int naslovId, PageCriteria pageCriteria);
        PagedList<SazetakDto> GetAllByVrstaID(int vrstaId, PageCriteria pageCriteria);
    }
}
