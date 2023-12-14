using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AB104APIIntro.Repositories.Interfaces;

public interface IRepository<T> where T : class,new()
{
    IEnumerable<T> GetAllAsync(params string[] includes);
    IQueryable<T> GetFilteredAsync(Expression<Func<T, bool>> expression,params string[] includes);
    Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression, params string[] includes);

    Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);
    Task CreateAsync(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task<int> SaveAsync();
}
