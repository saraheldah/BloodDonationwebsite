using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BloodDonation.Business.DTO
{
    public class CountryDTO
    {
        public int ID { get; set; }
        
        public string CountryName { get; set; }
        
        public List<CityDTO> Cities;
    }
}