using System;
using System.Collections.Generic;

namespace TheMailClient.Domain.Model
{
    public class Thread
    {
        public string Subject { get; set; }

        public string Id { get; internal set; }

        public SyncAccount Account { get; internal set; }

        public DateTime Last { get; internal set; }

        public DateTime Started { get; internal set; }

        public List<Participant> Participants { get; private set; }

        public string Snippet { get; internal set; }

        public List<Tag> Tags { get; private set; }

        public List<Message> Messages { get; private set; }

        public List<DraftMessage> Drafts { get; private set; }

        public Thread()
        {
            Participants = new List<Participant>();
            Tags = new List<Tag>();
            Messages = new List<Message>();
            Drafts = new List<DraftMessage>();
        }
    }

    public class Message
    {
        public bool Unread { get; set; }

        public string Subject { get; set; }

        public SyncAccount account { get; set; }

        public string Id { get; internal set; }

        public string Snippet { get; internal set; }

        public string Body { get; set; }

        public List<Participant> Senders { get; private set; }

        public List<Participant> Receivers { get; private set; }

        public List<Participant> CC { get; private set; }

        public List<Participant> BCC { get; private set; }

        public DateTime time { get; internal set; }

        public List<File> Files { get; private set; }

        public Message()
        {
            Senders = new List<Participant>();
            Receivers = new List<Participant>();
            CC = new List<Participant>();
            BCC = new List<Participant>();
            Files = new List<File>();
        }
    }

    public class DraftMessage : Message
    {
        public enum State { none, draft, sending, sent }

        public State state { get; set; }

        public string Version { get; set; }
    }

    public class Participant
    {
        public Participant()
        {
        }

        public Participant(string Name, string Email)
        {
            this.Name = Name;
            this.Email = Email;
        }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}