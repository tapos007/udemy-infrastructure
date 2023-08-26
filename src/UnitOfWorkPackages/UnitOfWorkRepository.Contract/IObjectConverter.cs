using System.Collections.Generic;
using System.Linq;

namespace UdemyInfrastructure.UnitOfWorkRepository.Contract
{
    public interface IObjectConverter<T, TDb> where T : class where TDb : class
    {
        IQueryable<T> ConvertDbQueryableToDomainQueryable(IQueryable source);

        IQueryable<TOther> ConvertDbQueryableToDomainQueryable<TOther>(IQueryable source) where TOther : class;

        T ConvertDbEntityToDomainEntity(TDb entity);
        
        IEnumerable<T> ConvertDbEntityToDomainEntity(IEnumerable<TDb> dbEntities);
        
        TDb ConvertDomainEntityToDbEntity(T domainObject);

        IEnumerable<TDb> ConvertDomainEntitiesToDbEntities(IEnumerable<T> domainObjects); 
        // change something
    }
}