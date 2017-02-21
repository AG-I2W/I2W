using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.IO;
using Interact2World.Models;
using Interact2World.Areas.Employees.Models;
using Data;

namespace Interact2World.Areas.Employees.Controllers
{
    public class InsertController : Controller
    {
        //
        // GET: /Employees/Insert/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult InsertMenu(MenuItem _objMenuData)
        {
            InsertModel _model = new InsertModel();

            // _objMenuData.MenuName = _model.MenuName;

            if (ModelState.IsValid && Session["UserName"] != null)
            {
                _objMenuData.IsExists = true;
                _objMenuData.LastUpdatedBy = Session["UserName"].ToString();
                _objMenuData.LastUpdatedOn = DateTime.Now;

                if (_model.InsertMenu(_objMenuData))
                {
                    TempData["ErrorMessage"] = "Saved";

                }
                else
                {
                    ModelState.AddModelError("", _model.ExceptionMessage);

                    TempData["ErrorMessage"] = _model.ExceptionMessage;
                }

                ModelState.Clear();
            }

            return View();
        }
        

        public ActionResult InsertServices(Services _objServiceData)
        {
            InsertModel _model = new InsertModel();

            if (ModelState.IsValid && Session["UserName"]!=null)
            {
                _objServiceData.ObjImage.ImageName = _objServiceData.ServiceName;
                _objServiceData.ObjImage.Description = "Image is for Services " + _objServiceData.ServiceName;
                _objServiceData.ObjImage.IsExists = true;
                _objServiceData.ObjImage.LastUpdatedBy = Session["UserName"].ToString();
                _objServiceData.ObjImage.LastUpdatedOn = DateTime.Now;
                _objServiceData.ObjImage.ImageType = "MainWebsite";
                
                _objServiceData.IsExists = true;
                _objServiceData.LastUpdatedBy = Session["UserName"].ToString();
                _objServiceData.LastUpdatedOn = DateTime.Now;

                if(_model.InsertServices(_objServiceData))
                {
                    TempData["ErrorMessage"] = "Saved";
                }
                else
                {
                    ModelState.AddModelError("", _model.ExceptionMessage);

                    TempData["ErrorMessage"] = _model.ExceptionMessage;
                }

                ModelState.Clear();
            }

            return View();
        }

        public ActionResult InsertContacts(Contacts _objContactData)
        {
            InsertModel _model = new InsertModel();

            if (ModelState.IsValid && Session["UserName"] != null)
            {
                _objContactData.LstContactInfo[0].ContactType = "WebsiteUrl";
                _objContactData.LstContactInfo[0].Preference = "1";
               // _objContactData.LstContactInfo[0].Value = _objContactData.LstContactInfo[0].Value;
                _objContactData.LstContactInfo[0].Description = "Work";
                _objContactData.LstContactInfo[0].LastUpdatedOn = DateTime.Now;
                _objContactData.LstContactInfo[0].LastUpdatedBy = Session["UserName"].ToString();
                _objContactData.LstContactInfo[0].IsExists = true;
                

                
                 _objContactData.LstContactInfo[1].ContactType = "Phone";
                 _objContactData.LstContactInfo[1].Preference = "1";
                 //_objContactData.LstContactInfo[1].Value = _objContactData.LstContactInfo[1].Value;
                 _objContactData.LstContactInfo[1].Description = _objContactData.LstContactInfo[1].Description;
                 _objContactData.LstContactInfo[1].LastUpdatedOn = DateTime.Now;
                 _objContactData.LstContactInfo[1].LastUpdatedBy = Session["UserName"].ToString();
                 _objContactData.LstContactInfo[1].IsExists = true;
                

                _objContactData.LstContactInfo[2].ContactType = "Email";
                _objContactData.LstContactInfo[2].Preference = "1";
               // _objContactData.LstContactInfo[2].Description = _objContactData.LstContactInfo[2].Description;
                _objContactData.LstContactInfo[2].Value = _objContactData.LstContactInfo[2].Value;
                _objContactData.LstContactInfo[2].LastUpdatedOn = DateTime.Now;
                _objContactData.LstContactInfo[2].LastUpdatedBy = Session["UserName"].ToString();
                _objContactData.LstContactInfo[2].IsExists = true;
                

                //_objContactData.LstContactInfo[0].ContactType = "WebsiteUrl";
                //_objContactData.LstContactInfo[0].Preference = "1";

                //_objContactData.LstContactInfo[1].ContactType = "Phone";
                //_objContactData.LstContactInfo[1].Preference = "1";

                //_objContactData.LstContactInfo[3].ContactType = "Email";
                //_objContactData.LstContactInfo[3].Preference = "1";
                _objContactData.IsExists = true;
                _objContactData.LastUpdatedBy = Session["UserName"].ToString();
                _objContactData.LastUpdatedOn = DateTime.Now;
                
                if(_model.InsertContacts(_objContactData))
                {
                    TempData["ErrorMessage"] = "Saved";
                }
                else
                {
                    ModelState.AddModelError("", _model.ExceptionMessage);

                    TempData["ErrorMessage"] = _model.ExceptionMessage;
                }

                ModelState.Clear();
            }

            return View();
        }

