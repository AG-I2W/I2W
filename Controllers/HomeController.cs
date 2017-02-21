using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Interact2World.Areas.Employees.Models;

namespace Main.Controllers
{
    public class HomeController : Controller
    {

        //public ActionResult Footer()
        //{
        //    return View();
        //}
        // GET: Home
        public ActionResult Index(string id)
        {
            IEnumerable<Contacts> _lstContacts = new List<Contacts>();
            ReadModel _readModel = new ReadModel();

            _lstContacts = _readModel.ReadContacts(id, "MainWebsite");

            if (!String.IsNullOrEmpty(_readModel.ExceptionMessage))
            {
                ModelState.AddModelError("", _readModel.ExceptionMessage);
                TempData["ErrorMessage"] = _readModel.ExceptionMessage;
            }
            string _Contact="";
            foreach(var contact in _lstContacts)
            {
                if (contact.IsExists)
                {
                    if (contact.Preference == "1")
                    {
                        foreach (var subitem in contact.LstContactInfo.Where(m => m.ContactType == "Phone"))
                        {
                            _Contact += subitem.Value;
                        }
                    }
                }
            }

            Session["Phone"] = _Contact;
            return View(_lstContacts);
        }


        public ActionResult AboutUs(string id)
        {
            return View();
        }

        public ActionResult Services()
        {
            IEnumerable<Services> _lstServiceData = new List<Services>();
            ReadModel _readModel = new ReadModel();

            _lstServiceData = _readModel.ReadServices();

            if(!String.IsNullOrEmpty(_readModel.ExceptionMessage))
            {
                ModelState.AddModelError("", _readModel.ExceptionMessage);
                TempData["ErrorMessage"] = _readModel.ExceptionMessage;
            }

            return View(_lstServiceData);
        }

        //IEnumerable<MenuItem> _lstMenuData = new List<MenuItem>();
        //    LoginModel _loginModel = new LoginModel();

        //    if (ModelState.IsValid && Session["UserName"] != null)
        //    {
        //        _loginModel.UserName = Session["UserName"].ToString();
        //        _loginModel.RoleID = Session["RoleId"].ToString();

        //        _lstMenuData = _loginModel.ReadMenu();

        //        Session["MenuList"] = _lstMenuData;
        //        //if(_lstMenuData.ToList<MenuItem>().Count <= 0)
        //        //{
        //        //    return RedirectToAction("Menu", "Login", "Login");
        //        //}
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", _loginModel.ExceptionMessage);
        //        TempData["ErrorMessage"] = _loginModel.ExceptionMessage;
        //    }

        //    return View(_lstMenuData);

        public ActionResult Portfolio()
        {
            IEnumerable<Portfolio> _lstPortfolio = new List<Portfolio>();
            ReadModel _readModel = new ReadModel();

            _lstPortfolio = _readModel.ReadExistingPortfolio();

            //var PortfolioGrouped = from b in _lstPortfolio
            //                   group b by b.ServiceId into g
            //                   select new Group<string, Data.Portfolio> { Key = g.Key, Values = g };

            if(!String.IsNullOrEmpty(_readModel.ExceptionMessage))
            {
                ModelState.AddModelError("", _readModel.ExceptionMessage);
                TempData["ErrorMessage"] = _readModel.ExceptionMessage;
            }
            return View(_lstPortfolio);
        }

        public ActionResult Contacts(string id)
        {
            IEnumerable<Contacts> _lstContacts = new List<Contacts>();
            ReadModel _readModel = new ReadModel();

            _lstContacts = _readModel.ReadContacts(id, "MainWebsite");

            if(!String.IsNullOrEmpty(_readModel.ExceptionMessage))
            {
                ModelState.AddModelError("", _readModel.ExceptionMessage);
                TempData["ErrorMessage"] = _readModel.ExceptionMessage;
            }
            return View(_lstContacts);
        }

        //public ActionResult Login()
        //{
        //    return View();
        //}


    }
}