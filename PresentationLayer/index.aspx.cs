using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using TheMailClient.Domain.Model;
using TheMailClient.Domain.Services;
using TheMailClient.Presentation.App_Code.Controller;

namespace TheMailClient.Presentation
{
    public partial class index : ThemedPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            RequireAuth();
            FillCommonControls(account_links, navigation_bar, lang_selector);
        }

        protected void lang_selector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentUser != null)
            {
                currentUser.Config.UICulture = lang_selector.SelectedValue;
                //UserService.getInstance().Update(currentUser);
            }

            Session["locale"] = lang_selector.SelectedValue;

            Response.Redirect(Request.RawUrl);
            Response.Flush();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string tag = Request.Params["tag"];
            string page = Request.Params["page"];
            string search = Request.Params["search"];
            string add = Request.Params["add"];
            string delete = Request.Params["remove"];
            string acc = Request.Params["acc"];
            string thread = Request.Params["thread"];
            int offset = 0;

            if (page != null && !(page.Equals(string.Empty)))
                offset = Convert.ToInt16(offset);

            List<Thread> threads = new List<Thread>();

            if (tag != null && !(tag.Equals(string.Empty)))
            {
                threads.AddRange(GetThreadsByTag(tag, offset));
            }
            else if (search != null && !(search.Equals(string.Empty)))
            {
                threads.AddRange(SearchThreads(search, offset));
            }
            else
                threads.AddRange(GetThreads(offset));

            if (add != null && !(add.Equals(string.Empty)))
            {
                AddTag(acc, thread, add);
            }
            else if (delete != null && !(delete.Equals(string.Empty)))
            {
                RemoveTag(acc, thread, delete);
            }

            foreach (Thread t in threads)
                right_column.InnerHtml += GenerateThreadHtml(t);

            right_column.InnerHtml += "<a href=\"index.aspx?page=" + (offset + 1).ToString() + "\">Next Page</a>";
        }

        private List<Thread> GetThreads(int page)
        {
            List<Thread> threads = new List<Thread>();

            foreach (SyncAccount acc in currentUser.accounts)
                threads.AddRange(MailAccountService.getInstance().getAllThreads(acc, 10, page));

            return threads;
        }

        private List<Thread> GetThreadsByTag(string tag, int page)
        {
            List<Thread> threads = new List<Thread>();
            string[] fields = new string[] { "tag" };

            foreach (SyncAccount acc in currentUser.accounts)
                threads.AddRange(MailAccountService.getInstance().getAllThreads(acc, 10, page, tag, fields));

            return threads;
        }

        private List<Thread> SearchThreads(string key, int page)
        {
            List<Thread> threads = new List<Thread>();
            string[] fields = new string[] { "subject", "any_email", "to", "from", "cc", "bcc", "filename" };

            foreach (SyncAccount acc in currentUser.accounts)
                threads.AddRange(MailAccountService.getInstance().getAllThreads(acc, 10, page, key, fields));

            return threads;
        }

        private string GenerateThreadHtml(Thread t)
        {
            string html = string.Empty;
            html += "<div class=\"ui-accordion\" title=\"" + t.Snippet + "\">" + t.Subject;
            html += "<button class=\"threadDelete\" onclick=\"window.location.href='index.aspx?add=trash&thread=" + t.Id + "&acc=" + t.Account.Namespace + "'\">Delete</button>";
            html += "<button class=\"threadReply\" onclick=\"window.location.href='compose.aspx?thread=" + t.Id + "acc=" + t.Account.Namespace + "'\">Reply</button>";
            html += "<button class=\"threadAddTag\" onclick=\"AddTag('" + t.Account.Namespace + "','" + t.Id + "')\">Add Tag</button>";
            html += "<button class=\"threadRemoveTag\" onclick=\"RemoveTag('" + t.Account.Namespace + "','" + t.Id + "')\">Remove Tag</button></div>";

            html += "<div class=\"ui-accordion-content meesage_body\">";
            html += "<header>Participants:</Header>";
            foreach (Participant p in t.Participants)
                html += p.Name + "(" + p.Email + "),";
            html += "<br/>";

            html += "<br/>";

            html += "<header>Started :</Header>";

            html += t.Started.ToString();

            html += "<br/>";
            html += "<header>Last Message :</Header>";

            html += t.Last.ToString();

            html += "<br/>";

            html += "<header>Tags:</Header>";
            foreach (Tag tag in t.Tags)
                if (tag != null)
                    html += tag.Name + ", ";

            html += "<hr/>";
            foreach (DraftMessage m in t.Drafts)
                html += "<div class=\"accordion\">" + GenerateDraftMessageHtml(m) + "</div>";
            foreach (Message m in t.Messages)
                html += "<div class=\"accordion\">" + GenerateMessageHtml(m) + "</div>";
            html += "</div>";

            return html;
        }

        private string GenerateMessageHtml(Message m)
        {
            string html = string.Empty;
            html += "<div class=\"ui-accordion\" title=\"" + m.Snippet + "\">" + m.Subject + "</div>";
            html += "      <div class=\"ui-accordion-content meessage_body\">";
            html += "<header>From:</Header>";
            foreach (Participant p in m.Senders)
                html += p.Name + "(" + p.Email + "),";
            html += "<br/>";
            html += "<header>CC:</Header>";
            foreach (Participant p in m.CC)
                html += p.Name + "(" + p.Email + "),";
            html += "<br/>";

            html += "<header>Sent :</Header>";

            html += m.time.ToString();

            html += "<br/>";
            html += "<hr/>";
            foreach (File f in m.Files)
                html += "<a href=\"" + f.URL + "\"><img src=\"http://api.webthumbnail.org?width=120&height=120&screen=1280&format=png&url=" + f.URL + "\" alt=\"Attachment Preview\"/></a>";
            html += "<hr/>";
            html += m.Body + "</div>";

            return html;
        }

        private string GenerateDraftMessageHtml(DraftMessage m)
        {
            string html = string.Empty;
            html += "<div class=\"ui-accordion\" title=\"" + m.Snippet + "\">" + m.Subject + "</div>";
            html += "      <div class=\"ui-accordion-content meessage_body\">";
            html += "<a href=\"compose.aspx?draft=" + m.Id + "&acc=" + m.account.Namespace + "\">Edit and Send</a>";
            html += "<header>From:</Header>";
            foreach (Participant p in m.Senders)
                html += p.Name + "(" + p.Email + "),";
            html += "<br/>";
            html += "<header>CC:</Header>";
            foreach (Participant p in m.CC)
                html += p.Name + "(" + p.Email + "),";
            html += "<br/>";

            html += "<header>Sent :</Header>";

            html += m.time.ToString();

            html += "<br/>";

            html += "<hr/>";

            foreach (File f in m.Files)
                html += "<a href=\"" + f.URL + "\"><img src=\"http://free.pagepeeker.com/v2/thumbs.php?size=s&url=" + HttpUtility.UrlEncode(f.URL) + "\" alt=\"Attachment Preview\"</a>";

            html += "<hr/>";
            html += m.Body + "</div>";

            return html;
        }

        private void AddTag(string account, string thread, string tag)
        {
            SyncAccount acc = MailAccountService.getInstance().getAccountByNameSpace(account);
            Thread thrd = MailAccountService.getInstance().getThread(thread, acc);

            foreach (Tag tg in thrd.Tags)
                if (tg.Name.Equals(tag))
                    return;

            thrd.Tags.Add(TagService.getInstance().getTagByName(tag, acc));

            MailAccountService.getInstance().UpdateThread(thrd);
        }

        private void RemoveTag(string account, string thread, string tag)
        {
            SyncAccount acc = MailAccountService.getInstance().getAccountByNameSpace(account);
            Thread t = MailAccountService.getInstance().getThread(thread, acc);

            foreach (Tag tg in t.Tags)
                if (tg.Name.Equals(tag))
                {
                    t.Tags.Remove(tg);
                    MailAccountService.getInstance().UpdateThread(t);
                }
        }
    }
}