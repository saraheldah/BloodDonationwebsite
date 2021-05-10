using Microsoft.AspNetCore.Mvc;

namespace BloodDonationweb.Controllers
{
    public class AvailableDonorsController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}