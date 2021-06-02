using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace GenieMistro.Models
{
    public partial class Purpose
    {
        public Purpose()
        {
            Visions = new HashSet<Vision>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PurposeText { get; set; }
        public string PurposeDescription { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<Vision> Visions { get; set; }
    }
}
