using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
    /// Interaction logic for RoomManagement.xaml
    /// </summary>
    public partial class RoomManagement : Window
    {
        IRoomInforRepository inforRepository;
        ICustomerRepository customerRepository = new CustomerRepository();
        public RoomManagement(IRoomInforRepository repository)
        {
            InitializeComponent();
            inforRepository = repository;
            ComboBoxRoomType.Items.Clear();
            using (var contect = new PRN212_SU24_AS1Context())
            {
                List<RoomType> ltsRoomType = contect.RoomTypes.ToList();
                foreach (RoomType item in ltsRoomType)
                {
                    ComboBoxRoomType.Items.Add(item);
                    ComboBoxRoomType.DisplayMemberPath = "RoomTypeName";
                }
            }
        }

        public void LoadRoomList()
        {
            using (var context = new PRN212_SU24_AS1Context())
            {
                lvMembers.ItemsSource = context.RoomTypes.Join(context.RoomInformations, b => b.RoomTypeId, c => c.RoomTypeId, (b, c) => new ModelRoom()
                {
                    RoomTypeId = b.RoomTypeId,
                    RoomId = c.RoomId,
                    RoomNumber = c.RoomNumber,
                    RoomDescription = c.RoomDetailDescription,
                    RoomMacCapacity = c.RoomMaxCapacity,
                    RoomType = b.RoomTypeName,
                    RoomPricePerDay = c.RoomPricePerDay
                }).ToList();

            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
             using (var context = new PRN212_SU24_AS1Context())
                {
                    RoomType roomType = (RoomType)ComboBoxRoomType.SelectedItem;

                    context.RoomInformations.Add(new RoomInformation()
                    {
                        RoomNumber = txtRomNumber.Text,
                        RoomDetailDescription = txtDescription.Text,
                        RoomMaxCapacity = int.Parse(txtMaxCapacity.Text),
                        RoomTypeId = roomType.RoomTypeId,
                        RoomPricePerDay = decimal.Parse(txtPricePerDay.Text)
                    });
                    context.SaveChanges();
                    LoadRoomList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't add this room!");
            }
        }

        private void ComboBoxRoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private ModelRoom GetModelRoom()
        {
            ModelRoom ModelRoom = null;
            try
            {
                RoomType r = (RoomType)ComboBoxRoomType.SelectedItem;
                ModelRoom = new ModelRoom
                {
                    RoomTypeId = int.Parse(r.RoomTypeId.ToString()),
                    RoomId = int.Parse(txtID.Text),
                    RoomNumber = txtRomNumber.Text,
                    RoomDescription = txtDescription.Text,
                    RoomMacCapacity = int.Parse(txtMaxCapacity.Text),
                    RoomPricePerDay = Decimal.Parse(txtPricePerDay.Text),
                    RoomType = null,
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Customer");
            }

            return ModelRoom;
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            ModelRoom room = GetModelRoom();
            if (room != null)
            {
                using (var context = new PRN212_SU24_AS1Context())
                {
                    RoomInformation roomInformation = new RoomInformation();
                    roomInformation.RoomId = room.RoomId;
                    roomInformation.RoomNumber = room.RoomNumber;
                    roomInformation.RoomDetailDescription = room.RoomDescription;
                    roomInformation.RoomMaxCapacity = room.RoomMacCapacity;
                    roomInformation.RoomTypeId = room.RoomTypeId;
                    roomInformation.RoomStatus = null;
                    roomInformation.RoomPricePerDay = room.RoomPricePerDay;

                    context.Entry(roomInformation).State = EntityState.Modified;
                    
                    context.SaveChanges();
                    LoadRoomList();
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //ModelRoom room = lvMembers.SelectedItem as ModelRoom;
            //if (room != null)
            //{
            //    using (var context = new PRN221_Spr24_AS1Context()) {
            //        RoomInformation roomInformation = context.RoomInformations.FirstOrDefault(r => r.RoomId == room.RoomId);
            //        if (roomInformation != null) {
            //            context.RoomInformations.Remove(roomInformation);
            //            context.SaveChanges();
            //        }
            //    }
            //}
        }


        private void lvMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ModelRoom room = lvMembers.SelectedItem as ModelRoom;   
            if (room != null)
            {
                txtID.Text = room.RoomId.ToString();
                txtRomNumber.Text = room.RoomNumber.ToString();
                txtMaxCapacity.Text = room.RoomMacCapacity.ToString();
                txtDescription.Text = room.RoomDescription.ToString();
                txtPricePerDay.Text = room.RoomPricePerDay.ToString();
                RoomType roomType = new RoomType();
                for (int i = 0; i < ComboBoxRoomType.Items.Count; i++)
                {
                    var prop = ComboBoxRoomType.Items[i].GetType().GetProperty("RoomTypeName");
                    string value = prop.GetValue(ComboBoxRoomType.Items[i], null).ToString();
                    if (prop != null && value == room.RoomType)
                    {
                        ComboBoxRoomType.SelectedIndex = i;
                    }
                }
            }
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
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

        private void btnBooking_Click(object sender, RoutedEventArgs e)
        {
            var lstBooking = new BookingWindow();
            lstBooking.Show();
            this.Close();
        }

        private void btnMember_Click(object sender, RoutedEventArgs e)
        {
            var customer = new UserManagement(customerRepository);
            customer.Show();
            this.Close();
            customer.LoadCustomerList();
        }

        //private RoomInformation GetRoomObject()
        //{
        //    //RoomInformation room = null;
        //    //try
        //    //{
        //    //    room = new RoomInformation() 
        //    //    {
        //    //        CustomerId = int.Parse(txtId.Text),
        //    //        EmailAddress = txtEmail.Text,
        //    //        CustomerFullName = txtFullname.Text,
        //    //        CustomerBirthday = DateTime.Parse(txtBirthday.Text),
        //    //        Telephone = txtTelephone.Text,
        //    //        Password = txtPassword.Text,
        //    //    };
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    MessageBox.Show(ex.Message, "Get Member");
        //    //}

        //    //return customer;
        //}

    }
    class ModelRoom
    {
        public int RoomTypeId { get; set; }
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomDescription { get; set; }

        public int ? RoomMacCapacity { get; set; }
        public decimal? RoomPricePerDay { get; set; }

        public string RoomType { get; set; }
    }
}
