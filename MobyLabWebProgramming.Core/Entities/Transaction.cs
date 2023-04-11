using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Transaction : BaseEntity
    {
        public decimal TotalPrice { get; set; } = 0;
        public string Status { get; set; }
        public Guid UserId { get; set; }

        public User Client { get; set; } = default!;
    }
}
