using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UdemyInfrastructure.UnitOfWorkRepository.Contract;

public interface IGenericRepository<TDto,TDb> where TDto: class where TDb: class
{
    Task<IEnumerable<TDto>> GetAll();
    IQueryable<TDb> AsQueryAble(Expression<Func<TDb, bool>> expression);
        

    Task<IEnumerable<TDto>> Find(Expression<Func<TDb, bool>> expression);
    Task<IEnumerable<TDto>> FindDistinct(Expression<Func<TDb, bool>> expression);
    Task<IEnumerable<TDto>> FindDistinct(Expression<Func<TDb, bool>> expression, IEqualityComparer<TDb> comparer);
        
    Task<IEnumerable<TOther>> Find<TOther>(Expression<Func<TDb, bool>> expression) where TOther : class;
    Task<IEnumerable<TOther>> FindDistinct<TOther>(Expression<Func<TDb, bool>> expression) where TOther : class;
    Task<IEnumerable<TOther>> FindDistinct<TOther>(Expression<Func<TDb, bool>> expression, IEqualityComparer<TDb> comparer) where TOther : class;
    Task<TDto> FindFirstOne(Expression<Func<TDb, bool>> expression);
        
    void Add(TDto entity);
    void AddRange(IEnumerable<TDto> entities);
    void Remove(TDto entity);
    void RemoveRange(IEnumerable<TDto> entities);
    void Update(TDto entityToUpdate);
    void BulkUpdate(IEnumerable<TDto> entitiesToUpdate);
    // some modification
}