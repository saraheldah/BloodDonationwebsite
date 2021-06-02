using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BloodDonation.Business.DTO
{
    public class BloodTypeDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int RareGrade { get; set; }

        public List<BloodTypeCompatibilityDto> CompatibleTypes;
    }
}
