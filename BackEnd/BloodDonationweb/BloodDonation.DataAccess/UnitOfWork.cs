using BloodDonation.DataAccess.Repositories;
using MySqlConnector;
using System;
using System.Data;

namespace BloodDonation.DataAccess
{
    public class UnitOfWork : IUnitOfWork //the UnitOfWork which is the database context is inheriting from class IUnitOfWork 
    {
        private IDbConnection _connection; //IDbConnection interface to enable the UnitOfWork class to implements a connection class (_connection)
        private IDbTransaction _transaction; //IDbTransaction interface to allow the UnitOfWork class to implements a Transaction class (_transaction) which represents the transaction to be performed at the database
        private bool _disposed;


        private IUserRepository _userRepository;
        private IBloodTypeRepository _bloodTypeRepository;
        private IBloodTypeCompatibilityRepository _bloodTypeCompatibilityRepository;
        private IBloodRequestRepository _bloodRequestRepository;
        private IDonationHistoryRepository _donationHistoryRepository;
        private ICityRepository _cityRepository;
        private ICountryRepository _countryRepository;
        


        public UnitOfWork(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
            _connection.Open(); //opens the connection to the database
            _transaction = _connection.BeginTransaction();
        }

        public IUserRepository UserRepository
        {
            get { return _userRepository ??= new UserRepository(_transaction); } //groups all the interfaces of the repositories which contain database operations into single transaction or "unit of work "
        }                                                                        //so that all operations either pass or fail(if there is a value in _userRepository return it otherwise create   
                                                                                 //a new instance and assign it the user repository and send it)
        public IBloodTypeRepository BloodTypeRepository
        {
            get { return _bloodTypeRepository ??= new BloodTypeRepository(_transaction); }
        }
        public IBloodTypeCompatibilityRepository BloodTypeCompatibilityRepository
        {
            get { return _bloodTypeCompatibilityRepository ??= new BloodTypeCompatibilityRepository(_transaction); }
        }

        public IBloodRequestRepository BloodRequestRepository
        {
            get { return _bloodRequestRepository ??= new BloodRequestRepository(_transaction); }
        }

        public IDonationHistoryRepository DonationHistoryRepository
        {
            get { return _donationHistoryRepository ??= new DonationHistoryRepository(_transaction); }
        }

        public ICityRepository CityRepository
        {
            get { return _cityRepository ??= new CityRepository(_transaction); }
        }

        public ICountryRepository CountryRepository
        {
            get { return _countryRepository ??= new CountryRepository(_transaction); }
        }


        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _userRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if(disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if(_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
