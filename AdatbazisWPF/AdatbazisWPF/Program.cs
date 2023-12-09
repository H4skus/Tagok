using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdatbazisWPF
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            CustomerService customerService = new CustomerService();
            if (args.Contains("--count"))
            {
                Console.WriteLine("A dolgozók száma: "+ customerService.GetAll().Count);
            }
            else
            {
                Application application = new Application();
                application.Run(new MainWindow(customerService));
            }
        }
    }
}
