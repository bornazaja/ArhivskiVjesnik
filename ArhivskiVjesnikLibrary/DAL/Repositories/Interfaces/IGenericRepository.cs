using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using System.Collections.Generic;

namespace ArhivskiVjesnikLibrary.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T GetByID(int id);
        IEnumerable<T> GetAll();
        IEnumerable<string> GetAll(string columnName, string term, int n = 10);
        PagedList<T> GetAll(QueryCriteria queryCriteria);
    }
}
