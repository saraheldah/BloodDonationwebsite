using BloodDonation.DataAccess.Entities;
using System.Collections.Generic;

namespace BloodDonation.DataAccess.Repositories
{
    public interface IResetPasswordRepository
    {
        void Add(ResetPassword resetPassword);
        ResetPassword ResetPasswordEntity(int UserId, string Code, bool Status);
        bool IsValidCode(string code);
        void ConsumeLink(string code);
        int GetUserId(string code);
      
    }
}