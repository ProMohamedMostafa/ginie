using System;
using System.Collections.Generic;

#nullable disable

namespace GenieMistro.Models
{
    public partial class Purpose
    {
        public Purpose()
        {
            Visions = new HashSet<Vision>();
        }

        public int Id { get; set; }
        public string PurposeText { get; set; }
        public string PurposeDescription { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<Vision> Visions { get; set; }
    }
}
