using HavalProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavalProject.Tools.ModelsOfETypes
{
    internal class WaterFallBarModel
    {
        public bool IsOrder;
        public Order order;
        public Supplies supply;
        public DateTime date;
        public WaterFallBarModel(Order order)
        {
            this.order = order;
            IsOrder = true;
            date = this.order.DateTimeOfOrder;
        }
        public WaterFallBarModel(Supplies supply)
        {
            this.supply = supply;
            IsOrder = false;
            date = this.supply.DateTimeOfSupply.Value;
        }
    }
}
