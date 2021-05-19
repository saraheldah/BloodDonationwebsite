using System.Collections.Generic;
using BloodDonation.Business.DTO;

namespace BloodDonation.Business.Managers
{
    public interface IUserManager
    {
        void LogIn();
        
        List<UserDTO> GetAll();

        List<UserDTO> FindDonorByCompatibleBloodTypeAndCity(int bloodTypeId, int cityId);
        
    }
}