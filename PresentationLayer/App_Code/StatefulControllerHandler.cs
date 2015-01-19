using System.Web;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

//http://www.codeproject.com/Tips/513522/Providing-session-state-in-ASP-NET-WebAPI
//http://www.strathweb.com/2012/11/adding-session-support-to-asp-net-web-api/
namespace TheMailClient.Presentation.App_Code
{
    public class StatefulControllerHandler : HttpControllerHandler, IRequiresSessionState
    {
        public StatefulControllerHandler(RouteData data) : base(data) { }
    }

    public class SessionRouteHandler : IRouteHandler
    {
        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            return new StatefulControllerHandler(requestContext.RouteData);
        }
    }
}