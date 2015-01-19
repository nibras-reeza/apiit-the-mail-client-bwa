using System;
using System.Collections.Generic;

namespace TheMailClient.Storage.DTO
{
    public class ThreadDTO
    {
        public string Subject { get; set; }

        public string Id { get; set; }

        public string Account { get; set; }

        public DateTime Last { get; set; }

        public DateTime Started { get; set; }

        public Dictionary<string, string> Participants { get; private set; }

        public string Snippet { get; set; }

        public List<string> Tags { get; private set; }

        public List<string> Messages { get; private set; }

        public List<string> DraftMessages { get; private set; }

        public ThreadDTO()
        {
            Participants = new Dictionary<string, string>();
            Tags = new List<string>();
            Messages = new List<string>();
            DraftMessages = new List<string>();
        }

        override public string ToString()
        {
            string str = String.Empty;
            str = String.Concat(str, "Subject = ", Subject, "\r\n");
            str = String.Concat(str, "Id = ", Id, "\r\n");
            str = String.Concat(str, "Account = ", Account, "\r\n");
            str = String.Concat(str, "Last = ", Last, "\r\n");
            str = String.Concat(str, "Started = ", Started, "\r\n");
            str = String.Concat(str, "Participants = ", Participants, "\r\n");
            str = String.Concat(str, "Snippet = ", Snippet, "\r\n");
            str = String.Concat(str, "Tags = ", Tags, "\r\n");
            str = String.Concat(str, "Messages = ", Messages, "\r\n");
            str = String.Concat(str, "DraftMessages = ", DraftMessages, "\r\n");
            return str;
        }
    }

    public class MessageDTO
    {
        public bool Unread { get; set; }

        public string Subject { get; set; }

        public string Namespace { get; set; }

        public string Id { get; set; }

        public string Snippet { get; set; }

        public string Body { get; set; }

        public Dictionary<string, string> Senders { get; private set; }

        public Dictionary<string, string> Receivers { get; private set; }

        public Dictionary<string, string> CC { get; private set; }

        public Dictionary<string, string> BCC { get; private set; }

        public string time { get; set; }

        public List<string> Files { get; private set; }

        public MessageDTO()
        {
            Senders = new Dictionary<string, string>();
            Receivers = new Dictionary<string, string>();
            CC = new Dictionary<string, string>();
            BCC = new Dictionary<string, string>();
            Files = new List<string>();
        }

        override public string ToString()
        {
            string str = String.Empty;
            str = String.Concat(str, "Unread = ", Unread, "\r\n");
            str = String.Concat(str, "Subject = ", Subject, "\r\n");
            str = String.Concat(str, "Namespace = ", Namespace, "\r\n");
            str = String.Concat(str, "Id = ", Id, "\r\n");
            str = String.Concat(str, "Snippet = ", Snippet, "\r\n");
            str = String.Concat(str, "Body = ", Body, "\r\n");
            str = String.Concat(str, "Senders = ", Senders, "\r\n");
            str = String.Concat(str, "Receivers = ", Receivers, "\r\n");
            str = String.Concat(str, "CC = ", CC, "\r\n");
            str = String.Concat(str, "BCC = ", BCC, "\r\n");
            str = String.Concat(str, "time = ", time, "\r\n");
            str = String.Concat(str, "Files = ", Files, "\r\n");
            return str;
        }
    }

    public class DraftMessageDTO : MessageDTO
    {
        public string state { get; set; }

        public string Version { get; set; }

        override public string ToString()
        {
            string str = String.Empty;
            str = String.Concat(str, "Namespace = ", Namespace, "\r\n");
            str = String.Concat(str, "Id = ", Id, "\r\n");
            str = String.Concat(str, "Snippet = ", Snippet, "\r\n");
            str = String.Concat(str, "Body = ", Body, "\r\n");
            str = String.Concat(str, "Senders = ", Senders, "\r\n");
            str = String.Concat(str, "Receivers = ", Receivers, "\r\n");
            str = String.Concat(str, "CC = ", CC, "\r\n");
            str = String.Concat(str, "BCC = ", BCC, "\r\n");
            str = String.Concat(str, "time = ", time, "\r\n");
            str = String.Concat(str, "Files = ", Files, "\r\n");
            str = String.Concat(str, "state = ", state, "\r\n");
            str = String.Concat(str, "Version = ", Version, "\r\n");
            return str;
        }
    }
}