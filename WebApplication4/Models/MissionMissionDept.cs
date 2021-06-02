using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace GenieMistro.Models
{
    public partial class MissionMissionDept
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MissionId { get; set; }
        public int MissionDeptId { get; set; }

        public virtual Mission Mission { get; set; }
        public virtual MissionDept MissionDept { get; set; }
    }
}
