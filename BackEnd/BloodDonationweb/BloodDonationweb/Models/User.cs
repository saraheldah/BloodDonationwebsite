using System;

namespace BloodDonationweb.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IsDonor { get; set; }
        public int BloodTypeId { get; set; }
        public string Country { get; set; }
    }
}