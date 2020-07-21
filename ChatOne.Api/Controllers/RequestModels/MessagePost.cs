using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatOne.Api.Controllers.RequestModels
{
    public class MessagePost
    {
        public int contactId { get; set; }

        public  int currentUserId { get; set; }

        public string message { get; set; }

        public string socket_id  { get; set; }

        
    }
}
