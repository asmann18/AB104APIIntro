using AB104APIIntro.DAL;
using AB104APIIntro.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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
        await SaveAsync();
    }

    public async Task Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        await SaveAsync();
    }

    public IEnumerable<T> GetAllAsync(params string[] includes)
    {
        IEnumerable<T> entities = _context.Set<T>();
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

    public async Task Update(T entity)
    {
        _context.Update(entity);
        await SaveAsync();
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
}
