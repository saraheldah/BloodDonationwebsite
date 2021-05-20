using System;
using System.Collections.Generic;
using System.Linq;
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
        
        

        public LoggedController(IUserManager userManager , IBloodTypeManager bloodTypeManager,ICityManager cityManager)
        {
            _userManager = userManager;
            _bloodTypeManager = bloodTypeManager;
            _cityManager = cityManager;
        }
        // GET
        public IActionResult Index()
        {
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
            return View();
        }

      
        [HttpPost]
        public IActionResult AvailableDonors(int BloodType,int city)
        {
            var bookId = BloodType;
            var cityId = city;
// fill request DTO (compose)
// in the manager map DTO to Entity (BloodRequest)
// use repo.Add()
// use unitofwork.Commit()
             List<UserDTO> objList = _userManager.FindDonorByCompatibleBloodTypeAndCity(bookId, city);
            return View(objList);

            // List<UserDTO> objList = _userManager.GetAll();
            // return View(objList);

        }

        // public JsonResult Test(int id)
        // {
        //     var result = id;
        //     var data = new {status = "ok", result = result};
        //     return Json(data);
        // }
        
        

       
    }
}