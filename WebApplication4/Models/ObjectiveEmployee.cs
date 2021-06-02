using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace GenieMistro.Models
{
    public partial class ObjectiveEmployee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ObjectiveId { get; set; }
        public int EmployeeId { get; set; }

        public virtual TbEmployee Employee { get; set; }
        public virtual Objective Objective { get; set; }
    }
}
