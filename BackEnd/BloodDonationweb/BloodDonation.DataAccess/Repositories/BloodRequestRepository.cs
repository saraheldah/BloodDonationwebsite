using System;
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

        public IEnumerable<BloodRequest> FindRequestByCompatibleBloodTypeAndCity(int bloodTypeId, int cityId)
        {
            var sqlQuery =
                $@"SELECT br.* from BloodRequest br 
		 inner join BloodTypeCompatibilty btc on br.BloodTypeID = btc.BloodTypeID
         WHERE br.CityId = @CityId and btc.CompatibleBloodTypeID = @BloodTypeId and br.Status=0 ";
            return Connection.Query<BloodRequest>(
                sqlQuery,
                param: new {BloodTypeId = bloodTypeId, CityId = cityId},
                transaction: Transaction
            ).ToList();
        }

        public BloodRequest Find(int id)
        {
            return Connection.Query<BloodRequest>(
                "SELECT * FROM BloodRequest WHERE BloodRequestId = @BloodRequestId",
                param: new {BloodRequestId = id},
                transaction: Transaction
            ).FirstOrDefault();
        }
        
        public IEnumerable<BloodRequest> FindRequestByUserId(int id)
        {
            return Connection.Query<BloodRequest>(
                "SELECT * FROM BloodRequest WHERE UserId = @UserId",
                param: new {UserId = id},
                transaction: Transaction
            ).ToList();
        }

        public void Add(BloodRequest bloodRequest)
        {
            bloodRequest.ID = Connection.ExecuteScalar<int>(
                "INSERT INTO BloodRequest(`RequestDate`, `Status`,`BloodTypeID`,`UserID`,`CityId`,`CenterName`) " +
                "VALUES(@RequestDate,@Status,@BloodTypeID,@UserID,@CityId,@CenterName); SELECT LAST_INSERT_ID()",
                param: new
                {
                    RequestDate = DateTime.UtcNow, Status = 0, BloodTypeID = bloodRequest.BloodTypeID, UserID = bloodRequest.UserID,
                    CityId = bloodRequest.CityId, CenterName = bloodRequest.CenterName
                },
                transaction: Transaction
            );
        }

        public void UpdateRequestStatus(BloodRequest statusRequest)
        {
            Connection.Execute(
                "UPDATE BloodRequest SET Status = 1 WHERE ID = @ID",
                param: new { Status = 1, ID = statusRequest.ID },
                transaction: Transaction
            );
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
                param: new {Name = Name},
                transaction: Transaction
            ).FirstOrDefault();
        }
    }
}