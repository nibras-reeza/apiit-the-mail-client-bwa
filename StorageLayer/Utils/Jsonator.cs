using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMailClient.Storage.DTO;

namespace TheMailClient.Storage.Utils
{
    public class Jsonator
    {
        private Jsonator()
        {
        }

        private static Jsonator instance = new Jsonator();

        public static Jsonator getInstance()
        {
            return instance;
        }

        public AccountDTO fromJson(string json, AccountDTO a)
        {
            dynamic obj = JsonConvert.DeserializeObject(json);

            a.email = obj.email_address;
            a.id = obj.id;
            a.account = obj.account_id;
            a.provider = obj.provider;
            a._namespace = obj.namespace_id;

            return a;
        }

        public string toJson(AccountDTO a)
        {
            string json = "";

            json += "{";

            json += "\"id\": \"" + a.id + "\",\"object\": \"namespace\",\"namespace_id\": \"" + a._namespace + "\",\"account_id\": \"" + a.account + "\",\"email_address\": \"" + a.email + "\",\"provider\": \"" + a.provider + "\"";

            json += "}";
            return json;
        }

        public TagDTO fromJson(string json, TagDTO t)
        {
            dynamic obj = JsonConvert.DeserializeObject(json);

            t._namespace = obj.namespace_id;
            t.id = obj.id;
            t.name = obj.name;

            return t;
        }

        public string toJson(TagDTO a)
        {
            string json = "";

            json += "{";

            json += " \"id\": \"" + a.id + "\",    \"name\": \"" + a.name + "\",    \"namespace_id\": \"" + a._namespace + "\",    \"object\": \"tag\"";

            json += "}";
            return json;
        }

        public ThreadDTO fromJson(string json, ThreadDTO t)
        {
            dynamic obj = JsonConvert.DeserializeObject(json);

            t.Account = obj.namespace_id;
            t.Id = obj.id;
            t.Last = new DateTime(Convert.ToInt32(obj.last_message_timestamp));

            foreach (dynamic message in obj.message_ids)
                t.Messages.Add(message.ToString());

            foreach (dynamic message in obj.draft_ids)
                t.DraftMessages.Add(message.ToString());

            foreach (dynamic part in obj.participants)
                if (!t.Participants.ContainsKey(part.name.ToString()))
                    t.Participants.Add(part.name.ToString(), part.email.ToString());

            t.Snippet = obj.snippet;

            t.Subject = obj.subject;

            foreach (dynamic tag in obj.tags)
                t.Tags.Add(tag.id.ToString());

            return t;
        }

        public MessageDTO fromJson(string json, MessageDTO m)
        {
            dynamic obj = JsonConvert.DeserializeObject(json);

            m.Id = obj.id;
            m.time = new DateTime(Convert.ToInt32(obj.date)).Ticks.ToString();

            foreach (dynamic part in obj.bcc)
                m.BCC.Add(part.name.ToString(), part.email.ToString());

            foreach (dynamic part in obj.cc)
                m.CC.Add(part.name.ToString(), part.email.ToString());

            foreach (dynamic part in obj.from)
                m.Senders.Add(part.name.ToString(), part.email.ToString());

            foreach (dynamic part in obj.to)
                m.Receivers.Add(part.name.ToString(), part.email.ToString());

            m.Namespace = obj.namespace_id;
            m.Snippet = obj.snippet;

            m.Body = obj.body;

            m.Subject = obj.subject;

            m.Unread = (obj.unread.ToString().Equals("true"));

            foreach (dynamic part in obj.files)
                m.Files.Add(part.id.ToString());

            return m;
        }

        public FileDTO fromJson(string json, FileDTO file)
        {
            dynamic obj = JsonConvert.DeserializeObject(json);

            file.Id = obj.id;

            file.Name = obj.filename;
            file.Namspace_ID = obj.namespace_id;
            file.Size = obj.size;
            file.Type = obj.content_type;

            return file;
        }

        public DraftMessageDTO fromJson(string json, DraftMessageDTO m)
        {
            dynamic obj = JsonConvert.DeserializeObject(json);
            m.Namespace = obj.namespace_id;
            m.Id = obj.id;
            // DateTime is 100 nanoseconds but Unix Time stamps are seconds
            //https://stackoverflow.com/questions/249760/how-to-convert-unix-timestamp-to-datetime-and-vice-versa
            System.DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            date = date.AddSeconds(Convert.ToInt32(obj.date.ToString())).ToLocalTime();
            m.time = date.Ticks.ToString();

            foreach (dynamic part in obj.bcc)
                m.BCC.Add(part.name.ToString(), part.email.ToString());

            foreach (dynamic part in obj.cc)
                m.CC.Add(part.name.ToString(), part.email.ToString());

            foreach (dynamic part in obj.to)
                m.Receivers.Add(part.name.ToString(), part.email.ToString());

            m.Snippet = obj.snippet;

            m.Body = obj.body;

            m.Subject = obj.subject;

            m.Unread = (obj.unread.ToString().Equals("true"));

            foreach (dynamic part in obj.files)
                m.Files.Add(part.id.ToString());

            m.Version = obj.version;

            object s = obj.state;

            if (s != null)
                m.state = s.ToString().ToLower();
            else
                m.state = "none";

            return m;
        }

        public string toJson(DraftMessageDTO m)
        {
            string json = "{";

            json += "\"id\":\"" + m.Id + "\",";

            List<string> temp = new List<string>();

            foreach (KeyValuePair<string, string> kv in m.BCC)
                temp.Add("{\"email\":\"" + kv.Value + "\",\"name\":\"" + kv.Key + "\"}");

            json += "\"bcc\":" + JsonConvert.SerializeObject(temp.ToArray()) + ",";

            temp = new List<string>();

            foreach (KeyValuePair<string, string> kv in m.CC)
                temp.Add("{\"email\":\"" + kv.Value + "\",\"name\":\"" + kv.Key + "\"}");

            json += "\"cc\":" + JsonConvert.SerializeObject(temp.ToArray()) + ",";

            temp = new List<string>();

            foreach (KeyValuePair<string, string> kv in m.Receivers)
                temp.Add("{\"email\":\"" + kv.Value + "\",\"name\":\"" + kv.Key + "\"}");

            json += "\"from\":" + JsonConvert.SerializeObject(temp.ToArray()) + ",";

            json += "\"body\":\"" + m.Body + "\",";

            json += "\"files\":\"" + JsonConvert.SerializeObject(m.Files.ToArray()) + "\",";

            json += "\"version\":\"" + m.Version + "\"}";

            return json;
        }
    }
}