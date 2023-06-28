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
using Microsoft.Win32;
using System.IO;

namespace HavalProject.Windows
{
    /// <summary>
    /// Interaction logic for CarsAddAndChange.xaml
    /// </summary>
    public partial class CarsAddAndChange : Window
    {
        bool ChangeMode = false;

        int PhotoCount;

        int id;

        List<CarPhoto> PhotosForAdd;


        public CarsAddAndChange(Car? car)
        {
            InitializeComponent();
            
            ModelsComboBox.ItemsSource = DbUtils.db.Models.ToList();
            EngineComboBox.ItemsSource = DbUtils.db.Engine.ToList();
            BodyComboBox.ItemsSource = DbUtils.db.Body.ToList();
            CountryComboBox.ItemsSource = DbUtils.db.Country.ToList();
            ColorComboBox.ItemsSource = DbUtils.db.Color.ToList();
            ConfigurationComboBox.ItemsSource = DbUtils.db.Configuration.ToList();
            CarStatusComboBox.ItemsSource = DbUtils.db.CarStatus.ToList();
            YearComboBox.ItemsSource = DbUtils.db.Year.ToList();
            RudderSideComboBox.ItemsSource = DbUtils.db.RudderSide.ToList();
            DriveTypeComboBox.ItemsSource = DbUtils.db.DriveType.ToList();
            TransmissionComboBox.ItemsSource = DbUtils.db.Transmission.ToList();
            EngineTypeComboBox.ItemsSource = DbUtils.db.EngineType.ToList();

            ModelsComboBox.DisplayMemberPath = "ModelName";
            EngineComboBox.DisplayMemberPath = "EngineName";
            BodyComboBox.DisplayMemberPath = "BodyName";
            CountryComboBox.DisplayMemberPath = "CountryName";
            ColorComboBox.DisplayMemberPath = "ColorName";
            ConfigurationComboBox.DisplayMemberPath = "ConfigurationName";
            CarStatusComboBox.DisplayMemberPath = "CarStatusName";
            YearComboBox.DisplayMemberPath = "YearValue";
            RudderSideComboBox.DisplayMemberPath = "RuddeSideName";
            DriveTypeComboBox.DisplayMemberPath = "DriveTypeName";
            TransmissionComboBox.DisplayMemberPath = "TransmissionName";
            EngineTypeComboBox.DisplayMemberPath = "EngineTypeName";

            if(car != null)
            {
                ModelsComboBox.SelectedIndex = car.IdModel - 1;
                EngineComboBox.SelectedIndex = car.IdEngine - 1;
                BodyComboBox.SelectedIndex = car.IdBody - 1;
                CountryComboBox.SelectedIndex = car.IdCountry - 1;
                ColorComboBox.SelectedIndex = car.IdColor - 1;
                ConfigurationComboBox.SelectedIndex = car.IdConfiguration - 1;
                CarStatusComboBox.SelectedIndex = car.IdCarStatus - 1;
                YearComboBox.SelectedValue = DbUtils.db.Year.Where(p => p.IdYear == car.IdYear).Single();
                RudderSideComboBox.SelectedIndex = car.IdRudderSide - 1;
                DriveTypeComboBox.SelectedIndex = car.IdDriveType - 1;
                TransmissionComboBox.SelectedIndex = car.IdTransmission - 1;
                EngineTypeComboBox.SelectedIndex = car.IdEngineType - 1;

                CLientPriceTextBox.Text = decimal.Round(car.ClientPrice, 2).ToString();
                PowerTextBox.Text = Convert.ToString(car.Power);
                EngineCapacityTextBox.Text = Convert.ToString(car.EngineCapacity);
                TankCapacityTextBox.Text = Convert.ToString(car.TankCapacity);
                QuantityTextBox.Text = Convert.ToString(car.Quantity);

                DescriptionTextBox.AppendText(car.Description);

                OutputPhoto.ItemsSource = DbUtils.db.CarPhoto.Where(p => p.IdCar == car.IdCar).ToList();

                ChangeMode = true;

                PhotoCount = DbUtils.db.CarPhoto.Where(p => p.IdCar == car.IdCar).Count();

                id = car.IdCar;

                EditCarWindow.Title = "Изменение существующего автомобиля";
            }
            else
            {
                ModelsComboBox.SelectedIndex = 0;
                EngineComboBox.SelectedIndex = 0;
                BodyComboBox.SelectedIndex = 0;
                CountryComboBox.SelectedIndex = 0;
                ColorComboBox.SelectedIndex = 0;
                ConfigurationComboBox.SelectedIndex = 0;
                CarStatusComboBox.SelectedIndex = 0;
                YearComboBox.SelectedIndex = 0;
                RudderSideComboBox.SelectedIndex = 0;
                DriveTypeComboBox.SelectedIndex = 0;
                TransmissionComboBox.SelectedIndex = 0;
                EngineTypeComboBox.SelectedIndex = 0;

                PhotoCount = 0;
                PhotosForAdd = new List<CarPhoto>();
                id = DbUtils.db.CarPhoto.Max(p => p.IdCar) + 1;

                EditCarWindow.Title = "Добавление нового автомобиля";
            }
        }

