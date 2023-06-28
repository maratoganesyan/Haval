using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class RudderSide
    {
        public RudderSide()
        {
            Car = new HashSet<Car>();
        }

        public int IdRudderSide { get; set; }
        public string RuddeSideName { get; set; }

        public virtual ICollection<Car> Car { get; set; }
    }
}
