using BusinessObject.Models;
using DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CustomerDAO
    {
        private static CustomerDAO? instance;
        private static readonly object instanceLook = new object();
        public CustomerDAO() { }
        public static CustomerDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new CustomerDAO();
                    }
                }
                return instance;
            }
        }
        public Role CheckLogin(string email, string password)
        {
            IConfiguration config = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", true, true)
                   .Build();
            var adminUser = config["AdminAccount:Email"];
            var adminPassword = config["AdminAccount:Password"];
            using var db = new FuminiHotelManagementContext();
            if (email == adminUser && password == adminPassword)
            {
                return Role.Admin;
            }
            else
            {
                Customer? result = db.Customers.FirstOrDefault(m => m.EmailAddress.Equals(email) && m.Password.Equals(password));
                return result != null ? Role.Customer : Role.None;
            }
        }
        public IEnumerable<Customer> GetAllCustomer()
        {
            var listCustomer = new List<Customer>();
            try
            {
                using var db = new FuminiHotelManagementContext();
                listCustomer = db.Customers.Include(a=> a.BookingReservations).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listCustomer;
        }
        public Customer? GetCustomerByID(string id)
        {
            int.TryParse(id, out var ID);
            using var db = new FuminiHotelManagementContext();
            Customer? customer = db.Customers.FirstOrDefault(c => c.CustomerId == ID);
            return customer;
        }

        public bool UpdateCustomer(Customer customerUpdate)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.Customers.Update(customerUpdate);
                var result = db.SaveChanges();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool CreateCustomer(Customer customerCreate)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.Customers.Add(customerCreate);
                var result = db.SaveChanges();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Customer? GetCustomerByEmail(string email)
        {
            using var db = new FuminiHotelManagementContext();
            Customer? customer = db.Customers.FirstOrDefault(c => c.EmailAddress == email);
            return customer;
        }

        public List<Customer> SearchCustomer(string searchValue)
        {
            var listCustomer = new List<Customer>();
            try
            {
                using var db = new FuminiHotelManagementContext();
                listCustomer = db.Customers.Include(a => a.BookingReservations).Where(a=> a.CustomerFullName.Contains(searchValue) || 
                a.EmailAddress.Contains(searchValue)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listCustomer;
        }

        public bool DeleteCustomer(int id)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                var customer = db.Customers.FirstOrDefault(a => a.CustomerId == id);
                db.Customers.Remove(customer);
                var result = db.SaveChanges();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
