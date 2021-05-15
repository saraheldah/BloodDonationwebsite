using BloodDonation.DataAccess.Repositories;
using MySqlConnector;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BloodDonation.DataAccess
{
    public class UnitOfWork : IUnitOfWork //the UnitOfWork which is the database context is inheriting from class IUnitOfWork 
    {
        private IDbConnection _connection; //IDbConnection interface to enable the UnitOfWork class to implements a connection class (_connection)
        private IDbTransaction _transaction; //IDbTransaction interface to allow the UnitOfWork class to implements a Transaction class (_transaction) which represents the transaction to be performed at the database
        private bool _disposed;


        private IUserRepository _userRepository;


        public UnitOfWork(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
            _connection.Open(); //opens the connection to the database
            _transaction = _connection.BeginTransaction();
        }

        public IUserRepository UserRepository
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(_transaction)); }
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
