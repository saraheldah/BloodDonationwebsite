using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using BloodDonation.Business.DTO;
using BloodDonation.Business.Managers;
using BloodDonation.Common;
using BloodDonation.DataAccess;
using BloodDonation.DataAccess.Repositories;
using BloodDonationweb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using User = BloodDonation.DataAccess.Entities.User;

namespace BloodDonationweb.Controllers
{
    public class SignInController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IBloodTypeManager _bloodTypeManager;
        private readonly ICityManager _cityManager;

        
        public SignInController(IUserManager userManager , IBloodTypeManager bloodTypeManager,ICityManager cityManager)
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

        

      
     
        public IActionResult Registration( string email,string password,string fname,string lname,string phone,DateTime birthDate,int city,int gender,int BloodType)
        {
            List<BloodTypeDto> bloodList = _bloodTypeManager.GetAll();
            List<CityDTO> cityList = _cityManager.GetAll();
            var tuple = new Tuple<List<BloodTypeDto>, List<CityDTO>>(bloodList, cityList);
           


           
            
            
            return View(tuple);
        }

     
 

   

        public IActionResult ForgotPassword()
        {
            return View();
        }


    
    }
}