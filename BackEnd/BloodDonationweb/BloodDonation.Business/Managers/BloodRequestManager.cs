using AutoMapper;
using BloodDonation.Business.DTO;
using BloodDonation.DataAccess;
using System.Collections.Generic;
using System.Linq;
using BloodDonation.DataAccess.Entities;

namespace BloodDonation.Business.Managers
{
    public class BloodRequestManager : BaseManager,IBloodRequestManager
    {
        public BloodRequestManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
          public List<BloodRequestDto> GetAll()
        {
            var requestEntityList = _unitOfWork.BloodRequestRepository.All().ToList();
           
            var requestDtoList = _mapper.Map<List<BloodRequestDto>>(requestEntityList);

            // _unitOfWork.UserRepository.Add(new User());
            // _unitOfWork.Commit();
            
         
            return requestDtoList;
        }

          public void Add(BloodRequest bloodRequest)
          {
              _unitOfWork.BloodRequestRepository.Add(bloodRequest);
               _unitOfWork.Commit();
          }
          


          public BloodRequest requestEntity(int BloodType,int city,string HospitalName)
          {
              BloodRequest bloodRequest = new BloodRequest();
              bloodRequest.BloodTypeID = BloodType;
              bloodRequest.CityId = city;
              bloodRequest.CenterName = HospitalName;
              return bloodRequest;
          }
          
          public List<BloodRequestDto> FindRequestByCompatibleBloodTypeAndCity(int bloodTypeId, int cityId )
          {
             
              var requestEntityList = _unitOfWork.BloodRequestRepository.FindRequestByCompatibleBloodTypeAndCity(bloodTypeId, cityId).ToList();
              var cities = _unitOfWork.CityRepository.All().ToList();
              var users =  _unitOfWork.UserRepository.All().ToList();
              var requestDtoList = _mapper.Map<List<BloodRequestDto>>(requestEntityList);
              
              foreach (var requestDto in requestDtoList)
              {
                  var relatedRequestEntity = requestEntityList.FirstOrDefault(x => x.ID == requestDto.ID);
                  var relatedCity = cities.FirstOrDefault(x=> x.ID == relatedRequestEntity.CityId);
                  var relatedUser = users.FirstOrDefault(x => x.Id == relatedRequestEntity.UserID);
                  requestDto.City = _mapper.Map<CityDTO>(relatedCity);
                  requestDto.UserInfo = _mapper.Map<UserDTO>(relatedUser);

              }
              return requestDtoList;
              
              // var bloodRequestEntityList = _unitOfWork.UserRepository.FindDonorByCompatibleBloodTypeAndCity(bloodTypeId, cityId).ToList();
              // var countries = _unitOfWork.CountryRepository.All().ToList();
              // var cities = _unitOfWork.CityRepository.All().ToList();
              // var userDtoList = _mapper.Map<List<UserDTO>>(DonorEntityList);
              //
              // foreach (var donorUserDto in userDtoList)
              // {
              //     var relatedUserEntity = DonorEntityList.FirstOrDefault(x => x.Id == donorUserDto.Id);
              //     var relatedCity = cities.FirstOrDefault(x=> x.ID == relatedUserEntity.CityId);
              //     var relatedCountry = countries.FirstOrDefault(x=> x.ID == relatedUserEntity.CountryId);
              //     donorUserDto.City = _mapper.Map<CityDTO>(relatedCity);
              //     donorUserDto.Country = _mapper.Map<CountryDTO>(relatedCountry);
              // }
              // return userDtoList;


          }
    }
}