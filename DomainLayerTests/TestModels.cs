using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using TheMailClient.Domain.Model;
using TheMailClient.Domain.Model.ObjectFactory;

// https://stackoverflow.com/questions/1122483/random-string-generator-returning-same-string
namespace DomainLayerTests
{
    [TestClass]
    public class TestModels
    {
        [TestMethod]
        public void TestAccount()
        {
            System.Diagnostics.Trace.WriteLine("Beginning Test Sequence for Account class...");

            SyncAccountFactory fact = new SyncAccountFactory();

            string id = getRandomString();
            string ns = getRandomString();
            string acc = getRandomString();
            string email = getRandomString() + "@" + getRandomString() + ".com";
            string prov = getRandomString();

            fact.Id = id;
            fact.NamespaceID = ns;
            fact.account = acc;
            fact.Email = email;
            fact.Provider = prov;

            SyncAccount a = fact.Create();

            Assert.AreEqual(a.Id, id);
            Assert.AreEqual(a.Namespace, ns);
            Assert.AreEqual(a.Account, acc);
            Assert.AreEqual(a.Email, email);
            Assert.AreEqual(a.Provider, prov);

            System.Diagnostics.Trace.WriteLine("Generated object:\n" + a);

            System.Diagnostics.Trace.WriteLine("Test sequence for Account class succeeded!");
        }

        [TestMethod]
        public void TestUser()
        {
            System.Diagnostics.Trace.WriteLine("Beginning Test Sequence for User class...");
            User u = new User();

            System.Diagnostics.Trace.WriteLine("Test sequence for user class succeeded!");
        }

        private string getRandomString()
        {
            return Path.GetRandomFileName().Replace(".", "");
        }
    }
}