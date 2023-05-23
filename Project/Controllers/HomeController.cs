using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.LoginViewModel model)
        {
            using (Models.ProjectIBMEntities context=new Models.ProjectIBMEntities())
            {
                Models.User usr = context.Users.FirstOrDefault(user=>
                user.UserName.ToLower()==model.UserName.ToLower() && user.UserPassword == model.Password);
                if(usr != null)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);

                    return RedirectToAction("Index",usr.UserRole);

                }
                ModelState.AddModelError("UserName", "Invalid Username or Password");
                return View(model);
            }
            
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}