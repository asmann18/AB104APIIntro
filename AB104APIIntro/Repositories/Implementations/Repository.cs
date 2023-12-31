﻿using AB104APIIntro.DAL;
using AB104APIIntro.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AB104APIIntro.Repositories.Implementations;

public class Repository<T> : IRepository<T> where T : class, new()
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public IQueryable<T> GetAllAsync(bool isTracking = false, params string[] includes)
    {
        
        IQueryable<T> entities = _context.Set<T>();
        if (!isTracking)
            entities.AsNoTracking();
        return entities;
    }

    

    public IQueryable<T> GetFilteredAsync(Expression<Func<T, bool>> expression, params string[] includes)
    {
        var query = _context.Set<T>().Where(expression).AsQueryable();
        foreach (string include in includes)
        {
            query = query.Include(include);
        }
        return query;
    }

    public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
    {
        return await _context.Set<T>().AnyAsync(expression);
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public  void Update(T entity)
    {
        _context.Update(entity);
    }

    public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression, params string[] includes)
    {
        var query = _context.Set<T>().AsQueryable<T>();
        foreach (var include in includes)
        {
            query.Include(include);
        }
        T? entity = await query.FirstOrDefaultAsync(expression);
        return entity;
    }

    public IQueryable<T> OrderBy(IQueryable<T> query, Expression<Func<T, object>> expression)
    {
        IQueryable<T> result=query.OrderBy(expression);
        return result;
    }

    public IQueryable<T> Paginate(IQueryable<T> query, int limit, int page=1)
    {
        IQueryable < T > result= query.Skip((page - 1) * limit).Take(limit);
        return result;
    }

}
