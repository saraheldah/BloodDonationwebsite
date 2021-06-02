using System;

namespace BloodDonation.DataAccess.Entities
{
    public class DonationHistory
    {
        public int ID { get; set; }
        
        public DateTime DonationDate { get; set; }
        
        public int DonorID { get; set; }
        
        public int RecipientID{ get; set; }
    }
}