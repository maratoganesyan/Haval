using HavalProject.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for DirectorWindow.xaml
    /// </summary>
    public partial class DirectorWindow : Window
    {
        private bool IsDirectRequest;
        MenuItem PreviewItem;
        public DirectorWindow(Users user)
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
                            case "Пользователи":
                                {
                                    var cellInfo = MainDataGrid.SelectedCells[0];
                                    int content = Convert.ToInt16((cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text);
                                    var table = DbUtils.db.Users.Where(p => p.IdUser == content).FirstOrDefault();
                                    UsersAddAndChangeWindow window = new UsersAddAndChangeWindow(table);
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

        private void StatisticModuleButton_Click(object sender, RoutedEventArgs e)
        {
            new StatisticsModule().ShowDialog();
        }

        private void SalesReportButton_Click(object sender, RoutedEventArgs e)
        {
            new SalesReportWindow().ShowDialog();
        }

        private void SuppliesReportButton_Click(object sender, RoutedEventArgs e)
        {
            new SupplyReportWindow().ShowDialog();
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
