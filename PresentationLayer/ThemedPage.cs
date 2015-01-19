using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using TheMailClient.Domain.Model;
using TheMailClient.Domain.Services;
using TheMailClient.Presentation.App_Code.Controller;

//https://stackoverflow.com/questions/9019997/how-to-change-a-asp-webapplication-theme-not-page-programmatically-and-during
namespace TheMailClient.Presentation
{
    public class ThemedPage : System.Web.UI.Page
    {
        protected User currentUser;

        protected override void OnPreInit(EventArgs e)
        {
            if (Session["User"] != null)
            {
                currentUser = Session["User"] as User;
                Page.Theme = currentUser.Config.Theme;
            }

            if (Session["User"] == null || currentUser.Config.Theme == null || currentUser.Config.Theme.Equals(string.Empty))
                Page.Theme = UIConfig.getInstance().getDefaultTheme();

            base.OnPreInit(e);
        }

        //http://msdn.microsoft.com/en-us/library/system.web.httpresponse.redirect%28v=vs.110%29.aspx
        protected void RequireAuth()
        {
            if (Session["User"] == null)
                Response.Redirect("login.aspx");
        }

        //https://stackoverflow.com/questions/10163238/correctly-send-user-to-404-if-dynamic-content-is-not-found-asp-net-mvc
        //http://www.w3.org/Protocols/HTTP/HTRESP.html
        //https://stackoverflow.com/questions/217678/how-to-generate-an-401-error-programatically-in-an-asp-net-page
        protected void RequireAdmin()
        {
            RequireAuth();
            if (UserService.getInstance().Find(currentUser.Username) is Administrator)
                return;

            Response.StatusCode = 401;
            Response.StatusDescription = "Sorry. You must be an administrator to view this page.";
            Response.End();
        }

        public List<ListItem> GetCultures()
        {//http://www.codeproject.com/Articles/163215/Dynamic-definition-of-the-list-of-available-langua
            List<ListItem> list = new List<ListItem>();

            IList<string> CultureList = null;

            CultureList = UIConfig.getInstance().GetAvailableCultures(
                     System.IO.Path.GetDirectoryName(
                         Request.PhysicalPath)
                         , HttpContext.Current.Request.
               AppRelativeCurrentExecutionFilePath.Substring(2)
                         );

            foreach (string culture in CultureList)
                list.Add(new ListItem(
                  CultureInfo.GetCultureInfo(
                  culture).NativeName.ToLowerInvariant(), culture));

            return list;
        }

        //http://msdn.microsoft.com/en-us/library/vstudio/bz9tc508%28v=vs.100%29.aspx

        protected override void InitializeCulture()
        {
            string selectedLanguage = null;

            if (Session["User"] != null)
            {
                currentUser = Session["User"] as User;
                Page.Theme = currentUser.Config.Theme;
            }

            if (currentUser != null)
                selectedLanguage = currentUser.Config.UICulture;
            else if (Session["locale"] != null)
                selectedLanguage = Session["locale"] as string;

            if (selectedLanguage != null)
            {
                UICulture = selectedLanguage;
                Culture = selectedLanguage;

                System.Threading.Thread.CurrentThread.CurrentCulture =
                    CultureInfo.CreateSpecificCulture(selectedLanguage);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new
                    CultureInfo(selectedLanguage);
            }
            base.InitializeCulture();
        }

        protected void FillCommonControls(BulletedList account, BulletedList navigation, DropDownList locale)
        {
            foreach (ListItem i in GetCultures())
                locale.Items.Add(i);

            if (currentUser != null)
            {
                account.Items.Add(new ListItem(GetGlobalResourceObject("Global", "MyAccount").ToString(), "mails.aspx"));
                account.Items.Add(new ListItem(GetGlobalResourceObject("Global", "Logout").ToString(), "logout.aspx"));
                navigation.Items.Add(new ListItem(GetGlobalResourceObject("Global", "MyProfile").ToString(), "update_profile.aspx?user_id=" + currentUser.Username));
                navigation.Items.Add(new ListItem(GetGlobalResourceObject("Global", "Contacts").ToString(), "contacts.aspx"));
            }
            else
            {
                account.Items.Add(new ListItem(GetGlobalResourceObject("Global", "Login").ToString(), "login.aspx"));
                account.Items.Add(new ListItem(GetGlobalResourceObject("Global", "Register").ToString(), "register.aspx"));
            }

            Dictionary<String, String> links = UIConfig.getInstance().getNavBarItems();

            //https://stackoverflow.com/questions/141088/what-is-the-best-way-to-iterate-over-a-dictionary-in-c
            foreach (KeyValuePair<String, String> link in links)
            {
                navigation.Items.Add(new ListItem(GetGlobalResourceObject("Global", link.Key).ToString(), link.Value));
            }

            if (currentUser is Administrator)
                navigation.Items.Add(new ListItem(GetGlobalResourceObject("Global", "AdminPanel").ToString(), "manage.aspx"));
        }
    }
}