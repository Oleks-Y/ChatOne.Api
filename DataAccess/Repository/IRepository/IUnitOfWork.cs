using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }

        void Save();
    }
}
