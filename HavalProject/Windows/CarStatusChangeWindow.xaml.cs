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
    /// Interaction logic for CarStatusChangeWindow.xaml
    /// </summary>
    public partial class CarStatusChangeWindow : Window
    {
        private int IdCurrentCar;
        public CarStatusChangeWindow(Car car)
        {
            InitializeComponent();
            IdCurrentCar = car.IdCar;
            FillTextBlocks(car);
            CarStatusComboBox.ItemsSource = DbUtils.db.CarStatus.ToList();
            CarStatusComboBox.SelectedValue = DbUtils
                .db
                .CarStatus
                .Single(cs => cs.CarStatusName == car.IdCarStatusNavigation.CarStatusName);
            PhotosItemsControl.ItemsSource = car.CarPhoto.ToList();
        }
        private void FillTextBlocks(Car car)
        {
            ModelNameTB.Text = car.IdModelNavigation.ModelName;
            EngineNameTB.Text = car.IdEngineNavigation.EngineName;
            BodyNameTB.Text = car.IdBodyNavigation.BodyName;
            CountryNameTB.Text = car.IdCountryNavigation.CountryName;
            ColorNameTB.Text= car.IdColorNavigation.ColorName;
            ConfigurationNameTB.Text = car.IdConfigurationNavigation.ConfigurationName;
            YearNameTB.Text = car.IdYearNavigation.YearValue;
            RudderSideNameTB.Text = car.IdRudderSideNavigation.RuddeSideName;
            DriveTypeNameTB.Text = car.IdDriveTypeNavigation.DriveTypeName;
            TransmissionNameTB.Text = car.IdTransmissionNavigation.TransmissionName;
            EngineTypeNameTB.Text = car.IdEngineTypeNavigation.EngineTypeName;
            ClientPriceNameTB.Text = decimal.Round(car.ClientPrice, 2).ToString();
            PowerTB.Text = car.Power.ToString();
            EngineCapacityTB.Text = decimal.Round(car.EngineCapacity, 1).ToString();
            TankCapacityTB.Text = car.TankCapacity.ToString();
            QuantityTB.Text = car.Quantity.ToString();
            DescriptionTextBox.AppendText(car.Description);
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (CarStatusComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Статус автомобиля не выбран!");
                return;
            }
            DbUtils.db.Car.Single(c => c.IdCar == IdCurrentCar).IdCarStatus =
                (CarStatusComboBox.SelectedItem as CarStatus).IdCarStatus;
            DbUtils.db.SaveChanges();
            MessageBox.Show("Изменения успешно сохранены!");
            this.Close();
        }
    }
}
