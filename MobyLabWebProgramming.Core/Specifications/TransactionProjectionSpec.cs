using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class TransactionProjectionSpec : BaseSpec<TransactionProjectionSpec, Transaction, TransactionDTO>
{
    protected override Expression<Func<Transaction, TransactionDTO>> Spec => e => new()
    {
        Id = e.Id,
        TotalPrice = e.TotalPrice,
        Status = e.Status,
        UserId = e.UserId,
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt
    };

    public TransactionProjectionSpec(Guid id) : base(id)
    {
    }

    public TransactionProjectionSpec(string? search)
    {
    }
}