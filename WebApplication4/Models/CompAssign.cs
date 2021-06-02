using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace GenieMistro.Models
{
    public partial class CompAssign
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? CompId { get; set; }
        public int? ComLevel { get; set; }
        public int? StructureLevel { get; set; }

        public virtual Competency Comp { get; set; }
    }
}
