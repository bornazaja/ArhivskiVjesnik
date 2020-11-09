using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Models;

namespace ArhivskiVjesnikLibrary.DAL.Repositories.Interfaces
{
    public interface IAutorRepository : IGenericRepository<Autor>
    {
        PagedList<Autor> GetAllByClanakID(int clanakId, PageCriteria pageCriteria);
        PagedList<Autor> GetAllByKljucnaRijecID(int kljucnaRijecId, PageCriteria pageCriteria);
        PagedList<Autor> GetAllByNaslovID(int naslovId, PageCriteria pageCriteria);
        PagedList<Autor> GetAllBySazetakID(int sazetakId, PageCriteria pageCriteria);
        PagedList<Autor> GetAllByVrstaID(int vrstaId, PageCriteria pageCriteria);
    }
}
