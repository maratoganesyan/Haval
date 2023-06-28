using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class Configuration
    {
        public Configuration()
        {
            Car = new HashSet<Car>();
        }

        public int IdConfiguration { get; set; }
        public string ConfigurationName { get; set; }

        public virtual ICollection<Car> Car { get; set; }
    }
}
