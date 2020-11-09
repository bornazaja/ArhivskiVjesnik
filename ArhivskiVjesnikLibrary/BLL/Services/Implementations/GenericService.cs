using ArhivskiVjesnikLibrary.BLL.Services.Interfaces;
using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikLibrary.DAL.Repositories.Interfaces;
using AutoMapper;
using System.Collections.Generic;

namespace ArhivskiVjesnikLibrary.BLL.Services.Implementations
{
    public class GenericService<TModel, TDto> : IGenericService<TModel, TDto> where TModel : class where TDto : class
    {
        private IGenericRepository<TModel> _genericRepository;
        private IMapper _mapper;

        public GenericService(IGenericRepository<TModel> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public IEnumerable<TDto> GetAll()
        {
            IEnumerable<TModel> models = _genericRepository.GetAll();
            return _mapper.Map<IEnumerable<TModel>, List<TDto>>(models);
        }

        public IEnumerable<string> GetAll(string columnName, string term, int n = 10)
        {
            IEnumerable<string> list = _genericRepository.GetAll(columnName, term, n);
            return list;
        }

        public PagedList<TDto> GetAll(QueryCriteria queryCriteria)
        {
            PagedList<TModel> pagedList = _genericRepository.GetAll(queryCriteria);
            return _mapper.Map<PagedList<TModel>, PagedList<TDto>>(pagedList);
        }

        public TDto GetByID(int id)
        {
            TModel model = _genericRepository.GetByID(id);
            return _mapper.Map<TModel, TDto>(model);
        }
    }
}
