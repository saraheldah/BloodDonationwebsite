using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BloodDonation.Business.DTO
{
    public class CityDTO
    {
        public int ID { get; set; }
        
        public string CityName { get; set; }
        public int CountryId { get; set; }
        
        
    }
}