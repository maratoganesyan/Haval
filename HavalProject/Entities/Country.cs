using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class Country
    {
        public Country()
        {
            Car = new HashSet<Car>();
        }

        public int IdCountry { get; set; }
        public string CountryName { get; set; }

        public virtual ICollection<Car> Car { get; set; }
    }
}
