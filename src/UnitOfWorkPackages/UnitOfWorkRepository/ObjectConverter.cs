using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using UdemyInfrastructure.UnitOfWorkRepository.Contract;

namespace UdemyInfrastructure.UnitOfWorkRepository
{
    public class ObjectConverter<T, TDb> : IObjectConverter<T, TDb> where T : class where TDb : class
    {
        private readonly IMapper _mapper;

        public ObjectConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IQueryable<T> ConvertDbQueryableToDomainQueryable(IQueryable source)
        {
            return source.ProjectTo<T>(_mapper.ConfigurationProvider);
        }

        public IQueryable<TOther> ConvertDbQueryableToDomainQueryable<TOther>(IQueryable source) where TOther: class
        {
            return source.ProjectTo<TOther>(_mapper.ConfigurationProvider);
        }

        public T ConvertDbEntityToDomainEntity(TDb entity) => _mapper.Map<T>(entity);

        public IEnumerable<T> ConvertDbEntityToDomainEntity(IEnumerable<TDb> dbEntities)
        {
            return _mapper.Map<IEnumerable<TDb>, List<T>>(dbEntities);
        }

        public TDb ConvertDomainEntityToDbEntity(T domainObject) => _mapper.Map<TDb>(domainObject);

        public IEnumerable<TDb> ConvertDomainEntitiesToDbEntities(IEnumerable<T> domainObjects) => _mapper.Map<IEnumerable<TDb>>(domainObjects);


    }
}
