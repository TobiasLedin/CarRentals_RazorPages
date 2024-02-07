using FribergCarRentals.Interfaces;
using FribergCarRentals.Models;
using FribergCarRentals.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages.Customers
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _auth;
        private readonly ICustomerRepository _customerRepo;

        public LoginModel(IAuthService auth, ICustomerRepository customerRepo)
        {
            _auth = auth;
            _customerRepo = customerRepo;
            LoginData = new LoginVM();
        }

        [BindProperty]
        public LoginVM LoginData { get; set; }

        public IActionResult OnGet()
        {
            ViewData["NavBar"] = "NoDisplay";
            LoginData.Action = "login";

            return Page();
        }

        public IActionResult OnGetLogout()
        {
            ViewData["NavBar"] = "NoDisplay";
            _auth.RemoveCustomerAuth();
            LoginData.Action = "logout";

            return Page();
        }

        public IActionResult OnPost()
        {
            var result = _auth.CustomerAuth(LoginData.Email, LoginData.Password);
            if (!result.Success)
            {
                ViewData["fail"] = result.Message;
                return RedirectToPage("/Customers/Login");
            }

            if (HttpContext.Session.TryGetValue("_vehicleId", out _))
            {
                return RedirectToPage("/Create", "Booking");
            }

            LoginData.Action = "login";
            return RedirectToPage("/Admins/Overview");
        }

        public IActionResult OnPostCreate()
        {
            Customer customer = LoginData.Customer;
            ModelState.Clear();
            if (TryValidateModel(customer))
            {
                _customerRepo.Create(customer);
                HttpContext.Session.SetInt32("_customer", customer.CustomerId);

                if (HttpContext.Session.TryGetValue("_admin", out _))
                {
                    HttpContext.Session.Remove("_admin");
                }

                if (HttpContext.Session.TryGetValue("_vehicleId", out _))
                {
                    return RedirectToPage("/Create", "Booking");
                }

                return RedirectToPage("/Customers/Overview");
            }

            LoginData.Action = "login";
            return Page();
        }
    }
}
