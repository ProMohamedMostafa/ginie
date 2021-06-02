using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace GenieMistro.Models
{
    public partial class MissionDept
    {
        public MissionDept()
        {
            MissionMissionDepts = new HashSet<MissionMissionDept>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DepName { get; set; }
        public string DeptDescription { get; set; }

        public virtual ICollection<MissionMissionDept> MissionMissionDepts { get; set; }
    }
}
