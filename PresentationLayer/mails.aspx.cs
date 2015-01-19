using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using TheMailClient.Domain.Model;
using TheMailClient.Domain.Services;
using TheMailClient.Presentation.App_Code.Controller;

namespace TheMailClient.Presentation
{
    public partial class mails : ThemedPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            RequireAuth();

            FillCommonControls(account_links, navigation_bar, lang_selector);

            foreach (SyncAccount acc in currentUser.accounts)
                listAccounts.InnerHtml += GenerateRowHtml(acc);
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
            foreach (SyncAccount a in currentUser.accounts)
                listAccounts.InnerHtml += GenerateRowHtml(a);

            if (Request.Params["delete"] != null)
            {
                string ac_id = Request.Params["delete"];

                SyncAccount acc = MailAccountService.getInstance().getAccountByNameSpace(ac_id);

                currentUser.removeAccount(acc.Email);

                Session["User"] = UserService.getInstance().Update(currentUser);
                Session["message"] = "Account deleted successfully!";
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

        public string GenerateRowHtml(SyncAccount acc)
        {
            string html = string.Empty;
            html += "<div class=\"row ui-widget-header ui-corner-all\">";
            html += "<span class=\"user\">" + acc.Email + "<br />";
            html += acc.Provider + "</span>";
            html += "<button value=\"" + GetLocalResourceObject("DeleteVal") + "\" title=\"" + GetLocalResourceObject("DeleteTitle") + "\" runat=\"server\" class=\"delete\" onclick=\"window.location.href='mails.aspx?delete=" + acc.Id + "';\" type=\"button\" />";

            html += "</div>";

            return html;
        }
    }
}