using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ICustomerRepository
    {
        Customer GetCustomerByLogin(string email, string pass);

        IEnumerable<Customer> GetAll();

        Customer GetById(int id);

        void DeleteCustomer(Customer customer);

        void UpdateCustomer (Customer customer);

        void AddCustomer (Customer customer);
    }
}
