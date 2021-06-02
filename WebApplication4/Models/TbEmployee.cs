using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace GenieMistro.Models
{
    public partial class TbEmployee
    {
        public TbEmployee()
        {
            ObjectiveEmployees = new HashSet<ObjectiveEmployee>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string EmpTitle { get; set; }
        public string EmpPassword { get; set; }
        public int? EmpTitleLevel { get; set; }
        public int? ManagerId { get; set; }
        public string EmpEmail { get; set; }
        public string EmpName { get; set; }

        public virtual ICollection<ObjectiveEmployee> ObjectiveEmployees { get; set; }
    }
}
