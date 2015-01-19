using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TheMailClient.Storage.DTO;
using TheMailClient.Storage.Utils;

namespace TheMailClient.Storage
{
    public class MailDataStore
    {
        private const string BASE_URL = "http://5.231.64.215/";
        private static MailDataStore instance = new MailDataStore();

        private List<AccountDTO> accounts = new List<AccountDTO>();

        //WebClient is instantiated as required since it's not thread safe.

        private MailDataStore()
        {
        }

        public static MailDataStore getInstance()
        {
            return instance;
        }

        public TagDTO add(TagDTO tag, string namespaceid)
        {
            using (WebClient client = new WebClient())
            {
                //https://stackoverflow.com/questions/5401501/how-to-post-data-to-specific-url-using-webclient-in-c-sharp
                client.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                string body = "{\"name\":\"" + tag.name + "\"}";
                string response = client.UploadString(BASE_URL + "n/" + namespaceid + "/tags", "POST", body);

                return Jsonator.getInstance().fromJson(response, new TagDTO());
            }
        }

        public List<DraftMessageDTO> GetAllDrafts(string namespaceid)
        {
            List<DraftMessageDTO> list = new List<DraftMessageDTO>();

            using (WebClient client = new WebClient())
            {
                string url = BASE_URL + "n/" + namespaceid + "/drafts";

                string response = client.DownloadString(url);

                dynamic obj = JsonConvert.DeserializeObject(response);

                foreach (dynamic itm in obj)
                    list.Add(Jsonator.getInstance().fromJson(itm.ToString(), new DraftMessageDTO()));
            }

            return list;
        }

        public List<AccountDTO> getAllMailAccounts()
        {
            using (WebClient client = new WebClient())
            {
                string response = client.DownloadString(BASE_URL + "n");

                dynamic accnts = JsonConvert.DeserializeObject(response);

                foreach (dynamic acct in accnts)
                {
                    accounts.Add(GetMailAccount(acct.namespace_id.ToString()));
                }

                return accounts;
            }
        }

        public List<MessageDTO> GetAllMessages(string thread, string namespaceid)
        {
            List<MessageDTO> msgs = new List<MessageDTO>();
            using (WebClient client = new WebClient())
            {
                client.QueryString.Add("thread_id", thread);
                string response = client.DownloadString(BASE_URL + "n/" + namespaceid + "/messages");

                dynamic obj = JsonConvert.DeserializeObject(response);
                foreach (dynamic msg in obj)
                    msgs.Add(Jsonator.getInstance().fromJson(msg.ToString(), new MessageDTO()));
            }

            return msgs;
        }

        public List<TagDTO> getAllTags(string namespace_id)
        {
            using (WebClient client = new WebClient())
            {
                client.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                string response = client.DownloadString(BASE_URL + "n/" + namespace_id + "/tags");
                dynamic tags = JsonConvert.DeserializeObject(response);

                List<TagDTO> tagsList = new List<TagDTO>();

                foreach (dynamic item in tags)
                {
                    TagDTO dto = new TagDTO();
                    Jsonator.getInstance().fromJson(item.ToString(), dto);

                    tagsList.Add(dto);
                }

                return tagsList;
            }
        }

        public List<ThreadDTO> getAllThreads(string namespace_id)
        {
            using (WebClient client = new WebClient())
            {
                string response = client.DownloadString(BASE_URL + "n/" + namespace_id + "/threads");
                dynamic threads = JsonConvert.DeserializeObject(response);

                List<ThreadDTO> threadList = new List<ThreadDTO>();

                foreach (dynamic item in threads)
                {
                    ThreadDTO dto = new ThreadDTO();
                    Jsonator.getInstance().fromJson(item.ToString(), dto);

                    threadList.Add(dto);
                }

                return threadList;
            }
        }

        public List<ThreadDTO> getAllThreads(string namespace_id, int limit, int set)
        {
            using (WebClient client = new WebClient())
            {
                string response = client.DownloadString(BASE_URL + "n/" + namespace_id + "/threads?limit=" + limit.ToString() + "&offset=" + set.ToString());
                dynamic threads = JsonConvert.DeserializeObject(response);

                List<ThreadDTO> threadList = new List<ThreadDTO>();

                foreach (dynamic item in threads)
                {
                    ThreadDTO dto = new ThreadDTO();
                    Jsonator.getInstance().fromJson(item.ToString(), dto);

                    threadList.Add(dto);
                }

                return threadList;
            }
        }

