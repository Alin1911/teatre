using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class PerformanceProjectionSpec : BaseSpec<PerformanceProjectionSpec, Performance, PerformanceDTO>
{
    protected override Expression<Func<Performance, PerformanceDTO>> Spec => e => new()
    {
        Id = e.Id,
        IdPiesa = e.IdPiesa,
        StartDate = e.StartDate,
        EndDate = e.EndDate,
        HallId = e.HallId,
        Price = e.Price,
        TicketMaxScan = e.TicketMaxScan,
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt
    };

    public PerformanceProjectionSpec(Guid id) : base(id)
    {
    }

    public PerformanceProjectionSpec(string? search)
    {
    }
}
