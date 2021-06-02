using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace GenieMistro.Models
{
    public partial class EmployeeIndicator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int? IndicatorId { get; set; }

        public virtual TbEmployee Emp { get; set; }
        public virtual Indicator Indicator { get; set; }
    }
}
