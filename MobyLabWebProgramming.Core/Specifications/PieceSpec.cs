using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Specifications
{
    public sealed class PieceSpec : BaseSpec<PieceSpec, Piece>
    {
        public PieceSpec(Guid id) : base(id)
        {
        }
    }
}
