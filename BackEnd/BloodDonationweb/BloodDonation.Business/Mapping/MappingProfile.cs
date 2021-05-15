using AutoMapper;
using BloodDonation.Business.DTO;
using BloodDonation.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodDonation.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BloodType, BloodTypeDto>(); // means you want to map from User to UserDTO
            CreateMap<BloodTypeDto, BloodType>(); // means you want to map from User to UserDTO
        }
    }
}
