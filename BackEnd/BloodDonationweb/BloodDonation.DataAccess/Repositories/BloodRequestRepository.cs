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
            var sqlQuery = $@"SELECT * FROM `blood-donner`.BloodRequest BR 
  inner join BloodTypeCompatibilty BTC on BTC.CompatibleBloodTypeID = BR.BloodTypeID
 WHERE BTC.BloodTypeID = @BloodTypeId AND BR.CityId=@CityId ;";
            return Connection.Query<BloodRequest>(
                sqlQuery,
                param: new{BloodTypeId = bloodTypeId, CityId = cityId},
                transaction: Transaction
            ).ToList();
            
            // SELECT USER.Fname,USER.Lname,USER.Email,USER.Phone,BloodRequest.RequestDate FROM `blood-donner`.`USER` INNER JOIN `blood-donner`.`BloodRequest` ON USER.ID=BloodRequest.UserID inner join BloodTypeCompatibilty  on BloodTypeCompatibilty.CompatibleBloodTypeID = BloodRequest.BloodTypeID
            // WHERE BloodTypeCompatibilty.BloodTypeID = 7 AND BloodRequest.CityId=7  ;  =>ready to be used
        }

        public BloodRequest Find(int id)
        {
            return Connection.Query<BloodRequest>(
                "SELECT * FROM BloodRequest WHERE BloodRequestId = @BloodRequestId",
                param: new {BloodRequestId = id},
                transaction: Transaction
            ).FirstOrDefault();
        }

        public void Add(BloodRequest bloodRequest)
        {
            bloodRequest.ID = Connection.ExecuteScalar<int>(
                "INSERT INTO BloodRequest(`RequestDate`, `Status`,`BloodTypeID`,`UserID`,`CityId`) VALUES(@RequestDate,@Status,@BloodTypeID,@UserID,@CityId); SELECT LAST_INSERT_ID()",
                param: new {RequestDate = DateTime.UtcNow, Status = 1, BloodTypeID = bloodRequest.BloodTypeID, UserID = 1, bloodRequest.CityId},
                transaction: Transaction
            );
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
                param: new {Name = Name},
                transaction: Transaction
            ).FirstOrDefault();
        }
    }
}