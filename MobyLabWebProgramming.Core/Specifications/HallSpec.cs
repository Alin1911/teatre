using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class HallSpec : BaseSpec<HallSpec, Hall>
{
    public HallSpec(Guid id) : base(id)
    {
    }

    public HallSpec(string nume)
    {
        Query.Where(e => e.Nume == nume);
    }
}