using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HadoukenApi.Tests
{
    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        public void TestSystemInfo()
        {
            var c = new Hadouken.HadoukenClient();
            var res = c.GetSystemInfo();
            Assert.IsNotNull(res);
            Console.WriteLine("Received: " + res.ToString());
        }
    }
}
