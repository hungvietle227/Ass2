using LeVietHungRazorPages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Text.Json;

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
                var customer = _customerRepository.GetCustomerByEmail(Email);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "Customer", customer);
                HttpContext.Response.Cookies.Append("Customer", JsonSerializer.Serialize(customer));
                return RedirectToPage("/RoomManagement/Index");
            }
            return RedirectToPage("Error");
        }
    }
}
