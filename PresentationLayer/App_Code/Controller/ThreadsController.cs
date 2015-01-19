using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TheMailClient.Domain.Model;
using TheMailClient.Domain.Services;

namespace TheMailClient.Presentation.App_Code.Controller
{
    public class ThreadsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Thread> Get()
        {
            User u = HttpContext.Current.Session["user"] as User;

            if (u == null || u.accounts.Count == 0)
                return null;

            List<Thread> threads = new List<Thread>();
            foreach (SyncAccount acc in u.accounts)
                threads.AddRange(MailAccountService.getInstance().getAllThreads(acc, 20, 0));

            return threads.AsEnumerable();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}