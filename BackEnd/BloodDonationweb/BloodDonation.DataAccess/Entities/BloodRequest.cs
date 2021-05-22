using System;

namespace BloodDonation.DataAccess.Entities
{
    public class BloodRequest
    {
        public int ID { get; set; }
        public DateTime RequestDate { get; set; }
        public int Status { get; set; }
        public string ProofDocumentName { get; set; }
        public int BloodTypeID { get; set; }
        
        public int UserID { get; set; }
        
        public int CityId { get; set; }
        
        public string CenterName { get; set; }
    }
}