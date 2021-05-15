using BloodDonation.DataAccess.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BloodDonation.DataAccess.Repositories
{
    internal class CountryRepository : RepositoryBase, ICountryRepository
    {
        public CountryRepository(IDbTransaction transaction) : base(transaction)
        {
        }
       
        public IEnumerable<Country> All()
        {
            return Connection.Query<Country>(
                "SELECT * FROM user",
                transaction: Transaction
            ).ToList();
        }

        public Country Find(int id)
        {
            return Connection.Query<Country>(
                "SELECT * FROM Country WHERE CountryId = @CountryId",
                param: new { CountryId = id },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public void Add(Country entity)
        {
            //entity.ID = Connection.ExecuteScalar<int>(
            //    "INSERT INTO Country(Name) VALUES(@Name); SELECT SCOPE_IDENTITY()",
            //    param: new { Name = entity.Name },
            //    transaction: Transaction
            //);
        }

        public void Update(Country entity)
        {
            //Connection.Execute(
            //    "UPDATE Country SET Name = @Name WHERE CountryId = @CountryId",
            //    param: new { Name = entity.Name, CountryId = entity.CountryId },
            //    transaction: Transaction
            //);
        }

        public void Delete(int id)
        {
            //Connection.Execute(
            //    "DELETE FROM Country WHERE CounrtyId = @CounrtyId",
            //    param: new { CounrtyId = id },
            //    transaction: Transaction
            //);
        }

        public void Delete(Country entity)
        {
            Delete(entity.ID);
        }

        public Country FindByName(string Name)
        {
            return Connection.Query<Country>(
                "SELECT * FROM Country WHERE Name = @Name",
                param: new { Name = Name },
                transaction: Transaction
            ).FirstOrDefault();
        }
    }
    }
