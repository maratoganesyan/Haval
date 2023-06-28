using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class EngineType
    {
        public EngineType()
        {
            Car = new HashSet<Car>();
        }

        public int IdEngineType { get; set; }
        public string EngineTypeName { get; set; }

        public virtual ICollection<Car> Car { get; set; }
    }
}
