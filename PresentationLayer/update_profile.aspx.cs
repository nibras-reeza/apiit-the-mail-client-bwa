using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;
using TheMailClient.Domain.Model;
using TheMailClient.Domain.Services;
using TheMailClient.Presentation.App_Code.Controller;

namespace TheMailClient.Presentation
{
    public partial class update_profile : ThemedPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            RequireAuth();

            if (!(currentUser is Administrator))
                comboType.Enabled = false;

            FillCommonControls(account_links, navigation_bar, lang_selector);
            foreach (KeyValuePair<string, string> kv in UIConfig.getInstance().getThemes())
                comboTheme.Items.Add(new ListItem(kv.Key, kv.Value));

            foreach (ListItem i in GetCultures())
                comboLocale.Items.Add(i);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["user_id"] != null)
            {
                TitleAddUpdate.Text = GetLocalResourceObject("UpdateUser").ToString();
                if (!(currentUser).Username.Equals(Request.Params["user_id"] as string))
                    RequireAdmin();
                if (IsPostBack)
                {
                    UpdateUser();
                    return;
                }
                User u = UserService.getInstance().Find(Request.Params["user_id"]);

                txtFirstName.Text = u.FirstName;
                txtLastName.Text = u.LastName;

                txtAddress.Text = u.Address;
                txtEmail.Text = u.EMail;
                txtPhone.Text = u.Phone;

                txtUsername.Text = u.Username;
            }
            else
                TitleAddUpdate.Text = GetLocalResourceObject("AddUser").ToString();

            if (!IsPostBack)
                return;

            AddUser();
        }

        //https://stackoverflow.com/questions/2725279/session-objects-not-updating-asp-net
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["message"] != null)
                message.InnerHtml = Session["message"] as string;

            Session["message"] = null;
        }

        private void AddUser()
        {
            if (UserService.getInstance().Find(txtUsername.Text).Username != null)
            {
                Session["message"] = "Username already taken. Try another.";
                return;
            }

            string pw = txtPassword.Text;
            if (pw != null && (!pw.Equals("")))
            {
                Session["message"] = "Password cannot be empty!";
                return;
            }

            User u = CreateUser(new User());

            u.Password = pw;

            UserService.getInstance().Add(u);

            Session["message"] = "User registration successful! Click <a href=\"login.aspx\">here</a> to login.";
        }

        private User CreateUser(User u)
        {
            u.FirstName = txtFirstName.Text;
            u.LastName = txtLastName.Text;
            u.Address = txtAddress.Text;
            u.EMail = txtEmail.Text;

            u.Phone = txtPhone.Text;
            u.Config.Theme = comboTheme.SelectedItem.Value;
            u.Config.UICulture = comboLocale.SelectedItem.Value;
            u.Username = txtUsername.Text;

            return u;
        }

        private void UpdateUser()
        {
            User u = CreateUser(UserService.getInstance().Find(Request.Params["user_id"]));

            string pw = txtPassword.Text;
            if (pw != null && (!pw.Equals("")))
                u.Password = pw;
            else
                u.Password = UserService.getInstance().Find(Request.Params["user_id"].ToString()).Password;

            UserService.getInstance().Update(u);

            Session["message"] = "User details changed succesfully!";
            Session["User"] = u;
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
    }
}