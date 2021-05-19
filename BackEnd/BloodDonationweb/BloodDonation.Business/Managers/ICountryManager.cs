using System.Collections.Generic;
using BloodDonation.Business.DTO;

namespace BloodDonation.Business.Managers
{
    public interface ICountryManager
    {
        List<CountryDTO> GetAll();
    }
}