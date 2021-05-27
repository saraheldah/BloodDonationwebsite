using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using BloodDonation.Business.DTO;
using BloodDonation.Business.Managers;
using BloodDonation.DataAccess;
using BloodDonation.DataAccess.Repositories;
using BloodDonationweb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User = BloodDonation.DataAccess.Entities.User;



namespace BloodDonationweb.Controllers
{
    public class LoggedController : Controller
    {

        private readonly IUserManager _userManager;
        private readonly IBloodTypeManager _bloodTypeManager;
        private readonly ICityManager _cityManager;
        private readonly IBloodRequestManager _bloodRequest;
        
        

        public LoggedController(IUserManager userManager , IBloodTypeManager bloodTypeManager,ICityManager cityManager,IBloodRequestManager bloodRequestManager)
        {
            _userManager = userManager;
            _bloodTypeManager = bloodTypeManager;
            _cityManager = cityManager;
            _bloodRequest = bloodRequestManager;
        }
        // GET
       
        public IActionResult Index()
        {
            
         //   var newUserEntity = _userManager.userEntity( email, password, fname, lname, phone, birthDate, city, gender, bloodType);
         //   _userManager.Add(newUserEntity);
            
            // the following user is just a dummy user and it should be logged in user we should change it in the future 
            
            var userDto = new UserDTO() {IsDonor = false};
            return View(userDto);
        }

        public IActionResult NewUser(string email, string password, string fname, string lname, string phone,
            DateTime birthDate, int city, int gender, int bloodType)
        {
            var newUserEntity = _userManager.userEntity( email, password, fname, lname, phone, birthDate, city, gender, bloodType);
            _userManager.Add(newUserEntity);
            return View();
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
            var tuple = new Tuple<List<BloodTypeDto>, List<CityDTO>,UserDTO>(bloodList, cityList,loggedUser);
            return View(tuple);
        }

        [HttpPost]
        public IActionResult UpdatedUserInfo(string firstname,string lastname,string phone,DateTime birthDate,int city,int gender,int bloodType)
        {
            var DOB = birthDate.Date;
            var newUpdateUserEntity = _userManager.updatedUserEntity( firstname, lastname, phone, DOB, city, gender, bloodType);
            _userManager.Update(newUpdateUserEntity);
            
            return View();
        }

      
        [HttpPost]
        public IActionResult AvailableDonors(int BloodType,int city,string HospitalName)
        {
            var booldId = BloodType;
            var cityId = city;
            var centerName = HospitalName;
            var bloodRequestentity = _bloodRequest.requestEntity(booldId, cityId,centerName);
            _bloodRequest.Add(bloodRequestentity);
            
             List<UserDTO> objList = _userManager.FindDonorByCompatibleBloodTypeAndCity(booldId, city);
            return View(objList);

        }

        public IActionResult BloodRequests()
        {
            List<BloodRequestDto> requestList = _bloodRequest.FindRequestByCompatibleBloodTypeAndCity(7, 7);
            return View(requestList);
        }


        [HttpPost]
        public IActionResult UpdatePasswordAction(string password)
        {
            var newPass = _userManager.changePasswordEntity(password);
            _userManager.UpdatePassword(newPass);
            return RedirectToAction("ChangePassword", "Logged", "1");
        }

        public IActionResult BecomeDonorAction()
        {
            var becomeDonor = _userManager.becomeDonorEntity();
            _userManager.BecomeDonor(becomeDonor);
            return View();
        }
       
    }
}