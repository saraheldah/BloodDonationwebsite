using System;
using BloodDonation.Common;
using BloodDonation.DataAccess.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BloodDonation.DataAccess.Repositories
{
    internal class ResetPasswordRepository :RepositoryBase, IResetPasswordRepository
    {
        public ResetPasswordRepository(IDbTransaction transaction) : base(transaction)
        {
        }
        public void Add(ResetPassword resetPassword)
        {
            resetPassword.ID = Connection.ExecuteScalar<int>(
                "INSERT INTO ResetPassword(UserId,Code,Date,Status) VALUES(@ID,@UserId,@Code,@Date,@Status); SELECT LAST_INSERT_ID()",
                param: new { UserId = resetPassword.UserId, Code = resetPassword.Code, Date = resetPassword.Date, Status = resetPassword.Status},
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