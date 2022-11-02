using AutoHome.Core.Entities;
using System.Linq.Expressions;

namespace AutoHome.Core;

public interface IRepository<T>
    where T : class, IEntity
{
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken);

    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<T>> GetAllAsync(
        CancellationToken cancellationToken,
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = "");

    Task<IPagedResult<T>> GetPageAsync(
        IPagedRequest pagedRequest,
        CancellationToken cancellationToken,
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = "");

    Task UpdateAsync(T entity, CancellationToken cancellationToken);

    IQueryable<T> Set();
}
