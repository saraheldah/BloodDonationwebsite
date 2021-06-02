using BloodDonation.DataAccess.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BloodDonation.DataAccess.Repositories
{
    internal class DonationHistoryRepository : RepositoryBase, IDonationHistoryRepository
    {
        public DonationHistoryRepository(IDbTransaction transaction) : base(transaction)
        {
        }
       
        
        public IEnumerable<DonationHistory> All()
        {
            return Connection.Query<DonationHistory>(
                "SELECT * FROM user",
                transaction: Transaction
            ).ToList();
        }

        public DonationHistory Find(int id)
        {
            return Connection.Query<DonationHistory>(
                "SELECT * FROM DonationHistory WHERE DonationHistoryId = @DonationHistoryId",
                param: new { DonationHistoryId = id },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public void Add(DonationHistory entity)
        {
            //entity.Id = Connection.ExecuteScalar<int>(
            //    "INSERT INTO User(Name) VALUES(@Name); SELECT SCOPE_IDENTITY()",
            //    param: new { Name = entity.Name },
            //    transaction: Transaction
            //);
        }

        public void Update(DonationHistory entity)
        {
            //Connection.Execute(
            //    "UPDATE DonationHistory SET Name = @Name WHERE UserId = @UserId",
            //    param: new { Name = entity.Name, UserId = entity.UserId },
            //    transaction: Transaction
            //);
        }

        public void Delete(int id)
        {
            //Connection.Execute(
            //    "DELETE FROM DonationHistory WHERE DonationHistoryId = @DonationHistoryId",
            //    param: new { DonationHistoryId = id },
            //    transaction: Transaction
            //);
        }

        public void Delete(DonationHistory entity)
        {
            Delete(entity.ID);
        }

        public DonationHistory FindByDonorId(int donorId)
        {
            return Connection.Query<DonationHistory>(
                "SELECT * FROM DonationHistory WHERE DonorID = @donorId",
                param: new { DonorID = donorId },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public DonationHistory FindByRecipientId(int recipientId)
        {
            return Connection.Query<DonationHistory>(
                "SELECT * FROM DonationHistory WHERE RecipientID = @recipientId",
                param: new { RecipientID = recipientId },
                transaction: Transaction
            ).FirstOrDefault();
        
        }

        
    }
    }
