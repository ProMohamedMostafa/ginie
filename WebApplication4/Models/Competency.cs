using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace GenieMistro.Models
{
    public partial class Competency
    {
        public Competency()
        {
            CompAssigns = new HashSet<CompAssign>();
            Indicators = new HashSet<Indicator>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ComType { get; set; }
        public string ComDeptName { get; set; }
        public int? ComLevels { get; set; }

        public virtual ICollection<CompAssign> CompAssigns { get; set; }
        public virtual ICollection<Indicator> Indicators { get; set; }
    }
}
