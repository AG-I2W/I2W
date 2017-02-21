using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Interact2World.Areas.Employees;
using Interact2World.Models;

namespace Interact2World.Areas.Employees.Controllers
{
    public class CommonController : Controller
    {
        //
        // GET: /Employees/Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            if(ModelState.IsValid)
            {
                if (Session["MenuList"] == null)
                    return RedirectToAction("InsertMenu", "Insert", "Insert");
            }

            return View();
        }




//        public ActionResult VerticalMenu()
//        {
//            IEnumerable<MenuItem> _lstMenuData = new List<MenuItem>();
//            LoginModel _loginModel = new LoginModel();

//            if (ModelState.IsValid)
//            {
//                _loginModel.UserName = Session["UserName"].ToString();
//                _loginModel.RoleID = Session["RoleId"].ToString();

//                _lstMenuData = _loginModel.ReadMenu();

//                Session["MenuList"] = _lstMenuData;
//                //if(_lstMenuData.ToList<MenuItem>().Count <= 0)
//                //{
//                //    return RedirectToAction("Menu", "Login", "Login");
//                //}
//            }
//            else
//            {
//                ModelState.AddModelError("", _loginModel.ExceptionMessage);
//                TempData["ErrorMessage"] = _loginModel.ExceptionMessage;
//            }

//            return View(_lstMenuData);
//        }

    }
}
