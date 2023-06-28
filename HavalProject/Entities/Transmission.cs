using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class Transmission
    {
        public Transmission()
        {
            Car = new HashSet<Car>();
        }

        public int IdTransmission { get; set; }
        public string TransmissionName { get; set; }

        public virtual ICollection<Car> Car { get; set; }
    }
}
