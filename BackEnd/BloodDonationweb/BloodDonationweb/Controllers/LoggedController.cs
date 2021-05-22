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
        [HttpPost]
        public IActionResult Index(string email,string password,string fname,string lname,string phone,DateTime birthDate,int city,int gender,int bloodType)
        {
            
            var newUserEntity = _userManager.userEntity( email, password, fname, lname, phone, birthDate, city, gender, bloodType);
            _userManager.Add(newUserEntity);
            return View();
        }

        public IActionResult LoggedDonor()
        {
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
            var tuple = new Tuple<List<BloodTypeDto>, List<CityDTO>>(bloodList, cityList);
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
            
            
// fill request DTO (compose)
// in the manager map DTO to Entity (BloodRequest)
// use repo.Add()
// use unitofwork.Commit()
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
            return View();
        }

        // public JsonResult Test(int id)
        // {
        //     var result = id;
        //     var data = new {status = "ok", result = result};
        //     return Json(data);
        // }
        
        

       
    }
}