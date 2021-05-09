using BloodDonation.DataAccess.Repositories;
using System;

namespace BloodDonation.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }

        void Commit();
    }
}
