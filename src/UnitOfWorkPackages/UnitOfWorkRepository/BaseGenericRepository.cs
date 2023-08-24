using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UdemyInfrastructure.UnitOfWorkRepository.Contract;

namespace UdemyInfrastructure.UnitOfWorkRepository;

public class BaseGenericRepository<TDto, TDb> : IGenericRepository<TDto, TDb> where TDb : class where TDto : class
{
    protected readonly DbContext Context;

    protected readonly IMapper _mapper;

    public BaseGenericRepository(DbContext context, IServiceProvider serviceProvider)
    {
        Context = context;
        _mapper = serviceProvider.GetRequiredService<IMapper>();
    }

    public async Task<IEnumerable<TDto>> GetAll()
    {
        var dbQueryable = await Context.Set<TDb>().ToListAsync();
        return _mapper.Map<IEnumerable<TDto>>(dbQueryable);
    }

    public IQueryable<TDb> AsQueryAble(Expression<Func<TDb, bool>> expression)
    {
        return Context.Set<TDb>().Where(expression);
    }


    public async Task<IEnumerable<TDto>> Find(Expression<Func<TDb, bool>> expression)
    {
        var dbQueryable = await Context.Set<TDb>().Where(expression).ToListAsync();

        return _mapper.Map<IEnumerable<TDto>>(dbQueryable);
    }


    public async Task<IEnumerable<TDto>> FindDistinct(Expression<Func<TDb, bool>> expression)
    {
        var dbQueryable = await Context.Set<TDb>().Where(expression).Distinct().ToListAsync();
        return _mapper.Map<IEnumerable<TDto>>(dbQueryable);
    }

    public async Task<IEnumerable<TDto>> FindDistinct(Expression<Func<TDb, bool>> expression,
        IEqualityComparer<TDb> comparer)
    {
        var dbQueryable = await Context.Set<TDb>().Where(expression).Distinct(comparer).ToListAsync();
        return _mapper.Map<IEnumerable<TDto>>(dbQueryable);
    }

    public async Task<IEnumerable<TOther>> Find<TOther>(Expression<Func<TDb, bool>> expression) where TOther : class
    {
        var dbQueryable = await Context.Set<TDb>().Where(expression).ToListAsync();
        return _mapper.Map<IEnumerable<TOther>>(dbQueryable);
    }

    public async Task<IEnumerable<TOther>> FindDistinct<TOther>(Expression<Func<TDb, bool>> expression)
        where TOther : class
    {
        var dbQueryable = await Context.Set<TDb>().Where(expression).Distinct().ToListAsync();
        return _mapper.Map<IEnumerable<TOther>>(dbQueryable);
    }

    public async Task<IEnumerable<TOther>> FindDistinct<TOther>(Expression<Func<TDb, bool>> expression,
        IEqualityComparer<TDb> comparer) where TOther : class
    {
        var dbQueryable = await Context.Set<TDb>().Where(expression).Distinct(comparer).ToListAsync();
        return _mapper.Map<IEnumerable<TOther>>(dbQueryable);
    }

    public async Task<TDto> FindFirstOne(Expression<Func<TDb, bool>> expression)
    {
        var dbo = await Context.Set<TDb>().Where(expression).FirstOrDefaultAsync();
        return dbo == null ? null! : _mapper.Map<TDto>(dbo);
    }

    public void Add(TDto entity)
    {
        var dbo = _mapper.Map<TDb>(entity);
        Context.Set<TDb>().Add(dbo);
    }

    public void AddRange(IEnumerable<TDto> entities)
    {
        var dbList = _mapper.Map<IEnumerable<TDb>>(entities);
        Context.Set<TDb>().AddRange(dbList);
    }

    public void Remove(TDto entity)
    {
        var dbo = _mapper.Map<TDb>(entity);
        Context.Set<TDb>().Remove(dbo);
    }

    public void RemoveRange(IEnumerable<TDto> entities)
    {
        var dbList = _mapper.Map<IEnumerable<TDb>>(entities);
        Context.Set<TDb>().RemoveRange(dbList);
    }

    public void Update(TDto entityToUpdate)
    {
        var dboToUpdate = _mapper.Map<TDb>(entityToUpdate);
        Context.Set<TDb>().Update(dboToUpdate);
    }

    public void BulkUpdate(IEnumerable<TDto> entities)
    {
        var dbList = _mapper.Map<IEnumerable<TDb>>(entities);
        Context.Set<TDb>().UpdateRange(dbList);
    }
}