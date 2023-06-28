using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavalProject.Entities
{
    internal static class DbUtils
    {
        public static Db db;

        static DbUtils()
        {
            db = new Db();
        }

        public static List<Entities.Models> GetAllModels() =>
            db
            .Models
            .ToList();
        public static int[] GetYearsOfOrders() =>
            db
            .Order
            .Select(x => x.DateTimeOfOrder.Year)
            .Distinct()
            .ToArray();
        public static List<int> GetIdModelsByName(List<string> ModelNames) =>
            db
            .Models
            .Where(x => ModelNames.Any(x1 => x1 == x.ModelName))
            .Select(x => x.IdModel)
            .OrderBy(x => x)
            .ToList();
        public static List<Order> GetOrdersByRestrictions(List<int> IdModels,
            DateTime StartDate, DateTime EndDate) =>
            db
            .Order
            .Where(o =>
                o.DateTimeOfOrder >= StartDate
                &&
                o.DateTimeOfOrder <= EndDate
                &&
                IdModels.Any(m => m == o.IdCarNavigation.IdModel))
            .ToList();
        public static List<Supplies> GetSuppliesByRestrictions(
            DateTime StartDate, DateTime EndDate) =>
            db
            .Supplies
            .Where(s =>
                s.DateTimeOfSupply >= StartDate
                &&
                s.DateTimeOfSupply <= EndDate)
            .ToList();
        public static int[] GetYearsOfSypplies() =>
            db
            .Supplies
            .Select(x => x.DateTimeOfSupply.Value.Year)
            .Distinct()
            .ToArray();
        public static List<GoodsInSupply> GetDataToSupplyReport(List<Supplies> suppliesList,
            List<int> modelsIds)
        {
            int[] suppliesIds = suppliesList.Select(x => x.IdSupply).ToArray();
            return db
                .GoodsInSupply
                .Where(g =>
                suppliesIds.Any(s => s == g.IdSupply)
                &&
                modelsIds.Any(m => m == g.IdCarNavigation.IdModel))
                .ToList();
        }
        public static (double[] Sales, string[] ModelsNames) GetSalesStatistics()
        {
            Dictionary<string, double> statistics = new();
            foreach (Models model in GetAllModels())
            {
                int CountOfSales = db.Order.Where(o => o.IdCarNavigation.IdModel == model.IdModel).Count();
                if (CountOfSales != 0)
                    statistics.Add(model.ModelName, CountOfSales);
            }
            return (statistics.Values.ToArray(), statistics.Keys.ToArray());
        }
        public static (double[] Sales, string[] ModelsNames) GetSupplyStatistics()
        {
            Dictionary<string, double> statistics = new();
            foreach (Models model in GetAllModels())
            {
                int CountOfSupplies = db.GoodsInSupply.Where(g => g.IdCarNavigation.IdModel == model.IdModel).Count();
                if (CountOfSupplies != 0)
                    statistics.Add(model.ModelName, CountOfSupplies);
            }
            return (statistics.Values.ToArray(), statistics.Keys.ToArray());
        }
        public static DateTime GetEarliestDateTimeOrder() =>
            db.Order.Select(x => x.DateTimeOfOrder).ToList().Min();
        public static DateTime GetLatestDateTimeOrder() =>
            db.Order.Select(x => x.DateTimeOfOrder).ToList().Max();
        public static (double[] Values, double[] DateTimes) GetOrdersByDates()
        {
            Dictionary<double, double> statistics = new();
            DateTime First = GetEarliestDateTimeOrder();
            int CountOfDays = (int)(GetLatestDateTimeOrder() - First).TotalDays + 1;
            List<Order> Orders = db.Order.ToList();
            for (int i = 0; i < CountOfDays; i++)
            {
                DateTime temp = First.AddDays(i);
                statistics.Add(temp.ToOADate(),
                    Orders
                    .Where(o => DateOnly.FromDateTime(o.DateTimeOfOrder) == DateOnly.FromDateTime(temp))
                    .Count());
            }
            return (statistics.Values.ToArray(), statistics.Keys.ToArray());
        }
        public static (double[] Values, double[] Dates) GetGrowthAndExpenses()
        {
            List<Tools.ModelsOfETypes.WaterFallBarModel> GnE = new();
            foreach (var o in db.Order.ToList())
                GnE.Add(new Tools.ModelsOfETypes.WaterFallBarModel(o));
            foreach (var s in db.Supplies.ToList())
                GnE.Add(new Tools.ModelsOfETypes.WaterFallBarModel(s));
            GnE = GnE.OrderBy(x => x.date).ToList();
            return (GnE.Select(x => x.IsOrder == true ?
                    Convert.ToDouble(x.order.TotalPrice) :
                    Convert.ToDouble(-x.supply.TotalPriceOfSupply)).ToArray(),
                    GnE.Select(x => x.date.ToOADate()).ToArray());
        }
    }
}
