using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;

namespace NguyenDoCaoLinhRazorPages.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [BindProperty(SupportsGet = true)]
        public string ErrorMsg { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPostAsync()
        {
            Customer value = new Customer()
            {
                Password = Customer.Password,
                CustomerStatus = Customer.CustomerStatus,
                CustomerBirthday = Customer.CustomerBirthday,
                CustomerFullName = Customer.CustomerFullName,
                EmailAddress = Customer.EmailAddress,
                Telephone = Customer.Telephone
            };
            var customer = _customerRepository.CreateCustomer1(value);
            if (customer == null)
            {
                ErrorMsg = "Error";
                return Page();
            }
            return RedirectToPage("/CustomerManagement/Index");
        }

        public static IEnumerable<SelectListItem> SelectStatus()
        {
            return new[]
            {
                new SelectListItem {Text = "Active", Value = "1"},
                new SelectListItem {Text = "Inactive", Value = "0"}
            };
        }
    }
}
