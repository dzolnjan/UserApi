using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UserApi.Models;
using WebMatrix.WebData;

namespace UserApi.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        [HttpPost]
        public ActionResult Signup(SignupModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    //WebSecurity.CreateUserAndAccount(model.Email, model.Password);
                    var token = Guid.NewGuid();
                    return Json(new { TokenId = token.ToString() });
                }
                catch (MembershipCreateUserException e)
                {
                    return Json(new { TokenId = "Already registered email." });
                }
            }

            return Json(new { TokenId = "Email or password is missing." });

        }

    }
}
