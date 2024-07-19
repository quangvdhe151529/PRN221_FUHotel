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
using System.Windows.Shapes;
using BusinessObject.Models;
using DataAccess.Repository;

namespace FuMiniHotel
{
    /// <summary>
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : Window
    {
        ICustomerRepository customerRepository;
        IRoomInforRepository inforRepository;
        public UserManagement(ICustomerRepository repository)
        {
            InitializeComponent();
            customerRepository = repository;
        }

        public void LoadCustomerList()
        {
            lvMembers.ItemsSource = customerRepository.GetAll();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            InsertNewCustomer insertMemberWindow = new InsertNewCustomer(customerRepository, this);
            insertMemberWindow.Show();
        }

        private bool blankInput()
        {
            if (txtFullname.Text.Length < 1 || txtTelephone.Text.Length < 1
                || txtBirthday.Text.Length < 1 || txtPassword.Text.Length < 1 || txtEmail.Text.Length < 1) 
                return false;
            return true;
        }

        private Customer GetMemberObjects()
        {
            Customer customer = null;
            try
            {
                customer = new Customer
                {
                    CustomerId = int.Parse(txtId.Text),
                    EmailAddress = txtEmail.Text,
                    CustomerFullName = txtFullname.Text,
                    CustomerBirthday = DateTime.Parse(txtBirthday.Text),
                    Telephone = txtTelephone.Text,
                    Password = txtPassword.Text,
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Member");
            }

            return customer;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (!blankInput())
                {
                    MessageBox.Show("Blank input", "Update Customer");
                    return;
                }
                Customer customer = GetMemberObjects();
                UpdateCustomer updateMemberWindow = new UpdateCustomer(customerRepository, this, customer);
                updateMemberWindow.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Member");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!blankInput())
                {
                    MessageBox.Show("Blank input", "Insert Member");
                    return;
                }
                var myContextDB = new PRN212_SU24_AS1Context();

                Customer customer = GetMemberObjects();
                List<BookingReservation> booking = new List<BookingReservation>();
                booking = myContextDB.BookingReservations.Where(item => item.CustomerId == customer.CustomerId).ToList();
                if (booking.Count != 0)
                {
                    MessageBox.Show($"{customer.EmailAddress} had order , can't delete member", "Delete Member");
                }
                else
                {
                    customerRepository.DeleteCustomer(customer);
                    this.LoadCustomerList();
                    MessageBox.Show($"{customer.EmailAddress} deleted successfully ", "Delete Member");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Member");
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var lstMember = customerRepository.GetAll().ToList();
            var Id = String.IsNullOrEmpty(txtId.Text) == true ? "" : txtId.Text;
            var email = String.IsNullOrEmpty(txtEmail.Text) == true ? "" : txtEmail.Text;
            var fullname = String.IsNullOrEmpty(txtFullname.Text) == true ? "" : txtFullname.Text;
            var birthday = String.IsNullOrEmpty(txtBirthday.Text) == true ? "" : txtBirthday.Text;
            var telephone = String.IsNullOrEmpty(txtTelephone.Text) == true ? "" : txtTelephone.Text;

            lstMember = lstMember.Where(x => x.EmailAddress.ToLower().Contains(email.ToLower())
               && x.CustomerFullName.ToLower().Contains(fullname.ToLower())
               ).ToList();
            lvMembers.ItemsSource = lstMember;
        }

        private void btnRoom_Click(object sender, RoutedEventArgs e)
        {
            var lstRoom = new RoomManagement(inforRepository);
            lstRoom.Show();
            this.Close();
            lstRoom.LoadRoomList();
        }

        private void lvMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(customerRepository);
            mainWindow.Show();
            this.Close();
        }

        private void btnBookingReservation_Click(object sender, RoutedEventArgs e)
        {
            var lstBooking = new ListBookingMng();
            lstBooking.Show();
            this.Close();
            lstBooking.LoadBookingList();
        }
    }
}
