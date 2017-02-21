using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Interact2World.Models;
using Data;

namespace Interact2World.Controllers
{
    public class InsertController : Controller
    {
        //
        // GET: /Insert/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult InsertMenu(MenuItem _objMenuData)
        {
            InsertModel _model = new InsertModel();

           // _objMenuData.MenuName = _model.MenuName;

            if(ModelState.IsValid)
            {
                _objMenuData.IsExists = true;
                _objMenuData.LastUpdatedBy = Session["UserName"].ToString();
                _objMenuData.LastUpdatedOn = DateTime.Now;

                if(_model.InsertMenu(_objMenuData))
                {

                }
                else
                {
                    ModelState.AddModelError("", _model.ExceptionMessage);

                    TempData["ErrorMessage"] = _model.ExceptionMessage;
                }
            }
            
            return View();
        }


        public ActionResult VerticalMenu()
        {
            IEnumerable<MenuItem> _lstMenuData = new List<MenuItem>();
            LoginModel _loginModel = new LoginModel();

            if (ModelState.IsValid)
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

        public ActionResult Footer()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");

        }

        public ActionResult Login(LoginModel login)
        {
            //if (ModelState.IsValid)
            //{
            //    Session["UserName"] = login.UserName;
            //    Session["Password"] = login.Password;

            //    if (login.LoginCheck())
            //    {

            //        Session["RoleId"] = login.RoleID;
            //        Session["RoleType"] = login.RoleType;
            //        Session["AccessRights"] = login.AccessRights;

            //        FormsAuthentication.SetAuthCookie(login.UserName, true);
            //        return RedirectToAction("Dashboard", "Login", "Login");
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("", login.ExceptionMessage);

            //        TempData["ErrorMessage"] = login.ExceptionMessage;
            //        return RedirectToAction("Index", "Home", "Home");
            //    }
            //}
            return PartialView(login);
        }

    }
}
