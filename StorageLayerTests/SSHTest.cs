using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using TheMailClient.Storage;

namespace StorageTests
{
    [TestClass]
    public class SSHTest
    {
        [TestMethod]
        public void TestAuthURL()
        {
            //System.Diagnostics.Trace.WriteLine(OAuthHandler.GetInstance().GetAuthURL("nibras@gmail.com"));
            // System.Diagnostics.Trace.WriteLine(OAuthHandler.GetInstance().Authenticate("nbwata003@gmail.com", "4/KIqSFUqeVSQVGx4j0j8_P4C8nyfu.wrvAlC5TqVwZcp7tdiljKKb7oDsbkQI"));
            //System.Diagnostics.Trace.WriteLine(OAuthHandler.GetInstance().GetAuthURL("nibras@ymail.com"));
            //System.Diagnostics.Trace.WriteLine(OAuthHandler.GetInstance().GetAuthURL("nibras@nibrasweb.com"));

            System.Diagnostics.Trace.WriteLine(OAuthHandler.GetInstance().Authenticate("nbwata001@yahoo.com", "MyPassword123"));
        }
    }
}