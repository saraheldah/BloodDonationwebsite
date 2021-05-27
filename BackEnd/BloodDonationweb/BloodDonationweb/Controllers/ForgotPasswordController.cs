using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodDonationweb.Controllers
{
    public class ForgotPasswordController : MyBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
  
    }
}
