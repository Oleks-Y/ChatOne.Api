﻿using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(User user)
        {
            var objFromDb = _db.Users.FirstOrDefault(s => s.Id == user.Id);

            if (objFromDb != null)
            {
                objFromDb.Name = user.Name;


            }
        }
    }
}

