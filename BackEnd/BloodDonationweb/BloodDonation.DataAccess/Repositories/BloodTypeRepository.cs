using BloodDonation.DataAccess.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BloodDonation.DataAccess.Repositories
{
    internal class BloodTypeRepository : RepositoryBase, IBloodTypeRepository
    {
        public BloodTypeRepository(IDbTransaction transaction) : base(transaction)
        {
        }
        
        public IEnumerable<BloodType> All()
        {
            return Connection.Query<BloodType>(
                "SELECT * FROM BloodType",
                transaction: Transaction
            ).ToList();
        }

        public BloodType Find(int id)
        {
            return Connection.Query<BloodType>(
                "SELECT * FROM BloodType WHERE BloodTypeId = @BloodTypeId",
                param: new { BloodTypeId = id },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public void Add(BloodType entity)
        {
            //entity.ID = Connection.ExecuteScalar<int>(
            //    "INSERT INTO BloodType(Name) VALUES(@Name); SELECT SCOPE_IDENTITY()",
            //    param: new { Name = entity.Name },
            //    transaction: Transaction
            //);
        }

        public void Update(BloodType entity)
        {
            //Connection.Execute(
            //    "UPDATE City SET Name = @Name WHERE BloodTypeId = @BloodTypeId",
            //    param: new { Name = entity.Name, BloodTypeId = entity.BloodTypeId },
            //    transaction: Transaction
            //);
        }

        public void Delete(int id)
        {
            //Connection.Execute(
            //    "DELETE FROM BloodType WHERE BloodTypeId = @BloodTypeId",
            //    param: new { BloodTypeId = id },
            //    transaction: Transaction
            //);
        }

        public void Delete(BloodType entity)
        {
            Delete(entity.ID);
        }

        public BloodType FindByName(string Name)
        {
            return Connection.Query<BloodType>(
                "SELECT * FROM BloodType WHERE Name = @Name",
                param: new { Name = Name },
                transaction: Transaction
            ).FirstOrDefault();
        }
    }

}