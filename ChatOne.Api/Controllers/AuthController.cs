using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

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

    }
}