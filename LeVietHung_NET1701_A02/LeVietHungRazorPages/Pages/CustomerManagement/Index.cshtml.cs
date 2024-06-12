using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace LeVietHungRazorPages.Pages.CustomerManagement
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public IndexModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IList<Customer> Customer { get;set; } = default!;

        public void OnGet()
        {
            Customer = _customerRepository.GetAllCustomer().ToList();
        }
    }
}
