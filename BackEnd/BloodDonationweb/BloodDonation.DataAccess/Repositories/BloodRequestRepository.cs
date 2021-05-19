using BloodDonation.DataAccess.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BloodDonation.DataAccess.Repositories
{
    internal class BloodRequestRepository : RepositoryBase, IBloodRequestRepository
    {
        public BloodRequestRepository(IDbTransaction transaction) : base(transaction)
        {
        }
        
        
        public IEnumerable<BloodRequest> All()
        {
            return Connection.Query<BloodRequest>(
                "SELECT * FROM BloodRequest",
                transaction: Transaction
            ).ToList();
        }

        public BloodRequest Find(int id)
        {
            return Connection.Query<BloodRequest>(
                "SELECT * FROM BloodRequest WHERE BloodRequestId = @BloodRequestId",
                param: new { BloodRequestId = id },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public void Add(BloodRequest entity)
        {
            //entity.ID = Connection.ExecuteScalar<int>(
            //    "INSERT INTO BloodRequest(`ID`,`RequestDate`, `Status`,  `ProofFile`,`BloodTypeID`,`UserID`) VALUES(@ID,@RequestDate,@Status,@ProofFile,@BloodTypeID,@UserID); SELECT SCOPE_IDENTITY()",
            //    param: new { ID = entity.ID , RequestDate = ,Status= ,ProofFile= entity.ProofFile ,BloodTypeID= entity.ProofFile, UserID = entity.UserID},
            //    transaction: Transaction
            //);
        }

        public void Update(BloodRequest entity)
        {
            //Connection.Execute(
            //    "UPDATE City SET Name = @Name WHERE CityId = @CityId",
            //    param: new { Name = entity.Name, CityId = entity.CityId },
            //    transaction: Transaction
            //);
        }

        public void Delete(int id)
        {
            //Connection.Execute(
            //    "DELETE FROM City WHERE CityId = @CityId",
            //    param: new { CityId = id },
            //    transaction: Transaction
            //);
        }

        public void Delete(BloodRequest entity)
        {
            Delete(entity.ID);
        }

        public BloodRequest FindByName(string Name)
        {
            return Connection.Query<BloodRequest>(
                "SELECT * FROM City WHERE Name = @Name",
                param: new { Name = Name },
               transaction: Transaction
            ).FirstOrDefault();
        }
    }
}