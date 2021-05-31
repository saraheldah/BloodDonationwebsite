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
          


          public BloodRequest requestEntity(int BloodType,int city,string HospitalName,int UserId)
          {
              BloodRequest bloodRequest = new BloodRequest();
              bloodRequest.BloodTypeID = BloodType;
              bloodRequest.CityId = city;
              bloodRequest.CenterName = HospitalName;
              bloodRequest.UserID = UserId;
              return bloodRequest;
          }


          public BloodRequest StatusEntity(int id)
          {
              BloodRequest statusRequest = new BloodRequest();
              statusRequest.ID = id;
              statusRequest.Status = 1;
              return statusRequest;
          }

          public List<BloodRequestDto> FindRequestByUserId(int id)
          {
              var RequestEntityList = _unitOfWork.BloodRequestRepository.FindRequestByUserId(id).ToList();
              var countries = _unitOfWork.CountryRepository.All().ToList();
              var cities = _unitOfWork.CityRepository.All().ToList();
              var requestDtoList = _mapper.Map<List<BloodRequestDto>>(RequestEntityList);

              foreach (var requestDto in requestDtoList)
              {
                  var relatedUserEntity = requestDtoList.FirstOrDefault(x => x.ID == requestDto.ID);
              }
              return requestDtoList;
          }
          public void UpdateRequestStatus(BloodRequest statusRequest)
          {
              _unitOfWork.BloodRequestRepository.UpdateRequestStatus(statusRequest);
              _unitOfWork.Commit();
          }
          
          public List<BloodRequestDto> FindRequestByCompatibleBloodTypeAndCity(int bloodTypeId, int cityId )
          {
             
              var requestEntityList = _unitOfWork.BloodRequestRepository.FindRequestByCompatibleBloodTypeAndCity(bloodTypeId, cityId).ToList();
              var cities = _unitOfWork.CityRepository.All().ToList();
              var users =  _unitOfWork.UserRepository.All().ToList();
              var bloodTypes = _unitOfWork.BloodTypeRepository.All().ToList();
              var requestDtoList = _mapper.Map<List<BloodRequestDto>>(requestEntityList);
              
              foreach (var requestDto in requestDtoList)
              {
                  var relatedRequestEntity = requestEntityList.FirstOrDefault(x => x.ID == requestDto.ID);
                  var relatedCity = cities.FirstOrDefault(x=> x.ID == relatedRequestEntity.CityId);
                  var relatedUser = users.FirstOrDefault(x => x.Id == relatedRequestEntity.UserID);
                  var relatedBloodType = bloodTypes.FirstOrDefault(x => x.ID == relatedRequestEntity.BloodTypeID);
                  requestDto.City = _mapper.Map<CityDTO>(relatedCity);
                  requestDto.UserInfo = _mapper.Map<UserDTO>(relatedUser);
                  requestDto.BloodType = _mapper.Map<BloodTypeDto>(relatedBloodType);


              }
              return requestDtoList;
          }
    }
}