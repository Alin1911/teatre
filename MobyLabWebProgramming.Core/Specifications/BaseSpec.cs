using System.Linq.Expressions;
using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// These classes are used to implement the specification design pattern over the Ardalis Specification nuget package.
/// It's constructors can be used by derived classes to avoid boiler plate code.
/// Note the constraints imposed on the generic parameters.
/// </summary>
public abstract class BaseSpec<TDerived, T> : Specification<T> where TDerived : BaseSpec<TDerived, T> where T : BaseEntity
{
    /// <summary>
    /// Note that this property is sealed as to not be overriden by any derived class and used the base class's implementation.
    /// </summary>
    public sealed override ISpecificationBuilder<T> Query => base.Query;

    /// <summary>
    /// This constructor will simply indicate for the database query to order the entries after the created timestamp.
    /// Note that if the constructor parameter is false the order by expression will not be evaluated and will not be send to the database.
    /// </summary>
    protected BaseSpec(bool orderByCreatedAt = true) => Query.OrderByDescending(x => x.CreatedAt, orderByCreatedAt);
    /// <summary>
    /// This constructor will search for an entity after the given Guid Id.
    /// </summary>
    protected BaseSpec(Guid id) => Query.Where(e => e.Id == id);
    /// <summary>
    /// This constructor will search for any entity that is as the Id in the given list.
    /// </summary>
    protected BaseSpec(ICollection<Guid> ids, bool orderByCreatedAt = true) => Query.Where(e => ids.Contains(e.Id)).OrderByDescending(e => e.CreatedAt, orderByCreatedAt);
}

/// <summary>
/// This class is the same as the one above except it also projects/maps the database entity to another object via the Spec property that has to be implemented by the derived class.
/// Note that the projection/mapping will be evaluated if possible on the database, not the server.
/// </summary>
public abstract class BaseSpec<TDerived, T, TOut> : Specification<T, TOut> where TDerived : BaseSpec<TDerived, T, TOut> where T : BaseEntity
{
    public sealed override ISpecificationBuilder<T, TOut> Query => base.Query;
    /// <summary>
    /// This method should be implemented by the derived class to specify the projection/mapping of the entity to another object.
    /// Note that this property is not a function or lambda but an expression that is evaluated on runtime to symbolically process the database request and serialize it accordingly.
    /// </summary>
    protected abstract Expression<Func<T, TOut>> Spec { get; }
    /// <summary>
    /// This property gives this as the derived class, it is useful as shown bellow to avoid ambiguity on which method overrides to be used like for example for the Spec property.
    /// </summary>
    protected TDerived Derived => (TDerived) this;

    protected BaseSpec(bool orderByCreatedAt = true) => Query.Select(Derived.Spec).OrderByDescending(x => x.CreatedAt, orderByCreatedAt);
    protected BaseSpec(Guid id) => Query.Select(Derived.Spec).Where(e => e.Id == id);
    protected BaseSpec(ICollection<Guid> ids, bool orderByCreatedAt = true) => Query.Select(Derived.Spec).Where(e => ids.Contains(e.Id)).OrderByDescending(e => e.CreatedAt, orderByCreatedAt);
}