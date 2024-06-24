using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAllCustomer();
        Role CheckLogin(string email, string password);
        Customer? GetCustomerByID(string id);
        bool UpdateCustomer(Customer customerUpdate);
        Customer CreateCustomer(RegisterRequestDTO request);
        Customer? GetCustomerByEmail(string email);
        List<Customer> SearchCustomer(string searchValue);
        bool DeleteCustomer(int id);
    }
}
