﻿using System;
using System.Collections.Generic;

#nullable disable

namespace GenieMistro.Models
{
    public partial class Competency
    {
        public Competency()
        {
            Indicators = new HashSet<Indicator>();
        }

        public int ComId { get; set; }
        public string ComType { get; set; }
        public string ComDeptName { get; set; }
        public int? ComLevels { get; set; }

        public virtual ICollection<Indicator> Indicators { get; set; }
    }
}
