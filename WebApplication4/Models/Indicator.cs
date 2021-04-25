using System;
using System.Collections.Generic;

#nullable disable

namespace GenieMistro.Models
{
    public partial class Indicator
    {
        public Indicator()
        {
            TbEmployees = new HashSet<TbEmployee>();
        }

        public int Id { get; set; }
        public string Indicator1 { get; set; }
        public int? LevelIndex { get; set; }
        public int? ComId { get; set; }

        public virtual Competency Com { get; set; }
        public virtual ICollection<TbEmployee> TbEmployees { get; set; }
    }
}
