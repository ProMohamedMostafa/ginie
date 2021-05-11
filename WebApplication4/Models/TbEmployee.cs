using System;
using System.Collections.Generic;

#nullable disable

namespace GenieMistro.Models
{
    public partial class TbEmployee
    {
        public TbEmployee()
        {
            ObjectiveEmployees = new HashSet<ObjectiveEmployee>();
        }

        public int EmpId { get; set; }
        public string EmpTitle { get; set; }
        public string EmpPassword { get; set; }
        public int? EmpTitleLevel { get; set; }
        public int? ManagerId { get; set; }
        public string EmpEmail { get; set; }
        public string EmpName { get; set; }

        public virtual ICollection<ObjectiveEmployee> ObjectiveEmployees { get; set; }
    }
}
