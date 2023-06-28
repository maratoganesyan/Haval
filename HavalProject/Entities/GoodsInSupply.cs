using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class GoodsInSupply
    {
        public int IdSupply { get; set; }
        public int IdCar { get; set; }
        public int Quantity { get; set; }
        public decimal SupplyPrice { get; set; }

        public virtual Car IdCarNavigation { get; set; }
        public virtual Supplies IdSupplyNavigation { get; set; }
    }
}
