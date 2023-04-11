using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{ 
    public class Actor : BaseEntity
    {
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public DateTime DataNastere { get; set; }
        public decimal Salariu { get; set; }
        public ICollection<Role> Roles { get; set; } = new List<Role>();

    }
}
