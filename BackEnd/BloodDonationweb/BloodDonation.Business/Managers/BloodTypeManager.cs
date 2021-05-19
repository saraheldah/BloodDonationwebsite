using AutoMapper;
using BloodDonation.Business.DTO;
using BloodDonation.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace BloodDonation.Business.Managers
{
    public class BloodTypeManager : BaseManager, IBloodTypeManager
    {
        public BloodTypeManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public List<BloodTypeDto> GetAll()
        {
            var bloodTypeEntities = _unitOfWork.BloodTypeRepository.All().ToList();
            var bloodTypeCompatibilityEntities = _unitOfWork.BloodTypeCompatibilityRepository.All().ToList();


            //// Mapping
            //var dtoList = new List<BloodTypeDto>();

            //foreach (var bloodTypeEntity in bloodTypeEntities)
            //{
            //    var bloodTypeDto = new BloodTypeDto()
            //    {
            //        ID = bloodTypeEntity.ID,
            //        Name = bloodTypeEntity.Name,
            //        RareGrade = bloodTypeEntity.RareGrade,
            //        CompatibleTypes = bloodTypeCompatibilityEntities
            //        .Where(x => x.BloodTypeID == bloodTypeEntity.ID)
            //        .Select(y =>
            //        new BloodTypeCompatibilityDto()
            //        {
            //            ID = y.CompatibleBloodTypeID,
            //            Name = bloodTypeEntities.FirstOrDefault(z => z.ID == y.CompatibleBloodTypeID).Name
            //        }).ToList()
            //    };
            //    dtoList.Add(bloodTypeDto);
            //}
            var dtoList = _mapper.Map<List<BloodTypeDto>>(bloodTypeEntities);
            foreach (var dto in dtoList)
            {
                var typeCompatibilityEntities = bloodTypeCompatibilityEntities.ToList();
                dto.CompatibleTypes = typeCompatibilityEntities
                    .Where(x => x.BloodTypeID == dto.ID)
                    .Select(y =>
                    new BloodTypeCompatibilityDto()
                    {
                        ID = y.CompatibleBloodTypeID,
                        Name = bloodTypeEntities.FirstOrDefault(z => z.ID == y.CompatibleBloodTypeID)?.Name
                    }).ToList();
            }

            return dtoList;
        }
    }
}
