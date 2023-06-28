using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class Year
    {
        public Year()
        {
            Car = new HashSet<Car>();
        }

        public int IdYear { get; set; }
        public string YearValue { get; set; }

        public virtual ICollection<Car> Car { get; set; }
    }
}
