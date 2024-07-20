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
            //using (var context = new PRN212_SU24_AS1Context())
            //{
            //    lvMembers.ItemsSource = context.BookingReservations.Join(context.Customers, 
            //        b => b.CustomerId, c => c.CustomerId, (b, c) => new { b, c })
            //        .GroupJoin(context.BookingDetails, bc => bc.b.BookingReservationId, 
            //        bd => bd.BookingReservationId, (bc, bds) => new
            //    {
            //      CustomerFullName = bc.c.CustomerFullName,
            //        BookingReservationId = bc.b.BookingReservationId,
            //        BookingDate = bc.b.BookingDate,
            //        TotalPrice = bc.b.TotalPrice,
            //        NumberOfRoomsBooked = bds.Count()
            //    }).ToList();

            //}
            using (var context = new PRN212_SU24_AS1Context())
            {
                var bookings = context.BookingReservations
                    .Join(context.Customers, b => b.CustomerId, c => c.CustomerId, (b, c) => new
                    {
                        b.BookingReservationId,
                        b.CustomerId,
                        b.BookingDate,
                        b.TotalPrice,
                        c.CustomerFullName
                    })
                    .ToList();

                var bookingDetails = context.BookingDetails
                    .GroupBy(bd => bd.BookingReservationId)
                    .Select(g => new { BookingReservationId = g.Key, Count = g.Count() })
                    .ToDictionary(x => x.BookingReservationId, x => x.Count);

                var result = bookings
                    .Where(b => b.BookingDate.HasValue)  // Lọc bỏ các đặt phòng không có ngày
                    .GroupBy(b => new { b.CustomerId, BookingDate = b.BookingDate.Value.Date, b.CustomerFullName })
                    .Select(g => new
                    {
                        g.Key.CustomerFullName,
                        BookingReservationId = g.First().BookingReservationId,
                        g.Key.BookingDate,
                        TotalPrice = g.Sum(b => b.TotalPrice),
                        NumberOfRoomsBooked = g.Sum(b =>
                            bookingDetails.ContainsKey(b.BookingReservationId) ? bookingDetails[b.BookingReservationId] : 0)
                    })
                    .ToList();

                lvMembers.ItemsSource = result;
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
