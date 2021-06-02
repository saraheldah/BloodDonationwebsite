using BloodDonation.Common;
using BloodDonation.DataAccess.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BloodDonation.DataAccess.Repositories
{
    internal class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IDbTransaction transaction) : base(transaction)   //constructor we are calling the base constructor and we are also passing transaction  
        {                                                                      //that we initialize at the UnitOfWork and this represents the transaction that is going to be performed at the database
        }
        public IEnumerable<User> All()
        {
            return Connection.Query<User>(
                "SELECT * FROM user",
                transaction: Transaction
            ).ToList();
        }

        public IEnumerable<User> FindDonorByCompatibleBloodTypeAndCity(int bloodTypeId, int cityId)
        {
            var sqlQuery = $@"SELECT * FROM `blood-donner`.USER U 
  inner join BloodTypeCompatibilty BTC on BTC.CompatibleBloodTypeID = U.BloodTypeID
 WHERE BTC.BloodTypeID = @BloodTypeId AND U.CityId=@CityId AND U.IsDonor=true;";
            return Connection.Query<User>(
                sqlQuery,
                param: new { BloodTypeId = bloodTypeId, CityId = cityId },
                transaction: Transaction
            ).ToList();
        }


        public User Find(int id)
        {
            return Connection.Query<User>(
                "SELECT * FROM User WHERE Id = @Id",
                param: new { Id = id },
                transaction: Transaction
            ).FirstOrDefault();
        }
        public User GetByEmailAndPassword(string email, string password)
        {
            if (email is null)
            {
                throw new System.ArgumentNullException(nameof(email));
            }

            if (password is null)
            {
                throw new System.ArgumentNullException(nameof(password));
            }

            return Connection.Query<User>(
                "SELECT * FROM User WHERE email = @email and password = @password",
                param: new { Email = email, Password = password },
                transaction: Transaction
            ).FirstOrDefault();
        }


        public User GetByEmail(string email)
        {
            if (email is null)
            {
                throw new System.ArgumentNullException(nameof(email));
            }
            
            return Connection.Query<User>(
                "SELECT * FROM User WHERE email = @email",
                param: new { email },
                transaction: Transaction
            ).FirstOrDefault();
        }


        public void Add(User newUser)
        {
            newUser.Id = Connection.ExecuteScalar<int>(
                "INSERT INTO User(Fname,Lname,DOB,Gender,Phone,Email,Password,IsDonor,Role,BloodTypeID,CountryId,CityId) VALUES(@fname,@lname,@DOB,@Gender,@Phone,@Email,@Password,@IsDonor,@Role,@BloodTypeID,@CountryId,@CityId); SELECT LAST_INSERT_ID()",
                param: new { Fname = newUser.Fname, Lname = newUser.Lname, DOB = newUser.DOB, Gender = newUser.Gender, Phone = newUser.Phone, Email = newUser.Email, Password = newUser.Password, IsDonor = false, Role = newUser.Role, BloodTypeID = newUser.BloodTypeID, CountryId = 125, CityId = newUser.CityId },
                transaction: Transaction
            );
        }

        public void Update(User updatedUser)
        {
            Connection.Execute(
                "UPDATE User SET Fname = @Fname, Lname = @Lname, DOB = @DOB , phone = @phone , Gender = @Gender, BloodTypeID = @BloodTypeID , CityId=@CityId  WHERE Id =@Id",
               param: new { Fname = updatedUser.Fname, Lname = updatedUser.Lname, DOB = updatedUser.DOB, phone = updatedUser.Phone, Gender = updatedUser.Gender, BloodTypeID = updatedUser.BloodTypeID, CityId = updatedUser.CityId,Id=updatedUser.Id },
                transaction: Transaction
            );
        }

        public void UpdatePassword(User updatedPassword)
        {
            Connection.Execute(
                "UPDATE User SET Password = @password WHERE Id = @id",
                param: new { password = updatedPassword.Password, id = updatedPassword.Id },
                transaction: Transaction
            );
        }

        public void MakeItDonor(int userId)
        {
            Connection.Execute("Update User SET IsDonor = true, Role = @role WHERE Id=@id",
                param: new { id = userId, role = Role.Donor },
                transaction: Transaction
                );
        }

        public void DeleteUser(int id)
        {
            Connection.Execute(
                "DELETE FROM User WHERE Id = @Id",
                param: new { Id = id },
                transaction: Transaction
            );
        }

        // public void Delete(User entity)
        // {
        //     Delete(entity.Id);
        // }

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
