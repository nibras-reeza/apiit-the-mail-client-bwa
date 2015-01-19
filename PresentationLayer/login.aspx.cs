using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using TheMailClient.Domain.Model;

using TheMailClient.Domain.Services;
using TheMailClient.Presentation.App_Code.Controller;

namespace TheMailClient.Presentation
{
    public partial class login : ThemedPage
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
            if (!IsPostBack)
            {
                if (currentUser != null)
                    Response.Redirect("index.aspx");
                return;
            }

            string username = txtUsername.Value;
            string password = txtPassword.Value;

            if (UserService.getInstance().Authenticate(username, password) == null)
                Session["message"] = "Login failure. Invalid username or password.";
            else
            {
                currentUser = UserService.getInstance().Authenticate(username, password);
                Session["User"] = currentUser;
                Session["message"] = "Login successful. Click <a href=\"index.aspx\">here<a/> to continue.";
            }

            if (remember.Checked)
                Session.Timeout = 60 * 24 * 14;
        }

        //https://stackoverflow.com/questions/2725279/session-objects-not-updating-asp-net
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["message"] != null)
                message.InnerHtml = Session["message"] as string;

            Session["message"] = null;
        }
    }
}