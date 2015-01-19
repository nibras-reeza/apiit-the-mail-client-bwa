using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TheMailClient.Domain.Model;
using TheMailClient.Domain.Model.ObjectFactory;
using TheMailClient.Domain.Services;
namespace TheMailClient.Presentation
{
    public class TagsController : ApiController
    {
        public class TagNameComparer : IEqualityComparer<Tag>
        {
            public bool Equals(Tag tag1, Tag tag2)
            {
                if (tag1.Name.Equals(tag2.Name))
                    return true;
                return false;
            }
            public int GetHashCode(Tag tag)
            {
                return tag.Name.GetHashCode();
            }
        }
        public IEnumerable<Tag> GetAllTags()
        {
            User u = HttpContext.Current.Session["User"] as User;

            if (u == null || u.accounts.Count == 0)
                return null;

            List<Tag> tags = new List<Tag>();
            foreach (SyncAccount acc in u.accounts)
                tags.AddRange(TagService.getInstance().getTags(acc));

            List<Tag> uniques = new List<Tag>();

            foreach (Tag t in tags)
                if (!uniques.Contains(t, new TagNameComparer()))
                    uniques.Add(t);

            return uniques.AsEnumerable();
        }

        public Tag GetTag(string query)
        {
            // Retrieve tags from memory.
            List<Tag> tags = HttpContext.Current.Session["tags"] as List<Tag>;

            // If tags are in session, search through them.
            if (tags != null)
                foreach (Tag t in tags)
                    if (t.Name == query || t.Id == query)
                        return t;

            // Look for tag in server.

            User u = HttpContext.Current.Session["user"] as User;

            if (u == null || u.accounts.Count == 0)
                return null;

            // Check the server in case a new tag has been added elsewhere.
            Tag tag = null;
            foreach (SyncAccount acc in u.accounts)
            {
                tag = TagService.getInstance().getTagByName(query, acc);

                if (tag == null)
                    tag = TagService.getInstance().getTagById(query, acc);
            }

            return tag;
        }

        // POST api/<controller>
        [HttpPost]
        public void Create(string query)
        {
            User u = HttpContext.Current.Session["User"] as User;

            if (u == null || u.accounts.Count == 0)
                return;

            TagFactory t = new TagFactory();
            t.Name = query;

            foreach (SyncAccount acc in u.accounts)
            {
                t.Account = acc;
                TagService.getInstance().Add(t.Create(), acc);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public Tag Rename(string query
            , [FromUri]string newname)
        {
            foreach (Tag t in GetAllTags())
                if (t.Name.Equals(query) || t.Id.Equals(query))
                {
                    t.Name = newname;
                    TagService.getInstance().Update(t, t.Account);
                }

            return GetTag(newname);
        }

        // DELETE api/<controller>/5
        [HttpPut]
        public void Delete(string query, [FromUri]string delete)
        {
            User u = HttpContext.Current.Session["User"] as User;
            if (u == null || u.accounts.Count == 0)
                return;

            foreach (SyncAccount acc in u.accounts)
            {
                List<Tag> tags = new List<Tag>();
                tags.AddRange(TagService.getInstance().getTags(acc));
                foreach (Tag t in tags)
                    if (t.Name.Equals(query) || t.Id.Equals(query))
                    {
                        TagService.getInstance().Delete(t, t.Account);
                    }
            }
        }
    }
}