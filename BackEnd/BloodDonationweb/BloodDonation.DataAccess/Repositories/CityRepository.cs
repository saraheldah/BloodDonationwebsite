using BloodDonation.DataAccess.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BloodDonation.DataAccess.Repositories
{
    
    internal class CityRepository : RepositoryBase, ICityRepository
    {
        
        public CityRepository (IDbTransaction transaction) : base(transaction)
        {
        }

        public IEnumerable<City> All()
        {
            return Connection.Query<City>(
                "SELECT * FROM City",
                transaction: Transaction
            ).ToList();
        }

        public City Find(int id)
        {
            return Connection.Query<City>(
                "SELECT * FROM City WHERE Id = @CityId",
                param: new { CityId = id },
                transaction: Transaction
            ).FirstOrDefault();
        }
        // public IEnumerable<City> FindCityByCountry(int id)
        // {
        //     return Connection.Query<City>(
        //         "SELECT * FROM City WHERE CountryId = @CountryId",
        //         param: new { CountryId = id },
        //         transaction: Transaction
        //     ).ToList();
        // }

        public void Add(City entity)
        {
            //entity.ID = Connection.ExecuteScalar<int>(
            //    "INSERT INTO City(Name) VALUES(@Name); SELECT SCOPE_IDENTITY()",
            //    param: new { Name = entity.Name },
            //    transaction: Transaction
            //);
        }

        public void Update(City entity)
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

        public void Delete(City entity)
        {
            Delete(entity.ID);
        }

        public City FindByName(string Name)
        {
            return Connection.Query<City>(
                "SELECT * FROM City WHERE Name = @Name",
                param: new { Name = Name },
                transaction: Transaction
            ).FirstOrDefault();
        }
    }

    }
