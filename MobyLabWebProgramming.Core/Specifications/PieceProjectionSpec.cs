using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class PieceProjectionSpec : BaseSpec<PieceProjectionSpec, Piece, PieceDTO>
{
    protected override Expression<Func<Piece, PieceDTO>> Spec => e => new()
    {
        Id = e.Id,
        Titlu = e.Titlu,
        Autor = e.Autor,
        Descriere = e.Descriere,
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt
    };

    public PieceProjectionSpec(Guid id) : base(id)
    {
    }

    public PieceProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Titlu, searchExpr) ||
                         EF.Functions.ILike(e.Descriere, searchExpr));
    }
}