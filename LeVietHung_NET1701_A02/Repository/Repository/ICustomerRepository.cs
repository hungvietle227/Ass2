using BusinessObject.Models;
using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAllCustomer();
        Role CheckLogin(string email, string password);
        Customer? GetCustomerByID(string id);
        bool UpdateCustomer(Customer customerUpdate);
        bool CreateCustomer(Customer customerCreate);
        Customer? GetCustomerByEmail(string email);
        List<Customer> SearchCustomer(string searchValue);
        bool DeleteCustomer(int id);
    }
}