        public List<ThreadDTO> getAllThreads(string namespace_id, int limit, int set, string query, string[] fields)
        {
            List<ThreadDTO> threadList = new List<ThreadDTO>();

            List<string> filters = new List<string>();

            filters.AddRange(fields);

            foreach (string s in filters)
                using (WebClient client = new WebClient())
                {
                    string url = BASE_URL + "n/" + namespace_id + "/threads";

                    //https://stackoverflow.com/questions/514892/how-do-i-make-a-http-get-request-with-parameters-in-c-sharp

                    client.QueryString.Add("limit", limit.ToString());
                    client.QueryString.Add("offset", set.ToString());

                    client.QueryString.Add(s, query);

                    string response = client.DownloadString(url);

                    dynamic threads = JsonConvert.DeserializeObject(response);

                    foreach (dynamic item in threads)
                    {
                        ThreadDTO dto = new ThreadDTO();
                        Jsonator.getInstance().fromJson(item.ToString(), dto);

                        threadList.Add(dto);
                    }
                }
            return threadList;
        }

        public DraftMessageDTO GetDraft(string namespaceid, string draftid)
        {
            using (WebClient client = new WebClient())
            {
                string url = BASE_URL + "n/" + namespaceid + "/drafts/" + draftid;

                string response = client.DownloadString(url);

                return Jsonator.getInstance().fromJson(response, new DraftMessageDTO());
            }
        }

        public FileDTO GetFile(string file, string namespaceid)
        {
            using (WebClient client = new WebClient())
            {
                string response = client.DownloadString(BASE_URL + "n/" + namespaceid + "/files/" + file);

                return Jsonator.getInstance().fromJson(response, new FileDTO());
            }
        }

        public List<FileDTO> GetFiles(string namespaceid)
        {
            List<FileDTO> files = new List<FileDTO>();

            using (WebClient client = new WebClient())
            {
                string response = client.DownloadString(BASE_URL + "n/" + namespaceid + "/files");

                dynamic obj = JsonConvert.DeserializeObject(response);

                foreach (dynamic item in obj)
                    files.Add(Jsonator.getInstance().fromJson(item.ToString(), new FileDTO()));
            }

            return files;
        }

        public AccountDTO GetMailAccount(string p)
        {
            // Cache the results.
            foreach (AccountDTO acc in accounts)
                if (acc._namespace == p)
                    return acc;

            using (WebClient client = new WebClient())
            {
                AccountDTO acc = Jsonator.getInstance().fromJson(client.DownloadString(BASE_URL + "n/" + p), new AccountDTO());
                accounts.Add(acc);
                return acc;
            }
        }

        public MessageDTO GetMessage(string message, string namespaceid)
        {
            using (WebClient client = new WebClient())
            {
                string response = client.DownloadString(BASE_URL + "n/" + namespaceid + "/messages/" + message);

                return Jsonator.getInstance().fromJson(response, new MessageDTO());
            }
        }

        public TagDTO getTag(string query, string namespace_id)
        {
            foreach (TagDTO tag in getAllTags(namespace_id))
                if (tag.name.Equals(query) || tag.id.Equals(query))
                    return tag;
            return null;
        }

        public ThreadDTO getThread(string thread, string namespaceid)
        {
            using (WebClient client = new WebClient())
            {
                string response = client.DownloadString(BASE_URL + "n/" + namespaceid + "/threads/" + thread);

                return Jsonator.getInstance().fromJson(response, new ThreadDTO());
            }
        }

        public void MarkRead(string namespace_id, MessageDTO message)
        {
            message.Unread = false;
            using (WebClient client = new WebClient())
            {
                string url = BASE_URL + "n/" + namespace_id + "/messages/" + message.Id;

                string body = "{\"unread\":false}";
                string response = client.UploadString(url, "PUT", body);
            }
        }

