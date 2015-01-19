using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using TheMailClient.Storage;
using TheMailClient.Storage.DTO;
using TheMailClient.Storage.Utils;

namespace StorageTests
{
    [TestClass]
    public class InboxTest
    {
        private const string BASE_URL = "http://5.231.64.215/";
        private const string namespaceid = "ajc73djxb9sxeqrkpjnds66om";

        [TestMethod]
        public void TestJsonItr()
        {
            using (WebClient client = new WebClient())
            {
                string response = client.DownloadString(BASE_URL + "n/" + namespaceid + "/tags");

                dynamic tags = JsonConvert.DeserializeObject(response);

                List<TagDTO> tagsList = new List<TagDTO>();

                foreach (dynamic item in tags)
                {
                    TagDTO dto = new TagDTO();
                    Jsonator.getInstance().fromJson(item.ToString(), dto);
                    System.Diagnostics.Trace.WriteLine(dto.ToString());
                }
            }
        }

        [TestMethod]
        public void TestAddTag()
        {
            string name = "Test" + (new Random()).Next(100).ToString();

            TagDTO dto = new TagDTO();
            dto.name = name;
            dto = MailDataStore.getInstance().add(dto, namespaceid);

            WriteLine(dto);

            foreach (TagDTO t in MailDataStore.getInstance().getAllTags(namespaceid))
                if (t.name.Equals(dto.name) && t.id.Equals(dto.id))
                    return;

            Assert.Fail();
        }

        [TestMethod]
        public void TestUpdateTag()
        {
            TestAddTag();
            string name = "Test" + (new Random()).Next(10000).ToString();

            TagDTO dto = new TagDTO();
            dto.name = name;
            dto = MailDataStore.getInstance().add(dto, namespaceid);

            WriteLine("Added Tag");
            WriteLine(dto);

            dto.name = "Test" + (new Random()).Next(10000).ToString();

            WriteLine("Renamed Tag");
            WriteLine(dto);

            dto = MailDataStore.getInstance().update(dto, namespaceid);

            WriteLine("Updated Tag");
            WriteLine(dto);

            foreach (TagDTO t in MailDataStore.getInstance().getAllTags(namespaceid))
                if (t.name.Equals(name))
                    Assert.Fail();
                else if (t.name.Equals(dto.name) && t.id.Equals(dto.id))
                    return;

            Assert.Fail();
        }

        [TestMethod]
        public void TestGetThread()
        {
            ThreadDTO dto = MailDataStore.getInstance().getThread("22g57bms3s2hytl96fdzpph0u", namespaceid);

            WriteLine(dto);
        }

        [TestMethod]
        public void TestGetAllThread()
        {
            List<ThreadDTO> thrds = MailDataStore.getInstance().getAllThreads(namespaceid);

            foreach (ThreadDTO dto in thrds)
                WriteLine(dto);
        }

        [TestMethod]
        public void TestGetThreads()
        {
            List<ThreadDTO> thrds = MailDataStore.getInstance().getAllThreads(namespaceid, 1, 1);

            foreach (ThreadDTO dto in thrds)
                WriteLine(dto);
        }

        [TestMethod]
        public void TestSearchThreads()
        {
            List<ThreadDTO> thrds = MailDataStore.getInstance().getAllThreads(namespaceid, 1, 1, "Stay more organized with Gmail's inbox", new string[] { "subject" });

            foreach (ThreadDTO dto in thrds)
                WriteLine(dto);
        }

        [TestMethod]
        public void TestGetMessage()
        {
            MessageDTO dto = MailDataStore.getInstance().GetMessage("3mb58nvgeb52su5sw53jiv3za", namespaceid);

            WriteLine(dto);
        }

        [TestMethod]
        public void TestGetMessages()
        {
            List<MessageDTO> dtos = MailDataStore.getInstance().GetAllMessages("22g57bms3s2hytl96fdzpph0u", namespaceid);
            foreach (MessageDTO dto in dtos)
                WriteLine(dto);
        }

        [TestMethod]
        public void TestSearchMessages()
        {
            List<MessageDTO> msgs = MailDataStore.getInstance().SearchMessages(namespaceid, 1, 1, "Stay more organized with Gmail's inbox", new string[] { "subject" });

            foreach (MessageDTO dto in msgs)
                WriteLine(dto);
        }

        [TestMethod]
        public void TestMessageRead()
        {
            MessageDTO dto = MailDataStore.getInstance().GetMessage("3mb58nvgeb52su5sw53jiv3za", namespaceid);

            MailDataStore.getInstance().MarkRead(namespaceid, dto);

            dto = MailDataStore.getInstance().GetMessage("3mb58nvgeb52su5sw53jiv3za", namespaceid);
            Assert.IsFalse(dto.Unread);
        }

        [TestMethod]
        public void TestThreadUpdate()
        {
            List<ThreadDTO> thrds = MailDataStore.getInstance().getAllThreads(namespaceid, 1, 1);

            foreach (string s in thrds[0].Tags)
                WriteLine(s);

            thrds[0].Tags.Add("Test85");

            WriteLine("After Change");

            foreach (string s in MailDataStore.getInstance().UpdateThread(thrds[0]).Tags)
                WriteLine(s);
        }

        [TestMethod]
        public void TestFileList()
        {
            foreach (FileDTO dto in MailDataStore.getInstance().GetFiles(namespaceid))
                WriteLine(dto);
        }

        [TestMethod]
        public void TestDrafts()
        {
            foreach (DraftMessageDTO dto in MailDataStore.getInstance().GetAllDrafts(namespaceid))
                WriteLine(dto);
        }

        public void WriteLine(Object s)
        {
            System.Diagnostics.Trace.WriteLine(s.ToString());
        }
    }
}