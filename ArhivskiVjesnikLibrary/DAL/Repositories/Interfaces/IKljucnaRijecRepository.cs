using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Models;

namespace ArhivskiVjesnikLibrary.DAL.Repositories.Interfaces
{
    public interface IKljucnaRijecRepository : IGenericRepository<KljucnaRijec>
    {
        PagedList<KljucnaRijec> GetAllByAutorID(int autorId, PageCriteria pageCriteria);
        PagedList<KljucnaRijec> GetAllByClanakID(int clanakId, PageCriteria pageCriteria);
        PagedList<KljucnaRijec> GetAllByNaslovID(int naslovId, PageCriteria pageCriteria);
        PagedList<KljucnaRijec> GetAllBySazetakID(int sazetakId, PageCriteria pageCriteria);
        PagedList<KljucnaRijec> GetAllByVrstaID(int vrstaId, PageCriteria pageCriteria);
    }
}
