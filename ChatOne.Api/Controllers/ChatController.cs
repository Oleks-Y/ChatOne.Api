using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using ChatOne.Api.Controllers.RequestModels;
using PusherServer;

namespace ChatOne.Api.Controllers
{
    public class ChatController : Controller
    {
        public IUnitOfWork _unitOfWork;
        private Pusher pusher;
        // Add Service later
        public ChatController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var options = new PusherOptions();
            options.Cluster = "eu";
            var pusher = new Pusher(
                "1041895",
                "0a54a9ffa32985979dd5",
                "ff562a59173102085807",
                options
                );
        }
        [Route("chat/allchats")]
        public IActionResult ChatsList(int currentId)
        {
            // For home page
            List<User> allUsers = _unitOfWork.Users.GetAll(u => u.Id != currentId).ToList();

            return Json(allUsers);

        }
        [Route("chat/getUser")]
        public IActionResult GetUser(int UserId)
        {
            User user = _unitOfWork.Users.Get(UserId);
            if(user == null)
            {
                return NotFound();
            }

            return Json(user);
        }
        [HttpGet]
        [Route("chat/chatwith")]
        public IActionResult ConversationWithContact(int contactId, int currentUserId)
        {

            var conversations = _unitOfWork.Conversations
                .GetAll(c => (c.Receiver_id == currentUserId && c.Sender_id == contactId) || (c.Receiver_id == contactId && c.Sender_id == currentUserId),
                c=> c.OrderBy(x=>x.Created_at)
                );

            return Json(conversations);    

        }
        [HttpPost]
        [Route("chat/sendmessage")]
        public IActionResult SendMessage([FromBody]MessagePost newmessage )
        {
            var user = _unitOfWork.Users.Get(newmessage.currentUserId);
            var contact = _unitOfWork.Users.Get(newmessage.contactId);
            if (user==null || contact == null)
            {
                return NotFound();
            }
            
            Conversation conv = new Conversation()
            {
                Sender_id = user.Id,
                Receiver_id = contact.Id,
                //Check if message != ''
                Message = newmessage.message,
                Created_at = DateTime.Now
            };

            _unitOfWork.Conversations.Add(conv);
            _unitOfWork.Save();

            var conversationChannel = getConvoChannel(newmessage.currentUserId, newmessage.contactId);

            pusher.TriggerAsync(
                conversationChannel,
                "new_message",
                conv,
                new TriggerOptions() { SocketId = newmessage.socket_id }
                );
            return Ok();
        }

        private string getConvoChannel(int user_id, int contact_id)
        {
            if (user_id > contact_id)
            {
                return "private-chat-" + contact_id + "-" + user_id;
            }

            return "private-chat-" + user_id + "-" + contact_id;
        }

    }
}