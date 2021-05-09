using BloodDonation.DataAccess.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BloodDonation.DataAccess.Repositories
{
    internal class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }
        public IEnumerable<User> All()
        {
            return Connection.Query<User>(
                "SELECT * FROM user",
                transaction: Transaction
            ).ToList();
        }

        public User Find(int id)
        {
            return Connection.Query<User>(
                "SELECT * FROM User WHERE UserId = @UserId",
                param: new { UserId = id },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public void Add(User entity)
        {
            //entity.Id = Connection.ExecuteScalar<int>(
            //    "INSERT INTO User(Name) VALUES(@Name); SELECT SCOPE_IDENTITY()",
            //    param: new { Name = entity.Name },
            //    transaction: Transaction
            //);
        }

        public void Update(User entity)
        {
            //Connection.Execute(
            //    "UPDATE User SET Name = @Name WHERE UserId = @UserId",
            //    param: new { Name = entity.Name, UserId = entity.UserId },
            //    transaction: Transaction
            //);
        }

        public void Delete(int id)
        {
            //Connection.Execute(
            //    "DELETE FROM User WHERE UserId = @UserId",
            //    param: new { UserId = id },
            //    transaction: Transaction
            //);
        }

        public void Delete(User entity)
        {
            Delete(entity.Id);
        }

        public User FindByFirstName(string FName)
        {
            return Connection.Query<User>(
                "SELECT * FROM User WHERE FName = @FName",
                param: new { FName = FName },
                transaction: Transaction
            ).FirstOrDefault();
        }
    }
}
