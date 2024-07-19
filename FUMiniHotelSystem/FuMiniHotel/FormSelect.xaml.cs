using DataAccess.Repository;
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
    /// Interaction logic for FormSelect.xaml
    /// </summary>
    public partial class FormSelect : Window
    {
        ICustomerRepository customerRepository;
        IRoomInforRepository inforRepository;
        public FormSelect()
        {
            InitializeComponent();
        }

        private void Booking_Click(object sender, RoutedEventArgs e)
        {
            var booking = new BookingWindow();
            booking.Show();
            this.Close();
        }

        private void Customer_Click(object sender, RoutedEventArgs e)
        {
            ICustomerRepository customerRepository = new CustomerRepository();
            var windowProducts = new UserManagement(customerRepository);
            windowProducts.Show();
            this.Close();
            windowProducts.LoadCustomerList();
        }

        private void ListBooking_Click(object sender, RoutedEventArgs e)
        {
            var lstBooking = new ListBookingMng();
            lstBooking.Show();
            this.Close();
            lstBooking.LoadBookingList();
        }

        private void Room_Click(object sender, RoutedEventArgs e)
        {
            var lstRoom = new RoomManagement(inforRepository);
            lstRoom.Show();
            this.Close();
            lstRoom.LoadRoomList();
        }
    }
}
