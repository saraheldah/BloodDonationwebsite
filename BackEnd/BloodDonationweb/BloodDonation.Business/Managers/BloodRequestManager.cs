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

     
          
          public BloodRequest requestEntity(int BloodType,int city)
          {
              BloodRequest bloodRequest = new BloodRequest();
              bloodRequest.BloodTypeID = BloodType;
              bloodRequest.CityId = city;
              return bloodRequest;
          }
          
          public List<BloodRequestDto> FindRequestByCompatibleBloodTypeAndCity(int bloodTypeId, int cityId )
          {
              // int bloodTypeId;
              // int cityId;
              var requestEntityList = _unitOfWork.BloodRequestRepository.FindRequestByCompatibleBloodTypeAndCity(bloodTypeId, cityId).ToList();
              var countries = _unitOfWork.CountryRepository.All().ToList();
              var cities = _unitOfWork.CityRepository.All().ToList();
              var requestDtoList = _mapper.Map<List<BloodRequestDto>>(requestEntityList);
            
              foreach (var requestDto in requestDtoList)
              {
                  var relatedRequestEntity = requestEntityList.FirstOrDefault(x => x.ID == requestDto.ID);
                  var relatedCity = cities.FirstOrDefault(x=> x.ID == relatedRequestEntity.CityId);
                  requestDto.City = _mapper.Map<CityDTO>(relatedCity);
                  
              }
              return requestDtoList;


          }
    }
}