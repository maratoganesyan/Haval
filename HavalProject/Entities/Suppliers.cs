using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class Suppliers
    {
        public Suppliers()
        {
            Supplies = new HashSet<Supplies>();
        }

        public int IdSupplier { get; set; }
        public int IdCity { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public virtual Cities IdCityNavigation { get; set; }
        public virtual ICollection<Supplies> Supplies { get; set; }
    }
}
