using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class Cities
    {
        public Cities()
        {
            Suppliers = new HashSet<Suppliers>();
        }

        public int IdCity { get; set; }
        public string CityName { get; set; }

        public virtual ICollection<Suppliers> Suppliers { get; set; }
    }
}
