using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Role : BaseEntity
    {
        public string TitluRol { get; set; }
        public Guid IdPiesa { get; set; }
        public Piece Piece { get; set; } = default!;
        public Guid IdActor { get; set; }
        public Actor Actor { get; set; } = default!;

    }

}
