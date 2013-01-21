using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using UserApi.Code;
using UserApi.Models;

namespace MvcApplication3.Controllers
{
    public class User1Controller : ApiController
    {
        public object Signup([FromBody]SignupModel model)
        {
            if (ModelState.IsValid)
            {
                var user = SimpleInMemoryRepo.Users.SingleOrDefault(x => x.Username == model.Email);
                if (user != null)
                {
                    return new { TokenId = "Email address already registered." };
                }

                var token = Guid.NewGuid().ToString();

                user = new User() { Username = model.Email, Password = model.Password, Token = token };
                SimpleInMemoryRepo.Users.Add(user);

                return new { TokenId = token };
            }

            if (ModelState.Keys.Any(key => key.Contains("Email")))
            {
                return new { TokenId = "Email address empty or invalid." };
            }

            return new { TokenId = "Password is empty." };
        }

        public object Login([FromBody]LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = SimpleInMemoryRepo.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);
                if (user != null)
                {
                    var token = Guid.NewGuid();
                    user.Token = token.ToString();
                    return new { TokenId = token.ToString() };
                }

                return new { TokenId = "Email address and password combination not found." };
            }

            return new { TokenId = "Username address or password is empty." };
        }

        public object Logout([FromBody]string token)
        {
            var user = SimpleInMemoryRepo.Users.SingleOrDefault(x => x.Token == token);
            if (user != null)
            {
                user.Token = null;
            }

            return new LogoutResponseModel { Code = "200", Detail = "Success", Message = "Token expired" };
        }
    }
}