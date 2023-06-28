using HavalProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavalProject.Tools.ModelsOfETypes
{
    internal class ModelForSupplyReport
    {
        public DateTime StartDate;
        public DateTime EndDate;

        public List<GoodsInSupply> GoodsInSupplies;

        public decimal TotalCost;

        public ModelForSupplyReport(DateTime startDate,
            DateTime endDate,
            List<GoodsInSupply> goodsInSupplies)
        {
            StartDate = startDate;
            EndDate = endDate;
            GoodsInSupplies = goodsInSupplies;
            TotalCost = 0M;
            GoodsInSupplies.ForEach(g => TotalCost += g.SupplyPrice * g.Quantity);

        }
    }
}
