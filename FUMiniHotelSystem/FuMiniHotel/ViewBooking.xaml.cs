using BusinessObject.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
    /// Interaction logic for ViewBooking.xaml
    /// </summary>
    public partial class ViewBooking : Window
    {
        public Customer customer;
        public ViewBooking(Customer _custoemr)
        {
            customer = _custoemr;
            InitializeComponent();
            loading();
        }

        private void btnBookingReservation_Click(object sender, RoutedEventArgs e)
        {
            var formProfile = new ProfileWF(customer);
            formProfile.Show();
            this.Close();
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            customer = null;
            var formProfile = new MainWindow();
            formProfile.Show();
            this.Close();
        }

        private void lvMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BookingReservation booking = lvMembers.SelectedItem as BookingReservation;
            if (booking != null) {
                using (var context = new PRN212_SU24_AS1Context()) {
                    lvDetail.ItemsSource = context.BookingDetails.Join(context.RoomInformations, b => b.RoomId, r => r.RoomId, (b, r) => new ModelRoomDetail
                    {
                        BookingReservationID = b.BookingReservationId,
                        RoomNumber = r.RoomNumber,
                        StartDate = b.StartDate,
                        EndDate = b.EndDate,
                        ActualPrice = b.ActualPrice,
                    }).Where(b => b.BookingReservationID == booking.BookingReservationId).ToList();
                }
            }
        }

        private void loading()
        {
            using (var context = new PRN212_SU24_AS1Context())
            {
                List<BookingReservation> ltb = context.BookingReservations.Select(b => b).Where(b => b.CustomerId == customer.CustomerId).ToList();
                lvMembers.ItemsSource = ltb;
                if (ltb.Count > 0)
                {
                    lvMembers.SelectedIndex = 0;

                }
            }
        }

    }

    public class ModelRoomDetail
    { 
        public int BookingReservationID { get; set; }
        public string RoomNumber { get; set; }
        public DateTime? StartDate{ get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? ActualPrice { get; set; }

    }
}
