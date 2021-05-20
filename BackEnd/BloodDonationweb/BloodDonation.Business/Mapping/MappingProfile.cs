using AutoMapper;
using BloodDonation.Business.DTO;
using BloodDonation.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using BloodDonation.Business.Managers;

namespace BloodDonation.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BloodType, BloodTypeDto>(); // means you want to map from User to UserDTO
            CreateMap<BloodTypeDto, BloodType>(); // means you want to map from User to UserDTO
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Fname,
                    opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Lname,
                    opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.DOB,
                    opt => opt.MapFrom(src => src.DateOfBirth));
            
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.Fname))
                .ForMember(dest => dest.LastName,
                    opt => opt.MapFrom(src => src.Lname))
                .ForMember(dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src => src.DOB));

            CreateMap<City, CityDTO>();
            CreateMap<CityDTO, City>();
            
            CreateMap<Country, CountryDTO>();
            CreateMap<CountryDTO, Country>();
            
            
            CreateMap<BloodRequestDto, BloodTypeDto>();
            CreateMap<BloodTypeDto, BloodRequestDto>();
            
            
        }
    }
}
