using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects
{
    public class ActorDTO
    {
        public Guid Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public DateTime DataNastere { get; set; }
        public decimal Salariu { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
