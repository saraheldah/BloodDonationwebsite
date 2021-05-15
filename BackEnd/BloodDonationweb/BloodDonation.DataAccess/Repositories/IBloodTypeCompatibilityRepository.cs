using BloodDonation.DataAccess.Entities;
using System.Collections.Generic;

namespace BloodDonation.DataAccess.Repositories
{
    public interface IBloodTypeCompatibilityRepository
    {
        void Add(BloodTypeCompatibility entity);
        
        IEnumerable<BloodTypeCompatibility> All();
        
        void Delete(int id);
        
        void Delete(BloodTypeCompatibility entity);
        
        BloodTypeCompatibility Find(int id);
        
       // BloodTypeCompatibility FindByName(string name);
        
        void Update(BloodTypeCompatibility entity);
    }
}