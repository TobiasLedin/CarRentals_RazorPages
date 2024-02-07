using FribergCarRentals.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRentals.Components
{
    [ViewComponent]
    public class NavBar : ViewComponent
    {
        public NavBar()
        {
            UserType = new NavBarVM();
        }

        public NavBarVM UserType { get; set; }

        public IViewComponentResult Invoke(string NoDisplay)
        {
            if (HttpContext.Session.TryGetValue("_admin", out _))
            {
                UserType.UserType = "admin";
            }
            else if (HttpContext.Session.TryGetValue("_customer", out _))
            {
                UserType.UserType = "customer";
            }
            else
            {
                UserType.UserType = "visitor";
            }

            if(NoDisplay != null)
            {
                UserType.UserType = NoDisplay;
            }

            return View(UserType);
        }
    }
}
