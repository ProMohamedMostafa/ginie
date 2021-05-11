using System;
using System.Collections.Generic;

#nullable disable

namespace GenieMistro.Models
{
    public partial class Vision
    {
        public Vision()
        {
            Missions = new HashSet<Mission>();
        }

        public int Id { get; set; }
        public string VisionText { get; set; }
        public string VisionDescription { get; set; }
        public int? PurposeId { get; set; }

        public virtual Purpose Purpose { get; set; }
        public virtual ICollection<Mission> Missions { get; set; }
    }
}
