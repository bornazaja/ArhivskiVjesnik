using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using System.Collections.Generic;

namespace ArhivskiVjesnikLibrary.BLL.Services.Interfaces
{
    public interface IGenericService<TModel, TDto> where TModel : class where TDto : class
    {
        TDto GetByID(int id);
        IEnumerable<TDto> GetAll();
        IEnumerable<string> GetAll(string columnName, string term, int n = 10);
        PagedList<TDto> GetAll(QueryCriteria queryCriteria);
    }
}
