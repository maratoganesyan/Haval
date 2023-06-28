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
using HavalProject.Entities;
using Xceed.Wpf.Toolkit;

namespace HavalProject.Windows
{
    /// <summary>
    /// Interaction logic for OrderAddAndChange.xaml
    /// </summary>
    public partial class OrderAddAndChange : Window
    {
        bool ChangeAnExistingOrder;
        class TemplateComboBoxItems
        {
            public int id { get; set; }

            public string value { get; set; }

            public TemplateComboBoxItems(int id, string value)
            {
                this.id = id;

                this.value = value;
            }
        }

        List<AdditationalServices> services = new List<AdditationalServices>();

        private List<TemplateComboBoxItems> items;
        private List<TemplateComboBoxItems> items2;
        private List<TemplateComboBoxItems> items3;

        int IdOrder;
        int IdCarBackup;
        int IdCar;
        int StartOrderStatusId;

        public OrderAddAndChange(Order? order)
        {
            InitializeComponent();

            #region Cars
            var listForCBCar = DbUtils.db.Car.Where(c => c.Quantity > 0 || c.IdCar == (order != null ? order.IdCarNavigation.IdCar : 0)).Select(p => new
            {
                id = p.IdCar,
                value = $"{p.IdModelNavigation.ModelName} " +
                $"{p.IdConfigurationNavigation.ConfigurationName} {p.IdColorNavigation.ColorName}",
            }).ToList();

            foreach (var item in listForCBCar)
            {
                TemplateComboBoxItems boxItems = new TemplateComboBoxItems(item.id, item.value);
                CarComboBox.Items.Add(boxItems);
                if (order != null && boxItems.id == order.IdCar)
                    CarComboBox.SelectedItem = boxItems;
            }
            #endregion

            #region Clients
            var listForCBClient = DbUtils.db.Users.Where(p => p.IdRole == 1).Select(p => new
            {
                id = p.IdUser,
                value = $"{p.Surname} {p.Name} {p.PhoneNumber}"
            }).ToList();

            foreach (var item in listForCBClient)
            {
                TemplateComboBoxItems boxItems = new TemplateComboBoxItems(item.id, item.value);
                ClientComboBox.Items.Add(boxItems);
                if (order != null && boxItems.id == order.IdClient)
                    ClientComboBox.SelectedItem = boxItems;
            }
            #endregion

            #region Managers
            var listForCBManager = DbUtils.db.Users.Where(p => p.IdRole == 2).Select(p => new
            {
                id = p.IdUser,
                value = $"{p.Surname} {p.Name} {p.PhoneNumber}",
            }).ToList();

            foreach (var item in listForCBManager)
            {
                TemplateComboBoxItems boxItems = new TemplateComboBoxItems(item.id, item.value);
                ManagerComboBox.Items.Add(boxItems);
                if (order != null && boxItems.id == order.IdManager)
                    ManagerComboBox.SelectedItem = boxItems;
            }
            #endregion

            #region OrderStatuses
            var listForCBOrderStatuses = DbUtils.db.OrderStatuses.Select(os => new
            {
                id = os.IdOrderStatus,
                value = $"{os.OrderStatusName}"
            }).ToList();

            foreach (var item in listForCBOrderStatuses)
            {
                TemplateComboBoxItems boxItems = new(item.id, item.value);
                OrderStatusComboBox.Items.Add(boxItems);
                if (order != null && boxItems.id == order.IdOrderStatus)
                {
                    OrderStatusComboBox.SelectedItem = boxItems;
                    StartOrderStatusId = boxItems.id;
                }

            }
            #endregion

            AddOrChangeServiceComboBox.ItemsSource = DbUtils.db.AdditationalServices.ToList();

            ReplacementServiceCB.ItemsSource = DbUtils.db.AdditationalServices.ToList();

            if (order == null)
            {
                ChangeAnExistingOrder = false;
                IdOrder = DbUtils.db.Order.Max(p => p.IdOrder) + 1;
                EditOrderWindow.Title = "Добавление нового заказа";
            }
            else
            {
                EditOrderWindow.Title = "Изменение существующего заказа";
                IdOrder = order.IdOrder;
                IdCarBackup = order.IdCar;
                IdCar = order.IdCar;
                ChangeAnExistingOrder = true;

                foreach (ServicesInOrders service in order.ServicesInOrders)
                {
                    services.Add(DbUtils.db.AdditationalServices.Single(s => s.IdService == service.IdService));
                }
                UpdateReplaceableServiceCB();

                foreach (AdditationalServices service in services)
                    ServicesDataGrid.Items.Add(service);

                DateOfOrderBox.Text = $"{order.DateTimeOfOrder:d}";
                TimeOfOrderBox.Text = $"{order.DateTimeOfOrder:t}";
            }

        }
        public void UpdateReplaceableServiceCB()
        {
            ReplaceableServiceCB.Items.Clear();
            foreach (AdditationalServices service in services)
            {
                ReplaceableServiceCB.Items.Add(service);
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientComboBox.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show("Клиент, оформляющий заказ, не выбран!");
                return;
            }
            if (ManagerComboBox.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show("Менеджер, работающий с заказом не выбран!");
                return;
            }
            if (CarComboBox.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show("Автомобиль, учавствующий в заказе, не выбран!");
                return;
            }
            int ChoosenCarId = (CarComboBox.SelectedItem as TemplateComboBoxItems).id;
            IdCar = ChoosenCarId;
            if (OrderStatusComboBox.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show("Статус текущего заказа не выбран!");
                return;
            }
            if (!DateOfOrderBox.SelectedDate.HasValue)
            {
                System.Windows.MessageBox.Show("Дата оформления заказа не выбрана!");
                return;
            }
            if (!TimeOfOrderBox.SelectedTime.HasValue)
            {
                System.Windows.MessageBox.Show("Время оформления заказа не выбрано!");
                return;
            }
            if (!ChangeAnExistingOrder && DbUtils.db.Car.Single(c => c.IdCar == IdCar).Quantity < 1)
            {
                System.Windows.MessageBox.Show("Покупка модели данного автомобиля невозможна, так как они отсутвуют в автосалоне!");
                return;
            }
            if (ChangeAnExistingOrder && (IdCar != IdCarBackup) && DbUtils.db.Car.Single(c => c.IdCar == IdCar).Quantity < 1)
            {
                System.Windows.MessageBox.Show("Покупка модели данного автомобиля невозможна, так как они отсутвуют в автосалоне!");
                return;
            }
            try
            {
                Order NewOrder;
                if (ChangeAnExistingOrder)
                    NewOrder = DbUtils.db.Order.Single(o => o.IdOrder == this.IdOrder);
                else
                    NewOrder = new Order();
                NewOrder.IdCar = ChoosenCarId;
                NewOrder.IdClient = (ClientComboBox.SelectedItem as TemplateComboBoxItems).id;
                NewOrder.IdManager = (ManagerComboBox.SelectedItem as TemplateComboBoxItems).id;
                DateTime DayOfOrder = DateOfOrderBox.SelectedDate.Value.Date;
                TimeSpan TimeOfOrder = TimeOfOrderBox.SelectedTime.Value.TimeOfDay;
                NewOrder.DateTimeOfOrder = new DateTime(DayOfOrder.Year, DayOfOrder.Month, DayOfOrder.Day,
                    TimeOfOrder.Hours, TimeOfOrder.Minutes, TimeOfOrder.Seconds);
                NewOrder.TotalPrice = DbUtils.db.Car.Single(c => c.IdCar == ChoosenCarId).ClientPrice + services.Select(s => s.ServicePrice).Sum();
                NewOrder.IdOrderStatus = (OrderStatusComboBox.SelectedItem as TemplateComboBoxItems).id;
                if (!ChangeAnExistingOrder)
                    DbUtils.db.Order.Add(NewOrder);
                DbUtils.db.SaveChanges();
                IdOrder = DbUtils.db.Order.OrderBy(o => o.IdOrder).Last().IdOrder;
                DeleteSIO(this.IdOrder);
                foreach (AdditationalServices service in services)
                {
                    DbUtils.db.ServicesInOrders.Add(new ServicesInOrders()
                    {
                        IdOrder = this.IdOrder,
                        IdService = service.IdService
                    });
                }
                if (!ChangeAnExistingOrder)
                    if((((OrderStatusComboBox.SelectedItem) as TemplateComboBoxItems).id != 2 || ((OrderStatusComboBox.SelectedItem) as TemplateComboBoxItems).id != 3))
                    {
                        DbUtils.db.Car.Single(c => c.IdCar == IdCar).Quantity -= 1;
                    }
                else
                {
                    if ((StartOrderStatusId == 2 || StartOrderStatusId == 3) && 
                        (((OrderStatusComboBox.SelectedItem) as TemplateComboBoxItems).id == 1 ||
                        ((OrderStatusComboBox.SelectedItem) as TemplateComboBoxItems).id == 4))
                    {
                        if (IdCar != IdCarBackup)
                        {
                            DbUtils.db.Car.Single(c => c.IdCar == IdCarBackup).Quantity += 1;
                            DbUtils.db.Car.Single(c => c.IdCar == IdCar).Quantity -= 1;
                        }
                        else 
                        {
                            DbUtils.db.Car.Single(c => c.IdCar == IdCar).Quantity -= 1;
                        }
                    }
                    if((StartOrderStatusId == 1 || StartOrderStatusId == 4) &&
                        (((OrderStatusComboBox.SelectedItem) as TemplateComboBoxItems).id == 2 ||
                        ((OrderStatusComboBox.SelectedItem) as TemplateComboBoxItems).id == 3))
                    {
                        if (IdCar != IdCarBackup)
                        {
                            DbUtils.db.Car.Single(c => c.IdCar == IdCarBackup).Quantity += 1;
                            DbUtils.db.Car.Single(c => c.IdCar == IdCar).Quantity += 1;
                        }
                        else
                        {
                            DbUtils.db.Car.Single(c => c.IdCar == IdCar).Quantity += 1;
                        }
                    }
                }
                DbUtils.db.SaveChanges();
                System.Windows.MessageBox.Show("Заказ успешно сохранен!");
                Close();
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Произошла ошибка сохранения заказа!");
            }
        }
        public static void DeleteSIO(int idOrder)
        {
            foreach (var Service in DbUtils.db.ServicesInOrders.Where(o => o.IdOrder == idOrder).ToList())
                DbUtils.db.ServicesInOrders.Remove(Service);
            DbUtils.db.SaveChanges();
        }
        private void AddServiceButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddOrChangeServiceComboBox.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show("Услуга для добавления в заказ не выбрана!");
                return;
            }
            AdditationalServices AddedS = DbUtils.db.AdditationalServices.Single(s =>
                    s.IdService == (AddOrChangeServiceComboBox.SelectedItem as AdditationalServices).IdService);
            if (services.All(s => s.IdService != AddedS.IdService))
            {
                services.Add(AddedS);
                ServicesDataGrid.Items.Add(AddedS);
                UpdateReplaceableServiceCB();
            }
        }

        private void ReplaceServiceButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReplaceableServiceCB.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show("Услуга, которую вы хотите заменить не выбрана!");
                return;
            }
            if (ReplacementServiceCB.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show("Услуга, на которую вы хотите заменить не выбрана!");
                return;
            }
            AdditationalServices ChoosenS = DbUtils.db.AdditationalServices.Single(s =>
                    s.IdService == (ReplaceableServiceCB.SelectedItem as AdditationalServices).IdService);
            AdditationalServices AddedS = DbUtils.db.AdditationalServices.Single(s =>
                    s.IdService == (ReplacementServiceCB.SelectedItem as AdditationalServices).IdService);
            if (ChoosenS.IdService == AddedS.IdService) //замена на саму себя
                return;
            if (services.Any(s => s.IdService == AddedS.IdService)) //на ту, что уже есть
            {
                services.Remove(ChoosenS);
                ServicesDataGrid.Items.Remove(ChoosenS);
                UpdateReplaceableServiceCB();
                return;
            }
            if (services.All(s => s.IdService != AddedS.IdService)) //на новую
            {
                services.Remove(ChoosenS);
                services.Add(AddedS);
                ServicesDataGrid.Items.Remove(ChoosenS);
                ServicesDataGrid.Items.Add(AddedS);
                UpdateReplaceableServiceCB();
                return;
            }
        }
    }
}
