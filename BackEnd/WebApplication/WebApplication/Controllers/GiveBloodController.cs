using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    public class GiveBloodController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}