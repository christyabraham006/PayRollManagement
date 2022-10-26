using Pay_Roll_Management.dBHandler;
using Pay_Roll_Management.Models;
using PAY_ROLL_MANAGEMENT.dBHandler;
using PAY_ROLL_MANAGEMENT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PAY_ROLL_MANAGEMENT.Controllers
{
    public class HomeController : Controller
    {
        Signup sbd = new Signup();    
        LogindBHandler sd = new LogindBHandler();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login lg )
        {
            if (sd.GetByUserName(lg.UserName) != null)
            {
                FormsAuthentication.SetAuthCookie(lg.UserName, true);
                return RedirectToAction ("Userdashboard","Home");
            }
            return View(lg);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");

            
        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (sbd.AddNewEmployee(employee))
                    {
                        ModelState.Clear();
                        return RedirectToAction("Userdashboard", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed");
                        return View();
                    }
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message + " " + ex.StackTrace);
                return View(employee);
            }
        }

        [Authorize]
        public ActionResult Userdashboard()
        {
           
            return View();
        }
    }
}