using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ChatOne.Api.Controllers
{
    public class ChatController : Controller
    {
        public IUnitOfWork _unitOfWork;
        public ChatController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

    }
}