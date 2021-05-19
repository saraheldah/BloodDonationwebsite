using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using BloodDonation.Business.DTO;
using BloodDonation.Business.Managers;
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
        
        
        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registration(int data )
        {
            return View();
        }

   

        public IActionResult ForgotPassword()
        {
            return View();
        }


    
    }
}