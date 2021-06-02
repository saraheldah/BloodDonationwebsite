using System;
using System.Collections.Generic;
using BloodDonation.Business.DTO;
using BloodDonation.Business.Managers;
using BloodDonation.Common;
using BloodDonationweb.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace BloodDonationweb.Controllers
{
    public class LoggedController : MyBaseController
    {

        private readonly IUserManager _userManager;
        private readonly IBloodTypeManager _bloodTypeManager;
        private readonly ICityManager _cityManager;
        private readonly IBloodRequestManager _bloodRequest;



        public LoggedController(IUserManager userManager, IBloodTypeManager bloodTypeManager, ICityManager cityManager, IBloodRequestManager bloodRequestManager)
        {
            _userManager = userManager;
            _bloodTypeManager = bloodTypeManager;
            _cityManager = cityManager;
            _bloodRequest = bloodRequestManager;
        }
        public IActionResult Index()
        {
         //   try
         //   {
                var user = UserManagement<UserDTO>.GetLoggedInUser(Request);
                if (user is null)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View(user);

                //  }
          //  catch (Exception ex)
           // {
                
          //  }
        }
        
        public IActionResult RequestBlood()
        {
            var user = GetLoggedInUser();
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }  
                List<BloodTypeDto> bloodList = _bloodTypeManager.GetAll();
                List<CityDTO> cityList = _cityManager.GetAll();
                var tuple = new Tuple<List<BloodTypeDto>, List<CityDTO>>(bloodList, cityList);
                return View(tuple);

        }
        
        public IActionResult BecomeDonor()
        {
            
            
                var user = GetLoggedInUser();
                if (user is null )
                {
                    return RedirectToAction("Index", "Home");
                }
                if(user.Role == Role.Donor) return RedirectToAction("Index", "Logged");
                return View();  
                
        }

        public IActionResult ChangePassword()
        {
        
            var user = GetLoggedInUser();
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();  
        }

        public IActionResult UpdateInformation()
        {
            var user = GetLoggedInUser();
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }   
            List<BloodTypeDto> bloodList = _bloodTypeManager.GetAll();
                List<CityDTO> cityList = _cityManager.GetAll();
                UserDTO loggedUser = _userManager.Find(user.Id);
                var tuple = new Tuple<List<BloodTypeDto>, List<CityDTO>, UserDTO>(bloodList, cityList, loggedUser);
                return View(tuple);
        }

        [HttpPost]
        public IActionResult UpdatedUserInfo(string firstname, string lastname, string phone, DateTime birthDate, int city, int gender, int bloodType ,int Id)
        {
            var DOB = birthDate.Date;
            var newUpdateUserEntity = _userManager.UpdatedUserEntity(firstname, lastname, phone, DOB, city, gender, bloodType,Id);
            _userManager.Update(newUpdateUserEntity);
            return RedirectToAction("UpdateInformation", "Logged", "1");
        }


        [HttpPost]
        public IActionResult AvailableDonors(int BloodType, int city, string HospitalName)
        {
            try
            {
                var user = GetLoggedInUser();
                var booldId = BloodType;
                var cityId = city;
                var centerName = HospitalName;
                var bloodRequestentity = _bloodRequest.requestEntity(booldId, cityId, centerName,user.Id);
                _bloodRequest.Add(bloodRequestentity);

                List<UserDTO> objList = _userManager.FindDonorByCompatibleBloodTypeAndCity(booldId, city);
                return View(objList);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public IActionResult BloodRequests()
        {
            
                var user = GetLoggedInUser();
                if (user is null)
                {
                    return RedirectToAction("Index", "Home");
                }  
                if (user.Role == Role.User)
                {
                    return RedirectToAction("Index", "Logged");
                }  
                List<BloodRequestDto> requestList = _bloodRequest.FindRequestByCompatibleBloodTypeAndCity(user.BloodType.ID, user.City.ID);
                return View(requestList);
        }


        [HttpPost]
        public IActionResult UpdatePasswordAction(string currentPassword,string newPassword,string confirmNewPassword)
        {
            
            var user = GetLoggedInUser();
            if (currentPassword is null || newPassword is null || confirmNewPassword is null)
            {
                return RedirectToAction("ChangePassword", "Logged", "3");
            }
            
            if (newPassword == confirmNewPassword && user.Password == currentPassword)
            {
                var newPassword1 = newPassword.Trim();
                var newPass = _userManager.ChangePasswordEntity(newPassword1 , user.Id);
                _userManager.UpdatePassword(newPass);
                return RedirectToAction("ChangePassword", "Logged", "1");
            }
            if (newPassword != confirmNewPassword) 
            {
                    return RedirectToAction("ChangePassword", "Logged", "0");
            }
            return RedirectToAction("ChangePassword", "Logged", "2");
            

        }

        public IActionResult BecomeDonorAction(string diabetes,string antibiotic,string COVID,string donate,string vaccination,string tattoo,string piercing,string blood)
        {
            if (diabetes == "Yes" && antibiotic == "Yes" && COVID == "Yes" && donate == "Yes" && vaccination == "Yes" &&
                tattoo == "Yes" && piercing == "Yes" && blood == "Yes")
            {
                var loggedInUser = UserManagement<UserDTO>.GetLoggedInUser(Request);
                if (loggedInUser == null) return RedirectToAction("Index", "Home");
                if (loggedInUser.Role == Role.Donor) return RedirectToAction("Index", "Home");

                _userManager.MakeUserDonor(loggedInUser);
                loggedInUser.Role = Role.Donor;
                LogOutUser();
                AuthenticateUser(loggedInUser);
                return RedirectToAction("Index", "Logged", "1");
            }
            else
            {
                return RedirectToAction("BecomeDonor", "Logged", "1");
            }
        }

        [HttpPost]
        public IActionResult DonateAction(int donate)
        {
            var user = GetLoggedInUser();
            var requestId = donate;
            var StatusEntity = _bloodRequest.StatusEntity(requestId);
            _bloodRequest.UpdateRequestStatus(StatusEntity);
            return RedirectToAction("BloodRequests", "Logged");
        }

        
        
        //Admin 
        public IActionResult SearchUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchUserAction(string email)
        {
            if(email is null) return RedirectToAction("SearchUser", "Logged", "3"); 
            email = email.Trim().ToLower();
            var user = _userManager.GetByEmail(email);
            if (user == null) return RedirectToAction("SearchUser", "Logged", "1"); 
            return View(user);
        }

        
        [HttpPost]
        public IActionResult ManageUser(int update, int delete)
        {
             if (delete != 0)
             {
                  _userManager.DeleteUser(delete);
                  return RedirectToAction("SearchUser", "Logged", "0"); 
             }
           
            List<BloodTypeDto> bloodList = _bloodTypeManager.GetAll();
            List<CityDTO> cityList = _cityManager.GetAll();
            UserDTO managedUser = _userManager.Find(update);
            var tuple = new Tuple<List<BloodTypeDto>, List<CityDTO>, UserDTO>(bloodList, cityList, managedUser);
            return View(tuple);
        }
        
        [HttpPost]
        public IActionResult ManageUserAction(string firstname, string lastname, string phone, DateTime birthDate, int city, int gender, int bloodType,int Id)
        {
            var DOB = birthDate.Date;
            var newUpdateUserEntity = _userManager.UpdatedUserEntity(firstname, lastname, phone, DOB, city, gender, bloodType,Id);
            _userManager.Update(newUpdateUserEntity);

            return RedirectToAction("SearchUser", "Logged", "2");
        }

        public IActionResult UserRequest()
        {
            var user = GetLoggedInUser();
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }  
            List<BloodRequestDto> bloodRequestList = _bloodRequest.FindRequestByUserId(user.Id);
            return View(bloodRequestList);
        }
    }
}