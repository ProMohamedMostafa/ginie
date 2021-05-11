using System;
using System.Collections.Generic;

#nullable disable

namespace GenieMistro.Models
{
    public partial class MissionMissionDept
    {
        public int MissionId { get; set; }
        public int MissionDeptId { get; set; }

        public virtual Mission Mission { get; set; }
        public virtual MissionDept MissionDept { get; set; }
    }
}
