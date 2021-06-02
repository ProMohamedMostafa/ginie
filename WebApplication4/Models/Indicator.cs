using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace GenieMistro.Models
{
    public partial class Indicator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string IndicatorText { get; set; }
        public int? LevelIndex { get; set; }
        public int? ComId { get; set; }

        public virtual Competency Com { get; set; }
    }
}
