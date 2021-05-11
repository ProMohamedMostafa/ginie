using System;
using System.Collections.Generic;

#nullable disable

namespace GenieMistro.Models
{
    public partial class ActionPlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ObjectiveId { get; set; }

        public virtual Objective Objective { get; set; }
    }
}
