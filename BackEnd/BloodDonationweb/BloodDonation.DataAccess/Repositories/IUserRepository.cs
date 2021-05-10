﻿using BloodDonation.DataAccess.Entities;
using System.Collections.Generic;

namespace BloodDonation.DataAccess.Repositories
{
    public interface IUserRepository
    {
        void Add(User entity);
        IEnumerable<User> All();
        void Delete(int id);
        void Delete(User entity);
        User Find(int id);
        User FindByFirstName(string name);
        void Update(User entity);
    }
}