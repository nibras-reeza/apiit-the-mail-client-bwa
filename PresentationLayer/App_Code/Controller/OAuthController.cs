using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TheMailClient.Domain.Model;
using TheMailClient.Domain.Model.ObjectFactory;
using TheMailClient.Domain.Services;
namespace TheMailClient.Presentation.App_Code.Controller
{
    public class OAuthController : ApiController
    {
        // POST api/<controller>
        public string Post([FromBody]string email)
        {
            return OAuthService.GetInstance().GetAuthURL(email);
        }

        // PUT api/<controller>/5
        public void Put([FromUri]string email, [FromBody]string password)
        {
            User u = HttpContext.Current.Session["user"] as User;

            u.accounts.Add(OAuthService.GetInstance().Authenticate(email, password));
            UserService.getInstance().Update(u);
            HttpContext.Current.Session["user"] = u;
        }
    }
}