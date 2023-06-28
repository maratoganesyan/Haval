using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class Engine
    {
        public Engine()
        {
            Car = new HashSet<Car>();
        }

        public int IdEngineName { get; set; }
        public string EngineName { get; set; }

        public virtual ICollection<Car> Car { get; set; }
    }
}
