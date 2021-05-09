using System;

namespace BloodDonationweb.Models
{
    public class BloodRequest
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public int Status { get; set; }
        public Object ProofDocument { get; set; }
        public int BloodTypeId { get; set; }
        public int UserId { get; set; }
    }
}