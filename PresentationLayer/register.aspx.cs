﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using TheMailClient.Domain.Model;
using TheMailClient.Domain.Services;
using TheMailClient.Presentation.App_Code.Controller;

namespace TheMailClient.Presentation
{
    public partial class register : ThemedPage
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
                return;

            Page.Validate();

            if (!Page.IsValid)
            {
                Session["message"] = "Invalid captcha. Please try again.";

                return;
            }

            User u2 = UserService.getInstance().Find(txtUsername.Text);
            if (u2 != null && u2.Username != null)
            {
                Session["message"] = "Username already taken. Try another.";
                return;
            }

            User u = new User();

            u.FirstName = txtFirstName.Text;
            u.LastName = txtLastName.Text;
            u.Address = txtAddress.Text;
            u.EMail = txtEmail.Text;
            u.Password = txtPassword.Text;
            u.Phone = txtPhone.Text;
            u.Config.Theme = Page.Theme;
            u.Config.UICulture = System.Globalization.CultureInfo.CurrentUICulture.Name;
            u.Username = txtUsername.Text;

            UserService.getInstance().Add(u);

            Session["message"] = "User registration successful! Click <a href=\"login.aspx\">here</a> to login.";
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