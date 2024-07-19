using BusinessObject.Models;
using DataAccess.ControllerDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public void AddCustomer(Customer customer)
        => CustomerDAO.Instance.AddNew(customer);

        public void DeleteCustomer(Customer customer)
        => CustomerDAO.Instance.Remove(customer);

        public IEnumerable<Customer> GetAll()
        => CustomerDAO.Instance.GetCustomers();

        public Customer GetById(int id)
        => CustomerDAO.Instance.GetCustomerByID(id);

        public Customer GetCustomerByLogin(string email, string pass)
        => CustomerDAO.Instance.GetCustomerByLogin(email, pass);

        public void UpdateCustomer(Customer customer)
        => CustomerDAO.Instance.Update(customer);
    }
}
