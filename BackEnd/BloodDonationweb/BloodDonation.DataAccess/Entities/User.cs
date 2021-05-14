
using System;

namespace BloodDonation.DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public DateTime  DOB { get; set; }
        public String Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IsDonor { get; set; }
        public int BloodTypeID { get; set; }
        public string Country { get; set; }
    }
}
