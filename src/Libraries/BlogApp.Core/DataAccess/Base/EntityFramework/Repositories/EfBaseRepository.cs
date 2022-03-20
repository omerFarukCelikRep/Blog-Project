﻿using BlogApp.Core.DataAccess.Abstract;
using BlogApp.Core.Entities.Base;
using BlogApp.Core.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogApp.Core.DataAccess.Base.EntityFramework.Repositories;
public class EfBaseRepository<TEntity, TContext> : IRepositoryAsync<TEntity>
    where TEntity : BaseEntity
    where TContext : DbContext
{
    protected readonly TContext _context;
    protected readonly DbSet<TEntity> _table;

    public EfBaseRepository(TContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _table.AddAsync(entity);

        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        entity.Status = Status.Deleted;

        await UpdateAsync(entity);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _table.Where(x => x.Status != Status.Deleted)
                           .AsNoTracking()
                           .ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _table.Where(x => x.Status != Status.Deleted)
                         .Where(expression)
                         .AsNoTracking()
                         .ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false)
    {
        var data = _table.Where(x => x.Status != Status.Deleted)
                         .Where(expression)
                         .AsNoTracking();
        if (orderDesc)
        {
            return await data.OrderByDescending(orderby)
                             .ToListAsync();
        }

        return await data.OrderBy(orderby)
                         .ToListAsync();
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _table.FirstOrDefaultAsync(expression);
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await _table.FindAsync(id);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }

}