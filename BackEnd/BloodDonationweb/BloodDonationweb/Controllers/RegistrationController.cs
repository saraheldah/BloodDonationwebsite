using BloodDonation.Business.DTO;
using BloodDonation.Business.Managers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodDonationweb.Controllers
{
    public class RegistrationController : Controller
    {

        private readonly IUserManager _userManager;
        private readonly IBloodTypeManager _bloodTypeManager;
        private readonly ICityManager _cityManager;


        public RegistrationController(IUserManager userManager, IBloodTypeManager bloodTypeManager, ICityManager cityManager)
        {
            _userManager = userManager;
            _bloodTypeManager = bloodTypeManager;
            _cityManager = cityManager;
        }
        public IActionResult Index()
        {
            List<BloodTypeDto> bloodList = _bloodTypeManager.GetAll();
            List<CityDTO> cityList = _cityManager.GetAll();
            var tuple = new Tuple<List<BloodTypeDto>, List<CityDTO>>(bloodList, cityList);
            return View(tuple);
        }

        public IActionResult Register(string email, string password, string fname, string lname, string phone,
            DateTime birthDate, int city, int gender, int bloodType)
        {
            var newUserEntity = _userManager.userEntity(email, password, fname, lname, phone, birthDate, city, gender, bloodType);
            _userManager.Add(newUserEntity);

            return RedirectToAction("LogIn", "Account", "1");
        }
    }
}
