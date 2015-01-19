/*****************************************************************************
 * This file contains a C# helper classes that convert entities between forms
 * understood by the persisetence/data  layer  and the model in presentation
 * layer.
 *
 * Author: Nibras Ahamed Reeza (CB004641)
 * E-Mail: nibras.ahamed@gmail.com
 *
 * Last Modified: 28/08/2014
 *
 *
 * ---------------
 * Version History
 * ---------------
 *
 * 28/08/2014  Created file
 *
******************************************************************************/

using System;
using System.Collections.Generic;
using TheMailClient.Domain.Model;
using TheMailClient.Domain.Model.ObjectFactory;
using TheMailClient.Domain.Services;
using TheMailClient.Storage.DTO;

namespace TheMailClient.Domain.Utils
{
    public class Utils
    {
        private static Utils instance = new Utils();

        private Utils()
        {
        }

        public static Utils getInstance()
        {
            return instance;
        }

        public AccountDTO toDTO(SyncAccount a)
        {
            AccountDTO dto = new AccountDTO();

            dto._namespace = a.Namespace;
            dto.account = a.Account;
            dto.email = a.Email;
            dto.id = a.Id;
            dto.provider = a.Provider;

            return dto;
        }

        public SyncAccount toDomain(AccountDTO a)
        {
            SyncAccountFactory fac = new SyncAccountFactory();

            fac.NamespaceID = a._namespace;
            fac.account = a.account;
            fac.Email = a.email;
            fac.Id = a.id;
            fac.Provider = a.provider;

            return fac.Create();
        }

        public TagDTO toDTO(Tag a)
        {
            TagDTO dto = new TagDTO();

            if (a.Account != null)
                dto._namespace = a.Account.Namespace;
            dto.name = a.Name;
            dto.id = a.Id;

            return dto;
        }

        public Tag toDomain(TagDTO a)
        {
            TagFactory fac = new TagFactory();

            if (a._namespace != null)
                fac.Account = Domain.Services.MailAccountService.getInstance().getAccountByNameSpace(a._namespace);
            fac.Name = a.name;
            fac.Id = a.id;

            return fac.Create();
        }

        public Thread toDomain(ThreadDTO a)
        {
            ThreadFactory t = new ThreadFactory();

            t.Account = Domain.Services.MailAccountService.getInstance().getAccountByNameSpace(a.Account);
            t.Id = a.Id;
            t.Last = a.Last;
            t.Subject = a.Subject;

            foreach (string s in a.Messages)
                t.Messages.Add(MailAccountService.getInstance().GetMessage(s, t.Account));

            foreach (string s in a.DraftMessages)
                t.Drafts.Add(MailAccountService.getInstance().GetDraft(t.Account, s));

            foreach (KeyValuePair<string, string> pair in a.Participants)
                t.Participants.Add(new Participant(pair.Key, pair.Value));
            t.Snippet = a.Snippet;

            t.Started = a.Started;

            foreach (string s in a.Tags)
                t.Tags.Add(TagService.getInstance().getTagById(s, t.Account));

            return t.Create();
        }

        public ThreadDTO toDTO(Thread t)
        {
            ThreadDTO dto = new ThreadDTO();
            dto.Account = t.Account.Namespace;
            dto.Id = t.Id;
            dto.Last = t.Last;
            dto.Subject = t.Subject;
            foreach (Model.Message m in t.Messages)
                dto.Messages.Add(m.Id);

            foreach (Model.DraftMessage m in t.Drafts)
                dto.DraftMessages.Add(m.Id);

            foreach (Participant p in t.Participants)
                dto.Participants.Add(p.Name, p.Email);

            dto.Snippet = t.Snippet;

            dto.Started = t.Started;

            foreach (Tag tag in t.Tags)
                dto.Tags.Add(tag.Name);

            return dto;
        }

        public File toDomain(FileDTO dto)
        {
            FileFactory f = new FileFactory();

            f.Account = MailAccountService.getInstance().getAccountByNameSpace(dto.Namspace_ID);
            f.Id = dto.Id;
            f.URL = dto.URL;
            f.Name = dto.Name;
            f.Size = dto.Size;
            f.Type = dto.Type;

            return f.Create();
        }

        public FileDTO toDTO(File f)
        {
            FileDTO dto = new FileDTO();
            dto.Type = f.Type;
            dto.Size = f.Size;
            dto.Namspace_ID = f.Account.Namespace;
            dto.Name = f.Name;
            dto.Id = f.Id;

            return dto;
        }

