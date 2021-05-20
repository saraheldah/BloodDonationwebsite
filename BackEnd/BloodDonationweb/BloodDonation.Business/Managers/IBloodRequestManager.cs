using BloodDonation.Business.DTO;
using System.Collections.Generic;
using BloodDonation.DataAccess.Entities;

namespace BloodDonation.Business.Managers
{
    public interface IBloodRequestManager
    {
        List<BloodRequestDto> GetAll();
        void Add(BloodRequest bloodRequest);

        BloodRequest requestEntity(int BloodType, int city);
        
        List<BloodRequestDto> FindRequestByCompatibleBloodTypeAndCity(int bloodTypeId, int cityId);
    }
}