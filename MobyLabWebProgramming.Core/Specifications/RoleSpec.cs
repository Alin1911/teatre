using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Specifications
{
    public sealed class RoleSpec : BaseSpec<RoleSpec, Role>
    {
        public RoleSpec(Guid id) : base(id)
        {
        }
    }
}