        public UserDTO toDTO(User u)
        {
            UserDTO user = new UserDTO();

            user.Address = u.Address;

            user.EMail = u.EMail;

            user.FirstName = u.FirstName;

            user.Theme = u.Config.Theme;

            user.HeaderHeight = u.Config.Header.Height.ToString();
            user.TagHeight = u.Config.Tag.Height.ToString();
            user.ThreadHeight = u.Config.Thread.Height.ToString();

            user.HeaderWidth = u.Config.Header.Width.ToString();
            user.TagWidth = u.Config.Tag.Width.ToString();
            user.ThreadWidth = u.Config.Thread.Width.ToString();

            user.HeaderTop = u.Config.Header.Top.ToString();
            user.TagTop = u.Config.Tag.Top.ToString();
            user.ThreadTop = u.Config.Thread.Top.ToString();

            user.HeaderLeft = u.Config.Header.Left.ToString();
            user.TagLeft = u.Config.Tag.Left.ToString();
            user.ThreadLeft = u.Config.Thread.Left.ToString();

            user.LastName = u.LastName;

            user.Locale = u.Config.UICulture;

            foreach (SyncAccount a in u.accounts)
                user.namespaces.Add(a.Namespace);

            user.Password = u.Password;
            user.Phone = u.Phone;

            if (u is Administrator)
                user.type = UserDTO.UserType.ADMIN;
            else
                user.type = UserDTO.UserType.USER;

            user.Username = u.Username;
            return user;
        }

        public User toDomain(UserDTO dto)
        {
            User u;
            if (dto == null)
                return null;

            if (dto.type.Equals(UserDTO.UserType.ADMIN))
                u = new Administrator();
            else
                u = new User();

            foreach (string a in dto.namespaces)
                u.accounts.Add(MailAccountService.getInstance().getAccountByNameSpace(a));

            u.Address = dto.Address;

            u.Config.Header.Height = Convert.ToInt32(dto.HeaderHeight);
            u.Config.Tag.Height = Convert.ToInt32(dto.TagHeight);
            u.Config.Thread.Height = Convert.ToInt32(dto.ThreadHeight);

            u.Config.Header.Width = Convert.ToInt32(dto.HeaderWidth);
            u.Config.Tag.Width = Convert.ToInt32(dto.TagWidth);
            u.Config.Thread.Width = Convert.ToInt32(dto.ThreadWidth);

            u.Config.Header.Top = Convert.ToInt32(dto.HeaderTop);
            u.Config.Tag.Top = Convert.ToInt32(dto.TagTop);
            u.Config.Thread.Top = Convert.ToInt32(dto.ThreadTop);

            u.Config.Header.Left = Convert.ToInt32(dto.HeaderLeft);
            u.Config.Tag.Left = Convert.ToInt32(dto.TagLeft);
            u.Config.Thread.Left = Convert.ToInt32(dto.ThreadLeft);

            u.Config.Theme = dto.Theme;
            u.Config.UICulture = dto.Locale;

            u.EMail = dto.EMail;
            u.FirstName = dto.FirstName;
            u.LastName = dto.LastName;
            u.Password = dto.Password;

            u.Phone = dto.Phone;
            u.Username = dto.Username;

            return u;
        }

        public Contact toDomain(ContactDTO dto)
        {
            ContactFactory c = new ContactFactory();

            c.StreetAddress = dto.StreetAddress;
            c.ID = dto.ID;
            c.name.FirstName = dto.FirstName;
            c.name.MiddleName = dto.MiddleName;
            c.name.LastName = dto.LastName;
            c.Notes = dto.Notes;
            c.PrimaryPhone = dto.PrimaryPhone;
            c.PrimayEmail = dto.PrimayEmail;

            c.SecondaryEmails.AddRange(dto.SecondaryEmails);
            c.SecondaryPhones.AddRange(dto.SecondaryPhones);
            foreach (string s in dto.Tags)
            {
                TagFactory f = new TagFactory();
                f.Name = s;
                c.Tags.Add(f.Create());
            }

            return c.Create();
        }

        public ContactDTO toDTO(Contact c)
        {
            ContactDTO dto = new ContactDTO();

            dto.FirstName = c.name.FirstName;
            dto.StreetAddress = c.StreetAddress;
            dto.ID = c.ID;
            dto.LastName = c.name.LastName;
            dto.MiddleName = c.name.MiddleName;
            dto.Notes = c.Notes;
            dto.PrimaryPhone = c.PrimaryPhone;
            dto.PrimayEmail = c.PrimayEmail;

            dto.SecondaryEmails.AddRange(c.SecondaryEmails);
            dto.SecondaryPhones.AddRange(c.SecondaryPhones);

            foreach (Tag t in c.Tags)
                if (t != null)
                    dto.Tags.Add(t.Name);

            return dto;
        }

