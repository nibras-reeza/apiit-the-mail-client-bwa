using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TheMailClient.Storage.DTO;

// SSH.net provided extensive documentation which was unnecassarily
// hefty and long. Hence, the code was generated using IntelliSense
// suggestions without any references.

namespace TheMailClient.Storage
{
    public class OAuthHandler
    {
        private const string host = "5.231.64.215";
        private const string username = "nibras";
        private const string password = "nibras";
        private static OAuthHandler instance = new OAuthHandler();

        public static OAuthHandler GetInstance()
        {
            return instance;
        }

        private OAuthHandler()
        {
        }

        public void StartSync()
        {
            using (SshClient client = new SshClient(host, username, password))
            {
                client.Connect();

                while (client.IsConnected)
                {
                    client.RunCommand("cd inbox");
                    client.RunCommand("python bin/inbox-sync --all start");
                }
            }
        }

        public string GetAuthURL(string email)
        {
            string url = string.Empty;
            // https://stackoverflow.com/questions/11462163/capture-stream-output-to-string
            using (SshClient client = new SshClient(host, username, password))
            {
                client.Connect();

                while (client.IsConnected)
                {
                    //https://stackoverflow.com/questions/19864617/powershell-read-write-to-ssh-net-streams

                    Stream str = client.CreateShellStream("dumb", 80, 24, 800, 600, 1024);

                    StreamReader read = new StreamReader(str);
                    StreamWriter write = new StreamWriter(str);
                    write.AutoFlush = true;

                    SendCommand(str, "cd  inbox ", write);

                    SendCommand(str, "python bin/inbox-auth " + email, write);

                    string line = read.ReadLine();

                    while (true)
                    {
                        if (line != null && (line.Contains("code:") || line.Contains("hidden")))
                            break;
                        line = read.ReadToEnd();
                    }

                    // https://stackoverflow.com/questions/10576686/c-sharp-regex-pattern-to-extract-urls-from-given-string-not-full-html-urls-but
                    Regex linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    url = linkParser.Match(line).Value;

                    str.Dispose();
                    client.Disconnect();
                }
            }

            return url;
        }

        public AccountDTO Authenticate(string email, string code)
        {
            string url = string.Empty;
            // https://stackoverflow.com/questions/11462163/capture-stream-output-to-string
            using (SshClient client = new SshClient(host, username, password))
            {
                client.Connect();

                while (client.IsConnected)
                {
                    //https://stackoverflow.com/questions/19864617/powershell-read-write-to-ssh-net-streams

                    Stream str = client.CreateShellStream("dumb", 80, 24, 800, 600, 1024);

                    StreamReader read = new StreamReader(str);
                    StreamWriter write = new StreamWriter(str);
                    write.AutoFlush = true;

                    SendCommand(str, "cd  inbox ", write);

                    SendCommand(str, "python bin/inbox-auth " + email, write);

                    string line = read.ReadLine();

                    while (true)
                    {
                        if (line != null && (line.Contains("code:") || line.Contains("hidden")))
                            break;
                        line = read.ReadToEnd();
                    }

                    SendCommand(str, code, write);

                    while (true)
                    {
                        if (line != null && (line.Contains("nibras@nibras")))
                            break;
                        line = read.ReadToEnd();
                    }

                    str.Dispose();
                    client.Disconnect();
                }
            }

            foreach (AccountDTO dto in MailDataStore.getInstance().getAllMailAccounts())
                if (dto.email.ToLower().Equals(email.ToLower()))
                    return dto;
            return null;
        }

        private void SendCommand(Stream str, string command, StreamWriter write)
        {
            write.WriteLine(command);
            write.Flush();
        }
    }
}