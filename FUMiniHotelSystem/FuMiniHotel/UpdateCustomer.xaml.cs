using BusinessObject.Models;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    /// Interaction logic for UpdateCustomer.xaml
    /// </summary>
    public partial class UpdateCustomer : Window
    {
        ICustomerRepository customerRepository;
        UserManagement userManagerMembers;
        Customer customer;
        public UpdateCustomer(ICustomerRepository repository, UserManagement um, Customer customer)
        {
            InitializeComponent();
            customerRepository = repository;
            userManagerMembers = um;
            txtId.Text = " " + customer.CustomerId;
            txtEmail.Text = customer.EmailAddress;
            txtFullname.Text = customer.CustomerFullName;
            txtBirthday.Text = ""+ customer.CustomerBirthday;
            txtTelephone.Text = "" + customer.Telephone;
            txtPassword.Text = customer.Password;
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
                MessageBox.Show(ex.Message, "Get Customer");
            }

            return customer;
        }

        private bool blankInput()
        {
            if (txtFullname.Text.Length < 1 || txtTelephone.Text.Length < 1
                || txtBirthday.Text.Length < 1 || txtPassword.Text.Length < 1 || txtEmail.Text.Length < 1)
                return false;
            return true;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!blankInput())
                {
                    MessageBox.Show("Blank input", "Insert Customer");
                    return;
                }
                Customer member = GetMemberObjects();
                customerRepository.UpdateCustomer(member);
                this.Close();
                userManagerMembers.LoadCustomerList();
                MessageBox.Show($"{member.EmailAddress} inserted successfully ", "Insert customer");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Insert customer");
            }
        }
    }
}
