using BloodDonation.DataAccess.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BloodDonation.DataAccess.Repositories
{
    internal class BloodTypeCompatibilityRepository : RepositoryBase, IBloodTypeCompatibilityRepository
    {
        public BloodTypeCompatibilityRepository(IDbTransaction transaction) : base(transaction)
        {
        }
        

        
        public IEnumerable<BloodTypeCompatibility> All()
        {
            return Connection.Query<BloodTypeCompatibility>(
                "SELECT * FROM BloodTypeCompatibility",
                transaction: Transaction
            ).ToList();
        }

        public BloodTypeCompatibility Find(int id)
        {
            return Connection.Query<BloodTypeCompatibility>(
                "SELECT * FROM BloodTypeCompatibility WHERE BloodTypeCompatibilityId = @BloodTypeCompatibilityId",
                param: new { BloodTypeCompatibilityId = id },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public void Add(BloodTypeCompatibility entity)
        {
            //entity.ID = Connection.ExecuteScalar<int>(
            //    "INSERT INTO City(Name) VALUES(@Name); SELECT SCOPE_IDENTITY()",
            //    param: new { Name = entity.Name },
            //    transaction: Transaction
            //);
        }

        public void Update(BloodTypeCompatibility entity)
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

        public void Delete(BloodTypeCompatibility entity)
        {
            Delete(entity.BloodTypeID);
        }

        public BloodTypeCompatibility FindByName(string Name)
        {
            return Connection.Query<BloodTypeCompatibility>(
                "SELECT * FROM BloodTypeCompatibility WHERE Name = @Name",
                param: new { Name = Name },
                transaction: Transaction
            ).FirstOrDefault();
        }
    }
}