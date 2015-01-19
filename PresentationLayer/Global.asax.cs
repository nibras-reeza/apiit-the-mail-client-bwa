using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

using  TheMailClient.Presentation.App_Code;

namespace TheMailClient.Presentation
{
    public class Global : System.Web.HttpApplication
    {
        

        protected void Application_Start(object sender, EventArgs e)
        {
            
            //http://www.asp.net/web-api/overview/advanced/configuring-aspnet-web-api
            //http://www.codeproject.com/Tips/513522/Providing-session-state-in-ASP-NET-WebAPI
            //http://www.strathweb.com/2012/11/adding-session-support-to-asp-net-web-api/
           
                

                RouteTable.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{query}",
                    defaults: new { query = RouteParameter.Optional }
                   
                ).RouteHandler = new SessionRouteHandler();

                

                

             
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}