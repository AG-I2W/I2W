using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BusinessLogic;
using Data;

namespace Interact2World.Models
{
    public class InsertModel
    {
        public MenuItem _objMenuData = new MenuItem();

        //[Required(ErrorMessage = "User Name is Required")]
        //[Display(Name = "MenuName")]
        //public string MenuName { get; set; }

        public string ExceptionMessage { get; set; }

        public bool InsertMenu(MenuItem _objMenuData)
        {
            try
            {
                bl_Menu _objMenu = new bl_Menu();

                return _objMenu.InserMenu(_objMenuData);
            }
            catch(Exception exp)
            {
                ExceptionMessage = exp.Message;
                return false;
            }
        }
    }
}