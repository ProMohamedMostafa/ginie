using System;
using System.Collections.Generic;

#nullable disable

namespace GenieMistro.Models
{
    public partial class StratigicObjective
    {
        public StratigicObjective()
        {
            InverseDepdancyStratigicObjectiveNavigation = new HashSet<StratigicObjective>();
            Projects = new HashSet<Project>();
        }

        public int Id { get; set; }
        public string StratigicObjectiveName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StrObjDescription { get; set; }
        public string Note { get; set; }
        public int? DepdancyStratigicObjective { get; set; }
        public int MissionId { get; set; }

        public virtual StratigicObjective DepdancyStratigicObjectiveNavigation { get; set; }
        public virtual Mission Mission { get; set; }
        public virtual ICollection<StratigicObjective> InverseDepdancyStratigicObjectiveNavigation { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
