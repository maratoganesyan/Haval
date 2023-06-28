using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class CarStatus
    {
        public CarStatus()
        {
            Car = new HashSet<Car>();
        }

        public int IdCarStatus { get; set; }
        public string CarStatusName { get; set; }

        public virtual ICollection<Car> Car { get; set; }
    }
}
