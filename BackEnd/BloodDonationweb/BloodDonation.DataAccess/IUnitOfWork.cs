using BloodDonation.DataAccess.Repositories;
using System;

namespace BloodDonation.DataAccess
{
    public interface  IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IBloodTypeCompatibilityRepository BloodTypeCompatibilityRepository { get; }
        IBloodTypeRepository BloodTypeRepository { get; }

        void Commit();
    }
}
