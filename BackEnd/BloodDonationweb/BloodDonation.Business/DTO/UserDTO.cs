using System;
using BloodDonation.Common;

namespace BloodDonation.Business.DTO
{


    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }

        public Role Role { get; set; }
        
        public string Password { get; set; }
        public string Phone { get; set; }

        private bool _isDonor;
        public bool IsDonor { get { return Role == Role.Donor ? true : false; } set { _isDonor = value; } }

        public BloodTypeDto BloodType { get; set; }

        public CountryDTO Country { get; set; }

        public CityDTO City { get; set; }
        
    }
}