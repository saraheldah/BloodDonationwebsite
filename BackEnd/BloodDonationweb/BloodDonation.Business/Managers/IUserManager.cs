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
        void Add(User entity);

    }
}