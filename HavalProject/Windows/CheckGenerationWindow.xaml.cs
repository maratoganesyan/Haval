using HavalProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HavalProject.Windows
{
    /// <summary>
    /// Interaction logic for CheckGenerationWindow.xaml
    /// </summary>
    public partial class CheckGenerationWindow : Window
    {
        public class ComboBoxTemplateFields
        {
            public int Id;
            public string Car { get; set; }
            public string Client { get; set; }
            public string Manager { get; set; }
            public DateTime DateTimeOfOrder { get; set; }
            public decimal TotalPrice { get; set; }
            public ComboBoxTemplateFields(Order order)
            {
                Id = order.IdOrder;
                Car = $"Haval {order.IdCarNavigation.IdModelNavigation.ModelName} " +
                    $"{order.IdCarNavigation.IdConfigurationNavigation.ConfigurationName} " +
                    $"{order.IdCarNavigation.IdColorNavigation.ColorName}";
                Client = $"{order.IdClientNavigation.Surname} {order.IdClientNavigation.Name.ElementAt(0)}.";
                Manager = $"{order.IdManagerNavigation.Surname} {order.IdManagerNavigation.Name.ElementAt(0)}.";
                DateTimeOfOrder = order.DateTimeOfOrder;
                TotalPrice = order.TotalPrice.Value;
            }
            public override string ToString()
            {
                return $"{Car} | {Client} | {Manager} | {DateTimeOfOrder:f} | {decimal.Round(TotalPrice, 2)} руб.";
            }
        }

        public CheckGenerationWindow()
        {
            InitializeComponent();

            foreach (Order order in DbUtils.db.Order.ToList())
            {
                ComboBoxTemplateFields temp = new ComboBoxTemplateFields(order);
                OrderComboBox.Items.Add(temp);
            }
        }

        private void GenerateCheckButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrderComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Заказ, по которому будет генерироваться чек не выбран!");
                return;
            }
            ComboBoxTemplateFields SelectedOrder = OrderComboBox.SelectedItem as ComboBoxTemplateFields;
            _ = Tools.GenerationsOfReports.DoACheckAsync(DbUtils.db.Order.Single(o => o.IdOrder == SelectedOrder.Id),
                MainGrid,
                RandomizationGrid);
        }
    }
}
