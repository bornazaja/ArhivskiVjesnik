using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Models;

namespace ArhivskiVjesnikLibrary.DAL.Repositories.Interfaces
{
    public interface INaslovRepository : IGenericRepository<Naslov>
    {
        PagedList<Naslov> GetAllByAutorID(int autorId, PageCriteria pageCriteria);
        PagedList<Naslov> GetAllByClanakID(int clanakId, PageCriteria pageCriteria);
        PagedList<Naslov> GetAllByKljucnaRijecID(int kljucnaRijecId, PageCriteria pageCriteria);
        PagedList<Naslov> GetAllBySazetakID(int sazetakId, PageCriteria pageCriteria);
        PagedList<Naslov> GetAllByVrstaID(int vrstaId, PageCriteria pageCriteria);
    }
}
