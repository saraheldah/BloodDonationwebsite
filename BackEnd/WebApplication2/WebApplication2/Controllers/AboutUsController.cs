using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    public class AboutUs : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Details(int id)
        {
            return Ok("you have entered id = " + id);
        }
    }
}