        private void AddPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if(ChangeMode)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                    {
                        openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg";
                        var NewPhoto = new CarPhoto();
                        NewPhoto.IdCar = id;
                        NewPhoto.PhotoNumber = DbUtils.db.CarPhoto.Where(p => p.IdCar == id).Max(p => p.PhotoNumber) + 1;
                        NewPhoto.Photo = File.ReadAllBytes(openFileDialog.FileName);

                        DbUtils.db.Add(NewPhoto);
                        DbUtils.db.SaveChanges();

                        OutputPhoto.ItemsSource = DbUtils.db.CarPhoto.Where(p => p.IdCar == id).ToList();
                    }
                }
                else
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                    {
                        CarPhoto photo = new CarPhoto();
                        photo.IdCar = id;
                        photo.PhotoNumber = ++PhotoCount;
                        photo.Photo = File.ReadAllBytes(openFileDialog.FileName);
                        PhotosForAdd.Add(photo);
                        OutputPhoto.ItemsSource = null;
                        OutputPhoto.ItemsSource = PhotosForAdd;
                    }
                }
            }
            catch
            {
                MessageBox.Show("error");
            }
        }

        private void PhotoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var b = sender as Button;
                if (ChangeMode)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                    {
                        openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg";
                        var NewPhoto = DbUtils.db.CarPhoto.FirstOrDefault(p => p.IdCar == id && p.PhotoNumber == Convert.ToInt32(b.Tag));
                        NewPhoto.Photo = File.ReadAllBytes(openFileDialog.FileName);
                        DbUtils.db.SaveChanges();

                        OutputPhoto.ItemsSource = DbUtils.db.CarPhoto.Where(p => p.IdCar == id).ToList();
                    }
                }
                else
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                    {
                        openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg";
                        foreach(var item in PhotosForAdd)
                        {
                            if(item.PhotoNumber == Convert.ToInt32(b.Tag))
                            {
                                item.Photo = File.ReadAllBytes(openFileDialog.FileName);
                                break;
                            }
                        }
                        OutputPhoto.ItemsSource = PhotosForAdd;
                    }
                }

            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Car car;
                if(ChangeMode)
                {
                    car = DbUtils.db.Car.FirstOrDefault(p => p.IdCar == id);
                }
                else
                {
                    car = new Car();
                }

                car.IdModel = (ModelsComboBox.SelectedItem as Models).IdModel;
                car.IdEngine = (EngineComboBox.SelectedItem as Engine).IdEngineName;
                car.IdBody = (BodyComboBox.SelectedItem as Body).IdBody;
                car.IdCountry = (CountryComboBox.SelectedItem as Country).IdCountry;
                car.IdColor = (ColorComboBox.SelectedItem as Entities.Color).IdColor;
                car.IdConfiguration = (ConfigurationComboBox.SelectedItem as Configuration).IdConfiguration;
                car.IdCarStatus = (CarStatusComboBox.SelectedItem as CarStatus).IdCarStatus;
                car.IdYear = (YearComboBox.SelectedItem as Year).IdYear;
                car.IdRudderSide = (RudderSideComboBox.SelectedItem as RudderSide).IdRudderSide;
                car.IdDriveType = (DriveTypeComboBox.SelectedItem as Entities.DriveType).IdDriveType;
                car.IdTransmission = (TransmissionComboBox.SelectedItem as Transmission).IdTransmission;
                car.IdEngineType = (EngineTypeComboBox.SelectedItem as EngineType).IdEngineType;

                car.ClientPrice = decimal.Parse(CLientPriceTextBox.Text);
                TextRange textRange = new TextRange(DescriptionTextBox.Document.ContentStart,
                    DescriptionTextBox.Document.ContentEnd);
                car.Description = textRange.Text.Replace("\r\n", string.Empty);
                car.Power = int.Parse(PowerTextBox.Text);
                car.EngineCapacity = decimal.Parse(EngineCapacityTextBox.Text);
                car.TankCapacity = int.Parse(TankCapacityTextBox.Text);
                car.Quantity = int.Parse(QuantityTextBox.Text);

                if(!ChangeMode)
                {
                    DbUtils.db.Add(car);

                    foreach(var item in PhotosForAdd)
                    {
                        DbUtils.db.CarPhoto.Add(item);
                    }
                }
                DbUtils.db.SaveChanges();
                MessageBox.Show("Машина успешно сохранена");
                this.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения данных, проверьте правильность ввода!");
            }
        }
    }
}
