using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Models;

namespace ArhivskiVjesnikLibrary.DAL.Repositories.Interfaces
{
    public interface IVrstaRepository : IGenericRepository<Vrsta>
    {
        PagedList<Vrsta> GetAllByAutorID(int autorId, PageCriteria pageCriteria);
        PagedList<Vrsta> GetAllByClanakID(int clanakId, PageCriteria pageCriteria);
        PagedList<Vrsta> GetAllByKljucnaRijecID(int kljucnaRijecId, PageCriteria pageCriteria);
        PagedList<Vrsta> GetAllByNaslovID(int naslovId, PageCriteria pageCriteria);
        PagedList<Vrsta> GetAllBySazetakID(int sazetakId, PageCriteria pageCriteria);
    }
}
