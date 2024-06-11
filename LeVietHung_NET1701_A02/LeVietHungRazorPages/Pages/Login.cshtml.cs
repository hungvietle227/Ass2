using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeVietHungRazorPages.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public string ErrorMessage { get; set; }

        private readonly ICustomerRepository _customerRepository;

        public LoginModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            var result = _customerRepository.CheckLogin(Email, Password);
            if (result == DataAccess.Enums.Role.Customer)
            {
                //SessionHelper.SetObjectAsJson(HttpContext.Session, "Customer", result);
                return RedirectToPage("Customer");
            }
            return RedirectToPage("Error");
        }
    }
}
