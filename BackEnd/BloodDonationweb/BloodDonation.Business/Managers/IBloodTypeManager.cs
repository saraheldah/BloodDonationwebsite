using BloodDonation.Business.DTO;
using System.Collections.Generic;

namespace BloodDonation.Business.Managers
{
    public interface IBloodTypeManager
    {
        List<BloodTypeDto> GetAll();
    }
}