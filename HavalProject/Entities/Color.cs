﻿using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class Color
    {
        public Color()
        {
            Car = new HashSet<Car>();
        }

        public int IdColor { get; set; }
        public string ColorName { get; set; }
        public string ColorDescription { get; set; }
        public string HEX { get; set; }

        public virtual ICollection<Car> Car { get; set; }
    }
}
