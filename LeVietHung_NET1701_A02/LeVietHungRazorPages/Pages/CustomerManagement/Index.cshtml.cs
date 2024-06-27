using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using BusinessObject.Models;

namespace NguyenDoCaoLinhRazorPages.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public IndexModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public IList<Customer> Customer { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public IActionResult OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                Customer = _customerRepository.SearchCustomer(SearchString);
            }
            else
            {
                Customer = _customerRepository.GetAllCustomer().ToList();
            }
            return Page();
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
