using BloodDonation.DataAccess.Entities;
using System.Collections.Generic;

namespace BloodDonation.DataAccess.Repositories
{
    public interface IUserRepository
    {
        void Add(User newUser);
        IEnumerable<User> All();
        void Delete(int id);
        void Delete(User entity);
        User Find(int id);
        User FindByFirstName(string name);
        void Update(User updatedUser);

        void UpdatePassword(User updatedPassword);
        IEnumerable<User> FindDonorByCompatibleBloodTypeAndCity(int bloodTypeId, int cityId);
        
    }
}
