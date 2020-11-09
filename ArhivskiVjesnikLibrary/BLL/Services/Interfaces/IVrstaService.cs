using ArhivskiVjesnikLibrary.BLL.DTO;
using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Models;

namespace ArhivskiVjesnikLibrary.BLL.Services.Interfaces
{
    public interface IVrstaService : IGenericService<Vrsta, VrstaDto>
    {
        PagedList<VrstaDto> GetAllByAutorID(int autorId, PageCriteria pageCriteria);
        PagedList<VrstaDto> GetAllByClanakID(int clanakId, PageCriteria pageCriteria);
        PagedList<VrstaDto> GetAllByKljucnaRijecID(int kljucnaRijecId, PageCriteria pageCriteria);
        PagedList<VrstaDto> GetAllByNaslovID(int naslovId, PageCriteria pageCriteria);
        PagedList<VrstaDto> GetAllBySazetakID(int sazetakId, PageCriteria pageCriteria);
    }
}
