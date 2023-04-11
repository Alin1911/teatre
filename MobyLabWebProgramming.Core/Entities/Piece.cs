using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Piece : BaseEntity
    {
        public string Titlu { get; set; }
        public string Autor { get; set; }
        public string Descriere { get; set; }
        public ICollection<Role> Roles { get; set; } = new List<Role>();

    }

}
