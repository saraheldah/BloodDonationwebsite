using System;

namespace BloodDonation.Business.DTO
{
    public class BloodRequest
    {
        public int ID { get; set; }
        public DateTime RequestDate { get; set; }
        public int Status { get; set; }
        public object ProofDocument { get; set; }
        public int BloodTypeID { get; set; }
        public int UserID { get; set; }
    }
}