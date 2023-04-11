using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Hall : BaseEntity
    {
        public string Nume { get; set; }
        public int Capacity { get; set; } = 0;
        public decimal AdministrationCost { get; set; }
    }
}
