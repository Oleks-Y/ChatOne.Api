using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public int Sender_id { get; set; }
        public int Receiver_id { get; set; }
        public string Message { get; set; }
        public MessageStatus Status { get; set; }
        public DateTime Created_at { get; set; }

        public Conversation()
        {
            Status = MessageStatus.Sent;
        }

        public enum MessageStatus
        {
            Sent,
            Delivered
        }

    }
}
