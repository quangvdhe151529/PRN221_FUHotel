using BusinessObject.Models;
using DataAccess.Repository;
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

namespace FuMiniHotel
{
    /// <summary>
    /// Interaction logic for InsertNewCustomer.xaml
    /// </summary>
    public partial class InsertNewCustomer : Window
    {
        ICustomerRepository customerRepository;
        UserManagement userManagerMembers;

        public InsertNewCustomer(ICustomerRepository repository, UserManagement um)
        {
            InitializeComponent();
            customerRepository = repository;
            userManagerMembers= um;
        }

        private Customer GetMemberObjects()
        {
            Customer customer = null;
            try
            {
                customer = new Customer
                {
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
        
        private bool isPasswordValid(string password)
        {
            if (password.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long", "Invalid Password");
                return false;
            }
            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsEmailUnique(string email)
        {
            // Gọi phương thức từ repository để kiểm tra xem email đã tồn tại hay chưa
            var existingCustomer = customerRepository.GetCustomerByLogin(email, "");
            return existingCustomer == null; // Trả về true nếu email chưa tồn tại
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!blankInput())
                {
                    MessageBox.Show("Blank input", "Insert Customer");
                    return;
                }
                // Kiểm tra độ dài mật khẩu
                if (!isPasswordValid(txtPassword.Text))
                {
                    return;
                }
                // Kiểm tra định dạng email
                if (!IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("Invalid email format", "Invalid Email");
                    return;
                }
                // Kiểm tra email đã tồn tại chưa
                if (!IsEmailUnique(txtEmail.Text))
                {
                    MessageBox.Show("Email already exists", "Duplicate Email");
                    return;
                }
                Customer member = GetMemberObjects();
                customerRepository.AddCustomer(new Customer()
                {
                    EmailAddress = txtEmail.Text,
                    CustomerFullName = txtFullname.Text,
                    CustomerBirthday = DateTime.Parse(txtBirthday.Text),
                    CustomerStatus = true,
                    Telephone = txtTelephone.Text,
                    Password = txtPassword.Text,
                });
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
