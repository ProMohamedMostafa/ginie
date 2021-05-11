using System;
using System.Collections.Generic;

#nullable disable

namespace GenieMistro.Models
{
    public partial class ObjectiveEmployee
    {
        public int ObjectiveId { get; set; }
        public int EmployeeId { get; set; }

        public virtual TbEmployee Employee { get; set; }
        public virtual Objective Objective { get; set; }
    }
}
