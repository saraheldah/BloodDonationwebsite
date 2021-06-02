using System;

namespace BloodDonation.DataAccess.Entities
{
    public class City
    {
        public int ID { get; set; }
        public string CityName { get; set; }
        public int CountryId { get; set; }
    }
}