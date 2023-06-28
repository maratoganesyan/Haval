using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace HavalProject
{
    /// <summary>
    /// Interaction logic for AddAndChangeWindow.xaml
    /// </summary>
    public partial class AddAndChangeWindow : Window
    {
        bool ChangeMode = true;

        int ToSwitchTable;

        int id;

        Type type;

        private void TranslateTextBlocks()
        {
            foreach (var blocks in MainStackPanel.Children)
            {
                if(blocks is TextBlock block)
                    block.Text = Translator.Translate(block.Text);
            }
        }

        public AddAndChangeWindow()
        {
            InitializeComponent();
        }

        public AddAndChangeWindow(AdditationalServices? services)
        {
            InitializeComponent();
            FirstBlock.Text = "ServiceName";
            SecondBlock.Text = "ServiceDescription";
            ThirdBlock.Text = "Price";
            ToSwitchTable = 1;
            TranslateTextBlocks();
            if (services == null)
            {
                ChangeMode = false;
                return;
            }
            FirstBox.Text = services.ServiceName;
            SecondBox.Text = services.ServiceDescription;
            ThirdBox.Text = Convert.ToString(services.ServicePrice);
            id = services.IdService;
        }
        public AddAndChangeWindow(Body? body)
        {
            InitializeComponent();
            SecondBlock.Visibility = Visibility.Collapsed;
            SecondBox.Visibility = Visibility.Collapsed;
            ThirdBlock.Visibility = Visibility.Collapsed;
            ThirdBox.Visibility = Visibility.Collapsed;
            FirstBlock.Text = "BodyName";

            ToSwitchTable = 2;
            TranslateTextBlocks();
            if (body == null)
            {
                ChangeMode = false;
                return;
            }
            FirstBox.Text = body.BodyName;


            id = body.IdBody;

        }
        public AddAndChangeWindow(CarStatus? status)
        {
            InitializeComponent();
            SecondBlock.Visibility = Visibility.Collapsed;
            SecondBox.Visibility = Visibility.Collapsed;
            ThirdBlock.Visibility = Visibility.Collapsed;
            ThirdBox.Visibility = Visibility.Collapsed;

            ToSwitchTable = 3;
            FirstBlock.Text = "CarStatusName";
            TranslateTextBlocks();
            if (status == null)
            {
                ChangeMode = false;
                return;
            }
            FirstBox.Text = status.CarStatusName;

            id = status.IdCarStatus;
        }
        public AddAndChangeWindow(Cities? cities)
        {
            InitializeComponent();
            SecondBlock.Visibility = Visibility.Collapsed;
            SecondBox.Visibility = Visibility.Collapsed;
            ThirdBlock.Visibility = Visibility.Collapsed;
            ThirdBox.Visibility = Visibility.Collapsed;
            FirstBlock.Text = "CityName";
            ToSwitchTable = 4;
            TranslateTextBlocks();
            if (cities == null)
            {
                ChangeMode = false;
                return;
            }
            FirstBox.Text = cities.CityName;

            id = cities.IdCity;
        }
        public AddAndChangeWindow(Entities.Color? color)
        {
            InitializeComponent();
            FirstBlock.Text = "ColorName";
            SecondBlock.Text = "ColorDescription";
            ThirdBlock.Text = "HEX";
            ToSwitchTable = 5;
            TranslateTextBlocks();
            if (color == null)
            {
                ChangeMode = false;
                return;
            }

            FirstBox.Text = color.ColorName;
            SecondBox.Text = color.ColorDescription;
            ThirdBox.Text = color.HEX;

            id = color.IdColor;
        }
        public AddAndChangeWindow(Configuration? configuration)
        {
            InitializeComponent();
            SecondBlock.Visibility = Visibility.Collapsed;
            SecondBox.Visibility = Visibility.Collapsed;
            ThirdBlock.Visibility = Visibility.Collapsed;
            ThirdBox.Visibility = Visibility.Collapsed;
            ToSwitchTable = 6;

            FirstBlock.Text = "ConfigurationName";
            TranslateTextBlocks();
            if (configuration == null)
            {
                ChangeMode = false;
                return;
            }
            FirstBox.Text = configuration.ConfigurationName;


            id = configuration.IdConfiguration;
        }
        public AddAndChangeWindow(Country? country)
        {
            InitializeComponent();
            SecondBlock.Visibility = Visibility.Collapsed;
            SecondBox.Visibility = Visibility.Collapsed;
            ThirdBlock.Visibility = Visibility.Collapsed;
            ThirdBox.Visibility = Visibility.Collapsed;

            ToSwitchTable = 7;
            FirstBlock.Text = "CountryName";
            TranslateTextBlocks();
            if (country == null)
            {
                ChangeMode = false;
                return;
            }
            FirstBox.Text = country.CountryName;


            id = country.IdCountry;
        }
        public AddAndChangeWindow(DriveType? type)
        {
            InitializeComponent();
            SecondBlock.Visibility = Visibility.Collapsed;
            SecondBox.Visibility = Visibility.Collapsed;
            ThirdBlock.Visibility = Visibility.Collapsed;
            ThirdBox.Visibility = Visibility.Collapsed;

            ToSwitchTable = 8;
            FirstBlock.Text = "DriveTypeName";
            TranslateTextBlocks();
            if (type == null)
            {
                ChangeMode = false;
                return;
            }
            FirstBox.Text = type.DriveTypeName;


            id = type.IdDriveType;
        }
        public AddAndChangeWindow(Engine? engine)
        {
            InitializeComponent();
            SecondBlock.Visibility = Visibility.Collapsed;
            SecondBox.Visibility = Visibility.Collapsed;
            ThirdBlock.Visibility = Visibility.Collapsed;
            ThirdBox.Visibility = Visibility.Collapsed;

            ToSwitchTable = 9;
            FirstBlock.Text = "EngineName";
            TranslateTextBlocks();
            if (engine == null)
            {
                ChangeMode = false;
                return;
            }
            FirstBox.Text = engine.EngineName;


            id = engine.IdEngineName;
        }
        public AddAndChangeWindow(EngineType? type)
        {
            InitializeComponent();
            SecondBlock.Visibility = Visibility.Collapsed;
            SecondBox.Visibility = Visibility.Collapsed;
            ThirdBlock.Visibility = Visibility.Collapsed;
            ThirdBox.Visibility = Visibility.Collapsed;
            ToSwitchTable = 10;

            FirstBlock.Text = "EngineTypeName";
            TranslateTextBlocks();
            if (type == null)
            {
                ChangeMode = false;
                return;
            }
            FirstBox.Text = type.EngineTypeName;


            id = type.IdEngineType;
        }
        public AddAndChangeWindow(Gender? gender)
        {
            InitializeComponent();
            SecondBlock.Visibility = Visibility.Collapsed;
            SecondBox.Visibility = Visibility.Collapsed;
            ThirdBlock.Visibility = Visibility.Collapsed;
            ThirdBox.Visibility = Visibility.Collapsed;
            ToSwitchTable = 11;

            FirstBlock.Text = "GenderName";
            TranslateTextBlocks();
            if (gender == null)
            {
                ChangeMode = false;
                return;
            }
            FirstBox.Text = gender.GenderName;
            id = gender.IdGender;
        }
        public AddAndChangeWindow(Models? models)
        {
            InitializeComponent();
            SecondBlock.Visibility = Visibility.Collapsed;
            SecondBox.Visibility = Visibility.Collapsed;
            ThirdBlock.Visibility = Visibility.Collapsed;
            ThirdBox.Visibility = Visibility.Collapsed;

            ToSwitchTable = 12;
            FirstBlock.Text = "ModelName";
            TranslateTextBlocks();
            if (models== null)
            {
                ChangeMode = false;
                return;
            }
            FirstBox.Text = models.ModelName;

            id = models.IdModel;
        }
        public AddAndChangeWindow(OrderStatuses? statuses)
        {
            InitializeComponent();
            SecondBlock.Visibility = Visibility.Collapsed;
            SecondBox.Visibility = Visibility.Collapsed;
            ThirdBlock.Visibility = Visibility.Collapsed;
            ThirdBox.Visibility = Visibility.Collapsed;
            FirstBlock.Text = "OrderStatusName";
            ToSwitchTable = 13;
            TranslateTextBlocks();
            if (statuses == null)
            {
                ChangeMode = false;
                return;
            }
            FirstBox.Text = statuses.OrderStatusName;

            id = statuses.IdOrderStatus;
        }
        public AddAndChangeWindow(Role? role)
        {
            InitializeComponent();
            SecondBlock.Visibility = Visibility.Collapsed;
            SecondBox.Visibility = Visibility.Collapsed;
            ThirdBlock.Visibility = Visibility.Collapsed;
            ThirdBox.Visibility = Visibility.Collapsed;
            FirstBlock.Text = "RoleName";
            ToSwitchTable = 14;
            TranslateTextBlocks();
            if (role == null)
            {
                ChangeMode = false;
                return;
            }
            FirstBox.Text = role.RoleName;


            id = role.IdRole;
        }
        public AddAndChangeWindow(RudderSide? side)
        {
            InitializeComponent();
            SecondBlock.Visibility = Visibility.Collapsed;
            SecondBox.Visibility = Visibility.Collapsed;
            ThirdBlock.Visibility = Visibility.Collapsed;
            ThirdBox.Visibility = Visibility.Collapsed;

            ToSwitchTable = 15;
            FirstBlock.Text = "RuddeSideName";
            TranslateTextBlocks();
            if (side == null)
            {
                ChangeMode = false;
                return;
            }
            FirstBox.Text = side.RuddeSideName;

            id = side.IdRudderSide;
        }

        public AddAndChangeWindow(SupplyStatuses? statuses)
        {
            InitializeComponent();
            SecondBlock.Visibility = Visibility.Collapsed;
            SecondBox.Visibility = Visibility.Collapsed;
            ThirdBlock.Visibility = Visibility.Collapsed;
            ThirdBox.Visibility = Visibility.Collapsed;

            FirstBlock.Text = "StatusName";
            ToSwitchTable = 17;
            TranslateTextBlocks();
            if (statuses == null)
            {
                ChangeMode = false;
                return;
            }

            FirstBox.Text = statuses.StatusName;

            id = statuses.IdStatus;
        }

        public AddAndChangeWindow(Transmission? transmission)
        {
            InitializeComponent();
            SecondBlock.Visibility = Visibility.Collapsed;
            SecondBox.Visibility = Visibility.Collapsed;
            ThirdBlock.Visibility = Visibility.Collapsed;
            ThirdBox.Visibility = Visibility.Collapsed;

            ToSwitchTable = 18;
            FirstBlock.Text = "TransmissionName";
            TranslateTextBlocks();
            if (transmission == null)
            {
                ChangeMode = false;
                return;
            }
            FirstBox.Text = transmission.TransmissionName;

            id = transmission.IdTransmission;
        }

        public AddAndChangeWindow(Year? year)
        {
            InitializeComponent();
            SecondBlock.Visibility = Visibility.Collapsed;
            SecondBox.Visibility = Visibility.Collapsed;
            ThirdBlock.Visibility = Visibility.Collapsed;
            ThirdBox.Visibility = Visibility.Collapsed;

            ToSwitchTable = 19;
            FirstBlock.Text = "Year";
            TranslateTextBlocks();
            if (year == null)
            {
                ChangeMode = false;
                return;
            }
            FirstBox.Text = Convert.ToString(year.YearValue);


            id = year.IdYear;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
           try
           {
                switch (ToSwitchTable)
                {
                    case 1:
                        {
                            if(ChangeMode)
                            {
                                try
                                {
                                    using (Db db = new Db())
                                    {
                                        AdditationalServices? services = db.AdditationalServices.FirstOrDefault(p => p.IdService == id);
                                        services.ServiceName = FirstBox.Text;
                                        services.ServiceDescription = SecondBox.Text;
                                        services.ServicePrice = Convert.ToDecimal(ThirdBox.Text);
                                        db.SaveChanges();
                                        MessageBox.Show("Изменения успешно сохранены");
                                        this.Close();
                                        return;
                                    }
                                }
                                catch
                                {
                                    MessageBox.Show("error");
                                    return;
                                }
                            }
                            try
                            {
                                AdditationalServices services = new AdditationalServices();
                                services.ServiceName = FirstBox.Text;
                                services.ServiceDescription = SecondBox.Text;
                                services.ServicePrice = Convert.ToDecimal(ThirdBox.Text);
                                DbUtils.db.AdditationalServices.Add(services);
                                DbUtils.db.SaveChanges();
                                MessageBox.Show("Добавление прошло успешно");
                                this.Close();
                                return;
                            }
                            catch
                            {
                                MessageBox.Show("error");
                                return;
                            }
                            break;
                        }
                    case 2:
                        {
                            if (ChangeMode)
                            {
                                try
                                {
                                    Body? body = DbUtils.db.Body.FirstOrDefault(p => p.IdBody == id);
                                    body.BodyName = FirstBox.Text;
                                    DbUtils.db.SaveChanges();
                                    MessageBox.Show("Изменения успешно сохранены");
                                    this.Close();
                                    return;
                                }
                                catch
                                {
                                    MessageBox.Show("error"); 
                                    return; 
                                } 
                            } 
                            try
                            {
                                Body body = new Body();
                                body.BodyName = FirstBox.Text;
                                DbUtils.db.Body.Add(body);
                                DbUtils.db.SaveChanges();
                                MessageBox.Show("Добавление прошло успешно");
                                this.Close();
                                return;
                            }
                            catch
                            {
                                MessageBox.Show("error");
                                return;
                            }
                            break;
                        }
                    case 3:
                        {
                            if (ChangeMode)
                            {
                                try
                                {
                                    CarStatus? status = DbUtils.db.CarStatus.FirstOrDefault(p => p.IdCarStatus == id);
                                    status.CarStatusName = FirstBox.Text;
                                    DbUtils.db.SaveChanges();
                                    MessageBox.Show("Изменения успешно сохранены");
                                    this.Close();
                                    return;
                                }
                                catch
                                {
                                    MessageBox.Show("error");
                                    return;
                                }
                            }
                            try
                            {
                                CarStatus status = new CarStatus();
                                status.CarStatusName = FirstBox.Text;
                                DbUtils.db.CarStatus.Add(status);
                                DbUtils.db.SaveChanges();
                                MessageBox.Show("Добавление прошло успешно");
                                this.Close();
                                return;
                            }
                            catch
                            {
                                MessageBox.Show("error");
                                return;
                            }
                            break;
                        }
                    case 4:
                        {
                            if (ChangeMode)
                            {
                                try
                                {
                                    Cities? city = DbUtils.db.Cities.FirstOrDefault(p => p.IdCity == id);
                                    city.CityName = FirstBox.Text;
                                    DbUtils.db.SaveChanges();
                                    MessageBox.Show("Изменения успешно сохранены");
                                    this.Close();
                                    return;
                                }
                                catch
                                {
                                    MessageBox.Show("error");
                                    return;
                                }
                            }
                            try
                            {
                                Cities city = new Cities();
                                city.CityName = FirstBox.Text;
                                DbUtils.db.Cities.Add(city);
                                DbUtils.db.SaveChanges();
                                MessageBox.Show("Добавление прошло успешно");
                                this.Close();
                                return;
                            }
                            catch
                            {
                                MessageBox.Show("error");
                                return;
                            }
                            break;
                        }
                    case 5:
                        {
                            if (ChangeMode)
                            {
                                try
                                {
                                    Entities.Color? color = DbUtils.db.Color.FirstOrDefault(p => p.IdColor == id);
                                    color.ColorName = FirstBox.Text;
                                    color.ColorDescription = SecondBox.Text;
                                    color.HEX = ThirdBox.Text;
                                    DbUtils.db.SaveChanges();
                                    MessageBox.Show("Изменения успешно сохранены");
                                    this.Close();
                                    return;
                                }
                                catch
                                {
                                    MessageBox.Show("error");
                                    return;
                                }
                            }
                            try
                            {
                                Entities.Color color = new Entities.Color();
                                color.ColorName = FirstBox.Text;
                                color.ColorDescription = SecondBox.Text;
                                color.HEX = ThirdBox.Text;
                                DbUtils.db.Color.Add(color);
                                DbUtils.db.SaveChanges();
                                MessageBox.Show("Добавление прошло успешно");
                                this.Close();
                                return;
                            }
                            catch
                            {
                                MessageBox.Show("error");
                                return;
                            }
                            break;
                        }
                    case 6:
                        {
                            if (ChangeMode)
                            {
                                try
                                {
                                    Configuration? configuration = DbUtils.db.Configuration.FirstOrDefault(p => p.IdConfiguration == id);
                                    configuration.ConfigurationName = FirstBox.Text;
                                    DbUtils.db.SaveChanges();
                                    MessageBox.Show("Изменения успешно сохранены");
                                    this.Close();
                                    return;
                                }
                                catch
                                {
                                    MessageBox.Show("error");
                                    return;
                                }
                            }
                            try
                            {
                                Configuration configuration = new Configuration();
                                configuration.ConfigurationName = FirstBox.Text;
                                DbUtils.db.Configuration.Add(configuration);
                                DbUtils.db.SaveChanges();
                                MessageBox.Show("Добавление прошло успешно");
                                this.Close();
                                return;
                            }
                            catch
                            {
                                MessageBox.Show("error");
                                return;
                            }
                            break;
                        }
                    case 7:
                        {
                            if (ChangeMode)
                            {
                                try
                                {
                                    Country? country = DbUtils.db.Country.FirstOrDefault(p => p.IdCountry  == id);
                                    country.CountryName = FirstBox.Text;
                                    DbUtils.db.SaveChanges();
                                    MessageBox.Show("Изменения успешно сохранены");
                                    this.Close();
                                    return;
                                }
                                catch
                                {
                                    MessageBox.Show("error");
                                    return;
                                }
                            }
                            try
                            {
                                Country country = new Country();
                                country.CountryName = FirstBox.Text;
                                DbUtils.db.Country.Add(country);
                                DbUtils.db.SaveChanges();
                                MessageBox.Show("Добавление прошло успешно");
                                this.Close();
                                return;
                            }
                            catch
                            {
                                MessageBox.Show("error");
                                return;
                            }
                            break;
                        }
                    case 8:
                        {
                            if (ChangeMode)
                            {
                                try
                                {
                                    DriveType? type = DbUtils.db.DriveType.FirstOrDefault(p => p.IdDriveType == id);
                                    type.DriveTypeName = FirstBox.Text;
                                    DbUtils.db.SaveChanges();
                                    MessageBox.Show("Изменения успешно сохранены");
                                    this.Close();
                                    return;
                                }
                                catch
                                {
                                    MessageBox.Show("error");
                                    return;
                                }
                            }
                            try
                            {
                                DriveType type = new DriveType();
                                type.DriveTypeName = FirstBox.Text;
                                DbUtils.db.DriveType.Add(type);
                                DbUtils.db.SaveChanges();
                                MessageBox.Show("Добавление прошло успешно");
                                this.Close();
                                return;
                            }
                            catch
                            {
                                MessageBox.Show("error");
                                return;
                            }
                            break;
                        }
                    case 9:
                        {
                            if (ChangeMode)
                            {
                                try
                                {
                                    Engine? engine = DbUtils.db.Engine.FirstOrDefault(p => p.IdEngineName == id);
                                    engine.EngineName = FirstBox.Text;
                                    DbUtils.db.SaveChanges();
                                    MessageBox.Show("Изменения успешно сохранены");
                                    this.Close();
                                    return;
                                }
                                catch
                                {
                                    MessageBox.Show("error");
                                    return;
                                }
                            }
                            try
                            {
                                Engine country = new Engine();
                                country.EngineName= FirstBox.Text;
                                DbUtils.db.Engine.Add(country);
                                DbUtils.db.SaveChanges();
                                MessageBox.Show("Добавление прошло успешно");
                                this.Close();
                                return;
                            }
                            catch
                            {
                                MessageBox.Show("error");
                                return;
                            }
                            break;
                        }
                    case 10:
                        {
                            if (ChangeMode)
                            {
                                try
                                {
                                    EngineType? type = DbUtils.db.EngineType.FirstOrDefault(p => p.IdEngineType == id);
                                    type.EngineTypeName = FirstBox.Text;
                                    DbUtils.db.SaveChanges();
                                    MessageBox.Show("Изменения успешно сохранены");
                                    this.Close();
                                    return;
                                }
                                catch
                                {
                                    MessageBox.Show("error");
                                    return;
                                }
                            }
                            try
                            {
                                EngineType type = new EngineType();
                                type.EngineTypeName = FirstBox.Text;
                                DbUtils.db.EngineType.Add(type);
                                DbUtils.db.SaveChanges();
                                MessageBox.Show("Добавление прошло успешно");
                                this.Close();
                                return;
                            }
                            catch
                            {
                                MessageBox.Show("error");
                                return;
                            }
                            break;
                        }
                    case 11:
                        {
                            if (ChangeMode)
                            {
                                try
                                {
                                    Gender? gender = DbUtils.db.Gender.FirstOrDefault(p => p.IdGender == id);
                                    gender.GenderName = FirstBox.Text;
                                    DbUtils.db.SaveChanges();
                                    MessageBox.Show("Изменения успешно сохранены");
                                    this.Close();
                                    return;
                                }
                                catch
                                {
                                    MessageBox.Show("error");
                                    return;
                                }
                            }
                            try
                            {
                                Gender gender = new Gender();
                                gender.GenderName = FirstBox.Text;
                                DbUtils.db.Gender.Add(gender);
                                DbUtils.db.SaveChanges();
                                MessageBox.Show("Добавление прошло успешно");
                                this.Close();
                                return;
                            }
                            catch
                            {
                                MessageBox.Show("error");
                                return;
                            }
                            break;
                        }
                    case 12:
                        {
                            if (ChangeMode)
                            {
                                try
                                {
                                    Models? model = DbUtils.db.Models.FirstOrDefault(p => p.IdModel == id);
                                    model.ModelName = FirstBox.Text;
                                    DbUtils.db.SaveChanges();
                                    MessageBox.Show("Изменения успешно сохранены");
                                    this.Close();
                                    return;
                                }
                                catch
                                {
                                    MessageBox.Show("error");
                                    return;
                                }
                            }
                            try
                            {

                                Models model = new Models();
                                model.ModelName = FirstBox.Text;
                                DbUtils.db.Models.Add(model);
                                DbUtils.db.SaveChanges();
                                MessageBox.Show("Добавление прошло успешно");
                                this.Close();
                                return;
                            }
                            catch
                            {
                                MessageBox.Show("error");
                                return;
                            }
                            break;
                        }
                    case 13:
                        {
                            if (ChangeMode)
                            {
                                try
                                {
                                    OrderStatuses? status = DbUtils.db.OrderStatuses.FirstOrDefault(p => p.IdOrderStatus == id);
                                    status.OrderStatusName = FirstBox.Text;
                                    DbUtils.db.SaveChanges();
                                    MessageBox.Show("Изменения успешно сохранены");
                                    this.Close();
                                    return;
                                }
                                catch
                                {
                                    MessageBox.Show("error");
                                    return;
                                }
                            }
                            try
                            {
                                OrderStatuses status = new OrderStatuses();
                                status.OrderStatusName = FirstBox.Text;
                                DbUtils.db.OrderStatuses.Add(status);
                                DbUtils.db.SaveChanges();
                                MessageBox.Show("Добавление прошло успешно");
                                this.Close();
                                return;
                            }
                            catch
                            {
                                MessageBox.Show("error");
                                return;
                            }
                            break;
                        }
                    case 14:
                        {
                            if (ChangeMode)
                            {
                                try
                                {
                                    Role? role = DbUtils.db.Role.FirstOrDefault(p => p.IdRole == id);
                                    role.RoleName = FirstBox.Text;
                                    DbUtils.db.SaveChanges();
                                    MessageBox.Show("Изменения успешно сохранены");
                                    this.Close();
                                    return;
                                }
                                catch
                                {
                                    MessageBox.Show("error");
                                    return;
                                }
                            }
                            try
                            {
                                Role role = new Role();
                                role.RoleName = FirstBox.Text;
                                DbUtils.db.Role.Add(role);
                                DbUtils.db.SaveChanges();
                                MessageBox.Show("Добавление прошло успешно");
                                this.Close();
                                return;
                            }
                            catch
                            {
                                MessageBox.Show("error");
                                return;
                            }
                            break;
                        }
                    case 15:
                        {
                            if (ChangeMode)
                            {
                                try
                                {
                                    RudderSide? status = DbUtils.db.RudderSide.FirstOrDefault(p => p.IdRudderSide == id);
                                    status.RuddeSideName = FirstBox.Text;
                                    DbUtils.db.SaveChanges();
                                    MessageBox.Show("Изменения успешно сохранены");
                                    this.Close();
                                    return;
                                }
                                catch
                                {
                                    MessageBox.Show("error");
                                    return;
                                }
                            }
                            try
                            {
                                RudderSide side = new RudderSide();
                                side.RuddeSideName = FirstBox.Text;
                                DbUtils.db.RudderSide.Add(side);
                                DbUtils.db.SaveChanges();
                                MessageBox.Show("Добавление прошло успешно");
                                this.Close();
                                return;
                            }
                            catch
                            {
                                MessageBox.Show("error");
                                return;
                            }
                            break;
                        }
                    case 17:
                        {
                            if (ChangeMode)
                            {
                                try
                                {
                                    SupplyStatuses? status = DbUtils.db.SupplyStatuses.FirstOrDefault(p => p.IdStatus == id);
                                    status.StatusName = FirstBox.Text;
                                    DbUtils.db.SaveChanges();
                                    MessageBox.Show("Изменения успешно сохранены");
                                    this.Close();
                                    return;
                                }
                                catch
                                {
                                    MessageBox.Show("error");
                                    return;
                                }
                            }
                            try
                            {
                                SupplyStatuses status = new SupplyStatuses();
                                status.StatusName = FirstBox.Text;
                                DbUtils.db.SupplyStatuses.Add(status);
                                DbUtils.db.SaveChanges();
                                MessageBox.Show("Добавление прошло успешно");
                                this.Close();
                                return;
                            }
                            catch
                            {
                                MessageBox.Show("error");
                                return;
                            }
                            break;
                        }
                    case 18:
                        {
                            if (ChangeMode)
                            {
                                try
                                {
                                    Transmission? transmission = DbUtils.db.Transmission.FirstOrDefault(p => p.IdTransmission == id);
                                    transmission.TransmissionName = FirstBox.Text;
                                    DbUtils.db.SaveChanges();
                                    MessageBox.Show("Изменения успешно сохранены");
                                    this.Close();
                                    return;
                                }
                                catch
                                {
                                    MessageBox.Show("error");
                                    return;
                                }
                            }
                            try
                            {
                                Transmission transmission = new Transmission();
                                transmission.TransmissionName= FirstBox.Text;
                                DbUtils.db.Transmission.Add(transmission);
                                DbUtils.db.SaveChanges();
                                MessageBox.Show("Добавление прошло успешно");
                                this.Close();
                                return;
                            }
                            catch
                            {
                                MessageBox.Show("error");
                                return;
                            }
                            break;
                        }
                    case 19:
                        {
                            if (ChangeMode)
                            {
                                try
                                {
                                    Year? year = DbUtils.db.Year.FirstOrDefault(p => p.IdYear == id);
                                    year.YearValue = FirstBox.Text;
                                    DbUtils.db.SaveChanges();
                                    MessageBox.Show("Изменения успешно сохранены");
                                    this.Close();
                                    return;
                                }
                                catch
                                {
                                    MessageBox.Show("error");
                                    return;
                                }
                            }
                            try
                            {
                                Year year = new Year();
                                year.YearValue = FirstBox.Text;
                                year.IdYear = Convert.ToInt32(year.YearValue); 
                                DbUtils.db.Year.Add(year);
                                DbUtils.db.SaveChanges();
                                MessageBox.Show("Добавление прошло успешно");
                                this.Close();
                                return;
                            }
                            catch
                            {
                                MessageBox.Show("error");
                                return;
                            }
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("Error");
                            break;
                        }

                }
            }
           catch
           {
                MessageBox.Show("Error");
           }
        }
    }
}
