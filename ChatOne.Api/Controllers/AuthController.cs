using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using PusherServer;

namespace ChatOne.Api.Controllers
{
    public class AuthController : Controller
    {
        public IUnitOfWork _unitOfWork;
        public AuthController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("auth/login")]
        public IActionResult Login(string username)
        {
            // Trim username
            //Username != ""
            User user = _unitOfWork.Users.GetFirstOrDefault(x => x.Name == username);
            if(user == null)
            {
                user = new User { Name = username, Created_At= DateTime.Now };
                _unitOfWork.Users.Add(user);
                _unitOfWork.Save();
            }

            return Json(user);
        }

        public IActionResult AuthForChannel(int currUserId ,string channel_name, string socket_id)
        {
            var user = _unitOfWork.Users.Get(currUserId);

            var options = new PusherOptions();
            options.Cluster = "eu";
            var pusher = new Pusher(
                "1041895",
                "0a54a9ffa32985979dd5",
                "ff562a59173102085807",
                options
                );

            var auth = pusher.Authenticate(channel_name, socket_id);
            return Json(auth);

        }

    }
} 