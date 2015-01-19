using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using TheMailClient.Domain.Model;
using TheMailClient.Domain.Services;
using TheMailClient.Presentation.App_Code.Controller;

namespace TheMailClient.Presentation
{
    public partial class manage : ThemedPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            RequireAuth();
            RequireAdmin();

            FillCommonControls(account_links, navigation_bar, lang_selector);

            foreach (User u in UserService.getInstance().GetAllUsers())
                listAccounts.InnerHtml += GenerateRowHtml(u);
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
            listAccounts.InnerHtml = "";
            foreach (User u in UserService.getInstance().GetAllUsers())
                listAccounts.InnerHtml += GenerateRowHtml(u);

            if (Request.Params["delete"] != null)
            {
                string un = Request.Params["delete"];
                if (UserService.getInstance().Find(un) == null)
                    Session["message"] = "Error. Cannot delete user!";

                UserService.getInstance().Delete(un);

                Session["message"] = "User deleted successfully!";
            }

            if (!IsPostBack)
                return;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["message"] != null)
                message.InnerHtml = Session["message"] as string;

            Session["message"] = null;
        }

        public string GenerateRowHtml(User u)
        {
            string html = string.Empty;
            html += "<div class=\"row ui-widget-header ui-corner-all\">";
            html += "<span class=\"user\">" + u.FirstName + " " + u.LastName + "<br />";
            html += u.EMail + "</span>";
            html += "<button value=\"" + GetLocalResourceObject("DeleteVal") + "\" title=\"" + GetLocalResourceObject("DeleteTitle") + "\" runat=\"server\" class=\"delete\" onclick=\"window.location.href='manage.aspx?delete=" + u.Username + "';\" type=\"button\" />";
            html += "<button value=\"" + GetLocalResourceObject("UpdateVal") + "\" title=\"" + GetLocalResourceObject("UpdateTitle") + "\" runat=\"server\"  class=\"update\" onclick=\"window.location.href='update_profile.aspx?user_id=" + u.Username + "';\" type=\"button\" />";
            html += "</div>";

            return html;
        }
    }
}