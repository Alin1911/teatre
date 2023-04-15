using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class TicketProjectionSpec : BaseSpec<TicketProjectionSpec, Ticket, TicketDTO>
{
    protected override Expression<Func<Ticket, TicketDTO>> Spec => e => new()
    {
        Id = e.Id,
        PerformanceId = e.PerformanceId,
        UserId = e.UserId,
        NumarScanari = e.NumarScanari,
        TransactionId = e.TransactionId,
        Code = e.Code,
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt
    };

    public TicketProjectionSpec(Guid id) : base(id)
    {
    }

    public TicketProjectionSpec(string? search)
    {
    }
}