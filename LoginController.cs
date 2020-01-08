using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //since by default get method is used
        public ActionResult Authorise(User user)
        {
            using(Entities1 db= new Entities1())
            {
                var userdetail = db.Users.Where(x => x.UserName == user.UserName && x.Password == user.Password).FirstOrDefault();
                if(userdetail==null)
                {
                    user.LoginErrorMsg = "Sorry ,Invalid username or Password";
                    return View("Index", user);
                }
                else
                {
                    Session["userID"] = user.UserID;
                    Session["userName"] = user.UserName;
                    return RedirectToAction("index", "Home");
                }
            }
            
        }

        public ActionResult Logout()
        {
            var userID = (int)Session["userId"];
            Session.Abandon();
            return RedirectToAction("index", "Login");
        }
    }
} 