using System;
using System.Collections.Generic;
using System.IO;
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
using System.Drawing;

namespace HavalProject.Windows
{
    /// <summary>
    /// Interaction logic for UsersAddAndChangeWindow.xaml
    /// </summary>
    public partial class UsersAddAndChangeWindow : Window
    {
        bool ChangeMode = false;

        string? path;

        int id;

        private void FillWindow(Users? user)
        {
            GenderComboBox.ItemsSource = DbUtils.db.Gender.ToList();
            GenderComboBox.DisplayMemberPath = "GenderName";
            RoleComboBox.ItemsSource = DbUtils.db.Role.ToList();
            RoleComboBox.DisplayMemberPath = "RoleName";

            if (user == null)
            {
                ChangeMode = false;
                EditUserWindow.Title = "Добавление нового пользователя";
            }
            else
            {
                ChangeMode = true;
                SurnameTextBox.Text = user.Surname;
                NameTextBox.Text = user.Name;
                DateBithdayControl.Text = user.DateOfBirth.ToString();
                EmailTextBox.Text = user.Email;
                PhoneTextBox.Text = user.PhoneNumber;
                LoginTextBox.Text = user.Login;
                PasswordTextBox.Text = user.Password;
                RepeatPasswordTextBox.Text = user.Password;
                id = user.IdUser;
                GenderComboBox.SelectedIndex = user.IdGender - 1;
                RoleComboBox.SelectedIndex = user.IdRole - 1;

                MemoryStream stream = new MemoryStream(user.Photo);
                var image = new ImageSourceConverter().ConvertFrom(user.Photo) as ImageSource;
                UserPhoto.Source = image;

                EditUserWindow.Title = "Изменение существующего пользователя";
            }
        }
        public UsersAddAndChangeWindow(Users? user)
        {
            InitializeComponent();
            FillWindow(user);
        }

        public UsersAddAndChangeWindow(Users? user, bool OnlyClient)
        {
            InitializeComponent();
            FillWindow(user);
            if(OnlyClient)
            {
                RoleComboBox.SelectedIndex = 0;
                RoleComboBox.IsEnabled = false;
            }

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Users users;
                if(ChangeMode)
                {
                    users = DbUtils.db.Users.FirstOrDefault(p => p.IdUser == id);
                }
                else
                {
                    users = new Users();
                }

                if(PasswordTextBox.Text != RepeatPasswordTextBox.Text)
                {
                    MessageBox.Show("Пароли не совпадают");
                    return;
                }
                if(path == null && !ChangeMode)
                {
                    users.Photo = File.ReadAllBytes("/Images/DefaultUserPhoto.jpg");
                }
                if(path != null)
                {
                    users.Photo = File.ReadAllBytes(path);
                }

                users.Surname = SurnameTextBox.Text;
                users.Name = NameTextBox.Text;
                DateTime dt;
                bool parse = DateTime.TryParse(DateBithdayControl.Text, out dt);
                users.DateOfBirth = dt;
                users.PhoneNumber = PhoneTextBox.Text;
                users.Email = EmailTextBox.Text;
                users.Login = LoginTextBox.Text;
                users.Password = PasswordTextBox.Text;
                users.IdGender = (GenderComboBox.SelectedItem as Gender).IdGender;
                users.IdRole = (RoleComboBox.SelectedItem as Role).IdRole;

                if (!ChangeMode)
                {
                    DbUtils.db.Users.Add(users);
                }
                DbUtils.db.SaveChanges();
                MessageBox.Show("Данные сохранены");
                this.Close();

            }
            catch
            {
                MessageBox.Show("Поля не заполнены или введены неверно");
            }
        }

        private void PhotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg";
                path = openFileDialog.FileName;
                UserPhoto.Source = new ImageSourceConverter().ConvertFrom(File.ReadAllBytes(path)) as ImageSource;
            }
        }
    }
}
