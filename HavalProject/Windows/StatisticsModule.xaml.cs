using HavalProject.Entities;
using ScottPlot;
using System;
using System.Linq;
using System.Windows;

namespace HavalProject.Windows
{
    /// <summary>
    /// Interaction logic for StatisticsModule.xaml
    /// </summary>
    public partial class StatisticsModule : Window
    {
        public StatisticsModule()
        {
            InitializeComponent();

            SetSalesStatisticsPlot();
            SetSupplyStatisticsPlot();
            SetPlotOrderByDate();
            SetPlotArriveByMonth();
        }
        public void SetSalesStatisticsPlot() =>
            SetPie(DbUtils.GetSalesStatistics, plot1, "Статистика продаж моделей автомобилей Haval.");
        public void SetSupplyStatisticsPlot() =>
            SetPie(DbUtils.GetSupplyStatistics, plot2, "Статистика поставок моделей автомобилей Haval.");
        public void SetPie(Func<(double[] Sales, string[] ModelsNames)> func, WpfPlot plot, string Title)
        {
            var Stats = func.Invoke();

            for (int i = 0; i < Stats.Sales.Length; i++)
                Stats.ModelsNames[i] += $" ({Stats.Sales[i] / Stats.Sales.Sum() * 100:.0}%)";

            var pie = plot.Plot.AddPie(Stats.Sales);

            pie.SliceLabels = Stats.Sales.Select(x => x.ToString()).ToArray();
            pie.ShowLabels = true;
            pie.LegendLabels = Stats.ModelsNames;

            pie.Explode = true;

            plot.Plot.Legend();
            plot.Plot.Title(Title);

            pie.Size = .75;
            plot.Refresh();
        }
        public void SetPlotOrderByDate()
        {
            var data = DbUtils.GetOrdersByDates();
            var bar = plot3.Plot.AddBar(values: data.Values, positions: data.DateTimes);
            plot3.Plot.XAxis.DateTimeFormat(true);
            plot3.Refresh();
            bar.BarWidth = 1.0 / 2 * .8;
            plot3.Plot.SetAxisLimits(yMin: 0);
            plot3.Plot.Layout(right: 20);
            plot3.Plot.Title("Количество проданных автомобилей, относительно первой и последней даты продажи.");
            plot3.Plot.XAxis.Label("Дни (от первой, до последней даты продажи)");
            plot3.Plot.YAxis.Label("Количество проданных автомобилей");
            plot3.Refresh();
        }
        public void SetPlotArriveByMonth()
        {
            var data = DbUtils.GetGrowthAndExpenses();
            var offsets = Enumerable.Range(0, data.Values.Length).Select(x => data.Values.Take(x).Sum()).ToArray();
            var bar = plot4.Plot.AddBar(values: data.Values, positions: data.Dates);
            bar.ValueOffsets = offsets;
            bar.FillColorNegative = System.Drawing.Color.Red;
            bar.FillColor = System.Drawing.Color.Green;

            plot4.Plot.XAxis.DateTimeFormat(true);

            plot4.Plot.XAxis2.Line(false);
            plot4.Plot.YAxis2.Line(false);

            plot4.Plot.XAxis.Label("Дни (от первой, до последней даты продажи/закупки)");
            plot4.Plot.YAxis.Label("Изменение бюджета автосалона (руб.)");
            plot4.Plot.XAxis2.Label("Красные столбцы - закупки, зелёные - продажи");

            plot4.Plot.Title("Водопадный график прибыли и закупок автосалона");
            plot4.Plot.Legend(location: Alignment.UpperRight);

            plot4.Refresh();
        }
    }
}
