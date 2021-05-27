using System.Collections.Generic;
using System.Linq;
using BloodDonation.Business.DTO;
using BloodDonation.Business.Managers;
using BloodDonation.DataAccess;
using BloodDonation.DataAccess.Repositories;
using BloodDonationweb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User = BloodDonation.DataAccess.Entities.User;


namespace BloodDonationweb.Controllers
{
    public class AvailableDonorsController : MyBaseController
    {
        
        
      private readonly IUserManager _userManager;

        public AvailableDonorsController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        // GET
        public IActionResult Index()
        {
            List<UserDTO> objList = _userManager.GetAll();
            return View(objList);
        }
    }
}