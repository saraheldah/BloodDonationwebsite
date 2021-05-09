using System;
using System.Collections.Generic;
using System.Text;

namespace BloodDonation.DataAccess
{
    public interface IUserRepository
    {
        void GetUser(int id);
        void GetUserDetails(int id);
    }
}
