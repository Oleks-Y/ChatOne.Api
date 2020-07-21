using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repository
{
    public class ConversationRepository : Repository<Conversation>,  IConversationRepository
    {
        private readonly ApplicationDbContext _db;
        public ConversationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


       
    }
}
