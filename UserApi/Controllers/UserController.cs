﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UserApi.Code;
using UserApi.Models;
using WebMatrix.WebData;

namespace UserApi.Controllers
{
    public class UserController : Controller
    {
        [HttpPost]
        public ActionResult Signup(SignupModel model)
        {
            if (ModelState.IsValid)
            {
                var user = SimpleInMemoryRepo.Users.SingleOrDefault(x => x.Username == model.Email);
                if (user != null)
                {
                    return Json(new { TokenId = "Email address already registered." });
                }

                var token = Guid.NewGuid().ToString();

                user = new User() { Username = model.Email, Password = model.Password, Token = token };
                SimpleInMemoryRepo.Users.Add(user);

                return Json(new { TokenId = token });
            }

            if (ModelState.Keys.Any(key => key.Contains("Email")))
            {
                return Json(new { TokenId = "Email address empty or invalid." });
            }

            return Json(new { TokenId = "Password is empty." });
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = SimpleInMemoryRepo.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);
                if (user != null)
                {
                    var token = Guid.NewGuid();
                    user.Token = token.ToString();
                    return Json(new { TokenId = token.ToString() });
                }

                return Json(new { TokenId = "Email address and password combination not found." });
            }

            return Json(new { TokenId = "Username address or password is empty." });
        }

        [HttpPost]
        public ActionResult Logout(string token)
        {
            var user = SimpleInMemoryRepo.Users.SingleOrDefault(x => x.Token == token);
            if (user != null)
            {
                user.Token = null;
            }

            return Json(new LogoutResponseModel { Code = "200", Detail = "Success", Message = "Token expired" });
        }

    }
}
