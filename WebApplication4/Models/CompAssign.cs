using System;
using System.Collections.Generic;

#nullable disable

namespace GenieMistro.Models
{
    public partial class CompAssign
    {
        public int Id { get; set; }
        public int? CompId { get; set; }
        public int? ComLevel { get; set; }
        public int? StructureLevel { get; set; }

        public virtual Competency Comp { get; set; }
    }
}
