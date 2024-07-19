using BusinessObject.Models;
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
    /// Interaction logic for BookingWindow.xaml
    /// </summary>
    public partial class BookingWindow : Window
    {
        IRoomTypeRepository _RoomType = new RoomTypeRepository();
        IRoomInforRepository _RoomInfor = new RoomInforRepository();
        List<ModelBooking> ltBooking = new List<ModelBooking> { };
        double Total = 0;
        public BookingWindow()
        {
            InitializeComponent();
            var ltRoomType = _RoomType.GetAll();

            ComboBoxRoomType.Items.Clear();
            foreach (RoomType roomType in ltRoomType)
            {
                ComboBoxRoomType.Items.Add(roomType);
                ComboBoxRoomType.DisplayMemberPath = "RoomTypeName";
            }
            ComboBoxRoomType.SelectedIndex = 0;

            using (var context = new PRN212_SU24_AS1Context())
            {
                var ltCustomEmail = context.Customers.ToList();
                if (ltCustomEmail != null)
                {
                    ComboBoxCustomerEmail.Items.Clear();
                    foreach (Customer customer in ltCustomEmail)
                    {
                        ComboBoxCustomerEmail.Items.Add(customer);
                        ComboBoxCustomerEmail.DisplayMemberPath = "EmailAddress";
                    }
                    ComboBoxCustomerEmail.SelectedIndex = 0;
                }
            }

            GetData();
            SetEnableSelect(false);
        }

        private void GetData()
        {
            double total = 0;
            foreach (ModelBooking item in ltBooking)
            {
                total += item.TotalPrice;
            }
            Total = total;
            lbTotalPrice.Content = $"Total Price: {total}";
            if (ltBooking.Count > 0) btnClear.IsEnabled = true;
            else btnClear.IsEnabled = false;
            lvBooking.ItemsSource = ltBooking.ToList();

        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBoxNumberRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RoomInformation? select = (RoomInformation)ComboBoxNumberRoom.SelectedItem;
            if (select is not null)
            {

                txtPrice.Text = select.RoomPricePerDay.ToString();

            }
        }


        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RoomType roomType = (RoomType)ComboBoxRoomType.SelectedItem;
                if (roomType == null)
                {
                    MessageBox.Show("Room type can't be empty!");
                    return;
                }
                RoomInformation roomInfor = (RoomInformation)ComboBoxNumberRoom.SelectedItem;
                if (roomInfor == null)
                {
                    MessageBox.Show("Room number can't be empty!");
                    return;
                }
                var dateStart = txtStartDate.Text;

                if (dateStart == null)
                {
                    MessageBox.Show("start date can't be empty!");
                    return;
                }
                var dateEnd = txtEndDate.Text;
                if (dateEnd == null)
                {
                    MessageBox.Show("end date can't be empty!");
                    return;
                }

                var email = ComboBoxCustomerEmail.SelectedItem;
                if (email == null)
                {
                    MessageBox.Show("Email customer can't be empty");
                    return;
                }
                DateTime _dateStart = DateTime.Parse(dateStart);
                DateTime _dateEnd = DateTime.Parse(dateEnd);
                if (_dateStart >= _dateEnd)
                {
                    MessageBox.Show("end date must be big than start date can't be empty!");
                    return;
                }


                double pricePerDay = double.Parse(txtPrice.Text.ToString());
                double totalPrice = pricePerDay * (_dateEnd - _dateStart).TotalDays;
                ModelBooking select = (ModelBooking)lvBooking.SelectedItems[0];
                if (select != null)
                {
                    for (int i = 0; i < ltBooking.Count; i++)
                    {
                        if (ltBooking[i] == select)
                        {
                            ltBooking[i] = new ModelBooking()
                            {
                                RoomType = roomType.RoomTypeName,
                                RoomNumber = roomInfor.RoomNumber,
                                DateStart = _dateStart,
                                DateEnd = _dateEnd,
                                PricePerDay = pricePerDay,
                                TotalPrice = totalPrice,
                            };
                        }
                    }
                }
                GetData();
                SetEnableSelect(false);
            }
            catch (Exception ex)
            {
            }

        }


        private void btnBooking_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                RoomType roomType = (RoomType)ComboBoxRoomType.SelectedItem;
                if (roomType == null)
                {
                    MessageBox.Show("Room type can't be empty!");
                    return;
                }
                RoomInformation roomInfor = (RoomInformation)ComboBoxNumberRoom.SelectedItem;
                if (roomInfor == null)
                {
                    MessageBox.Show("Room number can't be empty!");
                    return;
                }
                var dateStart = txtStartDate.Text;

                if (dateStart == null)
                {
                    MessageBox.Show("start date can't be empty!");
                    return;
                }
                var dateEnd = txtEndDate.Text;
                if (dateEnd == null)
                {
                    MessageBox.Show("end date can't be empty!");
                    return;
                }

                Customer customer = (Customer)ComboBoxCustomerEmail.SelectedItem;
                if (customer == null)
                {
                    MessageBox.Show("Email customer can't be empty");
                    return;
                }

                DateTime _dateStart = DateTime.Parse(dateStart);
                DateTime _dateEnd = DateTime.Parse(dateEnd);

                if (_dateStart >= _dateEnd)
                {
                    MessageBox.Show("end date must be big than start date can't be empty!");
                    return;
                }
                using (var context = new PRN212_SU24_AS1Context())
                {
                    var ltBookingDetail = context.BookingDetails
                    .Where(e => e.RoomId == roomInfor.RoomId &&
                     ((_dateStart >= e.StartDate && _dateStart <= e.EndDate) ||
                 (_dateEnd >= e.StartDate && _dateEnd <= e.EndDate) ||
                 (_dateStart <= e.StartDate && _dateEnd >= e.EndDate)))
                .ToList();
                    if (ltBookingDetail.Count != 0)
                    {
                        MessageBox.Show("room has been booked!");
                        return;
                    }
                }

                double pricePerDay = double.Parse(txtPrice.Text.ToString());
                double totalPrice = pricePerDay * (_dateEnd - _dateStart).TotalDays;


                ltBooking.Add(new ModelBooking()
                {
                    CustomerID = customer.CustomerId,
                    RoomID = roomInfor.RoomId,
                    RoomType = roomType.RoomTypeName,
                    RoomNumber = roomInfor.RoomNumber,
                    DateStart = _dateStart,
                    DateEnd = _dateEnd,
                    PricePerDay = pricePerDay,
                    TotalPrice = totalPrice,
                });
                MessageBox.Show("Successful");
                GetData();
                SetEnableSelect(false);
                ReloadRoomNumber();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't book this room!");
            }

        }

        private void lvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ModelBooking select = null;
            try
            {
                select = (ModelBooking)lvBooking.SelectedItems[0];
            }
            catch (Exception ex)
            {
                return;
            }

            RoomType roomType = new RoomType();
            for (int i = 0; i < ComboBoxRoomType.Items.Count; i++)
            {
                var prop = ComboBoxRoomType.Items[i].GetType().GetProperty("RoomTypeName");
                string value = prop.GetValue(ComboBoxRoomType.Items[i], null).ToString();
                if (prop != null && value == select.RoomType)
                {
                    ComboBoxRoomType.SelectedIndex = i;
                }
            }
            for (int i = 0; i < ComboBoxNumberRoom.Items.Count; i++)
            {
                var prop = ComboBoxNumberRoom.Items[i].GetType().GetProperty("RoomNumber");
                string value = prop.GetValue(ComboBoxNumberRoom.Items[i], null).ToString();
                if (prop != null && value == select.RoomNumber)
                {
                    ComboBoxNumberRoom.SelectedIndex = i;
                }
            }

            txtStartDate.Text = select.DateStart.ToString();
            txtEndDate.Text = select.DateEnd.ToString();
            txtPrice.Text = select.PricePerDay.ToString();
            SetEnableSelect(true);
        }

        private void SetEnableSelect(bool b)
        {
            btnDelete.IsEnabled = b;
            btnUpdate.IsEnabled = b;
        }

        private void ComboBoxRoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReloadRoomNumber();
        }


        private void ReloadRoomNumber()
        {
            RoomType select = (RoomType)ComboBoxRoomType.SelectedItem;
            if (select != null)
            {
                using (var context = new PRN212_SU24_AS1Context())
                {
                    var ltRoomInfor = context.RoomInformations.Select(r => r).Where(r => r.RoomTypeId == select.RoomTypeId).ToList();
                    ComboBoxNumberRoom.Items.Clear();
                    txtPrice.Text = "";
                    foreach (RoomInformation roomInfor in ltRoomInfor)
                    {
                        if (ltBooking.Find(b => b.RoomID == roomInfor.RoomId) != null) continue;
                        ComboBoxNumberRoom.Items.Add(roomInfor);
                        ComboBoxNumberRoom.DisplayMemberPath = "RoomNumber";
                    }
                    ComboBoxNumberRoom.SelectedIndex = 0;
                }
            }

        }

        private void btnDelete_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                ModelBooking select = (ModelBooking)lvBooking.SelectedItems[0];
                if (select != null)
                {
                    ltBooking.Remove(select);
                }
                ReloadRoomNumber();
                SetEnableSelect(false);
                GetData();
            }
            catch (Exception ex) { }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ltBooking = new List<ModelBooking>();
            GetData();
            ComboBoxRoomType.SelectedIndex = 0;
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            SetEnableSelect(false);
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new PRN212_SU24_AS1Context())
            {
                List<Booking> bookings = new List<Booking>();
                foreach (ModelBooking item in ltBooking)
                {
                    bool exist = false;
                    foreach (Booking booking in bookings)
                    {
                        if (booking.BookingReservation.CustomerId == item.CustomerID)
                        {
                            booking.BookingDetail.Add(new BookingDetail()
                            {
                                RoomId = item.RoomID,
                                StartDate = item.DateStart,
                                EndDate = item.DateEnd,
                                ActualPrice = Convert.ToDecimal(item.TotalPrice),
                            });
                            exist = true;
                            break;
                        }
                    }
                    if (!exist)
                    {
                        BookingReservation _booking = new BookingReservation()
                        {
                            BookingDate = DateTime.Now,
                            TotalPrice = Convert.ToInt32(Total),
                            CustomerId = item.CustomerID,
                            BookingStatus = 1,
                        };

                        context.BookingReservations.Add(_booking);
                        bookings.Add(new Booking()
                        {
                            BookingReservation = _booking,
                            BookingDetail = new List<BookingDetail> { new BookingDetail()
                            {
                                RoomId = item.RoomID,
                                StartDate = item.DateStart,
                                EndDate = item.DateEnd,
                                ActualPrice = Convert.ToDecimal(item.TotalPrice),
                            }}
                        });
                    }

                }
                MessageBox.Show("Successful");
                context.SaveChanges();
                foreach (Booking booking in bookings)
                {
                    foreach (BookingDetail bd in booking.BookingDetail)
                    {

                        context.BookingDetails.Add(new BookingDetail()
                        {
                            BookingReservationId = booking.BookingReservation.BookingReservationId,
                            RoomId = bd.RoomId,
                            StartDate = bd.StartDate,
                            EndDate = bd.EndDate,
                            ActualPrice = bd.ActualPrice,
                        });
                    }
                }
                context.SaveChanges();

            }
        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            ICustomerRepository customerRepository = new CustomerRepository();
            var windowProducts = new UserManagement(customerRepository);
            windowProducts.Show();
            this.Close();
            windowProducts.LoadCustomerList();
        }

        private void btnListBooking_Click(object sender, RoutedEventArgs e)
        {
            var windowProducts = new ListBookingMng();
            windowProducts.Show();
            this.Close();
            windowProducts.LoadBookingList();
        }

        private void btnRoom_Click(object sender, RoutedEventArgs e)
        {
            var windowProducts = new RoomManagement(_RoomInfor);
            windowProducts.Show();
            this.Close();
            windowProducts.LoadRoomList();
        }
    }
    public class ModelBooking
    {
        public int id { get; set; }
        public int CustomerID { get; set; }
        public int RoomID { get; set; }
        public string RoomType { get; set; }
        public string RoomNumber { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public double PricePerDay { get; set; }
        public double TotalPrice { get; set; }
    }
    public class Booking
    {
        public List<BookingDetail> BookingDetail { get; set; }
        public BookingReservation BookingReservation { get; set; }

    }
}
