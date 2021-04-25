using System;
using System.Collections.Generic;

#nullable disable

namespace GenieMistro.Models
{
    public partial class TbEmployee
    {
        public TbEmployee()
        {
            InverseManager = new HashSet<TbEmployee>();
        }

        public int EmpId { get; set; }
        public string EmpTitle { get; set; }
        public string EmpPassword { get; set; }
        public int? EmpTitleLevel { get; set; }
        public int? ManagerId { get; set; }
        public int? IndicatorId { get; set; }
        public string EmpEmail { get; set; }
        public string EmpName { get; set; }

        public virtual Indicator Indicator { get; set; }
        public virtual TbEmployee Manager { get; set; }
        public virtual ICollection<TbEmployee> InverseManager { get; set; }
    }
}
