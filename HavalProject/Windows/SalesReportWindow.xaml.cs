using HavalProject.Entities;
using HavalProject.Tools.SomeData;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace HavalProject.Windows
{
    /// <summary>
    /// Interaction logic for SalesReportWindow.xaml
    /// </summary>
    public partial class SalesReportWindow : Window
    {
        public SalesReportWindow()
        {
            InitializeComponent();

            StartSettings();
        }
        public void StartSettings()
        {
            FromDateToDateItem.Selected += (s, e) => ShowOneGrid(FromDateToDate);
            ByMonthItem.Selected += (s, e) => ShowOneGrid(ByMonth);
            ByYearItem.Selected += (s, e) => ShowOneGrid(ByYear);

            FromDateToDateItem.IsSelected = true;

            MonthSelection.ItemsSource = DatesTool.Months();
            MonthSelection.SelectedIndex = 0;

            FillCarModelSelection();

            FillYearComboBoxes();
        }
        public void FillCarModelSelection()
        {
            foreach (Models Model in DbUtils.GetAllModels())
            {
                CheckBox ck = new CheckBox()
                {
                    Content = new TextBlock()
                    {
                        Text = $"{Model.ModelName}",
                        FontSize = 20,
                        Margin = new Thickness(0, -3.5D, 0, 0),
                    }
                };
                CarModelSelection.Children.Add(ck);
            }
        }
        public void FillYearComboBoxes()
        {
            YearSelection.ItemsSource = DbUtils.GetYearsOfOrders();
            YearSelection.SelectedIndex = 0;
            YearSelectionSecond.ItemsSource = DbUtils.GetYearsOfOrders();
            YearSelectionSecond.SelectedIndex = 0;
        }
        public void ShowOneGrid(Grid grid)
        {
            FromDateToDate.Visibility = Visibility.Collapsed;
            ByMonth.Visibility = Visibility.Collapsed;
            ByYear.Visibility = Visibility.Collapsed;

            grid.Visibility = Visibility.Visible;
        }
        private (DateTime, DateTime) GetDatesFromDateToDate()
        {
            DateTime start = StartDate.SelectedDate.Value;
            DateTime end = EndDate.SelectedDate.Value;
            return (new DateTime(start.Year, start.Month, start.Day, 0, 0, 0),
                new DateTime(end.Year, end.Month, end.Day, 23, 59, 59));
        }
        private (DateTime, DateTime) GetDatesByMonth()
        {
            int Year = Int32.Parse(YearSelection.SelectedValue.ToString());
            int Month = Tools.SomeData.DatesTool.Months().IndexOf(MonthSelection.SelectedValue.ToString()) + 1;
            return (new DateTime(Year, Month, 1, 0, 0, 0),
                new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month), 23, 59, 59));
        }
        private (DateTime, DateTime) GetDatesByYear()
        {
            int Year = Int32.Parse(YearSelectionSecond.SelectedValue.ToString());
            return (new DateTime(Year, 1, 1, 0, 0, 0),
                new DateTime(Year, 12, 31, 23, 59, 59));
        }
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateAll())
                return;



            List<string> ActivatedModels = new();

            foreach (var item in CarModelSelection.Children)
                if ((item is CheckBox ck) && ck.IsChecked.Value)
                    ActivatedModels.Add((ck.Content as TextBlock).Text);

            List<int> Models = DbUtils.GetIdModelsByName(ActivatedModels);

            (DateTime, DateTime) Dates;

            if (FromDateToDate.Visibility == Visibility.Visible)
                Dates = GetDatesFromDateToDate();
            else if (ByMonth.Visibility == Visibility.Visible)
                Dates = GetDatesByMonth();
            else
                Dates = GetDatesByYear();

            _ = Tools
                .GenerationsOfReports
                .DoASalesReportAsync(new Tools
                    .ModelsOfETypes
                    .ModelForSalesReport(Dates.Item1,
                        Dates.Item2,
                        DbUtils
                        .GetOrdersByRestrictions(Models,
                            Dates.Item1,
                            Dates.Item2)),
                ParentGrid,
                RandomizationGrid);
        }
        #region Validation
        private bool ValidateAll()
        {
            if (FromDateToDate.Visibility == Visibility.Visible)
                return ValidateFromDateToDate();
            else if (ByMonth.Visibility == Visibility.Visible)
                return ValidateByMonth();
            else
                return ValidateByYear();
        }
        private bool ValidateFromDateToDate()
        {
            if (!StartDate.SelectedDate.HasValue)
                return ShowMessageBoxWithFalse("Дата для начала генерации отчёта не задана!");
            if (!EndDate.SelectedDate.HasValue)
                return ShowMessageBoxWithFalse("Дата для окончания генерации отчёта не задана!");
            if (StartDate.SelectedDate.Value > EndDate.SelectedDate.Value)
                return ShowMessageBoxWithFalse("Дата начала отчёта не может быть позже даты конца отчёта!");
            return true;
        }
        private bool ValidateByMonth()
        {
            if (YearSelection.SelectedIndex == -1)
                return ShowMessageBoxWithFalse("Год для генерации отчётности не выбран!");
            if (MonthSelection.SelectedIndex == -1)
                return ShowMessageBoxWithFalse("Месяц для генерации отчётности не выбран!");
            return true;
        }
        private bool ValidateByYear()
        {
            if (YearSelectionSecond.SelectedIndex == -1)
                return ShowMessageBoxWithFalse("Год для генерации отчётности не выбран!");
            return true;
        }
        private bool ShowMessageBoxWithFalse(string text)
        {
            MessageBox.Show(messageBoxText: text, caption: "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Stop);
            return false;
        }
        #endregion
    }
}
