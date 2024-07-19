using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ControllerDAO
{
    public class CustomerDAO
    {
        private static CustomerDAO instance = null;
        public static readonly object instanceLock = new object();
        private CustomerDAO() { }
        public static CustomerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CustomerDAO();
                    }
                    return instance;
                }
            }
        }

        public Customer GetCustomerByLogin(string email, string pass)
        {
            Customer customer = null;
            try
            {
                using (var context = new PRN212_SU24_AS1Context())
                {
                    customer = context.Customers.SingleOrDefault(i => i.EmailAddress.Equals(email) && i.Password.Equals(pass));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customer;
        }

        public IEnumerable<Customer> GetCustomers() { 
            List<Customer> customers;
            try
            {
                using (var context = new PRN212_SU24_AS1Context())
                {
                    customers = context.Customers.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customers;
        }

        public Customer GetCustomerByID(int id)
        {
            Customer customer = null;
            try
            {
                var context = new PRN212_SU24_AS1Context();
                customer = context.Customers.FirstOrDefault(item => item.CustomerId == id);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customer;
        }

        public void AddNew(Customer Customer)
        {
            try
            {
                Customer currentCustomer = GetCustomerByID(Customer.CustomerId);
                if (currentCustomer == null)
                {
                    using (var context = new PRN212_SU24_AS1Context())
                    {
                        context.Customers.Add(Customer);
                        context.SaveChanges();
                    }                  
                }
                else
                {
                    throw new Exception("The Customer is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Customer Customer)
        {
            try
            {
                Customer currentCustomer = GetCustomerByID(Customer.CustomerId);
                if (currentCustomer != null)
                {
                    using (var context = new PRN212_SU24_AS1Context())
                    {
                        context.Entry(Customer).State = EntityState.Modified;
                        context.SaveChanges();
                    } 
                }
                else
                {
                    throw new Exception("The Customer does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(Customer Customer)
        {
            try
            {
                Customer currentCustomer = GetCustomerByID(Customer.CustomerId);
                if (currentCustomer != null)
                {
                    var context = new PRN212_SU24_AS1Context();
                        context.Customers.Remove(Customer);
                        context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Customer does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
