using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.XPath;

namespace Interact2World.Models
{
    public class ReadXml
    {
        string DataSource = "";
        string InitialCatalog = "";
        string UserID = "";
        string Password = "";
        string IntegratedSecurity = "";
        string ServerPort = "";
        string TrustedConnection = "";

        string ConnectionString = "";

        public ReadXml()
        {
            DataSource = "";
            InitialCatalog = "";
            UserID = "";
            Password = "";
            IntegratedSecurity = "";
            ServerPort = "";
            TrustedConnection = "";

            ConnectionString = "";
        }

        public string GetConnectionString()
        {
            ReadXML();

            //ConnectionString = String.Format("Data Source={0},{1};Initial Catalog={2};User Id={3};Password={4};Integrated Security={5};Trusted_Connection={6};", DataSource, ServerPort, InitialCatalog, UserID, Password, IntegratedSecurity, TrustedConnection);
            //ConnectionString = String.Format("Data Source={0},{1};Initial Catalog={2};User Id={3};Password={4};Integrated Security={5};", DataSource, ServerPort, InitialCatalog, UserID, Password, IntegratedSecurity);

            ConnectionString = String.Format("Data Source={0};Initial Catalog={2};User Id={3};Password={4};Integrated Security={5};", DataSource, ServerPort, InitialCatalog, UserID, Password, IntegratedSecurity);

            return ConnectionString;
        }

        private void ReadXML()
        {
            string assemblyFile = (new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            var doc = new XPathDocument(AppDomain.CurrentDomain.BaseDirectory + "\\Connection.xml");
            var navigator = doc.CreateNavigator();

            DataSource = navigator.SelectSingleNode("//appsettings/ServerName").ToString();
            ServerPort = navigator.SelectSingleNode("//appsettings/ServerPort").ToString();
            UserID = navigator.SelectSingleNode("//appsettings/UserName").ToString();
            Password = navigator.SelectSingleNode("//appsettings/Password").ToString();
            InitialCatalog = navigator.SelectSingleNode("//appsettings/Database").ToString();
            IntegratedSecurity = navigator.SelectSingleNode("//appsettings/IntegratedSecurity").ToString();
          //  TrustedConnection = navigator.SelectSingleNode("//appsettings/TrustedConnection").ToString();
        }
    }
}