using HavalProject.Entities;
using HavalProject.Windows;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace HavalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool PasswordOpen;

        bool CaptchaOpen;

        DispatcherTimer Timer = new DispatcherTimer(DispatcherPriority.Render);

        int Attempt = 0;

        string Time;

        string RightAnswer = "";

        public MainWindow()
        {
            InitializeComponent();
            PasswordOpen = false;
            CaptchaOpen = false;

            Time = File.ReadAllText(Tools.Instruments.Directories.TimerSaves());

            Timer.Tick += new EventHandler(Timer_Tick);
            Timer.Interval = new TimeSpan(0, 0, 1);

            if (Time.Length != 0)
            {
                Timer.Start();
                MainStackPanel.IsEnabled = false;
                this.IsEnabled = false;
                TimerForAutorization.Visibility = Visibility.Visible;
            }

            using (Db db = new Db()) { var temp = db.Users.FirstOrDefault(); }

        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            Time = File.ReadAllText(Tools.Instruments.Directories.TimerSaves());

            if (Time.Length == 0)
            {
                File.AppendAllText(Tools.Instruments.Directories.TimerSaves(), "30");
                TimerForAutorization.Visibility = Visibility.Visible;
                return;
            }

            if (Time == "0")
            {
                TimerForAutorization.Visibility = Visibility.Collapsed;
                File.WriteAllText(Tools.Instruments.Directories.TimerSaves(), string.Empty);
                this.IsEnabled = true;
                MainStackPanel.IsEnabled = true;
                TimeSeconds.Text = "30";
                Timer.Stop();
            }
            else
            {
                Time = Convert.ToString(Convert.ToInt32(Time) - 1);
                File.WriteAllText(Tools.Instruments.Directories.TimerSaves(), Time);
                TimeSeconds.Text = Time;
            }


        }

        private void AuthButtonCLick_Click(object sender, RoutedEventArgs e)
        {
            Users? user =
                DbUtils
                .db
                .Users
                .FirstOrDefault(p => p.Login.ToLower() == LoginTextBox.Text.ToLower() && (p.Password == MainPasswordBox.Password || p.Password == PasswordTextBox.Text));
            if (user != null && (RightAnswer == CaptchaTextBox.Text && !CaptchaOpen))
            {
                switch (user.IdRole)
                {
                    case 1:
                        {
                            MessageBox.Show("Клиент не может пользоваться данным приложением");
                            break;
                        }
                    case 2:
                        {
                            SalesManagerWindow window = new SalesManagerWindow(user);
                            window.Show();
                            this.Close();
                            break;
                        }
                    case 3:
                        {
                            SupplyManagerWindow window = new SupplyManagerWindow(user);
                            window.Show();
                            this.Close();
                            break;
                        }
                    case 4:
                        {
                            DirectorWindow window = new DirectorWindow(user);
                            window.Show();
                            this.Close();
                            break;
                        }
                    case 5:
                        {
                            AdminWindow window = new AdminWindow(user);
                            window.Show();
                            this.Close();

                            break;
                        }
                }
            }
            else
            {
                Attempt++;
                if (Attempt == 3)
                {
                    MainStackPanel.IsEnabled = false;
                    this.IsEnabled = false;
                    TimerForAutorization.Visibility = Visibility.Visible;
                    Attempt = 0;
                    Timer.Start();
                }
                if (CaptchaOpen)
                {
                    MessageBox.Show("Логин или пароль введены не верно");
                }
                else
                {
                    this.Height = 700;
                    CreateCaptcha();
                    CaptchaPanel.Visibility = Visibility.Visible;
                    MessageBox.Show("Введенные данные не верны");
                }

            }
        }

        private void CheckPassword_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordOpen == false)
            {
                PasswordTextBox.Text = MainPasswordBox.Password;
                PasswordOpen = true;
                MainPasswordBox.Visibility = Visibility.Collapsed;
                PasswordTextBox.Visibility = Visibility.Visible;
                PasswordTextBox.Focus();
                CheckPasswordImage.Source = new BitmapImage(new Uri(@"/Images/hide.png", UriKind.Relative));
                CheckPassword.ToolTip = "Скрыть пароль";
            }
            else
            {
                MainPasswordBox.Password = PasswordTextBox.Text;
                MainPasswordBox.Visibility = Visibility.Visible;
                PasswordTextBox.Visibility = Visibility.Collapsed;
                MainPasswordBox.Focus();
                PasswordOpen = false;
                CheckPasswordImage.Source = new BitmapImage(new Uri(@"/Images/view.png", UriKind.Relative));
                CheckPassword.ToolTip = "Показать пароль";
            }
        }

        public void CreateCaptcha()
        {
            RightAnswer = "";
            string AllSymbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            Random rand = new Random();
            for (int i = 0; i < 6; i++)
            {
                RightAnswer += AllSymbols[rand.Next(0, AllSymbols.Length)];
            }

            var random = new Random();
            var pixels = new byte[256 * 256 * 4];
            random.NextBytes(pixels);
            BitmapSource bitmapSource = BitmapSource.Create(256, 256, 96, 96, PixelFormats.Pbgra32, null, pixels, 256 * 4);
            var visual = new DrawingVisual();
            using (DrawingContext drawingContext = visual.RenderOpen())
            {
                drawingContext.DrawImage(bitmapSource, new Rect(0, 0, 550, 200));
                drawingContext.DrawText(
                    new FormattedText(RightAnswer, CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                        new Typeface("Times New Roman"), 70, Brushes.DarkGreen), new Point(rand.Next(0, 256), rand.Next(0, 100)));
            }
            var image = new DrawingImage(visual.Drawing);
            CaptchaImage.Source = image;
        }

        private void ResetCaptchaButton_Click(object sender, RoutedEventArgs e)
        {
            CreateCaptcha();
        }
    }
}
