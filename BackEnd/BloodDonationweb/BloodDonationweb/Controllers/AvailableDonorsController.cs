using System.Collections.Generic;
using BloodDonation.DataAccess;
using BloodDonationweb.Models;
using Microsoft.AspNetCore.Mvc;
using User = BloodDonation.DataAccess.Entities.User;


namespace BloodDonationweb.Controllers
{
    public class AvailableDonorsController : Controller
    {
        private readonly UnitOfWork _db;

        public AvailableDonorsController(UnitOfWork db)
        {
            _db = db;
        }
        // GET
        public IActionResult Index()
        {
            IEnumerable<User> objList = _db.UserRepository.All();
            return View(objList as IEnumerable<Models.User>);
        }
    }
}