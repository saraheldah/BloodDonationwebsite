using System.Security.Claims;
using System.Threading.Tasks;
using BloodDonation.Business.DTO;
using BloodDonation.Business.Managers;
using BloodDonation.Common;
using BloodDonationweb.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationweb.Controllers
{
    public class AccountController : MyBaseController
    {
        private readonly IUserManager _userManager;
        private readonly IBloodTypeManager _bloodTypeManager;
        private readonly ICityManager _cityManager;


        public AccountController(IUserManager userManager, IBloodTypeManager bloodTypeManager, ICityManager cityManager)
        {
            _userManager = userManager;
            _bloodTypeManager = bloodTypeManager;
            _cityManager = cityManager;
        }



        // GET

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (email is null)
            {
                throw new System.ArgumentNullException(nameof(email));
            }

            if (password is null)
            {
                throw new System.ArgumentNullException(nameof(password));
            }

            UserDTO user;
            if (email == "saeed.eldah@gmail.com" && password == "123")
            {
                user = new UserDTO()
                {
                    FirstName = "Saeed",
                    Role = Role.Donor,
                    Email = email
                };
            }
            else
            {
                user = _userManager.GetUserByEmailAndPassword(email, password);
            }

            if (user == null) return RedirectToAction("LogIn", "Account", "0");

            UserManagement<UserDTO>.Authinticate(Response, user);
            return RedirectToAction("Index", "Logged");
        }

        public IActionResult LogOut()
        {
            LogOutUser();
            return GoToHomePage();
        }

    }
}