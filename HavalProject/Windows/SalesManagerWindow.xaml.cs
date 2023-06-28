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

namespace HavalProject.Windows
{
    /// <summary>
    /// Interaction logic for SalesManagerWindow.xaml
    /// </summary>
    public partial class SalesManagerWindow : Window
    {
        private bool IsDirectRequest;
        MenuItem PreviewItem;
        public SalesManagerWindow(Users user)
        {
            InitializeComponent();

            HideButtonChecks();

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
                    case "Заказы":
                        {
                            Order? table = null;
                            OrderAddAndChange window = new OrderAddAndChange(table);
                            window.ShowDialog();
                            break;
                        }
                    case "Пользователи":
                        {
                            Users? table = null;
                            UsersAddAndChangeWindow window = new UsersAddAndChangeWindow(table, true);
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
                            case "Машины":
                                {
                                    var cellInfo = MainDataGrid.SelectedCells[0];
                                    int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                    var table = DbUtils.db.Car.Where(p => p.IdCar == content).FirstOrDefault();
                                    CarStatusChangeWindow window = new CarStatusChangeWindow(table);
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
                            case "Пользователи":
                                {
                                    var cellInfo = MainDataGrid.SelectedCells[0];
                                    int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                    var table = DbUtils.db.Users.Where(p => p.IdUser == content).FirstOrDefault();
                                    UsersAddAndChangeWindow window = new UsersAddAndChangeWindow(table, true);
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
                    case "Машины":
                        {
                            Car_Click(Car, null);
                            break;
                        }
                    case "Заказы":
                        {
                            Order_Click(PreviewItem, null);
                            break;
                        }
                    case "Пользователи":
                        {
                            Users_Click(PreviewItem, null);
                            break;
                        }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при обновлении данных, получаемых из базы данных!");
            }
        }

        private void Car_Click(object sender, RoutedEventArgs e)
        {
            HideButtonAdd();
            HideButtonChecks();
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

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            ShowButtonAdd();
            ShowButtonChecks();
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
                ServicesInOrder = string
                    .Join(Environment.NewLine,
                        p.ServicesInOrders
                        .Select(x => x.IdServiceNavigation.ServiceName + " " +
                            decimal.Round(x.IdServiceNavigation.ServicePrice, 2))
                        .ToList())
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
        private void Users_Click(object sender, RoutedEventArgs e)
        {
            MainDataGrid.ItemsSource = DbUtils.db.Users.Where(u => u.IdRole == 1).Select(p => new
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
        private void HideButtonChecks() => CheckGenerationButton.Visibility = Visibility.Hidden;
        private void ShowButtonChecks() => CheckGenerationButton.Visibility = Visibility.Visible;
        private void HideButtonAdd() => AddButton.Visibility = Visibility.Hidden;
        private void ShowButtonAdd() => AddButton.Visibility = Visibility.Visible;
        private void CheckGenerationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var window = new CheckGenerationWindow();
                window.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка открытия окна для генерации чека." +
                    "\nПри возникновении данной ошибка неоднократно рекомендуется перезапустить приложение.");
            }
        }
        private void SearchTextBox_QuerySubmitted(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            try
            {
                UpdateMainGridButton_Click(null, null);
                var TextForSearch = SearchTextBox.Text;

                //List<object> DataFormGuides = 

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
