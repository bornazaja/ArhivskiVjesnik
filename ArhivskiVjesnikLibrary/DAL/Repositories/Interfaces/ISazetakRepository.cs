using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Models;

namespace ArhivskiVjesnikLibrary.DAL.Repositories.Interfaces
{
    public interface ISazetakRepository : IGenericRepository<Sazetak>
    {
        PagedList<Sazetak> GetAllByAutorID(int autorId, PageCriteria pageCriteria);
        PagedList<Sazetak> GetAllByClanakID(int clanakId, PageCriteria pageCriteria);
        PagedList<Sazetak> GetAllByKljucnaRijecID(int kljucnaRijecId, PageCriteria pageCriteria);
        PagedList<Sazetak> GetAllByNaslovID(int naslovId, PageCriteria pageCriteria);
        PagedList<Sazetak> GetAllByVrstaID(int vrstaId, PageCriteria pageCriteria);
    }
}
