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

namespace HavalProject.Windows
{
    public partial class SuppliersAddAndChange : Window
    {
        bool ChangeMode = false;

        int id;
        public SuppliersAddAndChange(Suppliers? supplier)
        {
            InitializeComponent();
            CityComboBox.ItemsSource = DbUtils.db.Cities.ToList();
            CityComboBox.DisplayMemberPath = "CityName";
            if(supplier == null)
            {
                CityComboBox.SelectedIndex = 0;
            }
            else
            {
                ChangeMode = true;
                CityComboBox.SelectedIndex = supplier.IdCity - 1;
                EmailBox.Text = supplier.Email;
                PhoneNumberBox.Text = supplier.PhoneNumber;
                AddressBox.Text = supplier.Address;
                id = supplier.IdSupplier;
            }

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Suppliers supplier;
                if(ChangeMode)
                {
                    supplier = DbUtils.db.Suppliers.FirstOrDefault(p => p.IdSupplier == id);
                }
                else
                {
                    supplier = new Suppliers();
                }
                supplier.IdCity = (CityComboBox.SelectedItem as Cities).IdCity;
                supplier.Address = AddressBox.Text;
                supplier.Email = EmailBox.Text;
                supplier.PhoneNumber = PhoneNumberBox.Text;

                if(!ChangeMode)
                {
                    DbUtils.db.Suppliers.Add(supplier);
                }
                DbUtils.db.SaveChanges();
                this.Close();

            }
            catch
            {
                MessageBox.Show("error");
            }
        }
    }
}
