using DataAccess.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace LeVietHungRazorPages.Pages.Register
{
    public class RegisterModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public RegisterModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [BindProperty]
        public RegisterRequestDTO RegisterRequest { get; set; } = null!;

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var customer = _customerRepository.CreateCustomer(RegisterRequest);

            if (customer != null)
            {
                return RedirectToPage("/Login");
            }

            return Page();
        }
    }
}
