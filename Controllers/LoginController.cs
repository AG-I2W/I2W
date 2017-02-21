using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Interact2World.Models;
using Data;
using Interact2World.Areas.Employees.Models;

namespace Interact2World.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["UserName"] = null;
            Session["RoleId"] = null;
            Session["RoleType"] = null;
            Session["AccessRights"] = null;

            return RedirectToAction("Index", "Home", new { area="" });
        }

        public ActionResult Login(LoginModel login)
        {
            if (!String.IsNullOrEmpty(login.UserName) && !String.IsNullOrEmpty(login.Password))
            {
                if (ModelState.IsValid)
                {
                    
             //       Session["Password"] = login.Password;

                    if (login.LoginCheck())
                    {
                        Session["UserName"] = login.UserName;
                        Session["RoleId"] = login.RoleID;
                        Session["RoleType"] = login.RoleType;
                        Session["AccessRights"] = login.AccessRights;

                        FormsAuthentication.SetAuthCookie(login.UserName, true);
                        return RedirectToAction("Dashboard", "Common", new { area = "Employees" });
                    }
                    else
                    {
                        ModelState.AddModelError("", login.ExceptionMessage);

                        TempData["ErrorMessage"] = login.ExceptionMessage;
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }
                }
            }
            
            return PartialView(login);
        }

        public ActionResult VerticalMenu()
        {
            IEnumerable<MenuItem> _lstMenuData = new List<MenuItem>();
            LoginModel _loginModel = new LoginModel();

            if (ModelState.IsValid && Session["UserName"] != null)
            {
                _loginModel.UserName = Session["UserName"].ToString();
                _loginModel.RoleID = Session["RoleId"].ToString();

                _lstMenuData = _loginModel.ReadMenu();

                Session["MenuList"] = _lstMenuData;
                //if(_lstMenuData.ToList<MenuItem>().Count <= 0)
                //{
                //    return RedirectToAction("Menu", "Login", "Login");
                //}
            }
            else
            {
                ModelState.AddModelError("", _loginModel.ExceptionMessage);
                TempData["ErrorMessage"] = _loginModel.ExceptionMessage;
            }

            return View(_lstMenuData);
        }
        //public ActionResult VerticalMenu()
        //{
        //    return View();
        //}

        //public ActionResult Dashboard()
        //{
        //    if (Session["MenuList"] == null)
        //        return RedirectToAction("InsertMenu", "Insert", "Insert");
        //    return View();
        //}

        public ActionResult Footer()
        {
            IEnumerable<Services> _lstServices = new List<Services>();
            ReadModel _readModel = new ReadModel();

            _lstServices = _readModel.ReadServices();

            return View(_lstServices);
        }

        public ActionResult SocialLinks()
        {
            IEnumerable<SocialLinks> _lstSocialLinks = new List<SocialLinks>();
            ReadModel _readModel = new ReadModel();

            _lstSocialLinks = _readModel.ReadSocialLinks();
            return View(_lstSocialLinks);
        }
        
    }
}