using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using Repository;
using BusinessObject.Models;

namespace NguyenDoCaoLinhRazorPages.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public EditModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        [BindProperty(SupportsGet = true)]
        public string ErrorMsg { get; set; }
        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public IActionResult OnGetAsync(int id)
        {
            var customer =  _customerRepository.GetCustomerByID(id.ToString());
            if (customer == null)
            {
                return NotFound();
            }
            Customer = customer;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPostAsync()
        {
            Customer customer = new Customer()
            {
                CustomerId = Customer.CustomerId,
                CustomerFullName = Customer.CustomerFullName,
                CustomerBirthday = Customer.CustomerBirthday,
                Telephone = Customer.Telephone,
                EmailAddress = Customer.EmailAddress,
                CustomerStatus = Customer.CustomerStatus,
                Password = Customer.Password,
            };
            var result = _customerRepository.UpdateCustomer(customer);
            if (!result)
            {
                ErrorMsg = "Fail";
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
