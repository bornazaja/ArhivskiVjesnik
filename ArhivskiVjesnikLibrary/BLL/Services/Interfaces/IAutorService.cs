using ArhivskiVjesnikLibrary.BLL.DTO;
using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Models;

namespace ArhivskiVjesnikLibrary.BLL.Services.Interfaces
{
    public interface IAutorService : IGenericService<Autor, AutorDto>
    {
        PagedList<AutorDto> GetAllByClanakID(int clanakId, PageCriteria pageCriteria);
        PagedList<AutorDto> GetAllByKljucnaRijecID(int kljucnaRijecId, PageCriteria pageCriteria);
        PagedList<AutorDto> GetAllByNaslovID(int naslovId, PageCriteria pageCriteria);
        PagedList<AutorDto> GetAllBySazetakID(int sazetakId, PageCriteria pageCriteria);
        PagedList<AutorDto> GetAllByVrstaID(int vrstaId, PageCriteria pageCriteria);
    }
}
