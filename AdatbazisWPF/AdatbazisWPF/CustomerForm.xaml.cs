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
using System.Xml.Linq;

namespace AdatbazisWPF
{
    /// <summary>
    /// Interaction logic for CustomerForm.xaml
    /// </summary>
    public partial class CustomerForm : Window
    {
        private CustomerService customerService;
        private Customer customerToUpdate;
        public CustomerForm(CustomerService customerService)
        {
            InitializeComponent();
            this.customerService = customerService;
        }

        public CustomerForm(CustomerService customerService, Customer customerToUpdate)
        {
            InitializeComponent();
            this.customerService = customerService;
            this.customerToUpdate = customerToUpdate;
            this.btnAdd.Visibility = Visibility.Collapsed;
            this.btnUpdate.Visibility = Visibility.Visible;

            tbName.Text = customerToUpdate.Name;
            tbBDay.Text = customerToUpdate.Birthyear.ToString();
            tbPCode.Text = customerToUpdate.Postal_Code.ToString();
            tbCountry.Text = customerToUpdate.Country;
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer customer = CreatecustomerFromInputData();
                if (customerService.Update(this.customerToUpdate.Id, customer))
                {
                    MessageBox.Show("Sikeres módosítás");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hiba történt a módosítás során! Javasoljuk az ablak bezárását!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer customer = CreatecustomerFromInputData();
                if (customerService.Create(customer))
                {
                    MessageBox.Show("Sikeres hozzáadás");
                    tbName.Text = "";
                    tbBDay.Text = "";
                    tbPCode.Text = "";
                    tbCountry.Text = "";
                }
                else
                {
                    MessageBox.Show("Hiba történt a hozzáadás során");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Customer CreatecustomerFromInputData()
        {
            string name = tbName.Text.Trim();
            string birthyearText = tbBDay.Text.Trim();
            string postalcodeText = tbPCode.Text.Trim();
            string country = tbCountry.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Név megadása kötelező");
            }

            if (string.IsNullOrEmpty(country))
            {
                throw new Exception("Ország megadása kötelező");
            }

            if (string.IsNullOrEmpty(postalcodeText))
            {
                throw new Exception("Irányítószám megadása kötelező");
            }
            if (!int.TryParse(postalcodeText, out int postal_code))
            {
                throw new Exception("Az irányítószám csak szám lehet");
            }

            if (string.IsNullOrEmpty(birthyearText))
            {
                throw new Exception("Születési év megadása kötelező");
            }
            if (!int.TryParse(birthyearText, out int birthyear))
            {
                throw new Exception("A születési év csak szám lehet");
            }

            Customer customer = new Customer();
            customer.Name = name;
            customer.Birthyear = birthyear;
            customer.Postal_Code = postal_code;
            customer.Country = country;
            return customer;
        }
    }
}
