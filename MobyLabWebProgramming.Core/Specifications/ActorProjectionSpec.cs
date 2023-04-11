using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace MobyLabWebProgramming.Core.Specifications
{
    public sealed class ActorProjectionSpec : BaseSpec<ActorProjectionSpec, Actor, ActorDTO>
    {
        protected override Expression<Func<Actor, ActorDTO>> Spec => e => new()
        {
            Id = e.Id,
            Nume = e.Nume,
            Prenume = e.Prenume,
            DataNastere = e.DataNastere,
            Salariu = e.Salariu
        };

        public ActorProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
        {
        }

        public ActorProjectionSpec(Guid id) : base(id)
        {
        }

        public ActorProjectionSpec(string? search)
        {
            search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

            if (search == null)
            {
                return;
            }

            var searchExpr = $"%{search.Replace(" ", "%")}%";

            Query.Where(e => EF.Functions.ILike(e.Nume, searchExpr) || EF.Functions.ILike(e.Prenume, searchExpr));

        }
    }
}
