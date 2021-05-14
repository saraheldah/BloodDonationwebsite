using Microsoft.AspNetCore.Mvc;

namespace BloodDonationweb.Controllers
{
    public class LoggedController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoggedDonor()
        {
            return View();
        }

        public IActionResult RequestBlood()
        {
            return View();
        }

        public IActionResult BecomeDonor()
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        public IActionResult UpdateInformation()
        {
            return View();
        }

       
    }
}