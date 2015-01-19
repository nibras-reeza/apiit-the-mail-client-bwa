using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using TheMailClient.Domain.Model;
using TheMailClient.Domain.Services;
using TheMailClient.Presentation.App_Code.Controller;

namespace TheMailClient.Presentation
{
    public partial class contacts : ThemedPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
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

            string search = Request.Params["search"];
            string perm = Request.Params["perm"];
            string add = Request.Params["add"];
            string delete = Request.Params["remove"];

            string contact = Request.Params["contact"];

            List<Contact> contacts = new List<Contact>();

            if (perm != null && !(perm.Equals(string.Empty)))
            {
                Contact c = ContactService.getInstance().getContact(contact, currentUser);
                if (c != null)
                    ContactService.getInstance().Delete(c, currentUser);
            }

            if (tag != null && !(tag.Equals(string.Empty)))
            {
                contacts.AddRange(GetContactsByTag(tag));
            }
            else if (search != null && !(search.Equals(string.Empty)))
            {
                contacts.AddRange(SearchContacts(search));
            }
            else
                contacts.AddRange(GetContacts());

            if (add != null && !(add.Equals(string.Empty)))
            {
                AddTag(contact, add);
            }
            else if (delete != null && !(delete.Equals(string.Empty)))
            {
                RemoveTag(contact, delete);
            }

            foreach (Contact t in contacts)
                right_column.InnerHtml += GenerateContactHtml(t);
        }

        private List<Contact> GetContacts()
        {
            List<Contact> contacts = new List<Contact>();

            contacts.AddRange(ContactService.getInstance().getAllContacts(currentUser));

            return contacts;
        }

        private List<Contact> GetContactsByTag(string tag)
        {
            List<Contact> contacts = new List<Contact>();

            Tag tg = ContactService.getInstance().GetTag(tag, currentUser);

            contacts.AddRange(ContactService.getInstance().getAllContacts(tg, currentUser));

            return contacts;
        }

        private List<Contact> SearchContacts(string key)
        {
            List<Contact> contacts = new List<Contact>();

            Contact c = ContactService.getInstance().getContactByEmail(key, currentUser);
            if (c != null)
                contacts.Add(c);
            c = ContactService.getInstance().getContactByName(key, currentUser);
            if (c != null)
                contacts.Add(c);

            return contacts;
        }

        private string GenerateContactHtml(Contact c)
        {
            string html = string.Empty;
            html += "<div class=\"ui-accordion\" title=\"" + c.name.LastName + " " + c.name.FirstName + "\">" + c.name.LastName + " " + c.name.FirstName;
            html += "<button class=\"contactDelete\" onclick=\"window.location.href='contacts.aspx?perm=trash&contact=" + c.ID + "'\">Delete</button>";
            html += "<button class=\"contactUpdate\" onclick=\"window.location.href='contact.aspx?contact=" + c.ID + "'\">Update</button>";
            html += "<button class=\"contactAddTag\" onclick=\"AddTag('" + c.ID + "')\">Add Tag</button>";
            html += "<button class=\"contactRemoveTag\" onclick=\"RemoveTag('" + c.ID + "')\">Remove Tag</button></div>";

            html += "<div class=\"ui-accordion-content meesage_body\">";

            html += "<header>Tags:</Header><br/>";
            foreach (Tag t in c.Tags)
                html += t.Name + ", ";

            html += "<br/><br/>";

            html += "<header>Name:</Header><br/>";
            html += c.name.FirstName + " " + c.name.MiddleName + " " + c.name.LastName + "<br/><br/>";

            html += "<header>Address:</Header><br/>";
            html += c.StreetAddress + "<br/><br/>";

            html += "<header>Phone:</Header><br/>";
            html += c.PrimaryPhone + "<br/><br/>";

            html += "<header>Email:</Header><br/>";
            html += c.PrimayEmail + "<br/><br/>";

            html += "<header>Other phones:</Header><br/>";
            foreach (string s in c.SecondaryPhones)
                html += s;

            html += "<br/><br/>";

            html += "<header>Other emails:</Header><br/>";
            foreach (Tag t in c.Tags)
                foreach (string s in c.SecondaryEmails)
                    html += s;

            html += "<br/><br/>";

            html += "<header>Notes:</Header><br/>";
            html += c.Notes + "<br/><br/>";

            html += "</div>";

            return html;
        }

        private void AddTag(string contact, string tag)
        {
            Contact c = ContactService.getInstance().getContact(contact, currentUser);

            foreach (Tag tg in c.Tags)
                if (tg.Name.Equals(tag))
                    return;

            c.Tags.Add(ContactService.getInstance().GetTag(tag, currentUser));

            ContactService.getInstance().update(c, currentUser);
        }

        private void RemoveTag(string contact, string tag)
        {
            Contact c = ContactService.getInstance().getContact(contact, currentUser);

            for (int i = 0; i < c.Tags.Count; i++)
            {
                Tag tg = c.Tags[i];
                if (tg.Name.Equals(tag))
                {
                    c.Tags.Remove(tg);
                    ContactService.getInstance().update(c, currentUser);
                    return;
                }
            }
        }
    }
}