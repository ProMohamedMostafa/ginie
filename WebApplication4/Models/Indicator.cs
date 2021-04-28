using System;
using System.Collections.Generic;

#nullable disable

namespace GenieMistro.Models
{
    public partial class Indicator
    {
        public int Id { get; set; }
        public string IndicatorText { get; set; }
        public int? LevelIndex { get; set; }
        public int? ComId { get; set; }

        public virtual Competency Com { get; set; }
    }
}
