using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Ticket : BaseEntity
    {
        public Guid PerformanceId { get; set; }
        public Performance Performance { get; set; } = default!;
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        public int NumarScanari { get; set; } = 0;
        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; } = default!;
        public string Code { get; set; }
    }
}
