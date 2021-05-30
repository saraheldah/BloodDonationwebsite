using BloodDonation.DataAccess.Repositories;
using System;

namespace BloodDonation.DataAccess
{
    public interface  IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IBloodTypeCompatibilityRepository BloodTypeCompatibilityRepository { get; }
        IBloodTypeRepository BloodTypeRepository { get; }
        
        IBloodRequestRepository BloodRequestRepository { get; }
        
        IDonationHistoryRepository DonationHistoryRepository { get; }
        
        ICityRepository CityRepository { get; }
        
        ICountryRepository CountryRepository { get; }
        IResetPasswordRepository ResetPasswordRepository { get; }
        
        

        void Commit();
    }
}
