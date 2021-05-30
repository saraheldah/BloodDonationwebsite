using System;
using BloodDonation.Common;
using BloodDonation.DataAccess.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BloodDonation.DataAccess.Repositories
{
    internal class ResetPasswordRepository : RepositoryBase, IResetPasswordRepository
    {
        public ResetPasswordRepository(IDbTransaction transaction) : base(transaction)
        {
        }
        public void Add(ResetPassword resetPassword)
        {
            resetPassword.ID = Connection.ExecuteScalar<int>(
                "INSERT INTO ResetPassword(UserId,Code,Date,Status) VALUES(@ID,@UserId,@Code,@Date,@Status); SELECT LAST_INSERT_ID()",
                param: new { UserId = resetPassword.UserId, Code = resetPassword.Code, Date = resetPassword.Date, Status = resetPassword.Status },
                transaction: Transaction
            );

        }

        public bool IsValidCode(string code)
        {
            var password = Connection.Query<int>(
            "SELECT ID FROM ResetPassword WHERE Code = @code and Status = 1",
            param: new { code },
            transaction: Transaction
            ).FirstOrDefault();

            return password > 0;
        }
        public void ConsumeLink(string code)
        {
            Connection.Execute(
            "UPDATE ResetPassword SET Status = 0 WHERE Code = @code",
            param: new { code },
            transaction: Transaction
            );
        }

        public ResetPassword ResetPasswordEntity(int UserId, string Code, bool Status)
        {
            var resetPassword = new ResetPassword
            {
                UserId = UserId,
                Code = Code,
                Date = DateTime.Now,
                Status = Status

            };
            return resetPassword;
        }
    }
}