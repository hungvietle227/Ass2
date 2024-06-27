using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace NguyenDoCaoLinhRazorPages.Pages.Students
{
    public class DeleteModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [BindProperty]
      public Customer Customer { get; set; } = default!;

        public IActionResult OnGetAsync(int id)
        {
            var customer = _customerRepository.GetCustomerByID(id.ToString());

            if (customer == null)
            {
                return NotFound();
            }
            else 
            {
                Customer = customer;
            }
            return Page();
        }

        public IActionResult OnPostAsync(int id)
        {
            var customerCheck = _customerRepository.DeleteCustomer(id);
            if (customerCheck)
            {
                return RedirectToPage("/CustomerManagement/Index");
            }
            return NotFound();
        }
    }
}
