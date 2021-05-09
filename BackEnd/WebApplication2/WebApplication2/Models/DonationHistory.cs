using System;

namespace WebApplication2.Models
{
    public class DonationHistory
    {
        public int Id { get; set; }
        public DateTime DonationDate { get; set; }
        public int DonorId { get; set; }
        public int RecioientId{ get; set; }
    }
}