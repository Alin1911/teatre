using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class RoleProjectionSpec : BaseSpec<RoleProjectionSpec, Role, RoleDTO>
{
    protected override Expression<Func<Role, RoleDTO>> Spec => e => new()
    {
        Id = e.Id,
        TitluRol = e.TitluRol,
        IdPiesa = e.IdPiesa,
        IdActor = e.IdActor,
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt
    };

    public RoleProjectionSpec(Guid id) : base(id)
    {
    }

    public RoleProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.TitluRol, searchExpr));
    }
}
