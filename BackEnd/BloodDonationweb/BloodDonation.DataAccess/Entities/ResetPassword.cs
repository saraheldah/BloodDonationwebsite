using System;

namespace BloodDonation.DataAccess.Entities
{
    public class ResetPassword
    {
        public int ID { get; set; }
        
        public int UserId { get; set; }
        
        public string Code { get; set; }
        
        public DateTime Date{ get; set; }
        
        public bool Status{ get; set; }
    }
}