        public ActionResult InsertPortfolio(HttpPostedFileBase file, Portfolio objPortfolio)
        {
            InsertModel _model = new InsertModel();

            ReadModel _readModel = new ReadModel();

            List<Services> _lstServices = _readModel.ReadServices();

            List<SelectListItem> list = new List<SelectListItem>();
            var servlst = (from c in _lstServices select c).ToArray();
            for (int i = 0; i < servlst.Length; i++)
            {
                list.Add(new SelectListItem
                {
                    Text = servlst[i].ServiceName,
                    Value = servlst[i].ServiceId.ToString(),
                });
            }
            ViewData["ServiceLst"] = list;

            if (ModelState.IsValid && Session["UserName"] != null)
            {
                try
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string ImageName = objPortfolio.Title + "-" + objPortfolio.ServiceId + Path.GetExtension(file.FileName);

                        string path = Path.Combine(Server.MapPath("~/Content/images/Portfolio"),
                                                     ImageName);

                        file.SaveAs(path);

                        if(!objPortfolio.Link.StartsWith("http"))
                        {
                            string Link = objPortfolio.Link;
                            objPortfolio.Link = "http://" + Link;
                        }


                        objPortfolio.objImage = new Image();
                        objPortfolio.objImage.ImagePath = "Content/images/Portfolio/" + ImageName;

                        objPortfolio.objImage.ImageName = objPortfolio.Title;
                        objPortfolio.objImage.Description = "Image is for Portfolio " + objPortfolio.Title;
                        objPortfolio.objImage.IsExists = true;
                        objPortfolio.objImage.LastUpdatedBy = Session["UserName"].ToString();
                        objPortfolio.objImage.LastUpdatedOn = DateTime.Now;
                        objPortfolio.objImage.ImageType = "PortfolioImage";

                        //objPortfolio.objServices = new Services();
                        //objPortfolio.objServices.ServiceId = objPortfolio.ServiceId;

                        objPortfolio.IsExists = true;
                        objPortfolio.LastUpdatedBy = Session["UserName"].ToString();
                        objPortfolio.LastUpdatedOn = DateTime.Now;

                        if (_model.InsertPortfolio(objPortfolio))
                        {
                            TempData["ErrorMessage"] = "Saved";
                        }
                        else
                        {
                            ModelState.AddModelError("", _model.ExceptionMessage);

                            TempData["ErrorMessage"] = _model.ExceptionMessage;
                        }

                        ModelState.Clear();
                    }
                }
                catch(Exception exp)
                {
                    ModelState.AddModelError("Error in Inserting Portfolio", exp.Message.ToString());
                }
            }

