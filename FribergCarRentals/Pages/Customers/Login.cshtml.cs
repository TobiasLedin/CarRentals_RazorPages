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

        public LoginVM LoginData { get; set; }

        public IActionResult OnGet()
        {
            LoginData.Action = "login";
            return Page();
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove(sessionCustomer);
            LoginData.Action = "logout";
            return Page();
        }

        public IActionResult OnPost(string email, string password)
        {
            LoginData.Customer = _customerRepo.GetByEmail(email);
            if (LoginData.Customer != null && LoginData.Customer.Password == password)
            {
                HttpContext.Session.SetInt32(sessionCustomer, LoginData.Customer.CustomerId);   // Session state

                return RedirectToPage("Overview");
            }
            if (LoginData.Customer == null)
            {
                ViewData["Fail"] = "There is no account with this email!";
            }
            if (LoginData.Customer != null && LoginData.Customer.Password != password)
            {
                ViewData["Fail"] = "The email and password does not match!";
            }
            return Page();
        }

        public IActionResult OnPostCreate()
        {
            Customer customer = LoginData.Customer;
            if (TryValidateModel(customer))
            {
                _customerRepo.Create(customer);
                HttpContext.Session.SetInt32(sessionCustomer, customer.CustomerId);   // Session state

                return RedirectToPage("Overview");
            }
            
            return Page();
        }
    }
}
