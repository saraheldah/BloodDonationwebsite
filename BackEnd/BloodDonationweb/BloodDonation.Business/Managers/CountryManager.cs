using AutoMapper;
using BloodDonation.Business.DTO;
using BloodDonation.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace BloodDonation.Business.Managers
{
    public class CountryManager : BaseManager , ICountryManager
    {

        public CountryManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            
        }

        public List<CountryDTO> GetAll()
        {
            var countryEntities = _unitOfWork.CountryRepository.All();
            var cityEntities = _unitOfWork.CityRepository.All();
            
            
            var dtoList = _mapper.Map<List<CountryDTO>>(countryEntities);
            foreach (var dto in dtoList)
            {
                var citiesEntities = cityEntities.ToList();
                dto.Cities = citiesEntities
                    .Where(x => x.CountryId == dto.ID)
                    .Select(y =>
                        new CityDTO()
                        {
                            CountryId = y.ID,
                           // Name = countryEntities.FirstOrDefault(z => z.ID == y.ID).Name
                        }).ToList();
            }

            return dtoList;
        }

    }
}