            return View();
        }


        public ActionResult Portfolio(HttpPostedFileBase file, Portfolio objPortfolio)
        {
            InsertModel _model = new InsertModel();

            ReadModel _readModel = new ReadModel();

            List<Services> _lstServices = _readModel.ReadServices();

            List<SelectListItem> list = new List<SelectListItem>();
            var servlst = (from c in _lstServices select c).ToArray();
            for (int i = 0; i < servlst.Length; i++)
            {
                list.Add(new SelectListItem
                {
                    Text = servlst[i].ServiceName,
                    Value = servlst[i].ServiceId.ToString(),
                });
            }
            ViewData["ServiceLst"] = list;

            if (ModelState.IsValid && Session["UserName"] != null)
            {
                try
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string ImageName = objPortfolio.Title + "-" + objPortfolio.ServiceId + Path.GetExtension(file.FileName);

                        string path = Path.Combine(Server.MapPath("~/Content/images/Portfolio"),
                                                     ImageName);

                        file.SaveAs(path);

                        if (!objPortfolio.Link.StartsWith("http"))
                        {
                            string Link = objPortfolio.Link;
                            objPortfolio.Link = "http://" + Link;
                        }


                        objPortfolio.objImage = new Image();
                        objPortfolio.objImage.ImagePath = "Content/images/Portfolio/" + ImageName;

                        objPortfolio.objImage.ImageName = objPortfolio.Title;
                        objPortfolio.objImage.Description = "Image is for Portfolio " + objPortfolio.Title;
                        objPortfolio.objImage.IsExists = true;
                        objPortfolio.objImage.LastUpdatedBy = Session["UserName"].ToString();
                        objPortfolio.objImage.LastUpdatedOn = DateTime.Now;
                        objPortfolio.objImage.ImageType = "PortfolioImage";

                        //objPortfolio.objServices = new Services();
                        //objPortfolio.objServices.ServiceId = objPortfolio.ServiceId;

                        objPortfolio.IsExists = true;
                        objPortfolio.LastUpdatedBy = Session["UserName"].ToString();
                        objPortfolio.LastUpdatedOn = DateTime.Now;

                        if (_model.InsertPortfolio(objPortfolio))
                        {
                            TempData["ErrorMessage"] = "Saved";
                        }
                        else
                        {
                            ModelState.AddModelError("", _model.ExceptionMessage);

                            TempData["ErrorMessage"] = _model.ExceptionMessage;
                        }

                        ModelState.Clear();
                    }
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("Error in Inserting Portfolio", exp.Message.ToString());
                }
            }

            return View();
        }

        //[HttpPost]
        //public ActionResult InsertSocialLinks(HttpPostedFileBase file)
        //{
        //    string filepath = System.IO.Path.GetFileName(file.FileName);
        //    return View();
        //}

        //public ActionResult InsertSocialLinks(SocialLinks objSocialLinks)
        //{
        //    return View();
        //}

        //[HttpPost] 
        public ActionResult InsertSocialLinks(HttpPostedFileBase file, SocialLinks objSocialLinks)
        {
            InsertModel _model = new InsertModel();

            if (ModelState.IsValid && Session["UserName"] != null)
            {

                if (file != null && file.ContentLength > 0)
                {
                    string path = Path.Combine(Server.MapPath("~/Content/images/SocialMedia"),
                                                 objSocialLinks.LinkType + Path.GetExtension(file.FileName));                   
                    try
                    {
                        
                        file.SaveAs(path);
                                       
                        //string filePath = objSocialLinks.objImage.ImagePath;

                        objSocialLinks.objImage = new Image();
                        objSocialLinks.objImage.ImagePath = "Content/images/SocialMedia/" + objSocialLinks.LinkType + Path.GetExtension(file.FileName);
                        objSocialLinks.objImage.ImageName = objSocialLinks.LinkType;
                        objSocialLinks.objImage.IsExists = true;
                        objSocialLinks.objImage.LastUpdatedBy = Session["UserName"].ToString();
                        objSocialLinks.objImage.LastUpdatedOn = DateTime.Now;
                        objSocialLinks.objImage.ImageType = "Social Link Image";
                        objSocialLinks.objImage.Description = "Social Link Images";

                        objSocialLinks.IsExists = true;
                        objSocialLinks.LastUpdatedBy = Session["UserName"].ToString();
                        objSocialLinks.LastUpdatedOn = DateTime.Now;

                        if (_model.InsertSocialLinks(objSocialLinks))
                        {
                            TempData["ErrorMessage"] = "Saved";
                        }
                        else
                        {
                            ModelState.AddModelError("", _model.ExceptionMessage);

                            TempData["ErrorMessage"] = _model.ExceptionMessage;
                        }

                        ModelState.Clear();
                    }
                    catch (Exception ex)
                    {
                        FileInfo fileinfo = new FileInfo(path);
                        fileinfo.Delete();
                        ModelState.AddModelError("", ex.Message.ToString());
                        
                    }
                }
            }

            return View();
        }

    }
}
