using FribergCarRentals.DataAccess.Interfaces;
using FribergCarRentals.Models;
using FribergCarRentals.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FribergCarRentals.Pages.Customers
{
    public class LoginModel : PageModel
    {
        private readonly ICustomerRepository _customerRepo;
        private const string sessionCustomer = "_customer";

        public LoginModel(ICustomerRepository customerRepo)
        {
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
            HttpContext.Session.Remove(sessionCustomer);
            LoginData.Action = "logout";

            return Page();
        }

        public IActionResult OnPost()
        {
            LoginData.Customer = _customerRepo.GetByEmail(LoginData.Email);
            if (LoginData.Customer != null && LoginData.Customer.Password == LoginData.Password)
            {
                HttpContext.Session.SetInt32(sessionCustomer, LoginData.Customer.CustomerId);   // Session state (Key: "_customer", Value: CustomerId)

                if (HttpContext.Session.TryGetValue("_admin", out _))     // Search and close admin session if present.
                {
                    HttpContext.Session.Remove("_admin");
                }

                if (HttpContext.Session.TryGetValue("_vehicleId", out _))
                {
                    return RedirectToPage("/Create", "Booking");
                }

                return RedirectToPage("Overview");
            }
            if (LoginData.Customer == null)
            {
                ViewData["Fail"] = "There is no account with this email!";
            }
            if (LoginData.Customer != null && LoginData.Customer.Password != LoginData.Password)
            {
                ViewData["Fail"] = "The email and password does not match!";
            }

            LoginData.Action = "login";
            return Page();
        }

        public IActionResult OnPostCreate()
        {
            Customer customer = LoginData.Customer;
            ModelState.Clear();
            if (TryValidateModel(customer))
            {
                _customerRepo.Create(customer);
                HttpContext.Session.SetInt32(sessionCustomer, customer.CustomerId);   // Session state

                return RedirectToPage("Overview");
            }

            LoginData.Action = "login";
            return Page();
        }
    }
}
