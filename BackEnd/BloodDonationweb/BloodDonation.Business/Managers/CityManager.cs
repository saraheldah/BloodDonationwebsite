using AutoMapper;
using BloodDonation.Business.DTO;
using BloodDonation.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace BloodDonation.Business.Managers
{
    public class CityManager : BaseManager, ICityManager
    {
        public CityManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public List<CityDTO> GetAll()
        {
            var CityEntities = _unitOfWork.CityRepository.All().ToList();
            var cityDtoList = _mapper.Map<List<CityDTO>>(CityEntities);
            return cityDtoList;
        }
    }  
}