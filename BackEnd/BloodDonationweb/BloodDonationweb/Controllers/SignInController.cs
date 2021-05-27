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


        public SignInController(IUserManager userManager, IBloodTypeManager bloodTypeManager, ICityManager cityManager)
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

        public IActionResult Authenticate(string email, string password)
        {
            if (email == "saeed.eldah@gmail.com" && password == "123")
            {
                return RedirectToAction("Index", "Logged");
            }
            return RedirectToAction("Index", "SignIn", "0");
        }

    }
}