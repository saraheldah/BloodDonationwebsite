using System;
using System.Collections.Generic;
using BloodDonation.Business.DTO;
using BloodDonation.Common;
using BloodDonation.DataAccess.Entities;

namespace BloodDonation.Business.Managers
{
    public interface IUserManager
    {

        List<UserDTO> GetAll();
        UserDTO GetUserByEmailAndPassword(string email, string apssword);

        List<UserDTO> FindDonorByCompatibleBloodTypeAndCity(int bloodTypeId, int cityId);
        void Add(User newUser);

        void Update(User updatedUser);

        void UpdatePassword(User updatedPassword);

        User ChangePasswordEntity(string Password, int id);

        User UserEntity(string email, string password, string fname, string lname, string phone, DateTime birthDate, int city, int gender, int bloodType,Role role);

        User UpdatedUserEntity(string firstname, string lastname, string phone, DateTime birthDate, int city, int gender, int bloodType,int Id);

        void DeleteUser(int id);
        void MakeUserDonor(UserDTO user);
        UserDTO Find(int id);
        UserDTO GetByEmail(string email);
    }
}