using BloodDonation.Business.DTO;
using System.Collections.Generic;


namespace BloodDonation.Business.Managers
{
    public interface ICityManager
    {
        List<CityDTO> GetAll();
    }
}