using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using TheMailClient.Domain.Model;
using TheMailClient.Domain.Services;
using TheMailClient.Presentation.App_Code.Controller;

namespace TheMailClient.Presentation
{
    public partial class compose : ThemedPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            FillCommonControls(account_links, navigation_bar, lang_selector);

            foreach (SyncAccount a in currentUser.accounts)

                DropdownFrom.Items.Add(new ListItem(a.Email, a.Namespace));
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
            DraftMessage m = null;
            string draft = Request.Params["draft"];
            string thread = Request.Params["thread"];
            string acc = Request.Params["acc"];
            string act = Request.Params["act"];

            if (act == null || (act.Equals("")))
            {
                if (thread != null && !(thread.Equals("")))
                {
                    m = new DraftMessage();
                    SyncAccount sa = MailAccountService.getInstance().getAccountByNameSpace(acc);
                    m.Receivers.AddRange(MailAccountService.getInstance().getThread(thread, sa).Participants);
                    m.account = sa;
                }
                else
                    if (draft != null && !(draft.Equals("")))
                    {
                        SyncAccount sa = MailAccountService.getInstance().getAccountByNameSpace(acc);
                        if (draft != null && !(draft.Equals("")))
                            m = MailAccountService.getInstance().GetDraft(sa, draft);
                    }
                    else
                        m = new DraftMessage();

                Session["draft"] = m;
            }
            else

                if (act != null && (act.Equals("send")))
                {
                    UploadFiles();
                    m = Session["draft"] as DraftMessage;
                    FormToDraft(m);
                    MailAccountService.getInstance().Send(m);
                }
                else

                    if (act != null && (act.Equals("save")))
                    {
                        UploadFiles();
                        m = Session["draft"] as DraftMessage;
                        FormToDraft(m);
                        m = MailAccountService.getInstance().Update(m);
                        Session["draft"] = m;
                    }
                    else

                        if (act != null && (act.Equals("upload")))
                        {
                            UploadFiles();
                        }
        }

        private void FormToDraft(DraftMessage m)
        {
            m.account = MailAccountService.getInstance().getAccountByNameSpace(DropdownFrom.SelectedValue);
            m.Body = draft_body.Value;
            m.Subject = txtSubject.Text;

            foreach (string s in To.Text.Split(','))
                m.Receivers.Add(new Participant(s, s));

            m.Files.RemoveRange(0, m.Files.Count);
            if (Session["files"] != null)
                foreach (Domain.Model.File file in Session["files"] as List<Domain.Model.File>)
                    m.Files.Add(file);
        }

        private void DraftToForm(DraftMessage m)
        {
            if (m.account != null)
                for (int i = 0; i < DropdownFrom.Items.Count; i++)
                {
                    if (DropdownFrom.Items[i].Value.Equals(m.account.Namespace))
                    {
                        DropdownFrom.SelectedIndex = i;
                        DropdownFrom.Enabled = false;
                    }
                }

            draft_body.Value = m.Body;
            txtSubject.Text = m.Subject;

            To.Text = "";

            foreach (Participant p in m.Receivers)
                To.Text += p.Email + ",";
        }

        private void UploadFiles()
        {
            if (FileUploadControl.HasFile)
            {
                string file = Path.GetFileName(FileUploadControl.FileName);
                FileUploadControl.SaveAs(Server.MapPath("~/") + file);

                TheMailClient.Domain.Model.File f = FileService.getInstance().UploadFile(Server.MapPath("~/") + file, MailAccountService.getInstance().getAccountByNameSpace(DropdownFrom.SelectedValue));

                DropdownFrom.Enabled = false;

                List<TheMailClient.Domain.Model.File> files = Session["files"] as List<TheMailClient.Domain.Model.File>;

                if (files == null)
                {
                    files = new List<Domain.Model.File>();
                    Session["files"] = files;
                }

                files.Add(f);
                Session["files"] = files;
            }

            List<TheMailClient.Domain.Model.File> filesfrmsess = Session["files"] as List<TheMailClient.Domain.Model.File>;

            foreach (TheMailClient.Domain.Model.File file in filesfrmsess)
            {
                attachment_previews.InnerHtml += "<a href=\"" + file.URL + "\"><img src=\"http://free.pagepeeker.com/v2/thumbs.php?size=s&url=" + HttpUtility.UrlEncode(file.URL) + "\" alt=\"Attachment Preview\"</a>";
            }
        }
    }
}