using HavalProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavalProject.Tools.ModelsOfETypes
{
    internal class ModelForSalesReport
    {
        public DateTime StartDate;
        public DateTime EndDate;

        public List<Order> OrdersForReport;

        public decimal TotalCostOfReport;
        public ModelForSalesReport(DateTime startDate, DateTime endDate, List<Order> ordersForReport)
        {
            StartDate = startDate;
            EndDate = endDate;
            OrdersForReport = ordersForReport.OrderBy(x => x.IdCarNavigation.IdModel).ToList();
            TotalCostOfReport = OrdersForReport.Sum(x => x.IdCarNavigation.ClientPrice);
        }
    }
}
