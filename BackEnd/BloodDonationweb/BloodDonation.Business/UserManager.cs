using System;
using System.Collections.Generic;
using System.Text;
using BloodDonation.DataAccess;

namespace BloodDonation.Business
{
    public class UserManager : IUserManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void LogIn()
        {
            var users = _unitOfWork.UserRepository.All();
        }
    }
}
