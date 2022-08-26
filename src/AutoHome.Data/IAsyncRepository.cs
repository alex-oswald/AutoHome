namespace AutoHome.Data;

using AutoHome.Data.Entities;
using System.Linq.Expressions;

public interface IAsyncRepository<T>
    where T : BaseEntity
{
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken);

    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<T>?> ListAsync(
        CancellationToken cancellationToken,
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = "");

    Task UpdateAsync(T entity, CancellationToken cancellationToken);
}
