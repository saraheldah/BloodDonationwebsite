using BloodDonation.DataAccess.Entities;
using System.Collections.Generic;

namespace BloodDonation.DataAccess.Repositories
{
    public interface IBloodRequestRepository
    {
        void Add(BloodRequest entity);
        
        IEnumerable<BloodRequest> All();
        
        void Delete(int id);
        
        void Delete(BloodRequest entity);
        
        BloodRequest Find(int id);
        
        BloodRequest FindByName(string name);
        
        void Update(BloodRequest entity);
    }
}