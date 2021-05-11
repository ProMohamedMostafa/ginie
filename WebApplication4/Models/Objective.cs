using System;
using System.Collections.Generic;

#nullable disable

namespace GenieMistro.Models
{
    public partial class Objective
    {
        public Objective()
        {
            ActionPlans = new HashSet<ActionPlan>();
            ObjectiveEmployees = new HashSet<ObjectiveEmployee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ProjectId { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<ActionPlan> ActionPlans { get; set; }
        public virtual ICollection<ObjectiveEmployee> ObjectiveEmployees { get; set; }
    }
}
