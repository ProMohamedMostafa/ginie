using System;
using System.Collections.Generic;

#nullable disable

namespace GenieMistro.Models
{
    public partial class MissionDept
    {
        public MissionDept()
        {
            MissionMissionDepts = new HashSet<MissionMissionDept>();
        }

        public int Id { get; set; }
        public string DepName { get; set; }
        public string DeptDescription { get; set; }

        public virtual ICollection<MissionMissionDept> MissionMissionDepts { get; set; }
    }
}
