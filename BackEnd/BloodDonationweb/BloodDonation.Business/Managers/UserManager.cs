

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BloodDonation.Business.DTO;
using BloodDonation.Common;
using BloodDonation.DataAccess;
using BloodDonation.DataAccess.Entities;

namespace BloodDonation.Business.Managers
{
    public class UserManager : BaseManager, IUserManager
    {
        public UserManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public void LogIn()
        {
            var users = _unitOfWork.UserRepository.All();
        }

        public List<UserDTO> GetAll()
        {
            var userEntityList = _unitOfWork.UserRepository.All().ToList();
            var countries = _unitOfWork.CountryRepository.All().ToList();
            var cities = _unitOfWork.CityRepository.All().ToList();
            var userDtoList = _mapper.Map<List<UserDTO>>(userEntityList);

            // _unitOfWork.UserRepository.Add(new User());
            // _unitOfWork.Commit();
            
            foreach (var userDto in userDtoList)
            {
                var relatedUserEntity = userEntityList.FirstOrDefault(x => x.Id == userDto.Id);
                var relatedCity = cities.FirstOrDefault(x=> x.ID == relatedUserEntity.CityId);
                var relatedCountry = countries.FirstOrDefault(x=> x.ID == relatedUserEntity.CountryId);
                userDto.City = _mapper.Map<CityDTO>(relatedCity);
                userDto.Country = _mapper.Map<CountryDTO>(relatedCountry);
            }
            return userDtoList;
        }

        public List<UserDTO> FindDonorByCompatibleBloodTypeAndCity(int bloodTypeId, int cityId )
        {
            // int bloodTypeId;
            // int cityId;
            var DonorEntityList = _unitOfWork.UserRepository.FindDonorByCompatibleBloodTypeAndCity(bloodTypeId, cityId).ToList();
            var countries = _unitOfWork.CountryRepository.All().ToList();
            var cities = _unitOfWork.CityRepository.All().ToList();
            var userDtoList = _mapper.Map<List<UserDTO>>(DonorEntityList);
            
            foreach (var donorUserDto in userDtoList)
            {
                var relatedUserEntity = DonorEntityList.FirstOrDefault(x => x.Id == donorUserDto.Id);
                var relatedCity = cities.FirstOrDefault(x=> x.ID == relatedUserEntity.CityId);
                var relatedCountry = countries.FirstOrDefault(x=> x.ID == relatedUserEntity.CountryId);
                donorUserDto.City = _mapper.Map<CityDTO>(relatedCity);
                donorUserDto.Country = _mapper.Map<CountryDTO>(relatedCountry);
            }
            return userDtoList;


        }

        public void Add(User newUser)
        {
            _unitOfWork.UserRepository.Add(newUser);
            _unitOfWork.Commit();
            
        }
        public User userEntity(string email,string password,string fname,string lname,string phone,DateTime birthDate,int city,int gender,int bloodType)
        {
            User newUser = new User();
            newUser.Email = email;
            newUser.Password = password;
            newUser.Fname = fname;
            newUser.Lname = lname;
            newUser.Phone = phone;
            newUser.DOB = birthDate.Date;
            newUser.CityId = city;
            newUser.Gender = (Gender) gender;
            newUser.BloodTypeID = bloodType;
            return newUser;
        }
    }
}
