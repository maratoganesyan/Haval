using HavalProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace HavalProject.Windows
{
    /// <summary>
    /// Interaction logic for SuppliesAddAndChange.xaml
    /// </summary>
    public partial class SuppliesAddAndChange : Window
    {
        class DataGridItemTemplate
        {
            public int Id { get; set; }
            public string CarInfo { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public DataGridItemTemplate(GoodsInSupply TempGood)
            {
                Id = TempGood.IdCar;
                CarInfo = $"{TempGood.IdCarNavigation.IdModelNavigation.ModelName} " +
                    $"{TempGood.IdCarNavigation.IdConfigurationNavigation.ConfigurationName} " +
                    $"{TempGood.IdCarNavigation.IdColorNavigation.ColorName}";
                Price = decimal.Round(TempGood.SupplyPrice, 2);
                Quantity = TempGood.Quantity;
            }
        }

        private List<GoodsInSupply> goods = new List<GoodsInSupply>();
        private List<GoodsInSupply> goodsBackup = new List<GoodsInSupply>();
        private int? IdSupply;
        private bool IsItChange;
        private int SelectedStatusOfSupply;

        public SuppliesAddAndChange(Supplies? supply)
        {
            InitializeComponent();

            FillComboBoxesAndSelectItem(supply);

            if (supply == null)
            {
                IsItChange = false;
                SupplyChangeWindow.Title = "Оформление новой поставки в автосалон";
            }
            else
            {
                IdSupply = supply.IdSupply;
                SelectedStatusOfSupply = DbUtils
                    .db
                    .SupplyStatuses
                    .Single(s => s.StatusName == ((SupplyStatuses)SupplyStatusesComboBox.SelectedValue).StatusName)
                    .IdStatus;
                IsItChange = true;
                SupplyChangeWindow.Title = "Изменение существующей поставки";

                goods = supply.GoodsInSupply.ToList();

                foreach (GoodsInSupply good in goods)
                {
                    goodsBackup.Add(new GoodsInSupply()
                    {
                        IdCar = good.IdCar,
                        IdCarNavigation = good.IdCarNavigation,
                        Quantity = good.Quantity,
                        SupplyPrice = good.SupplyPrice,
                    });
                }

                SyncListExistedAndDataGrid();

                FillFieldsIfChange(supply);
            }

        }
        public void FillComboBoxesAndSelectItem(Supplies? supply)
        {
            CarComboBox.ItemsSource = DbUtils.db.Car.ToList();
            CarComboBoxReplace.ItemsSource = DbUtils.db.Car.ToList();
            SuppliersComboBox.ItemsSource = DbUtils.db.Suppliers.ToList();
            SupplyStatusesComboBox.ItemsSource = DbUtils.db.SupplyStatuses.ToList();
            if (supply != null)
            {
                SuppliersComboBox.SelectedItem =
                    DbUtils.db.Suppliers.Single(s => s.IdSupplier == supply.IdSupplier);
                SupplyStatusesComboBox.SelectedItem =
                    DbUtils.db.SupplyStatuses.Single(ss => ss.IdStatus == supply.IdSupplyStatus);
            }
        }
        public void FillFieldsIfChange(Supplies? supply)
        {
            DatePickerOfSupply.Text = $"{supply.DateTimeOfSupply:d}";
            TimePickerOfSupply.Text = $"{supply.DateTimeOfSupply:t}";
        }
        public void UpdateReplaceableGoodInSupply()
        {
            ExistingCarComboBoxReplace.Items.Clear();
            foreach (GoodsInSupply good in goods)
                ExistingCarComboBoxReplace.Items.Add(good);
        }
        public void InitializeDataGrid()
        {
            GoodsDataGrid.Items.Clear();
            foreach (GoodsInSupply good in goods)
                GoodsDataGrid.Items.Add(new DataGridItemTemplate(good));
        }
        public void SyncListExistedAndDataGrid()
        {
            UpdateReplaceableGoodInSupply();
            InitializeDataGrid();
        }
        public bool ValidateAddGoodInSupply()
        {
            if (CarComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Модель автомобиля для добавления в поставку не выбрана!");
                return false;
            }
            if (!int.TryParse(QuantityAddCarTextBox.Text, out _))
            {
                Car car = CarComboBox.SelectedItem as Car;
                MessageBox.Show($"Количество автомобилей Haval " +
                    $"{car.IdModelNavigation.ModelName} " +
                    $"{car.IdConfigurationNavigation.ConfigurationName} " +
                    $"{car.IdColorNavigation.ColorName} в поставке не выбрано, либо введено неверно!");
                return false;
            }
            if (int.Parse(QuantityAddCarTextBox.Text) < 1)
            {
                MessageBox.Show("Введено неверное количество автомобилей в поставке!");
                return false;
            }
            if (!decimal.TryParse(PriceAddCarTextBox.Text, out _))
            {
                Car car = CarComboBox.SelectedItem as Car;
                MessageBox.Show($"Цена автомобиля Haval " +
                    $"{car.IdModelNavigation.ModelName} " +
                    $"{car.IdConfigurationNavigation.ConfigurationName} " +
                    $"{car.IdColorNavigation.ColorName} за 1 штуку не указана, либо указана неверно!");
                return false;
            }
            if (decimal.Parse(PriceAddCarTextBox.Text) < 1)
            {
                MessageBox.Show("Введена неверная стоимость автомобилей в поставке, либо указана неверно!");
                return false;
            }
            return true;
        }
        public bool ValidateReplaceGoodBlock()
        {
            if (ExistingCarComboBoxReplace.SelectedIndex == -1)
            {
                MessageBox.Show("Заменяемая модель автомобиля не выбрана!");
                return false;
            }
            if (CarComboBoxReplace.SelectedIndex == -1)
            {
                MessageBox.Show("Заменяющая модель автомобиля не выбран!");
                return false;
            }
            if (!int.TryParse(QuantityAddCarTextBoxReplace.Text, out _))
            {
                Car car = CarComboBoxReplace.SelectedItem as Car;
                MessageBox.Show($"Количество автомобилей Haval " +
                    $"{car.IdModelNavigation.ModelName} " +
                    $"{car.IdConfigurationNavigation.ConfigurationName} " +
                    $"{car.IdColorNavigation.ColorName} в поставке не выбрано, либо введено неверно!");
                return false;
            }
            if (int.Parse(QuantityAddCarTextBoxReplace.Text) < 1)
            {
                MessageBox.Show("Введено неверное количество автомобилей в поставке!");
                return false;
            }
            if (!decimal.TryParse(PriceAddCarTextBoxReplace.Text, out _))
            {
                Car car = CarComboBoxReplace.SelectedItem as Car;
                MessageBox.Show($"Цена автомобиля Haval " +
                    $"{car.IdModelNavigation.ModelName} " +
                    $"{car.IdConfigurationNavigation.ConfigurationName} " +
                    $"{car.IdColorNavigation.ColorName} за 1 штуку не указана, либо указана неверно!");
                return false;
            }
            if (decimal.Parse(PriceAddCarTextBoxReplace.Text) < 1)
            {
                MessageBox.Show("Введена неверная стоимость автомобилей в поставке, либо указана неверно!");
                return false;
            }
            return true;
        }
        public bool ValidateSupplyFields()
        {
            if (SuppliersComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Поставщик текущей поставки не выбран!");
                return false;
            }
            if (!DatePickerOfSupply.SelectedDate.HasValue)
            {
                MessageBox.Show("Дата текущей поставки не выбрано!");
                return false;
            }
            if(!TimePickerOfSupply.SelectedTime.HasValue)
            {
                MessageBox.Show("Время текущей поставки не выбрана!");
                return false;
            }
            if (SupplyStatusesComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Статус текущей поставки не выбран!");
                return false;
            }
            return true;
        }
        public void AddCarInGoods(Car car, int quantity, decimal PriceOfOne)
        {
            GoodsInSupply gis = new GoodsInSupply()
            {
                IdCar = car.IdCar,
                IdCarNavigation = car,
                Quantity = quantity,
                SupplyPrice = PriceOfOne,
            };
            goods.Add(gis);
            SyncListExistedAndDataGrid();
        }
        private void ReplaceCarInSupplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateReplaceGoodBlock())
                return;
            GoodsInSupply Old = ExistingCarComboBoxReplace.SelectedItem as GoodsInSupply;
            Car SelectedCar = CarComboBoxReplace.SelectedItem as Car;
            GoodsInSupply New = new GoodsInSupply()
            {
                IdCar = SelectedCar.IdCar,
                IdCarNavigation = SelectedCar,
                Quantity = int.Parse(QuantityAddCarTextBoxReplace.Text),
                SupplyPrice = decimal.Parse(PriceAddCarTextBoxReplace.Text),
            };
            if ((Old.IdCar == New.IdCar && Old.Quantity == New.Quantity) &&
                (Old.IdCar == New.IdCar && Old.SupplyPrice == New.SupplyPrice)) // на саму себя
                return;
            if ((Old.IdCar == New.IdCar && Old.Quantity != New.Quantity) ||
                (Old.IdCar == New.IdCar && Old.SupplyPrice != New.SupplyPrice))
            {
                goods.Find(g => g.IdCar == Old.IdCar).Quantity = New.Quantity;
                goods.Find(g => g.IdCar == Old.IdCar).SupplyPrice = New.SupplyPrice;
                SyncListExistedAndDataGrid();
                return;
            }
            if (goods.Any(g => g.IdCar == New.IdCar)) // на ту, что уже есть
            {
                if ((goods.Find(g => g.IdCar == New.IdCar).Quantity != New.Quantity) ||
                    (goods.Find(g => g.IdCar == New.IdCar).SupplyPrice != New.SupplyPrice))
                {
                    goods.Find(g => g.IdCar == New.IdCar).Quantity = New.Quantity;
                    goods.Find(g => g.IdCar == New.IdCar).SupplyPrice = New.SupplyPrice;
                }
                goods.Remove(Old);
                SyncListExistedAndDataGrid();
                return;
            }
            if (goods.All(g => g.IdCar != New.IdCar)) //на новую
            {
                goods.Remove(Old);
                goods.Add(New);
                SyncListExistedAndDataGrid();
                return;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateSupplyFields())
                return;
            Supplies supply;
            if (IsItChange)
                supply = DbUtils.db.Supplies.Single(s => s.IdSupply == IdSupply);
            else
                supply = new Supplies();
            supply.IdSupplier = (SuppliersComboBox.SelectedItem as Suppliers).IdSupplier;
            DateTime DayOfSupply = DatePickerOfSupply.SelectedDate.Value.Date;
            TimeSpan TimeOfSupply = TimePickerOfSupply.SelectedTime.Value.TimeOfDay;
            supply.DateTimeOfSupply = new DateTime(DayOfSupply.Year, DayOfSupply.Month, DayOfSupply.Day,
                TimeOfSupply.Hours, TimeOfSupply.Minutes, TimeOfSupply.Seconds);
            supply.TotalPriceOfSupply = goods.Select(g => g.SupplyPrice * g.Quantity).Sum();
            supply.IdSupplyStatus = (SupplyStatusesComboBox.SelectedItem as SupplyStatuses).IdStatus;
            if (!IsItChange)
                DbUtils.db.Supplies.Add(supply);
            DbUtils.db.SaveChanges();

            IdSupply = DbUtils.db.Supplies.OrderBy(s => s.IdSupply).Last().IdSupply;
            DeleteGIS(IdSupply.Value);
            foreach (GoodsInSupply good in goods)
            {
                DbUtils.db.GoodsInSupply.Add(new GoodsInSupply()
                {
                    IdSupply = IdSupply.Value,
                    IdCar = good.IdCar,
                    Quantity = good.Quantity,
                    SupplyPrice = good.SupplyPrice,
                    IdCarNavigation = good.IdCarNavigation,
                });
            }
            DbUtils.db.SaveChanges();

            

            if (((SupplyStatuses)SupplyStatusesComboBox.SelectedValue).StatusName ==
                DbUtils.db.SupplyStatuses.Single(s => s.IdStatus == 3).StatusName)
            {
                if (!IsItChange)
                    AddQuantityOfCars();
                else if (IsItChange && (SelectedStatusOfSupply !=
                    DbUtils.db.SupplyStatuses.Single(s => s.StatusName ==
                    ((SupplyStatuses)SupplyStatusesComboBox.SelectedValue).StatusName).IdStatus))
                    AddQuantityOfCars();
                else if (IsItChange)
                    ChangeQuantityOfCars();
            }

            MessageBox.Show("Поставка успешно сохранена!");
            Close();
        }
        public void ChangeQuantityOfCars()
        {
            List<GoodsInSupply> Removed = new List<GoodsInSupply>();
            List<GoodsInSupply> Added = new List<GoodsInSupply>();
            List<GoodsInSupply> QuantityOfPriceChanged = new List<GoodsInSupply>();

            foreach (GoodsInSupply good in goods)
            {
                if (goodsBackup.All(g => g.IdCar != good.IdCar)) //добавилась
                    Added.Add(good);
                if (goodsBackup.Any(g => g.IdCar == good.IdCar) &&
                    goodsBackup.Single(g => g.IdCar == good.IdCar).Quantity != good.Quantity) // изменилась
                    QuantityOfPriceChanged.Add(good);
            }
            foreach (GoodsInSupply good in goodsBackup) // удалённые
                if (goods.All(g => g.IdCar != good.IdCar))
                    Removed.Add(good);

            foreach (GoodsInSupply good in Removed)
                DbUtils.db.Car.Single(c => c.IdCar == good.IdCar).Quantity -= good.Quantity;
            foreach (GoodsInSupply good in Added)
                DbUtils.db.Car.Single(c => c.IdCar == good.IdCar).Quantity += good.Quantity;
            foreach (GoodsInSupply good in QuantityOfPriceChanged)
            {
                Car some = DbUtils.db.Car.Single(c => c.IdCar == good.IdCar);
                some.Quantity -= goodsBackup.Single(g => g.IdCar == good.IdCar).Quantity;
                some.Quantity += good.Quantity;
            }
            DbUtils.db.SaveChanges();
        }
        public void AddQuantityOfCars()
        {
            foreach (GoodsInSupply good in goods)
                DbUtils.db.Car.Single(c => c.IdCar == good.IdCar).Quantity += good.Quantity;
            DbUtils.db.SaveChanges();
        }
        public static void DeleteGIS(int idSupply)
        {
            foreach (var good in DbUtils.db.GoodsInSupply.Where(o => o.IdSupply == idSupply).ToList())
                DbUtils.db.GoodsInSupply.Remove(good);
            DbUtils.db.SaveChanges();
        }
        private void AddGoodInSupplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateAddGoodInSupply())
                return;
            Car car = CarComboBox.SelectedItem as Car;
            if (goods.All(g => g.IdCar != car.IdCar))
                AddCarInGoods(car, int.Parse(QuantityAddCarTextBox.Text), decimal.Parse(PriceAddCarTextBox.Text));
        }
    }
}
