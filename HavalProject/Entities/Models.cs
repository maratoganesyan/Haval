using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class Models
    {
        public Models()
        {
            Car = new HashSet<Car>();
        }

        public int IdModel { get; set; }
        public string ModelName { get; set; }

        public virtual ICollection<Car> Car { get; set; }
    }
}
