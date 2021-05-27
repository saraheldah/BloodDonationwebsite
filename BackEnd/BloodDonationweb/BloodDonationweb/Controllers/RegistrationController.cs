using BloodDonation.Business.DTO;
using BloodDonation.Business.Managers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodDonationweb.Controllers
{
    public class RegistrationController : MyBaseController
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
            if (email is null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (fname is null)
            {
                throw new ArgumentNullException(nameof(fname));
            }
            email = email.Trim().ToLower();
            password = password.Trim();
            var user = _userManager.GetByEmail(email);
            if (user != null) return RedirectToAction("Index", "Registration", 1); // Go To Registration Form with an error
            var newUserEntity = _userManager.UserEntity(email, password, fname, lname, phone, birthDate, city, gender, bloodType);
            _userManager.Add(newUserEntity);

            return GoToLogIn(1);
        }
    }
}