        public MessageDTO toDTO(Message m)
        {
            MessageDTO dto = new MessageDTO();

            dto.Unread = m.Unread;

            dto.Subject = m.Subject;

            foreach (Participant p in m.BCC)
                dto.BCC.Add(p.Name, p.Email);

            dto.Body = m.Body;

            foreach (Participant p in m.CC)
                dto.CC.Add(p.Name, p.Email);

            foreach (Participant p in m.Senders)
                dto.Senders.Add(p.Name, p.Email);

            foreach (Participant p in m.Receivers)
                dto.Receivers.Add(p.Name, p.Email);

            foreach (File f in m.Files)
                dto.Files.Add(f.Id);

            dto.Id = m.Id;
            dto.Namespace = m.account.Namespace;
            dto.Snippet = m.Snippet;
            dto.time = m.time.Ticks.ToString();

            return dto;
        }

        public DraftMessageDTO toDTO(DraftMessage m)
        {
            DraftMessageDTO dto = new DraftMessageDTO();

            dto.Subject = m.Subject;

            foreach (Participant p in m.BCC)
                dto.BCC.Add(p.Name, p.Email);

            dto.Body = m.Body;

            foreach (Participant p in m.CC)
                dto.CC.Add(p.Name, p.Email);

            foreach (Participant p in m.Senders)
                dto.Senders.Add(p.Name, p.Email);

            foreach (Participant p in m.Receivers)
                dto.Receivers.Add(p.Name, p.Email);

            foreach (File f in m.Files)
                dto.Files.Add(f.Id);

            dto.Id = m.Id;
            dto.Namespace = m.account.Namespace;
            dto.Snippet = m.Snippet;
            dto.time = m.time.Ticks.ToString();
            dto.Version = m.Version;
            dto.state = m.state.ToString();

            return dto;
        }

        public Message toDomain(MessageDTO dto)
        {
            MessageFactory fac = new MessageFactory();

            fac.Unread = dto.Unread;

            fac.Subject = dto.Subject;

            foreach (KeyValuePair<string, string> p in dto.BCC)
                fac.BCC.Add(new Participant(p.Key, p.Value));

            fac.Body = dto.Body;

            foreach (KeyValuePair<string, string> p in dto.CC)
                fac.CC.Add(new Participant(p.Key, p.Value));

            foreach (KeyValuePair<string, string> p in dto.Senders)
                fac.Senders.Add(new Participant(p.Key, p.Value));

            foreach (KeyValuePair<string, string> p in dto.Receivers)
                fac.Receivers.Add(new Participant(p.Key, p.Value));

            fac.Id = dto.Id;
            fac.Account = MailAccountService.getInstance().getAccountByNameSpace(dto.Namespace);

            foreach (string f in dto.Files)
                fac.Files.Add(FileService.getInstance().getFile(f, fac.Account));

            fac.Snippet = dto.Snippet;
            fac.time = new DateTime(Convert.ToInt64(dto.time));

            return fac.Create();
        }

        public DraftMessage toDomain(DraftMessageDTO dto)
        {
            MessageFactory fac = new MessageFactory();

            fac.Subject = dto.Subject;

            foreach (KeyValuePair<string, string> p in dto.BCC)
                fac.BCC.Add(new Participant(p.Key, p.Value));

            fac.Body = dto.Body;

            foreach (KeyValuePair<string, string> p in dto.CC)
                fac.CC.Add(new Participant(p.Key, p.Value));

            foreach (KeyValuePair<string, string> p in dto.Senders)
                fac.Senders.Add(new Participant(p.Key, p.Value));

            foreach (KeyValuePair<string, string> p in dto.Receivers)
                fac.Receivers.Add(new Participant(p.Key, p.Value));

            fac.Id = dto.Id;
            fac.Account = MailAccountService.getInstance().getAccountByNameSpace(dto.Namespace);

            foreach (string f in dto.Files)
                fac.Files.Add(FileService.getInstance().getFile(f, fac.Account));

            fac.Snippet = dto.Snippet;
            fac.time = new DateTime(Convert.ToInt64(dto.time));
            fac.Version = dto.Version;

            switch (dto.state)
            {
                case "draft":
                    fac.state = DraftMessage.State.draft;
                    break;

                case "sending":
                    fac.state = DraftMessage.State.sending;
                    break;

                case "sent":
                    fac.state = DraftMessage.State.sent;
                    break;

                default:
                    fac.state = DraftMessage.State.none;
                    break;
            }

            return fac.Create() as DraftMessage;
        }
    }
}