        public List<MessageDTO> SearchMessages(string namespace_id, int limit, int set, string query, string[] fields)
        {
            List<MessageDTO> msgList = new List<MessageDTO>();

            List<string> filters = new List<string>();

            filters.AddRange(fields);

            foreach (string s in filters)
                using (WebClient client = new WebClient())
                {
                    string url = BASE_URL + "n/" + namespace_id + "/messages";

                    //https://stackoverflow.com/questions/514892/how-do-i-make-a-http-get-request-with-parameters-in-c-sharp

                    client.QueryString.Add("limit", limit.ToString());
                    client.QueryString.Add("offset", set.ToString());
                    client.QueryString.Add(s, query);

                    string response = client.DownloadString(url);

                    dynamic msgs = JsonConvert.DeserializeObject(response);

                    foreach (dynamic item in msgs)
                    {
                        MessageDTO dto = new MessageDTO();
                        Jsonator.getInstance().fromJson(item.ToString(), dto);

                        msgList.Add(dto);
                    }
                }
            return msgList;
        }

        public DraftMessageDTO Send(DraftMessageDTO draft, string namespaceid)
        {
            using (WebClient client = new WebClient())
            {
                string body = Jsonator.getInstance().toJson(draft);
                string url = BASE_URL + "n/" + namespaceid + "/send";

                string response = client.UploadString(url, "POST", body);

                return Jsonator.getInstance().fromJson(response, new DraftMessageDTO());
            }
        }

        public DraftMessageDTO SendDraft(string namespaceid, string draftid, string version)
        {
            using (WebClient client = new WebClient())
            {
                string body = "{\"draft_id\": \"" + draftid + "\",\"version\": \"" + version + "\"}";
                string url = BASE_URL + "n/" + namespaceid + "/drafts";

                string response = client.UploadString(url, "POST", body);

                return Jsonator.getInstance().fromJson(response, new DraftMessageDTO());
            }
        }

        public TagDTO update(TagDTO tag, string namespaceid)
        {
            using (WebClient client = new WebClient())
            {
                //https://stackoverflow.com/questions/5401501/how-to-post-data-to-specific-url-using-webclient-in-c-sharp

                string body = "{\"name\":\"" + tag.name + "\"}";
                string response = client.UploadString(BASE_URL + "n/" + namespaceid + "/tags/" + tag.id, "PUT", body);

                return Jsonator.getInstance().fromJson(response, new TagDTO());
            }
        }

        public DraftMessageDTO Update(DraftMessageDTO draft, string namespaceid)
        {
            using (WebClient client = new WebClient())
            {
                string body = Jsonator.getInstance().toJson(draft);
                string url = BASE_URL + "n/" + namespaceid + "/drafts/" + draft.Id;

                string response = client.UploadString(url, "POST", body);

                return Jsonator.getInstance().fromJson(response, new DraftMessageDTO());
            }
        }

        public ThreadDTO UpdateThread(ThreadDTO thread)
        {
            ThreadDTO orig = getThread(thread.Id, thread.Account);

            List<string> TagsToRemove = new List<string>();
            List<string> TagsToAdd = new List<string>();

            foreach (string s in orig.Tags.Except(thread.Tags))
                TagsToRemove.Add(s);

            foreach (string s in thread.Tags.Except(orig.Tags))
                TagsToAdd.Add(s);

            string body = "{\"add_tags\":";
            body += JsonConvert.SerializeObject(TagsToAdd);
            body += ",\"remove_tags\":";
            body += JsonConvert.SerializeObject(TagsToRemove);
            body += "}";

            using (WebClient client = new WebClient())
            {
                string url = BASE_URL + "n/" + thread.Account + "/threads/" + thread.Id;

                string response = client.UploadString(url, "PUT", body);
            }

            return getThread(thread.Id, thread.Account);
        }

        public FileDTO UploadFile(string file, string namespaceid)
        {
            using (WebClient client = new WebClient())
            {
                byte[] resp = client.UploadFile(BASE_URL + "n/" + namespaceid + "/files", "POST", file);
                string response = "";

                foreach (byte b in resp)
                    response += b.ToString();

                return Jsonator.getInstance().fromJson(response, new FileDTO());
            }
        }
    }
}