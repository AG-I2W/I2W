using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogic;
using Data;

namespace Interact2World.Areas.Employees.Models
{
    public class InsertModel
    {
        public string ExceptionMessage { get; set; }

        public bool InsertMenu(MenuItem _objMenuData)
        {
            try
            {
                bl_Menu _objMenu = new bl_Menu();

                return _objMenu.InserMenu(_objMenuData);
            }
            catch (Exception exp)
            {
                ExceptionMessage = exp.Message;
                return false;
            }
        }



        public bool InsertServices(Services _objServiceData)
        {
            try
            {
                bl_Services _objServices = new bl_Services();              

                return _objServices.InsertServices(_objServiceData);
            }
            catch (Exception exp)
            {
                ExceptionMessage = exp.Message;
                return false;
            }
        }


        public bool InsertContacts(Contacts _objContactsData)
        {
            try
            {
                bl_Contacts _objContacts = new bl_Contacts();

                return _objContacts.InsertContacts(_objContactsData);
            }
            catch(Exception exp)
            {
                ExceptionMessage = exp.Message;
                return false;
            }
        }

        public bool InsertPortfolio(Portfolio objPortfolio)
        {
            try
            {
                bl_Portfolio _objPortfolio = new bl_Portfolio();
                return _objPortfolio.InsertPortfolio(objPortfolio);
            }
            catch(Exception exp)
            {
                ExceptionMessage = exp.Message;
                return false;
            }
        }

        public bool Portfolio(Portfolio objPortfolio)
        {
            try
            {
                bl_Portfolio _objPortfolio = new bl_Portfolio();
                return _objPortfolio.InsertPortfolio(objPortfolio);
            }
            catch (Exception exp)
            {
                ExceptionMessage = exp.Message;
                return false;
            }
        }

        public bool InsertSocialLinks(SocialLinks objSocialLinks)
        {
            try
            {
                bl_SocialLinks _objSocialLinks = new bl_SocialLinks();
                return _objSocialLinks.InsertSocialLinks(objSocialLinks);
            }
            catch(Exception exp)
            {
                ExceptionMessage = exp.Message;
                return false;
            }
        }
    }
}