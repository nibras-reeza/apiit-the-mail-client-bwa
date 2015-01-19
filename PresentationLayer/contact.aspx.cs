using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using TheMailClient.Domain.Model;
using TheMailClient.Domain.Services;
using TheMailClient.Presentation.App_Code.Controller;

namespace TheMailClient.Presentation
{
    public partial class contact : ThemedPage
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
            string contact = Request.Params["contact"];

            if (IsPostBack)
            {
                Contact c = FormToContact();

                if (c.ID == null || c.ID.Equals(""))
                    ContactService.getInstance().Add(c, currentUser);
                else
                    ContactService.getInstance().update(c, currentUser);
            }
            else if (contact != null && !contact.Equals(""))
                ContactToForm(ContactService.getInstance().getContact(contact, currentUser));
        }

        private Contact FormToContact()
        {
            Contact c = new Contact();
            if (cont_id.Value != null || !cont_id.Value.Equals(""))
                c.ID = cont_id.Value;

            c.name.FirstName = txtFirstName.Value;
            c.name.LastName = txtLastName.Value;
            c.name.MiddleName = txtMiddleName.Value;

            c.StreetAddress = txtAddress.Value;
            c.PrimaryPhone = txtPhone.Value;
            c.PrimayEmail = txtEmail.Value;

            c.Notes = txtNotes.Value;

            foreach (string s in txtSecondaryPhone.Value.Split(','))
                c.SecondaryPhones.Add(s);

            foreach (string s in txtSecondaryEmail.Value.Split(','))
                c.SecondaryEmails.Add(s);

            return c;
        }

        private void ContactToForm(Contact c)
        {
            cont_id.Value = c.ID;

            txtFirstName.Value = c.name.FirstName;
            txtLastName.Value = c.name.LastName;
            txtMiddleName.Value = c.name.MiddleName;

            txtAddress.Value = c.StreetAddress;
            txtPhone.Value = c.PrimaryPhone;
            txtEmail.Value = c.PrimayEmail;

            txtNotes.Value = c.Notes;

            foreach (string s in c.SecondaryPhones)
                txtSecondaryPhone.Value += s + ",";

            foreach (string s in c.SecondaryEmails)
                txtSecondaryEmail.Value += s + ",";
        }
    }
}