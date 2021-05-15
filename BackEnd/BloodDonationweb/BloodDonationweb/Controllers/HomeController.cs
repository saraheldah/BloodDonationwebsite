using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BloodDonationweb.Models;
using BloodDonation.Business.Managers;

namespace BloodDonationweb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserManager _userManager;
        private readonly IBloodTypeManager _bloodTypeManager;

        public HomeController(ILogger<HomeController> logger
            , IUserManager userManager, IBloodTypeManager bloodTypeManager)//dependencies injection
        {
            _logger = logger;
            _userManager = userManager;
            _bloodTypeManager = bloodTypeManager;
        }

        public IActionResult Index()
        {
            var dtos = _bloodTypeManager.GetAll();
            return View(dtos);
        }
        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult GiveBlood()
        {
            return View();
        }

        public IActionResult Eligibility()
        {
            return View();
        }

        public IActionResult DonationOfPlasma()
        {
            return View();
        }

        public IActionResult LearnAboutBlood()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}