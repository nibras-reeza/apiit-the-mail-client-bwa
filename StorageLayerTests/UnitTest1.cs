using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using TheMailClient.Storage;

namespace StorageTests
{
    [TestClass]
    public class SqlTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            UserDataStore.getInstance().Find(" ");
        }
    }
}