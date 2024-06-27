using AutoMapper;
using BusinessObject.Models;
using DataAccess;
using DataAccess.DTO;
using DataAccess.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMapper _mapper;
        public CustomerRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Role CheckLogin(string email, string password)
        {
            return CustomerDAO.Instance.CheckLogin(email, password);
        }

        public IEnumerable<Customer> GetAllCustomer()
        {
            return CustomerDAO.Instance.GetAllCustomer();
        }

        public Customer? GetCustomerByID(string id)
        {
            return CustomerDAO.Instance.GetCustomerByID(id);
        }

        public bool UpdateCustomer(Customer customerUpdate)
        {
            return CustomerDAO.Instance.UpdateCustomer(customerUpdate);
        }
        public Customer CreateCustomer(RegisterRequestDTO request)
        {
            //Random random = new Random();
            //int randomId = random.Next(1, 9999999);
            var customer = new Customer()
            {
                CustomerFullName = request.CustomerFullName,
                CustomerBirthday = request.CustomerBirthday,
                CustomerStatus = 1,
                EmailAddress = request.EmailAddress,
                Password = request.Password,
                Telephone = request.Telephone,
            };

            var registeredCustomer = CustomerDAO.Instance.CreateCustomer(customer);

            if (registeredCustomer != null)
            {
                return registeredCustomer;
            }

            return null;
        }

        public Customer? GetCustomerByEmail(string email)
        {
            return CustomerDAO.Instance.GetCustomerByEmail(email);
        }

        public List<Customer> SearchCustomer(string searchValue)
        {
            return CustomerDAO.Instance.SearchCustomer(searchValue);
        }

        public bool DeleteCustomer(int id)
        {
            return CustomerDAO.Instance.DeleteCustomer(id);
        }

        public Customer CreateCustomer1(Customer request)
        {
            return CustomerDAO.Instance.CreateCustomer(request);
        }
    }
}
