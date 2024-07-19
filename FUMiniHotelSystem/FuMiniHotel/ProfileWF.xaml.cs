using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
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

namespace FuMiniHotel
{
    /// <summary>
    /// Interaction logic for ProfileWF.xaml
    /// </summary>
    public partial class ProfileWF : Window
    {
        public Customer customer;
        public ProfileWF(Customer _Customer)
        {
            InitializeComponent();
            this.customer = _Customer;
            InitializeComponent();
            LoadProfile();
            txtName.Text = customer.CustomerFullName;
            txtEmail.Text = customer.EmailAddress;
            txtEmail.IsEnabled = false;
            txtBirthDay.Text = customer.CustomerBirthday?.ToString();
            txtPhone.Text = customer.Telephone;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new PRN212_SU24_AS1Context())
            {
                context.Entry(new Customer()
                {
                    CustomerId = customer.CustomerId,
                    CustomerFullName = txtName.Text,
                    CustomerBirthday = DateTime.Parse(txtBirthDay.Text),
                    Telephone = txtPhone.Text,
                    EmailAddress = customer.EmailAddress,
                    CustomerStatus = customer.CustomerStatus,   
                    Password = customer.Password,
                }).State = EntityState.Modified;
                context.SaveChanges();  
            }
            LoadProfile();
        }
        private void btnBookingReservation_Click(object sender, RoutedEventArgs e)
        {
            var form = new ViewBooking(customer);
            form.Show();
            this.Close();
        }

        private void LoadProfile()
        {
            using (var context = new PRN212_SU24_AS1Context()) {
                customer = context.Customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);
            }

        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            customer = null;
            var formMain = new MainWindow();
            formMain.Show();
            this.Close();
        }

        private void btnViewBooking_Click(object sender, RoutedEventArgs e)
        {
            var form = new ViewBooking(customer);
            form.Show();
            this.Close();
        }
    }
}
