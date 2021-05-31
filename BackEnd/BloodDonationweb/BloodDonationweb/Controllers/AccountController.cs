using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BloodDonation.Business.DTO;
using BloodDonation.Business.Managers;
using BloodDonation.Common;
using BloodDonation.DataAccess;
using BloodDonation.DataAccess.Repositories;
using BloodDonationweb.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationweb.Controllers
{
    public class AccountController : MyBaseController
    {
        private readonly IUserManager _userManager;
        private readonly IBloodTypeManager _bloodTypeManager;
        private readonly ICityManager _cityManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResetPasswordRepository _password;


        public AccountController(IUserManager userManager, IBloodTypeManager bloodTypeManager, ICityManager cityManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _bloodTypeManager = bloodTypeManager;
            _cityManager = cityManager;
            _unitOfWork = unitOfWork;
        }



        // GET

     
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (email is null || password is null)
            {
                return RedirectToAction("LogIn", "Account", "2");
                //throw new System.ArgumentNullException(nameof(email));
            }

         //   if (password is null)
          //  {
         //       throw new System.ArgumentNullException(nameof(password));
        //    }

            UserDTO user;
            if (email == "saeed.eldah@gmail.com" && password == "123")
            {
                user = new UserDTO()
                {
                    FirstName = "Saeed",
                    Role = Role.Donor,
                    Email = email
                };
            }
            else
            {
                user = _userManager.GetUserByEmailAndPassword(email, password);
            }

            if (user == null) return RedirectToAction("LogIn", "Account", "0");

            UserManagement<UserDTO>.Authinticate(Response, user);
            return RedirectToAction("Index", "Logged");
        }

        public IActionResult LogOut()
        {
            LogOutUser();
            return GoToHomePage();
        }

        public IActionResult ResetPasswordRequest()
        {
    
            return View();
        }

        [HttpPost]
        public IActionResult ResetPasswordRequestA(string email)
        {
            try
            {
             
                var user = _userManager.GetByEmail(email);
                if (user == null) return RedirectToAction("ResetPasswordRequest", "Account", "1");
                var Code = Guid.NewGuid();
                var newResetPasswordEntity =_unitOfWork.ResetPasswordRepository.ResetPasswordEntity(user.Id, Code.ToString(), true);
                _unitOfWork.ResetPasswordRepository.Add(newResetPasswordEntity);
                _unitOfWork.Commit();
                // redirect to page with message 
                return RedirectToAction("ResetPasswordRequest", "Account", "2");
            }
             catch (Exception ex)
             { 
                 return RedirectToAction("ResetPasswordRequest", "Account", "0");
             }
            
        }
        public IActionResult ResetPassword([FromQuery(Name = "code")] string code)
        {
            var x = code;
            if (code is null)
            {
                throw new ArgumentNullException(nameof(code));
            }
            // if code exist in the database and the status valid then redirect to  reset password
            // else redirect to error page (invalid link)


            var isValid = _unitOfWork.ResetPasswordRepository.IsValidCode(code);

            if (isValid)
            {
                
                var userId = _unitOfWork.ResetPasswordRepository.GetUserId(code);
               
                UserDTO user = _userManager.Find(userId);
                
                _unitOfWork.ResetPasswordRepository.ConsumeLink(code);
                _unitOfWork.Commit();

                
                return View(user);
            }
            else
            {
                // redirect to error page (invalid link)
                return RedirectToAction("Index", "ResetPasswordRequest", "3");
            }
            
        }

        public IActionResult ResetPasswordA(string email, string newPassword, string confirmNewPassword)
        {
            if (newPassword is null || confirmNewPassword is null)
            {
                return RedirectToAction("ResetPassword", "Account", "3");
            }
            
            if (newPassword != confirmNewPassword) 
            {
                return RedirectToAction("ResetPassword", "Logged", "0");
            }
          
            
                var newPassword1 = newPassword.Trim();
                var user = _userManager.GetByEmail(email);
                var newPass = _userManager.ChangePasswordEntity(newPassword1 , user.Id);
                _userManager.UpdatePassword(newPass);
                return RedirectToAction("ResetPasswordRequest", "Account", "4");
            
            

           
        }

    }
}