using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Performance : BaseEntity
    {
        public Guid IdPiesa { get; set; }
        public Piece Piece { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid HallId { get; set; }
        public Hall Hall { get; set; } = default!;
        public decimal Price { get; set; }
        public int TicketMaxScan { get; set; }
    }

}
