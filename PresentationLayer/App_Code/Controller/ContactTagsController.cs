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
    public class ContactTagsController : ApiController
    {
        public IEnumerable<Tag> GetAllTags()
        {
            User u = HttpContext.Current.Session["user"] as User;

            if (u == null || u.accounts.Count == 0)
                return null;

            List<Tag> tags = new List<Tag>();

            tags.AddRange(ContactService.getInstance().GetAllTags(u));

            return tags.AsEnumerable();
        }

        // PUT api/<controller>/5
        [HttpPut]
        public void Rename(string query
            , [FromUri]string newname)
        {
            User u = HttpContext.Current.Session["user"] as User;
            foreach (Tag t in GetAllTags())
                if (t.Name.Equals(query) || t.Id.Equals(query))
                {
                    List<Contact> con = ContactService.getInstance().getAllContacts(t, u);

                    foreach (Contact c in con)
                    {
                        TagFactory tf = new TagFactory();
                        tf.Id = newname;
                        tf.Name = newname;

                        for (int i = 0; i < c.Tags.Count; i++)
                        {
                            Tag tg = c.Tags[i];
                            if (tg.Name.Equals(t.Name))
                                c.Tags.Remove(tg);
                        }
                        c.Tags.Add(tf.Create());

                        ContactService.getInstance().update(c, u);
                    }
                }
        }

        // DELETE api/<controller>/5
        [HttpPut]
        public void Delete(string query, [FromUri]string delete)
        {
            User u = HttpContext.Current.Session["user"] as User;
            foreach (Tag t in GetAllTags())
                if (t.Name.Equals(query) || t.Id.Equals(query))
                {
                    List<Contact> con = ContactService.getInstance().getAllContacts(t, u);

                    foreach (Contact c in con)
                    {
                        c.Tags.Remove(t);

                        ContactService.getInstance().update(c, u);
                    }
                }
        }
    }
}