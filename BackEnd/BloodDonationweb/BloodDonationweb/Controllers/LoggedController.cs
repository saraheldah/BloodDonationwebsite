using System;
using System.Collections.Generic;
using BloodDonation.Business.DTO;
using BloodDonation.Business.Managers;
using BloodDonation.Common;
using BloodDonationweb.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace BloodDonationweb.Controllers
{
    public class LoggedController : MyBaseController
    {

        private readonly IUserManager _userManager;
        private readonly IBloodTypeManager _bloodTypeManager;
        private readonly ICityManager _cityManager;
        private readonly IBloodRequestManager _bloodRequest;



        public LoggedController(IUserManager userManager, IBloodTypeManager bloodTypeManager, ICityManager cityManager, IBloodRequestManager bloodRequestManager)
        {
            _userManager = userManager;
            _bloodTypeManager = bloodTypeManager;
            _cityManager = cityManager;
            _bloodRequest = bloodRequestManager;
        }
        public IActionResult Index()
        {
            var user = UserManagement<UserDTO>.GetLoggedInUser(Request);
            return View(user);
        }


        public IActionResult RequestBlood()
        {

            List<BloodTypeDto> bloodList = _bloodTypeManager.GetAll();
            List<CityDTO> cityList = _cityManager.GetAll();
            var tuple = new Tuple<List<BloodTypeDto>, List<CityDTO>>(bloodList, cityList);
            return View(tuple);
        }


        public IActionResult BecomeDonor()
        {
            var loggedInUser = UserManagement<UserDTO>.GetLoggedInUser(Request);
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        public IActionResult UpdateInformation()
        {
            List<BloodTypeDto> bloodList = _bloodTypeManager.GetAll();
            List<CityDTO> cityList = _cityManager.GetAll();
            UserDTO loggedUser = _userManager.Find(2);
            var tuple = new Tuple<List<BloodTypeDto>, List<CityDTO>, UserDTO>(bloodList, cityList, loggedUser);
            return View(tuple);
        }

        [HttpPost]
        public IActionResult UpdatedUserInfo(string firstname, string lastname, string phone, DateTime birthDate, int city, int gender, int bloodType)
        {
            var DOB = birthDate.Date;
            var newUpdateUserEntity = _userManager.UpdatedUserEntity(firstname, lastname, phone, DOB, city, gender, bloodType);
            _userManager.Update(newUpdateUserEntity);

            return View();
        }


        [HttpPost]
        public IActionResult AvailableDonors(int BloodType, int city, string HospitalName)
        {
            var booldId = BloodType;
            var cityId = city;
            var centerName = HospitalName;
            var bloodRequestentity = _bloodRequest.requestEntity(booldId, cityId, centerName);
            _bloodRequest.Add(bloodRequestentity);

            List<UserDTO> objList = _userManager.FindDonorByCompatibleBloodTypeAndCity(booldId, city);
            return View(objList);

        }

        public IActionResult BloodRequests()
        {
            var user = GetLoggedInUser();
            if (user.Role != Role.Donor)
            {
                return GoToHomePage();
            }
            List<BloodRequestDto> requestList = _bloodRequest.FindRequestByCompatibleBloodTypeAndCity(user.BloodType.ID, user.City.ID);
            return View(requestList);
        }


        [HttpPost]
        public IActionResult UpdatePasswordAction(string password)
        {
            var newPass = _userManager.ChangePasswordEntity(password);
            _userManager.UpdatePassword(newPass);
            return RedirectToAction("ChangePassword", "Logged", "1");
        }

        public IActionResult BecomeDonorAction()
        {
            var loggedInUser = UserManagement<UserDTO>.GetLoggedInUser(Request);
            if (loggedInUser == null) return RedirectToAction("index", "Home");
            if (loggedInUser.Role == Role.Donor) return RedirectToAction("index", "Home");

            _userManager.MakeUserDonor(loggedInUser);
            return View();
        }

    }
}