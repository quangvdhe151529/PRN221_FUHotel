using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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

namespace FuMiniHotel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ICustomerRepository customerRepository = new CustomerRepository();
        Customer customer;


        public MainWindow(ICustomerRepository repository)
        {
            InitializeComponent();
            customerRepository = repository;
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var config = new ConfigurationBuilder() 
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
            string adminEmail = config["Admin:Email"];
            string adminPassword = config["Admin:Password"];
            string user = email.Text;
            string password = pass.Password;
            if (user.Equals(adminEmail) && password.Equals(adminPassword))
            {
                var windowMembers = new FormSelect();
                windowMembers.Show();
                this.Close();
                return;
            }
            else
            {
                Customer cus = customerRepository.GetCustomerByLogin(user, password);
                if (cus != null)
                {
                    customer = cus;
                    var formCustomer = new ProfileWF(customer);
                    formCustomer.Show();    
                    this.Close();
                    return;
                }
            }
            MessageBox.Show("Login Fail");
        }
    }
}
