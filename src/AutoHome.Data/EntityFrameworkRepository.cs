using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace AutoHome.Data;

public class EntityFrameworkRepository<T, TDbContext> : IAsyncRepository<T>
        where T : class, IEntity
        where TDbContext : DbContext
{
    private readonly ILogger<EntityFrameworkRepository<T, TDbContext>> _logger;
    protected readonly TDbContext _dbContext;
    internal DbSet<T> _dbSet;

    public EntityFrameworkRepository(
        ILogger<EntityFrameworkRepository<T, TDbContext>> logger,
        TDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return entity;
    }

    public virtual async Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        _dbContext.Set<T>().Remove(entity);
        var writes = await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return writes > 0;
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<T>().FirstOrDefaultAsync(a => a.Id == id, cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task<IReadOnlyList<T>?> ListAsync(
        CancellationToken cancellationToken,
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = "")
    {
        try
        {
            // Get the dbSet from the Entity passed in                
            IQueryable<T> query = _dbSet;

            // Apply the filter
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Include the specified properties
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            // Sort
            if (orderBy != null)
            {
                return await orderBy(query).AsNoTracking().ToListAsync(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                return await query.AsNoTracking().ToListAsync(cancellationToken).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, nameof(ListAsync));
            return null;
        }
    }

    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
