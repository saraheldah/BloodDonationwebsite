using System;

namespace BloodDonationweb.Models
{
    public class DonationHistory
    {
        public int Id { get; set; }
        public DateTime DonationDate { get; set; }
        public int DonorId { get; set; }
        public int RecipientId{ get; set; }
    }
}