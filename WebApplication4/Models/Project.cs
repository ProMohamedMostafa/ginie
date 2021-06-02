using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace GenieMistro.Models
{
    public partial class Project
    {
        public Project()
        {
            Objectives = new HashSet<Objective>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ProjectWeight { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? StratigicObjectiveId { get; set; }

        public virtual StratigicObjective StratigicObjective { get; set; }
        public virtual ICollection<Objective> Objectives { get; set; }
    }
}
