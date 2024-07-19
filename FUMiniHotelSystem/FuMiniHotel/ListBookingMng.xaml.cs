using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore.Internal;
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

namespace FuMiniHotel
{
    /// <summary>
    /// Interaction logic for ListBookingMng.xaml
    /// </summary>
    public partial class ListBookingMng : Window
    {
        ICustomerRepository customerRepository = new CustomerRepository();
        IRoomInforRepository inforRepository;
        public ListBookingMng()
        {
            InitializeComponent();
        }

        public void LoadBookingList()
        {
            using (var context = new PRN212_SU24_AS1Context())
            {
                lvMembers.ItemsSource = context.BookingReservations.Join(context.Customers, b => b.CustomerId, c => c.CustomerId, (b, c) => new
                {
                  CustomerFullName = c.CustomerFullName,
                    BookingReservationId = b.BookingReservationId,
                    BookingDate = b.BookingDate,
                    TotalPrice = b.TotalPrice
                }).ToList();

            }
        }

        private void btnBooking_Click(object sender, RoutedEventArgs e)
        {
            var booking = new BookingWindow();
            booking.Show();
            this.Close();
        }

        private void btnRoom_Click(object sender, RoutedEventArgs e)
        {
            var lstRoom = new RoomManagement(inforRepository);
            lstRoom.Show();
            this.Close();
            lstRoom.LoadRoomList(); 
        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            var customer = new UserManagement(customerRepository);
            customer.Show();
            this.Close();
            customer.LoadCustomerList();
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        
    }
}
