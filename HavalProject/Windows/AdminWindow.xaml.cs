using HavalProject.Entities;
using HavalProject.Tools.Instruments;
using HavalProject.Windows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HavalProject
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        string TableName;
        readonly string ConnectionString = File.ReadAllText(Directories.ConnectionString());
        private bool IsDirectRequest;
        MenuItem PreviewItem;
        public AdminWindow(Users user)
        {
            InitializeComponent();

            MemoryStream stream = new MemoryStream(user.Photo);
            var image = new ImageSourceConverter().ConvertFrom(user.Photo) as ImageSource;
            UserImage.Source = image;
            using (Db db = new Db())
            {
                UserInformation.Text = user.Surname + " " + user.Name + " (" + db.Role.FirstOrDefault(p => p.IdRole == user.IdRole).RoleName + ")";
                MainDataGrid.Items.Clear();
            }

            IsDirectRequest = false;

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = sender as MenuItem;
                TableName = item.Name;
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string cmd = $"SELECT * FROM {TableName}";
                    SqlCommand command = new SqlCommand(cmd, connection);
                    command.ExecuteNonQuery();
                    SqlDataAdapter dataAdp = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    dataAdp.Fill(dt);
                    MainDataGrid.ItemsSource = dt.DefaultView;
                    connection.Close();
                }
                PreviewItem = item;

                SearchTextBox.IsEnabled = true;

                foreach (var col in MainDataGrid.Columns)
                {
                    col.Header = Translator.Translate(col.Header.ToString());
                }
                MainDataGrid.Columns[0].MaxWidth = 0;

                IsDirectRequest = true;
            }
            catch
            {
                MessageBox.Show("error");
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (PreviewItem == null)
            {
                MessageBox.Show("Сущность для манипулирования данными не выбрана!");
                return;
            }
            try
            {
                switch (PreviewItem.Header)
                {
                    case "Роли":
                        {
                            Role table = null;
                            AddAndChangeWindow window = new AddAndChangeWindow(table);
                            window.ShowDialog();
                            break;
                        }
                    case "Пол":
                        {
                            Gender table = null;
                            AddAndChangeWindow window = new AddAndChangeWindow(table);
                            window.ShowDialog();
                            break;
                        }
                    case "Статусы заказа":
                        {
                            OrderStatuses table = null;
                            AddAndChangeWindow window = new AddAndChangeWindow(table);
                            window.ShowDialog();
                            break;
                        }
                    case "Дополнительные услуги":
                        {
                            AdditationalServices table = null;
                            AddAndChangeWindow window = new AddAndChangeWindow(table);
                            window.ShowDialog();
                            break;
                        }
                    case "Статусы поставки":
                        {
                            SupplyStatuses table = null;
                            AddAndChangeWindow window = new AddAndChangeWindow(table);
                            window.ShowDialog();
                            break;
                        }
                    case "Города":
                        {
                            Cities table = null;
                            AddAndChangeWindow window = new AddAndChangeWindow(table);
                            window.ShowDialog();
                            break;

                        }
                    case "Статус автомобилей":
                        {
                            CarStatus table = null;
                            AddAndChangeWindow window = new AddAndChangeWindow(table);
                            window.ShowDialog();
                            break;
                        }
                    case "Года":
                        {
                            Year table = null;
                            AddAndChangeWindow window = new AddAndChangeWindow(table);
                            window.ShowDialog();
                            break;
                        }
                    case "Коробки передач":
                        {
                            Transmission table = null;
                            AddAndChangeWindow window = new AddAndChangeWindow(table);
                            window.ShowDialog();
                            break;
                        }
                    case "Тип двигателей":
                        {
                            EngineType table = null;
                            AddAndChangeWindow window = new AddAndChangeWindow(table);
                            window.ShowDialog();
                            break;
                        }
                    case "Тип привода":
                        {
                            Entities.DriveType table = null;
                            AddAndChangeWindow window = new AddAndChangeWindow(table);
                            window.ShowDialog();
                            break;
                        }
                    case "Сторона руля":
                        {
                            RudderSide table = null;
                            AddAndChangeWindow window = new AddAndChangeWindow(table);
                            window.ShowDialog();
                            break;
                        }
                    case "Комплектации":
                        {
                            Configuration table = null;
                            AddAndChangeWindow window = new AddAndChangeWindow(table);
                            window.ShowDialog();

                            break;
                        }
                    case "Цвета":
                        {
                            Entities.Color table = null;
                            AddAndChangeWindow window = new AddAndChangeWindow(table);
                            window.ShowDialog();
                            break;
                        }
                    case "Страны":
                        {
                            Country table = null;
                            AddAndChangeWindow window = new AddAndChangeWindow(table);
                            window.ShowDialog();
                            break;
                        }
                    case "Кузова":
                        {
                            Body table = null;
                            AddAndChangeWindow window = new AddAndChangeWindow(table);
                            window.ShowDialog();
                            break;
                        }
                    case "Двигатели":
                        {
                            Engine table = null;
                            AddAndChangeWindow window = new AddAndChangeWindow(table);
                            window.ShowDialog();
                            break;
                        }
                    case "Название моделей":
                        {
                            Models table = null;
                            AddAndChangeWindow window = new AddAndChangeWindow(table);
                            window.ShowDialog();
                            break;
                        }
                    case "Пользователи":
                        {
                            Users? table = null;
                            UsersAddAndChangeWindow window = new UsersAddAndChangeWindow(table);
                            window.ShowDialog();
                            break;
                        }
                    case "Машины":
                        {
                            Car? table = null;
                            CarsAddAndChange window = new CarsAddAndChange(table);
                            window.ShowDialog();
                            break;
                        }
                    case "Поставщики":
                        {
                            Suppliers? table = null;
                            SuppliersAddAndChange window = new SuppliersAddAndChange(table);
                            window.ShowDialog();
                            break;
                        }
                    case "Заказы":
                        {
                            Order? table = null;
                            OrderAddAndChange window = new OrderAddAndChange(table);
                            window.ShowDialog();
                            break;
                        }
                    case "Поставки":
                        {
                            Supplies? table = null;
                            SuppliesAddAndChange window = new SuppliesAddAndChange(table);
                            window.ShowDialog();
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("Error");
                            return;
                        }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при добавлении новой записи в базу данных!");
            }
        }

        private void MainDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите отредактировать данную запись?", "Предупреждение",
                 MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (PreviewItem == null)
                    {
                        MessageBox.Show("Сущность для манипулирования данными не выбрана!");
                        return;
                    }
                    try
                    {
                        switch (PreviewItem.Header)
                        {
                            case "Роли":
                                {
                                    using (Db db = new Db())
                                    {
                                        var cellInfo = MainDataGrid.SelectedCells[0];
                                        int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);

                                        var table = db.Role.Where(p => p.IdRole == content).FirstOrDefault();
                                        AddAndChangeWindow window = new AddAndChangeWindow(table);
                                        window.ShowDialog();
                                    }
                                    break;
                                }
                            case "Пол":
                                {
                                    using (Db db = new Db())
                                    {
                                        var cellInfo = MainDataGrid.SelectedCells[0];
                                        int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                        var table = db.Gender.Where(p => p.IdGender == content).FirstOrDefault();
                                        AddAndChangeWindow window = new AddAndChangeWindow(table);
                                        window.ShowDialog();
                                    }
                                    break;
                                }
                            case "Статусы заказа":
                                {
                                    using (Db db = new Db())
                                    {
                                        var cellInfo = MainDataGrid.SelectedCells[0];
                                        int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                        var table = db.OrderStatuses.Where(p => p.IdOrderStatus == content).FirstOrDefault();
                                        AddAndChangeWindow window = new AddAndChangeWindow(table);
                                        window.ShowDialog();
                                    }
                                    break;
                                }
                            case "Дополнительные услуги":
                                {
                                    using (Db db = new Db())
                                    {
                                        var cellInfo = MainDataGrid.SelectedCells[0];
                                        int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                        var table = db.AdditationalServices.Where(p => p.IdService == content).FirstOrDefault();
                                        AddAndChangeWindow window = new AddAndChangeWindow(table);
                                        window.ShowDialog();
                                    }
                                    break;
                                }
                            case "Статусы поставки":
                                {
                                    using (Db db = new Db())
                                    {
                                        var cellInfo = MainDataGrid.SelectedCells[0];
                                        int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                        var table = db.SupplyStatuses.Where(p => p.IdStatus == content).FirstOrDefault();
                                        AddAndChangeWindow window = new AddAndChangeWindow(table);
                                        window.ShowDialog();
                                    }
                                    break;
                                }
                            case "Города":
                                {
                                    using (Db db = new Db())
                                    {
                                        var cellInfo = MainDataGrid.SelectedCells[0];
                                        int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                        var table = db.Cities.Where(p => p.IdCity == content).FirstOrDefault();
                                        AddAndChangeWindow window = new AddAndChangeWindow(table);
                                        window.ShowDialog();
                                    }
                                    break;

                                }
                            case "Статус автомобилей":
                                {
                                    using (Db db = new Db())
                                    {
                                        var cellInfo = MainDataGrid.SelectedCells[0];
                                        int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                        var table = db.CarStatus.Where(p => p.IdCarStatus == content).FirstOrDefault();
                                        AddAndChangeWindow window = new AddAndChangeWindow(table);
                                        window.ShowDialog();
                                    }
                                    break;
                                }
                            case "Года":
                                {
                                    using (Db db = new Db())
                                    {
                                        var cellInfo = MainDataGrid.SelectedCells[0];
                                        int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                        var table = db.Year.Where(p => p.IdYear == content).FirstOrDefault();
                                        AddAndChangeWindow window = new AddAndChangeWindow(table);
                                        window.ShowDialog();
                                    }
                                    break;
                                }
                            case "Коробки передач":
                                {
                                    using (Db db = new Db())
                                    {
                                        var cellInfo = MainDataGrid.SelectedCells[0];
                                        int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                        var table = db.Transmission.Where(p => p.IdTransmission == content).FirstOrDefault();
                                        AddAndChangeWindow window = new AddAndChangeWindow(table);
                                        window.ShowDialog();
                                    }
                                    break;
                                }
                            case "Тип двигателей":
                                {
                                    using (Db db = new Db())
                                    {
                                        var cellInfo = MainDataGrid.SelectedCells[0];
                                        int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                        var table = db.EngineType.Where(p => p.IdEngineType == content).FirstOrDefault();
                                        AddAndChangeWindow window = new AddAndChangeWindow(table);
                                        window.ShowDialog();
                                    }
                                    break;
                                }
                            case "Тип привода":
                                {
                                    using (Db db = new Db())
                                    {
                                        var cellInfo = MainDataGrid.SelectedCells[0];
                                        int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                        var table = db.DriveType.Where(p => p.IdDriveType == content).FirstOrDefault();
                                        AddAndChangeWindow window = new AddAndChangeWindow(table);
                                        window.ShowDialog();
                                    }
                                    break;
                                }
                            case "Сторона руля":
                                {
                                    using (Db db = new Db())
                                    {
                                        var cellInfo = MainDataGrid.SelectedCells[0];
                                        int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                        var table = db.RudderSide.Where(p => p.IdRudderSide == content).FirstOrDefault();
                                        AddAndChangeWindow window = new AddAndChangeWindow(table);
                                        window.ShowDialog();
                                    }
                                    break;
                                }
                            case "Комплектации":
                                {
                                    using (Db db = new Db())
                                    {
                                        var cellInfo = MainDataGrid.SelectedCells[0];
                                        int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                        var table = db.Configuration.Where(p => p.IdConfiguration == content).FirstOrDefault();
                                        AddAndChangeWindow window = new AddAndChangeWindow(table);
                                        window.ShowDialog();
                                    }
                                    break;
                                }
                            case "Цвета":
                                {
                                    using (Db db = new Db())
                                    {
                                        var cellInfo = MainDataGrid.SelectedCells[0];
                                        int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                        var table = db.Color.Where(p => p.IdColor == content).FirstOrDefault();
                                        AddAndChangeWindow window = new AddAndChangeWindow(table);
                                        window.ShowDialog();
                                    }
                                    break;
                                }
                            case "Страны":
                                {
                                    using (Db db = new Db())
                                    {
                                        var cellInfo = MainDataGrid.SelectedCells[0];
                                        int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                        var table = db.Country.Where(p => p.IdCountry == content).FirstOrDefault();
                                        AddAndChangeWindow window = new AddAndChangeWindow(table);
                                        window.ShowDialog();
                                    }
                                    break;
                                }
                            case "Кузова":
                                {
                                    using (Db db = new Db())
                                    {
                                        var cellInfo = MainDataGrid.SelectedCells[0];
                                        int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                        var table = db.Body.Where(p => p.IdBody == content).FirstOrDefault();
                                        AddAndChangeWindow window = new AddAndChangeWindow(table);
                                        window.ShowDialog();
                                    }
                                    break;
                                }
                            case "Двигатели":
                                {
                                    using (Db db = new Db())
                                    {
                                        var cellInfo = MainDataGrid.SelectedCells[0];
                                        int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                        var table = db.Engine.Where(p => p.IdEngineName == content).FirstOrDefault();
                                        AddAndChangeWindow window = new AddAndChangeWindow(table);
                                        window.ShowDialog();
                                    }
                                    break;
                                }
                            case "Название моделей":
                                {
                                    using (Db db = new Db())
                                    {
                                        var cellInfo = MainDataGrid.SelectedCells[0];
                                        int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                        var table = db.Models.Where(p => p.IdModel == content).FirstOrDefault();
                                        AddAndChangeWindow window = new AddAndChangeWindow(table);
                                        window.ShowDialog();
                                    }
                                    break;
                                }
                            case "Пользователи":
                                {
                                    var cellInfo = MainDataGrid.SelectedCells[0];
                                    int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                    var table = DbUtils.db.Users.Where(p => p.IdUser == content).FirstOrDefault();
                                    UsersAddAndChangeWindow window = new UsersAddAndChangeWindow(table);
                                    window.ShowDialog();
                                    break;
                                }
                            case "Машины":
                                {
                                    var cellInfo = MainDataGrid.SelectedCells[0];
                                    int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                    var table = DbUtils.db.Car.Where(p => p.IdCar == content).FirstOrDefault();
                                    CarsAddAndChange window = new CarsAddAndChange(table);
                                    window.ShowDialog();
                                    break;
                                }
                            case "Поставщики":
                                {
                                    var cellInfo = MainDataGrid.SelectedCells[0];
                                    int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                    var table = DbUtils.db.Suppliers.Where(p => p.IdSupplier == content).FirstOrDefault();
                                    SuppliersAddAndChange window = new SuppliersAddAndChange(table);
                                    window.ShowDialog();
                                    break;
                                }
                            case "Заказы":
                                {
                                    var cellInfo = MainDataGrid.SelectedCells[0];
                                    int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                    var table = DbUtils.db.Order.Where(p => p.IdOrder == content).FirstOrDefault();
                                    OrderAddAndChange window = new OrderAddAndChange(table);
                                    window.ShowDialog();
                                    break;
                                }
                            case "Поставки":
                                {
                                    var cellInfo = MainDataGrid.SelectedCells[0];
                                    int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                    var table = DbUtils.db.Supplies.Single(s => s.IdSupply == content);
                                    SuppliesAddAndChange window = new SuppliesAddAndChange(table);
                                    window.ShowDialog();
                                    break;
                                }
                            default:
                                {
                                    MessageBox.Show("Error");
                                    return;
                                }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка выбора записи для изменения!");
                    }
                }
                catch (System.ArgumentOutOfRangeException) { }
                catch (Exception) { }
            }
        }

        private void UpdateMainGridButton_Click(object sender, RoutedEventArgs e)
        {
            if (PreviewItem == null)
            {
                MessageBox.Show("Сущность для манипулирования данными не выбрана!");
                return;
            }
            try
            {
                switch (PreviewItem.Header)
                {
                    case "Пользователи":
                        {
                            Users_Click(Users, null);
                            break;
                        }
                    case "Машины":
                        {
                            Car_Click(Car, null);
                            break;
                        }
                    case "Поставщики":
                        {
                            Suppliers_Click(Suppliers, null);
                            break;
                        }
                    case "Заказы":
                        {
                            Order_Click(PreviewItem, null);
                            break;
                        }
                    case "Поставки":
                        {
                            Supplies_Click(Supplies, null);
                            break;
                        }
                    case "Дополнительные услуги":
                        {
                            AdditationalServices_Click(AdditationalServices, null);
                            break;
                        }
                    default:
                        {
                            MenuItem_Click(PreviewItem, null);
                            break;
                        }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при обновлении данных, получаемых из базы данных!");
            }
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            MainDataGrid.ItemsSource = DbUtils.db.Users.Select(p => new
            {
                p.IdUser,
                p.Surname,
                p.Name,
                DateOfBirth = $"{p.DateOfBirth:D}",
                p.PhoneNumber,
                p.Email,
                p.Login,
                p.Password,
                p.IdGenderNavigation.GenderName,
                p.IdRoleNavigation.RoleName
            }).ToList();

            PreviewItem = sender as MenuItem;

            SearchTextBox.IsEnabled = true;

            foreach (var col in MainDataGrid.Columns)
            {
                col.Header = Translator.Translate(col.Header.ToString());
            }

            MainDataGrid.Columns[0].MaxWidth = 0;

            IsDirectRequest = false;
        }

        private void Car_Click(object sender, RoutedEventArgs e)
        {
            MainDataGrid.ItemsSource = DbUtils.db.Car.Select(p => new
            {
                p.IdCar,
                p.IdModelNavigation.ModelName,
                p.IdEngineNavigation.EngineName,
                p.IdBodyNavigation.BodyName,
                p.IdCountryNavigation.CountryName,
                p.IdColorNavigation.ColorName,
                p.IdConfigurationNavigation.ConfigurationName,
                p.IdCarStatusNavigation.CarStatusName,
                p.IdYear,
                p.IdRudderSideNavigation.RuddeSideName,
                p.IdDriveTypeNavigation.DriveTypeName,
                p.IdTransmissionNavigation.TransmissionName,
                p.IdEngineTypeNavigation.EngineTypeName,
                ClientPrice = decimal.Round(p.ClientPrice, 2).ToString(),
                p.Description,
                p.Power,
                p.EngineCapacity,
                p.TankCapacity,
                p.Quantity
            }).ToList();

            PreviewItem = sender as MenuItem;

            SearchTextBox.IsEnabled = true;

            foreach (var col in MainDataGrid.Columns)
            {
                col.Header = Translator.Translate(col.Header.ToString());
            }

            MainDataGrid.Columns[0].MaxWidth = 0;

            IsDirectRequest = false;
        }

        private void Suppliers_Click(object sender, RoutedEventArgs e)
        {
            MainDataGrid.ItemsSource = DbUtils.db.Suppliers.Select(p => new
            {
                p.IdSupplier,
                p.IdCityNavigation.CityName,
                p.Address,
                p.Email,
                p.PhoneNumber
            }).ToList();

            PreviewItem = sender as MenuItem;

            SearchTextBox.IsEnabled = true;

            foreach (var col in MainDataGrid.Columns)
            {
                col.Header = Translator.Translate(col.Header.ToString());
            }
            MainDataGrid.Columns[0].MaxWidth = 0;

            IsDirectRequest = false;

        }

        private void Supplies_Click(object sender, RoutedEventArgs e)
        {
            MainDataGrid.ItemsSource = DbUtils.db.Supplies.Select(p => new
            {
                p.IdSupply,
                Supplier = $"{p.IdSupplierNavigation.Address} {p.IdSupplierNavigation.PhoneNumber}",
                DateTimeOfSupply = $"{p.DateTimeOfSupply:f}",
                TotalPrice = decimal.Round(p.TotalPriceOfSupply.Value, 2).ToString(),
                p.IdSupplyStatusNavigation.StatusName,
                Goods = string.Join(Environment.NewLine, p.GoodsInSupply.Select(
                    x => x.IdCarNavigation.IdModelNavigation.ModelName + " " +
                    x.IdCarNavigation.IdConfigurationNavigation.ConfigurationName + " " +
                    x.IdCarNavigation.IdColorNavigation.ColorName + " " +
                    x.Quantity + " " + decimal.Round(x.SupplyPrice, 2).ToString()).ToList())
            }).ToList();

            PreviewItem = sender as MenuItem;

            SearchTextBox.IsEnabled = true;

            foreach (var col in MainDataGrid.Columns)
            {
                col.Header = Translator.Translate(col.Header.ToString());
            }

            MainDataGrid.Columns[0].MaxWidth = 0;

            IsDirectRequest = false;
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            MainDataGrid.ItemsSource = DbUtils.db.Order.Select(p => new
            {
                p.IdOrder,
                Client = $"{p.IdClientNavigation.Surname} {p.IdClientNavigation.Name} {p.IdClientNavigation.PhoneNumber}",
                Manager = $"{p.IdManagerNavigation.Surname} {p.IdManagerNavigation.Name} {p.IdManagerNavigation.PhoneNumber}",
                Car = $"{p.IdCarNavigation.IdModelNavigation.ModelName} " +
                $"{p.IdCarNavigation.IdConfigurationNavigation.ConfigurationName} {p.IdCarNavigation.IdColorNavigation.ColorName}",
                DateTimeOfOrder = $"{p.DateTimeOfOrder:f}",
                TotalPrice = decimal.Round(p.TotalPrice.Value, 2).ToString(),
                p.IdOrderStatusNavigation.OrderStatusName,
                ServicesInOrder = string.Join(Environment.NewLine,
                    p.ServicesInOrders.Select(x => x.IdServiceNavigation.ServiceName + " " +
                    decimal.Round(x.IdServiceNavigation.ServicePrice, 2).ToString()).ToList())
            }).ToList();
            PreviewItem = sender as MenuItem;

            SearchTextBox.IsEnabled = true;

            foreach (var col in MainDataGrid.Columns)
            {
                col.Header = Translator.Translate(col.Header.ToString());
            }

            MainDataGrid.Columns[0].MaxWidth = 0;

            IsDirectRequest = false;
        }
        private void AdditationalServices_Click(object sender, RoutedEventArgs e)
        {
            MainDataGrid.ItemsSource = DbUtils.db.AdditationalServices.Select(a => new
            {
                a.IdService,
                a.ServiceName,
                a.ServiceDescription,
                ServicePrice = decimal.Round(a.ServicePrice, 2).ToString()
            }).ToList();
            PreviewItem = sender as MenuItem;

            SearchTextBox.IsEnabled = true;

            foreach (var col in MainDataGrid.Columns)
            {
                col.Header = Translator.Translate(col.Header.ToString());
            }

            MainDataGrid.Columns[0].MaxWidth = 0;

            IsDirectRequest = false;
        }
        private void SearchTextBox_QuerySubmitted(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            try
            {
                UpdateMainGridButton_Click(null, null);
                var TextForSearch = SearchTextBox.Text;

                List<object> DataFromTable = MainDataGrid.Items.OfType<object>().ToList();
                var MainList = new List<object>();
                foreach (var item in DataFromTable)
                {
                    string ItemInString;
                    Regex regex = new Regex(@"\{([^\}]*)\}");
                    List<string> Values = new();

                    if (IsDirectRequest)
                    {
                        ItemInString = "{ " + string.Join(", ", (item as DataRowView).Row.ItemArray) + " }";
                        string Inside = regex.Match(ItemInString).Groups[1].Value;
                        List<string> Splitted = Inside.Split(", ").ToList();
                        Splitted.RemoveAt(0);
                        Values = Splitted.Select(s => s.Trim()).ToList();
                    }
                    else
                    {
                        ItemInString = item.ToString();
                        Regex regex2 = new Regex(@"([\s\S]*), [a-zA-Z]*");
                        string Inside = regex.Match(ItemInString).Groups[1].Value;
                        List<string> Splitted = Inside.Split('=').ToList();
                        Splitted.RemoveAt(0);
                        foreach (var str in Splitted)
                        {
                            if (str == Splitted[Splitted.Count - 1])
                                Values.Add(str.Trim());
                            Values.Add(regex2.Match(str).Groups[1].Value.Trim());
                        }
                        Values.RemoveAt(0);
                        Values.RemoveAt(Values.Count - 1);
                    }
                    if (Values.Any(p => p.Contains(TextForSearch)))
                    {
                        MainList.Add(item);
                    }
                }
                MainDataGrid.ItemsSource = null;
                MainDataGrid.ItemsSource = MainList;
                foreach (var col in MainDataGrid.Columns)
                {
                    col.Header = Translator.Translate(col.Header.ToString());
                }
                MainDataGrid.Columns[0].MaxWidth = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Поиск не дал результатов");
            }
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}