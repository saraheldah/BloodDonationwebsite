using System;
using System.Collections.Generic;
using BloodDonation.Business.DTO;
using BloodDonation.DataAccess.Entities;

namespace BloodDonation.Business.Managers
{
    public interface IUserManager
    {
        void LogIn();
        
        List<UserDTO> GetAll();

        List<UserDTO> FindDonorByCompatibleBloodTypeAndCity(int bloodTypeId, int cityId);
        void Add(User newUser);

        void Update(User updatedUser);

        void UpdatePassword(User updatedPassword);

        User changePasswordEntity(string Password);

         User userEntity(string email,string password,string fname,string lname,string phone,DateTime birthDate,int city,int gender,int bloodType);

         User updatedUserEntity(string firstname, string lastname, string phone, DateTime birthDate, int city, int gender, int bloodType);

    }
}