using Microsoft.AspNetCore.Mvc;

namespace BloodDonationweb.Controllers
{
    public class SignInController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }
    }
}