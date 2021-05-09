using System;
using System.Collections.Generic;
using System.Text;

namespace BloodDonation.DataAccess
{
    public class UserRepository : IUserRepository
    {
        public void GetUser(int id)
        {
            /*
             do action A
            then do action B
            then C 
             */
            var x = 3;
        }

        public void GetUserDetails(int id)
        {
            throw new NotImplementedException();
        }
    }
}
