using System;

namespace BloodDonation.DataAccess.Entities
{
    public class City
    {
        public int ID { get; set; }
        public String CityName { get; set; }
        public int CountryId { get; set; }
    }
}