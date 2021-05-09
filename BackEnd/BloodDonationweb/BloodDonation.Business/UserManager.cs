using System;
using System.Collections.Generic;
using System.Text;
using BloodDonation.DataAccess;

namespace BloodDonation.Business
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepo;
        public UserManager(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        public void LogIn()
        {
            _userRepo.GetUser(5);
        }
    }
}
