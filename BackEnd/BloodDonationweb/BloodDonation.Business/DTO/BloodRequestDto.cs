using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BloodDonation.Business.DTO
{
    public class BloodRequestDto
    {
        public int ID { get; set; }
        
        public DateTime RequestDate { get; set; }
        
        public int Status { get; set; }
        
        public object ProofDocument { get; set; }
        
        public int BloodTypeID { get; set; }
        
        public int UserID { get; set; }
        
        public CityDTO City { get; set; }
    }
}