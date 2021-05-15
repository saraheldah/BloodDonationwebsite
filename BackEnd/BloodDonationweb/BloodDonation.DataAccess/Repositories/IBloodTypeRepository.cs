using BloodDonation.DataAccess.Entities;
using System.Collections.Generic;

namespace BloodDonation.DataAccess.Repositories
{
    public interface IBloodTypeRepository
    {
        void Add(BloodType entity);
        
        IEnumerable<BloodType> All();
        
        void Delete(int id);
        
        void Delete(BloodType entity);
        
        BloodType Find(int id);
        
        BloodType FindByName(string name);
        
        void Update(BloodType entity);
    }
}