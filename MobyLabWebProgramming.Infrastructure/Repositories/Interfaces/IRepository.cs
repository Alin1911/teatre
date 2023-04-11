using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;

/// <summary>
/// This interface provides the generic methods to work with the database context easier and use the specification design pattern.
/// </summary>
public interface IRepository<out TDb> where TDb : DbContext
{
    /// <summary>
    /// It may be useful to get the database context if necessary, this is why this property exists.
    /// </summary>
    public TDb DbContext { get; }

    /// <summary>
    /// Gets a single entity or null by the provided id.
    /// </summary>
    public Task<T?> GetAsync<T>(Guid id, CancellationToken cancellationToken = default) where T : BaseEntity;
    /// <summary>
    /// Gets a single entity or null using the specifications provided.
    /// </summary>
    public Task<T?> GetAsync<T>(ISpecification<T> spec, CancellationToken cancellationToken = default) where T : BaseEntity;
    /// <summary>
    /// Gets a single entity projected on another object or null using the specifications provided.
    /// </summary>
    public Task<TOut?> GetAsync<T, TOut>(ISpecification<T, TOut> spec, CancellationToken cancellationToken = default) where T : BaseEntity;
    /// <summary>
    /// Counts the entities found in the database.
    /// </summary>
    public Task<int> GetCountAsync<T>(CancellationToken cancellationToken = default) where T : BaseEntity;
    /// <summary>
    /// Counts the entities found in the database that satisfy the specifications.
    /// </summary>
    public Task<int> GetCountAsync<T>(ISpecification<T> spec, CancellationToken cancellationToken = default) where T : BaseEntity;
    /// <summary>
    /// Counts the entities found in the database that satisfy the specifications, this exists if using a specification with a projection, the actual projection will be ignored.
    /// </summary>
    public Task<int> GetCountAsync<T, TOut>(ISpecification<T, TOut> spec, CancellationToken cancellationToken = default) where T : BaseEntity;
    /// <summary>
    /// Gets a list of entities that satisfy the specifications.
    /// </summary>
    public Task<List<T>> ListAsync<T>(ISpecification<T> spec, CancellationToken cancellationToken = default) where T : BaseEntity;
    /// <summary>
    /// Gets a list of entities that satisfy the specifications. This returns un-tracked entities that are not tracked by the framework so you cannot use the to persist data but reduces memory consumption.
    /// </summary>
    public Task<List<T>> ListNoTrackingAsync<T>(ISpecification<T> spec, CancellationToken cancellationToken = default) where T : BaseEntity;
    /// <summary>
    /// Gets a list of entities that satisfy the specifications projected onto another object type.
    /// </summary>
    public Task<List<TOut>> ListAsync<T, TOut>(ISpecification<T, TOut> spec, CancellationToken cancellationToken = default) where T : BaseEntity;
    /// <summary>
    /// Gets a page of entities that satisfy the specifications according to the pagination parameters.
    /// </summary>
    public Task<PagedResponse<T>> PageAsync<T>(PaginationQueryParams pagination, ISpecification<T> spec, CancellationToken cancellationToken = default) where T : BaseEntity;
    /// <summary>
    /// Gets a page of entities that satisfy the specifications projected onto another object type according to the pagination parameters.
    /// </summary>
    public Task<PagedResponse<TOut>> PageAsync<T, TOut>(PaginationQueryParams pagination, ISpecification<T, TOut> spec, CancellationToken cancellationToken = default) where T : BaseEntity;
    /// <summary>
    /// Adds an entity to the database.
    /// </summary>
    public Task<T> AddAsync<T>(T entity, CancellationToken cancellationToken = default) where T : BaseEntity;
    /// <summary>
    /// Adds an entity to the database and gets the projection via a specification.
    /// </summary>
    public Task<TOut> AddAsync<T, TOut>(T entity, ISpecification<T, TOut> spec, CancellationToken cancellationToken = default) where T : BaseEntity;
    /// <summary>
    /// Adds multiple entities to the database.
    /// </summary>
    public Task<List<T>> AddRangeAsync<T>(List<T> entities, CancellationToken cancellationToken = default) where T : BaseEntity;
    /// <summary>
    /// Adds multiple entities to the database and projects them to another object type.
    /// </summary>
    public Task<List<TOut>> AddRangeAsync<T, TOut>(List<T> entities, ISpecification<T, TOut> spec, CancellationToken cancellationToken = default) where T : BaseEntity;
    /// <summary>
    /// Adds multiple entities to the database and gets the added entities as un-tracked objects.
    /// </summary>
    public Task AddRangeNoTrackingAsync<T>(List<T> entities, CancellationToken cancellationToken = default) where T : BaseEntity;
    /// <summary>
    /// Updates an entity to the database.
    /// </summary>
    public Task<T> UpdateAsync<T>(T entity, CancellationToken cancellationToken = default) where T : BaseEntity;
    /// <summary>
    /// Updates an entity to the database and gets the projection via a specification.
    /// </summary>
    public Task<TOut> UpdateAsync<T, TOut>(T entity, ISpecification<T, TOut> spec, CancellationToken cancellationToken = default) where T : BaseEntity;
    /// <summary>
    /// Updates multiple entities to the database.
    /// </summary>
    public Task<List<T>> UpdateRangeAsync<T>(List<T> entities, CancellationToken cancellationToken = default) where T : BaseEntity;
    /// <summary>
    /// Updates multiple entities to the database and projects them to another object type.
    /// </summary>
    public Task<List<TOut>> UpdateRangeAsync<T, TOut>(List<T> entities, ISpecification<T, TOut> spec, CancellationToken cancellationToken = default) where T : BaseEntity;
    /// <summary>
    /// Deletes an entity identified by the id.
    /// </summary>
    public Task<int> DeleteAsync<T>(Guid id, CancellationToken cancellationToken = default) where T : BaseEntity;
    /// <summary>
    /// Deletes entities that satisfy the specifications.
    /// </summary>
    public Task<int> DeleteAsync<T>(ISpecification<T> spec, CancellationToken cancellationToken = default) where T : BaseEntity;
}