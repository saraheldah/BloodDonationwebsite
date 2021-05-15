using BloodDonation.DataAccess.Entities;
using System.Collections.Generic;

namespace BloodDonation.DataAccess.Repositories
{
    public interface ICountryRepository
    {
        void Add(Country entity);
        
        IEnumerable<Country> All();
        
        void Delete(int id);
        
        void Delete(Country entity);
        
        Country Find(int id);
        
        Country FindByName(string name);
        
        void Update(Country entity);
    }
}