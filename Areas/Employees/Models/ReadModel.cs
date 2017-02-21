using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogic;
using Data;

namespace Interact2World.Areas.Employees.Models
{
    public class ReadModel
    {
        public string ExceptionMessage { get; set; }

        public List<Services> ReadServices()
        {
            List<Services> _lstServices = new List<Services>();
            try
            {
                bl_Services _objServices = new bl_Services();

                _lstServices = _objServices.ReadServices();
            }
            catch (Exception exp)
            {
                ExceptionMessage = exp.Message;
            }

            return _lstServices;
        }

        public List<Contacts> ReadContacts(string ContactId, string ContactOf)
        {
            List<Contacts> _lstContacts = new List<Contacts>();

            try
            {
                bl_Contacts _objContacts = new bl_Contacts();
                _lstContacts = _objContacts.ReadContacts(ContactId, ContactOf);
            }
            catch(Exception exp)
            {
                ExceptionMessage = exp.Message;
            }

            return _lstContacts;
        }

        public List<Portfolio> ReadExistingPortfolio()
        {
            List<Portfolio> _lstPortfolio = new List<Portfolio>();
            try
            {
                bl_Portfolio _objPortfolio = new bl_Portfolio();
                _lstPortfolio = _objPortfolio.ReadExistingPortfolio();
            }
            catch (Exception exp)
            {
                ExceptionMessage = exp.Message;
            }
            return _lstPortfolio;
        }

        public List<SocialLinks> ReadSocialLinks()
        {
            List<SocialLinks> _lstSocialLinks = new List<SocialLinks>();

            try
            {
                bl_SocialLinks _objSocialLinks = new bl_SocialLinks();

                _lstSocialLinks = _objSocialLinks.ReadSocialLinks();
            }
            catch (Exception exp)
            {
                ExceptionMessage = exp.Message;
            }

            return _lstSocialLinks;
        }

        public List<Portfolio> ReadCompletePortfolio()
        {
            List<Portfolio> _lstPortfolio = new List<Portfolio>();
            try
            {
                bl_Portfolio _objPortfolio = new bl_Portfolio();
                _lstPortfolio = _objPortfolio.ReadCompletePortfolio();
            }
            catch (Exception exp)
            {
                ExceptionMessage = exp.Message;
            }
            return _lstPortfolio;
        }
    }
}