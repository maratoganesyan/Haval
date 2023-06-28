using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class Supplies
    {
        public Supplies()
        {
            GoodsInSupply = new HashSet<GoodsInSupply>();
        }

        public int IdSupply { get; set; }
        public int IdSupplier { get; set; }
        public DateTime? DateTimeOfSupply { get; set; }
        public decimal? TotalPriceOfSupply { get; set; }
        public int IdSupplyStatus { get; set; }

        public virtual Suppliers IdSupplierNavigation { get; set; }
        public virtual SupplyStatuses IdSupplyStatusNavigation { get; set; }
        public virtual ICollection<GoodsInSupply> GoodsInSupply { get; set; }
    }
}
