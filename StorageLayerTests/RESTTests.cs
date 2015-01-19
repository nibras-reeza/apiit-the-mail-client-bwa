using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TheMailClient.Storage;

using TheMailClient.Storage.DTO;
namespace StorageTests
{
    [TestClass]
    public class RESTTests
    {
        [TestMethod]
        public void TestNamespace()
        {
            AccountDTO acc = MailDataStore.getInstance().GetMailAccount("eynnjko44ozrs9cmo0m1i0cri");
            System.Diagnostics.Trace.WriteLine(acc);

        }

         [TestMethod]
        public void TestAllNamespace()
        {
            foreach(AccountDTO acc in MailDataStore.getInstance().getAllMailAccounts())
            System.Diagnostics.Trace.WriteLine(acc);

        }

         [TestMethod]
         public void TestAllTags() { 
            foreach(TagDTO tag in MailDataStore.getInstance().getAllTags("eynnjko44ozrs9cmo0m1i0cri"))
                System.Diagnostics.Trace.WriteLine(tag);
         }
    }
}
