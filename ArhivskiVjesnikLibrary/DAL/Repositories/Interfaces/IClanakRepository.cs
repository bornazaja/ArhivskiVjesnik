using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Models;
using System.Collections.Generic;

namespace ArhivskiVjesnikLibrary.DAL.Repositories.Interfaces
{
    public interface IClanakRepository : IGenericRepository<Clanak>
    {
        PagedList<Clanak> GetAllByAutorID(int autorId, PageCriteria pageCriteria);
        PagedList<Clanak> GetAllByKljucnaRijecID(int kljucnaRijecId, PageCriteria pageCriteria);
        PagedList<Clanak> GetAllByNaslovID(int naslovId, PageCriteria pageCriteria);
        PagedList<Clanak> GetAllBySazetakID(int sazetakId, PageCriteria pageCriteria);
        PagedList<Clanak> GetAllByVrstaID(int vrstaId, PageCriteria pageCriteria);
    }
}
