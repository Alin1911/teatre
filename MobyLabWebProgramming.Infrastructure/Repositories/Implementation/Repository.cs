using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Repositories.Implementation;

public sealed class Repository<TDb> : IRepository<TDb> where TDb : DbContext
{
    /// <summary>
    /// Inject the database context.
    /// </summary>
    public Repository(TDb dbContext) => DbContext = dbContext;

    public TDb DbContext { get; }

    public async Task<T?> GetAsync<T>(Guid id, CancellationToken cancellationToken = default) where T : BaseEntity =>
        await DbContext.Set<T>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async Task<T?> GetAsync<T>(ISpecification<T> spec, CancellationToken cancellationToken = default) where T : BaseEntity =>
        await new SpecificationEvaluator().GetQuery(DbContext.Set<T>().AsQueryable(), spec).FirstOrDefaultAsync(cancellationToken);

    public async Task<TOut?> GetAsync<T, TOut>(ISpecification<T, TOut> spec, CancellationToken cancellationToken = default) where T : BaseEntity =>
        await new SpecificationEvaluator().GetQuery(DbContext.Set<T>().AsQueryable(), spec).FirstOrDefaultAsync(cancellationToken);

    public async Task<List<T>> ListAsync<T>(ISpecification<T> spec, CancellationToken cancellationToken = default) where T : BaseEntity =>
        await new SpecificationEvaluator().GetQuery(DbContext.Set<T>().AsQueryable(), spec).ToListAsync(cancellationToken);

    public async Task<List<T>> ListNoTrackingAsync<T>(ISpecification<T> spec, CancellationToken cancellationToken = default) where T : BaseEntity =>
        await new SpecificationEvaluator().GetQuery(DbContext.Set<T>().AsQueryable().AsNoTracking(), spec).ToListAsync(cancellationToken);

    public async Task<List<TOut>> ListAsync<T, TOut>(ISpecification<T, TOut> spec, CancellationToken cancellationToken = default) where T : BaseEntity =>
        await new SpecificationEvaluator().GetQuery(DbContext.Set<T>().AsQueryable(), spec).ToListAsync(cancellationToken);

    public async Task<T> AddAsync<T>(T entity, CancellationToken cancellationToken = default) where T : BaseEntity
    {
        await DbContext.Set<T>().AddAsync(entity, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken); // This saves all changes that are made on tracked entities.

        return entity;
    }

    public async Task<TOut> AddAsync<T, TOut>(T entity, ISpecification<T, TOut> spec, CancellationToken cancellationToken = default) where T : BaseEntity =>
        new SpecificationEvaluator().GetQuery(new List<T> { await AddAsync(entity, cancellationToken) }.AsQueryable(), spec).Single();

    public async Task<List<T>> AddRangeAsync<T>(List<T> entities, CancellationToken cancellationToken = default) where T : BaseEntity
    {
        await DbContext.Set<T>().AddRangeAsync(entities, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);

        return entities;
    }

