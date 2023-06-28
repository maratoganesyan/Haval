using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class SupplyStatuses
    {
        public SupplyStatuses()
        {
            Supplies = new HashSet<Supplies>();
        }

        public int IdStatus { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<Supplies> Supplies { get; set; }
    }
}
