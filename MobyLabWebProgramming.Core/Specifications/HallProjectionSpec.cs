using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class HallProjectionSpec : BaseSpec<HallProjectionSpec, Hall, HallDTO>
{
    protected override Expression<Func<Hall, HallDTO>> Spec => e => new()
    {
        Id = e.Id,
        Nume = e.Nume,
        Capacity = e.Capacity,
        AdministrationCost = e.AdministrationCost,
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt
    };

    public HallProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public HallProjectionSpec(Guid id) : base(id)
    {
    }

    public HallProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Nume, searchExpr));
    }
}