    public async Task AddRangeNoTrackingAsync<T>(List<T> entities, CancellationToken cancellationToken = default) where T : BaseEntity
    {
        await DbContext.Set<T>().AddRangeAsync(entities, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
        DbContext.ChangeTracker.Clear(); // This is used to un-track the entities that where added now.
    }

    public async Task<List<TOut>> AddRangeAsync<T, TOut>(List<T> entities, ISpecification<T, TOut> spec, CancellationToken cancellationToken = default) where T : BaseEntity =>
        new SpecificationEvaluator().GetQuery((await AddRangeAsync(entities, cancellationToken)).AsQueryable(), spec).ToList();

    public async Task<T> UpdateAsync<T>(T entity, CancellationToken cancellationToken = default) where T : BaseEntity
    {
        entity.UpdateTime(); // Sets the UpdatedAt to a new value.
        DbContext.Entry(entity).State = EntityState.Modified;
        await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task<TOut> UpdateAsync<T, TOut>(T entity, ISpecification<T, TOut> spec, CancellationToken cancellationToken = default) where T : BaseEntity =>
        new SpecificationEvaluator().GetQuery(new List<T> { await UpdateAsync(entity, cancellationToken) }.AsQueryable(), spec).Single();

    public async Task<List<T>> UpdateRangeAsync<T>(List<T> entities, CancellationToken cancellationToken = default) where T : BaseEntity
    {
        foreach (var entity in entities)
        {
            entity.UpdateTime(); // Sets the UpdatedAt to a new value.
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        await DbContext.SaveChangesAsync(cancellationToken);

        return entities;
    }

    public async Task<List<TOut>> UpdateRangeAsync<T, TOut>(List<T> entities, ISpecification<T, TOut> spec, CancellationToken cancellationToken = default) where T : BaseEntity =>
        new SpecificationEvaluator().GetQuery((await UpdateRangeAsync(entities, cancellationToken)).AsQueryable(), spec).ToList();

    public async Task<int> DeleteAsync<T>(Guid id, CancellationToken cancellationToken = default) where T : BaseEntity
    {
        var entity = await GetAsync<T>(id, cancellationToken); // Get the entity.

        if (entity == null)
        {
            return 0;
        }

        DbContext.Remove(entity); // And remove it.

        return await DbContext.SaveChangesAsync(cancellationToken); // Save the changes.
    }

    public async Task<int> DeleteAsync<T>(ISpecification<T> spec, CancellationToken cancellationToken = default) where T : BaseEntity
    {
        var entities = await ListAsync(spec, cancellationToken);
        DbContext.RemoveRange(entities);
        await DbContext.SaveChangesAsync(cancellationToken);

        return entities.Count;
    }

    public async Task<int> GetCountAsync<T>(CancellationToken cancellationToken = default) where T : BaseEntity =>
        await DbContext.Set<T>().CountAsync(cancellationToken);

    public async Task<int> GetCountAsync<T>(ISpecification<T> spec, CancellationToken cancellationToken = default) where T : BaseEntity =>
        await new SpecificationEvaluator().GetQuery(DbContext.Set<T>().AsQueryable(), spec).CountAsync(cancellationToken);

    public async Task<int> GetCountAsync<T, TOut>(ISpecification<T, TOut> spec, CancellationToken cancellationToken = default) where T : BaseEntity =>
        await new SpecificationEvaluator().GetQuery(DbContext.Set<T>().AsQueryable(), spec).CountAsync(cancellationToken);

    public async Task<PagedResponse<T>> PageAsync<T>(PaginationQueryParams pagination, ISpecification<T> spec, CancellationToken cancellationToken = default) where T : BaseEntity =>
        new(pagination.Page,
            pagination.PageSize,
            (uint)await GetCountAsync(spec, cancellationToken),
            await new SpecificationEvaluator().GetQuery(DbContext.Set<T>().AsQueryable(), spec)
                .Skip((int)((pagination.Page - 1) * pagination.PageSize))
                .Take((int)pagination.PageSize)
                .ToListAsync(cancellationToken)); // Here the limits for the page are computed using skip and limit query statements on the database, the specifications should include an order by,
                                                  // otherwise the results are non-deterministic.

    public async Task<PagedResponse<TOut>> PageAsync<T, TOut>(PaginationQueryParams pagination, ISpecification<T, TOut> spec, CancellationToken cancellationToken = default) where T : BaseEntity =>
        new(pagination.Page,
            pagination.PageSize,
            (uint)await GetCountAsync(spec, cancellationToken),
            await new SpecificationEvaluator().GetQuery(DbContext.Set<T>().AsQueryable(), spec)
                .Skip((int)((pagination.Page - 1) * pagination.PageSize))
                .Take((int)pagination.PageSize)
                .ToListAsync(cancellationToken));
}