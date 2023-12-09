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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdatbazisWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CustomerService customerService;
        public MainWindow(CustomerService customerService)
        {
            InitializeComponent();
            this.customerService = customerService;
            Read();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            CustomerForm form = new CustomerForm(customerService);
            form.Closed += (_, _) =>
            {
                Read();
            };
            form.ShowDialog();
        }

        private void Read()
        {
            customerTable.ItemsSource = customerService.GetAll();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Customer selected = customerTable.SelectedItem as Customer;
            if (selected == null)
            {
                MessageBox.Show("Módosításhoz előbb válasszon ki dolgozót!");
                return;
            }
            CustomerForm form = new CustomerForm(customerService, selected);
            form.Closed += (_, _) =>
            {
                Read();
            };
            form.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Customer selected = customerTable.SelectedItem as Customer;
            if (selected == null)
            {
                MessageBox.Show("Törléshez előbb válasszon ki ügyfelet!");
                return;
            }
            MessageBoxResult selectedButton =
                MessageBox.Show($"Biztos, hogy törölni szeretné az alábbi ügyfelet: {selected.Name}?",
                    "Biztos?", MessageBoxButton.YesNo);
            if (selectedButton == MessageBoxResult.Yes)
            {
                if (customerService.Delete(selected.Id))
                {
                    MessageBox.Show("Sikeres törlés!");
                }
                else
                {
                    MessageBox.Show("Hiba történt a törlés során, a megadott elem nem található");
                }
                Read();
            }
        }
    }
}
