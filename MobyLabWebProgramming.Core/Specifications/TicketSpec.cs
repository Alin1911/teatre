using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Specifications
{
    public sealed class TicketSpec : BaseSpec<TicketSpec, Ticket>
    {
        public TicketSpec(Guid id) : base(id)
        {
        }
    }
}
