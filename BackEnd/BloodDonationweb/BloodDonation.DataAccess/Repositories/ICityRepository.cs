using BloodDonation.DataAccess.Entities;
using System.Collections.Generic;

namespace BloodDonation.DataAccess.Repositories
{
    public interface ICityRepository
    {
        void Add(City entity);
        
        IEnumerable<City> All();
        
        void Delete(int id);
        
        void Delete(City entity);
        
        City Find(int id);

        // City FindCityByCountry(int id);
        
        City FindByName(string name);
        
        void Update(City entity);
    }
}