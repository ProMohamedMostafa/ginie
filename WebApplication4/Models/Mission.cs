using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace GenieMistro.Models
{
    public partial class Mission
    {
        public Mission()
        {
            MissionMissionDepts = new HashSet<MissionMissionDept>();
            StratigicObjectives = new HashSet<StratigicObjective>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string MissionText { get; set; }
        public string MissionDescription { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? WightFromVision { get; set; }
        public string MissionType { get; set; }
        public string AffectingMisionSuccess { get; set; }
        public string AffectingMissionFailure { get; set; }
        public int? VisionId { get; set; }

        public virtual Vision Vision { get; set; }
        public virtual ICollection<MissionMissionDept> MissionMissionDepts { get; set; }
        public virtual ICollection<StratigicObjective> StratigicObjectives { get; set; }
    }
}
