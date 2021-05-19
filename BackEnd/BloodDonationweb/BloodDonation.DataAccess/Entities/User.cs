
using System;
using BloodDonation.Common;

namespace BloodDonation.DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public DateTime  DOB { get; set; }
        
        public Gender Gender { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public string Phone { get; set; }

        public bool IsDonor { get; set; }
        
        public int BloodTypeID { get; set; }
        
        public int CountryId { get; set; }
        
        public int CityId { get; set; }
    }
}
