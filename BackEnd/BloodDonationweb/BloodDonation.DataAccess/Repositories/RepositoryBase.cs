using System.Data;

namespace BloodDonation.DataAccess.Repositories
{
    internal abstract class  RepositoryBase
    {
        protected IDbTransaction Transaction { get; private set; } //IDbConnection interface to enable the UnitOfWork class to implements a connection class (_connection)
        protected IDbConnection Connection { get { return Transaction.Connection; } } //IDbTransaction interface to allow the UnitOfWork class to implements a Transaction class (_transaction) which represents the transaction to be performed at the database

        public RepositoryBase(IDbTransaction transaction)
        {
            Transaction = transaction;
        }
    }
}
