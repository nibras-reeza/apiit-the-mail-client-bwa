using System;

using System.Collections.Generic;

namespace TheMailClient.Domain.Model.ObjectFactory
{
    public class FileFactory
    {
        public SyncAccount Account { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string URL { get; set; }

        public long Size { get; set; }

        public string Type { get; set; }

        public File Create()
        {
            File f = new File();
            f.Account = Account;
            f.Name = Name;
            f.Type = Type;
            f.URL = URL;
            f.Id = Id;
            f.Size = Size;

            return f;
        }
    }

    public class MessageFactory
    {
        public MessageFactory()
        {
            Senders = new List<Participant>();
            Receivers = new List<Participant>();
            CC = new List<Participant>();
            BCC = new List<Participant>();
            Files = new List<File>();
        }

        public bool Unread { get; set; }

        public string Subject { get; set; }

        public SyncAccount Account { get; set; }

        public List<Participant> BCC { get; set; }

        public string Body { get; set; }

        public List<Participant> CC { get; set; }

        public List<File> Files { get; set; }

        public string Id { get; set; }

        public List<Participant> Receivers { get; set; }

        public List<Participant> Senders { get; set; }

        public string Snippet { get; set; }

        public DraftMessage.State state { get; set; }

        public DateTime time { get; set; }

        public string Version { get; set; }

        public Message Create()
        {
            Message m = null;

            if (state != DraftMessage.State.none && Version != null)
            {
                m = new DraftMessage();
                ((DraftMessage)m).state = state;
                ((DraftMessage)m).Version = Version;
            }
            else
                m = new DraftMessage();

            m.Subject = Subject;
            m.Unread = Unread;
            m.Id = Id;
            m.Snippet = Snippet;
            m.Body = Body;
            m.Senders.AddRange(Senders);
            m.Receivers.AddRange(Receivers);
            m.CC.AddRange(CC);
            m.BCC.AddRange(BCC);
            m.time = time;
            m.Files.AddRange(Files);

            m.account = Account;

            return m;
        }
    }

    public class SyncAccountFactory
    {
        public string account { get; set; }

        public string Email { get; set; }

        public string Id { get; set; }

        public string NamespaceID { get; set; }

        public string Provider { get; set; }

        public SyncAccount Create()
        {
            SyncAccount acc = new SyncAccount(Email);
            acc.Namespace = NamespaceID;
            acc.Account = account;
            acc.Email = Email;
            acc.Id = Id;
            acc.Provider = Provider;

            return acc;
        }
    }

    public class TagFactory
    {
        public SyncAccount Account { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public Tag Create()
        {
            Tag t = new Tag();
            t.Name = Name;
            t.Id = Id;
            t.Account = Account;
            return t;
        }
    }

    public class ThreadFactory
    {
        public ThreadFactory()
        {
            Participants = new List<Participant>();
            Tags = new List<Tag>();
            Messages = new List<Message>();
            Drafts = new List<DraftMessage>();
        }

        public string Subject { get; set; }

        public SyncAccount Account { get; set; }

        public string Id { get; set; }

        public DateTime Last { get; set; }

        public List<Message> Messages { get; set; }

        public List<Participant> Participants { get; set; }

        public string Snippet { get; set; }

        public DateTime Started { get; set; }

        public List<Tag> Tags { get; set; }

        public List<DraftMessage> Drafts { get; set; }

        public Thread Create()
        {
            Thread t = new Thread();
            t.Subject = Subject;

            t.Id = Id;
            t.Account = Account;
            t.Last = Last;
            t.Started = Started;
            t.Participants.AddRange(Participants);
            t.Snippet = Snippet;
            t.Tags.AddRange(Tags);
            t.Messages.AddRange(Messages);
            t.Drafts.AddRange(Drafts);
            return t;
        }
    }

    public class ContactFactory
    {
        public string ID { get; set; }

        public string StreetAddress { get; set; }

        public class Name
        {
            public string FirstName { get; set; }

            public string MiddleName { get; set; }

            public string LastName { get; set; }
        }

        public ContactFactory()
        {
            name = new Name();
            SecondaryEmails = new List<string>();
            SecondaryPhones = new List<string>();
            Tags = new List<Tag>();
        }

        public Name name { get; private set; }

        public string PrimayEmail { get; set; }

        public List<string> SecondaryEmails { get; private set; }

        public string PrimaryPhone { get; set; }

        public List<string> SecondaryPhones { get; private set; }

        public string Notes { get; set; }

        public List<Tag> Tags { get; private set; }

        public Contact Create()
        {
            Contact c = new Contact();
            c.ID = ID;
            c.name.FirstName = name.FirstName;
            c.name.MiddleName = name.MiddleName;
            c.name.LastName = name.LastName;

            c.PrimayEmail = PrimayEmail;
            c.PrimaryPhone = PrimaryPhone;

            c.SecondaryEmails.AddRange(SecondaryEmails);
            c.SecondaryPhones.AddRange(SecondaryPhones);
            c.Notes = Notes;

            c.StreetAddress = StreetAddress;

            c.Tags.AddRange(Tags);

            return c;
        }
    }
}