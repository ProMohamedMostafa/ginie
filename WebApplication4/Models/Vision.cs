using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace GenieMistro.Models
{
    public partial class Vision
    {
        public Vision()
        {
            Missions = new HashSet<Mission>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string VisionText { get; set; }
        public string VisionDescription { get; set; }
        public int? PurposeId { get; set; }

        public virtual Purpose Purpose { get; set; }
        public virtual ICollection<Mission> Missions { get; set; }
    }
}
