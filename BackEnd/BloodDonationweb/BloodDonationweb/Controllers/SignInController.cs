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
           
            // UserDTO user = new UserDTO();
            // user.Email = email;
            // // user.password = password;
            // user.FirstName = fname;
            // user.LastName = lname;
            // user.Phone = phone;
            // user.DateOfBirth = birthDate.Date;
            // user.City = city;
            // user.Gender = (Gender) gender;
            // user.BloodType = BloodType;

            
            
            // User user = new User();
            // user.Email = email;
            // user.Password = password;
            // user.Fname = fname;
            // user.Lname = lname;
            // user.Phone = phone;
            // user.DOB = birthDate.Date;
            // user.CityId = city;
            // user.Gender = (Gender) gender;
            // user.BloodTypeID = BloodType;

           
            
            
            return View(tuple);
        }

     
        // public IActionResult Registration(string email,string password,string fname,string lname,string phone,DateTime birthDate,int city,int gender,int BloodType)
        // {
        //     UserDTO user = new UserDTO();
        //     
        //     
        //     return View();
        // }

        // public void Create(string email,string password,string fname,string lname,string phone,DateTime birthDate,int city,int gender,int BloodType)
        //  {
        //
        //  }

   

        public IActionResult ForgotPassword()
        {
            return View();
        }


    
    }
}