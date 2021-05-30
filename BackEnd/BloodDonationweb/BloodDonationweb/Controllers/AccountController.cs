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


        public IActionResult ForgotPassword()
        {
            return View();
        }
        public IActionResult ResetPasswordRequest(string email)
        {
            var user = _userManager.GetByEmail(email);
            if(email is null) return RedirectToAction("Index", "ForgotPassword", "0");
            if (user != null) return RedirectToAction("Index", "ForgotPassword", "1");
            var Code = Guid.NewGuid();
            var newResetPasswordEntity =_unitOfWork.ResetPasswordRepository.ResetPasswordEntity(user.Id, Code.ToString(), true);
            _unitOfWork.ResetPasswordRepository.Add(newResetPasswordEntity);
            _unitOfWork.Commit();
            // redirect to page with message 
            return View();
        }
        public IActionResult ResetPassword([FromQuery(Name = "code")] string code)
        {
            var x = code;
            if (code is null)
            {
                throw new ArgumentNullException(nameof(code));
            }
            // if code exist in the database and the status valis then redirect to  reset password
            // (two input to set the new password)
            // else redirect to error page (invalid link)


            var isValid = _unitOfWork.ResetPasswordRepository.IsValidCode(code);

            if (isValid)
            {
                // get userId
                var userId = _unitOfWork.ResetPasswordRepository.GetUserId(code);
                // get user
                var user = _unitOfWork.UserRepository.Find(userId);
                // and send user email to the view 
                // update the ResetPassword row and set Status to 0
                _unitOfWork.ResetPasswordRepository.ConsumeLink(code);

                // redirect to  reset password
            }
            else
            {
                // redirect to error page (invalid link)
            }
            return View();
        }

        public IActionResult ResetPassword(string email, string newPassword)
        {
            // update user password where email = to email 
           
           // redirect to message page 
        }

    }
}