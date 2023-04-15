using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class ActorSpec : BaseSpec<ActorSpec, Actor>
{
    public ActorSpec(Guid id) : base(id)
    {
    }

    public ActorSpec(string nume)
    {
        Query.Where(e => e.Nume == nume);
    }

    public ActorSpec(string nume, string prenume)
    {
        Query.Where(e => e.Nume == nume && e.Prenume == prenume);
    }
}