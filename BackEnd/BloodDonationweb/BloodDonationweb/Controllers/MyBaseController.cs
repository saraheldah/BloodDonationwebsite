using BloodDonation.Business.DTO;
using BloodDonationweb.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodDonationweb.Controllers
{
    public class MyBaseController : Controller
    {
        protected UserDTO GetLoggedInUser()
        {
            return UserManagement<UserDTO>.GetLoggedInUser(Request);
        }

        protected void AuthenticateUser(UserDTO user)
        {
            UserManagement<UserDTO>.Authinticate(Response, user);
        }
        protected void LogOutUser()
        {
            UserManagement<UserDTO>.LogOut(Response);
        }
        protected IActionResult GoToHomePage()
        {
            return RedirectToAction("Index", "Home");
        }

        protected IActionResult GoToLogIn(object routVale = null)
        {
            if (routVale == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            return RedirectToAction("LogIn", "Account", routVale);
        }
    }
}
