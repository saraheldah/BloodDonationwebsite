

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
                var relatedCity = cities.FirstOrDefault(x => x.ID == relatedUserEntity.CityId);
                var relatedCountry = countries.FirstOrDefault(x => x.ID == relatedUserEntity.CountryId);
                userDto.City = _mapper.Map<CityDTO>(relatedCity);
                userDto.Country = _mapper.Map<CountryDTO>(relatedCountry);
            }
            return userDtoList;
        }

        public List<UserDTO> FindDonorByCompatibleBloodTypeAndCity(int bloodTypeId, int cityId)
        {

            var DonorEntityList = _unitOfWork.UserRepository.FindDonorByCompatibleBloodTypeAndCity(bloodTypeId, cityId).ToList();
            var countries = _unitOfWork.CountryRepository.All().ToList();
            var cities = _unitOfWork.CityRepository.All().ToList();
            var userDtoList = _mapper.Map<List<UserDTO>>(DonorEntityList);

            foreach (var donorUserDto in userDtoList)
            {
                var relatedUserEntity = DonorEntityList.FirstOrDefault(x => x.Id == donorUserDto.Id);
                var relatedCity = cities.FirstOrDefault(x => x.ID == relatedUserEntity.CityId);
                var relatedCountry = countries.FirstOrDefault(x => x.ID == relatedUserEntity.CountryId);
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
        public User UserEntity(string email, string password, string fname, string lname, string phone, DateTime birthDate, int city, int gender, int bloodType,Role role)
        {
            User newUser = new User
            {
                Email = email,
                Password = password,
                Fname = fname,
                Lname = lname,
                Phone = phone,
                DOB = birthDate.Date,
                Role = role,
                CityId = city,
                Gender = (Gender)gender,
                BloodTypeID = bloodType
            };
            return newUser;
        }


        public UserDTO GetByEmail(string email)
        {
            if (email is null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            var user = _unitOfWork.UserRepository.GetByEmail(email);
            if (user == null) return null;
            return GetUserDtoWithRelatedEntities(user);
        }
        public void Update(User updatedUser)
        {
            _unitOfWork.UserRepository.Update(updatedUser);
            _unitOfWork.Commit();
        }

        public User UpdatedUserEntity(string firstname, string lastname, string phone, DateTime birthDate, int city, int gender, int bloodType,int Id)
        {
            User updatedUser = new User
            {
                Fname = firstname,
                Lname = lastname,
                Phone = phone,
                DOB = birthDate.Date,
                CityId = city,
                Gender = (Gender)gender,
                BloodTypeID = bloodType,
                Id = Id
            };
            return updatedUser;
        }

        public void UpdatePassword(User updatedPassword)
        {
            _unitOfWork.UserRepository.UpdatePassword(updatedPassword);
            _unitOfWork.Commit();
        }
        public void DeleteUser(int id)
        {
            _unitOfWork.UserRepository.DeleteUser(id);
            _unitOfWork.Commit();
        }


        public User ChangePasswordEntity(string Password,int id)
        {
            User updatedPassword = new User();
            updatedPassword.Password = Password;
            updatedPassword.Id = id;
            return updatedPassword;
        }


        public void MakeUserDonor(UserDTO user)
        {
            _unitOfWork.UserRepository.MakeItDonor(user.Id);
            _unitOfWork.Commit();
        }
        private UserDTO GetUserDtoWithRelatedEntities(User userEntity)
        {
            var userDTO = _mapper.Map<UserDTO>(userEntity);
            if (userEntity != null)
            {
                var city = _unitOfWork.CityRepository.Find(userEntity.CityId);
                var bloodtype = _unitOfWork.BloodTypeRepository.Find(userEntity.BloodTypeID);
                userDTO.City = _mapper.Map<CityDTO>(city);
                userDTO.BloodType = _mapper.Map<BloodTypeDto>(bloodtype);
            }

            return userDTO;
        }
        public UserDTO Find(int id)
        {
            var userEntity = _unitOfWork.UserRepository.Find(id);
            if (userEntity == null) throw new Exception("user un available");
            return GetUserDtoWithRelatedEntities(userEntity);
        }

        public UserDTO GetUserByEmailAndPassword(string email, string password)
        {
            var userEntity = _unitOfWork.UserRepository.GetByEmailAndPassword(email, password);
        //    if (userEntity == null) throw new Exception("user un available");
            return GetUserDtoWithRelatedEntities(userEntity);
        }
    }
}
