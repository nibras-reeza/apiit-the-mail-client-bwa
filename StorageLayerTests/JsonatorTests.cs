using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using TheMailClient.Storage.DTO;
using TheMailClient.Storage.Utils;

namespace StorageTests
{
    [TestClass]
    public class JsonatorTests
    {
        [TestMethod]
        public void testAccountJSONator()
        {
            System.Diagnostics.Trace.WriteLine("Beginning Test Sequence for JSON  > Account...");

            AccountDTO a = new AccountDTO();

            string json = "{\"id\": \"awa6ltos76vz5hvphkp8k17nt\",\"object\": \"namespace\",\"namespace_id\": \"awa6ltos76vz5hvphkp8k17nt\",\"account_id\":\"c5zc216uat483slypx67mu8i3\",\"email_address\": \"ben.bitdiddle@gmail.com\",\"provider\": \"Gmail\"}";

            Jsonator.getInstance().fromJson(json, a);

            Assert.AreEqual(a.id, "awa6ltos76vz5hvphkp8k17nt");
            Assert.AreEqual(a._namespace, "awa6ltos76vz5hvphkp8k17nt");
            Assert.AreEqual(a.account, "c5zc216uat483slypx67mu8i3");
            Assert.AreEqual(a.email, "ben.bitdiddle@gmail.com");
            Assert.AreEqual(a.provider, "Gmail");

            System.Diagnostics.Trace.WriteLine("Test sequence succeeded!");

            System.Diagnostics.Trace.WriteLine("Beginning Test Sequence for Account > JSON...");

            string jsonString = Jsonator.getInstance().toJson(a);

            AccountDTO a2 = new AccountDTO();
            Jsonator.getInstance().fromJson(jsonString, a2);

            Assert.AreEqual(a.id, a2.id);
            Assert.AreEqual(a._namespace, a2._namespace);
            Assert.AreEqual(a.account, a2.account);
            Assert.AreEqual(a.email, a2.email);
            Assert.AreEqual(a.provider, a2.provider);

            System.Diagnostics.Trace.WriteLine("Test sequence succeeded!");
        }

        [TestMethod]
        public void TestDraftJson()
        {
            using (WebClient client = new WebClient())
            {
                string reply = client.DownloadString("http://5.231.64.215/n/ajc73djxb9sxeqrkpjnds66om/drafts/bgj5qi318pemmw50sg2xwca5o");

                DraftMessageDTO m = new DraftMessageDTO();
                m = Jsonator.getInstance().fromJson(reply, m);

                System.Diagnostics.Trace.WriteLine(m.ToString());
            }
        }

        [TestMethod]
        public void TestDraftJsonTo()
        {
            using (WebClient client = new WebClient())
            {
                string reply = client.DownloadString("http://5.231.64.215/n/ajc73djxb9sxeqrkpjnds66om/drafts/bgj5qi318pemmw50sg2xwca5o");

                DraftMessageDTO m = new DraftMessageDTO();
                m = Jsonator.getInstance().fromJson(reply, m);

                System.Diagnostics.Trace.WriteLine(Jsonator.getInstance().toJson(m));
            }
        }
    }
}