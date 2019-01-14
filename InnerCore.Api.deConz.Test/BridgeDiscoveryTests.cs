using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InnerCore.Api.deConz.Test
{

    [TestClass]

    public class BridgeDiscoveryTests

    {

        [TestMethod]

        public async Task TestHttpBridgeLocator()

        {

            var locator = new HttpBridgeLocator();



            var bridgeIPs = await locator.LocateBridgesAsync(TimeSpan.FromSeconds(5));



            Assert.IsNotNull(bridgeIPs);

            Assert.IsTrue(bridgeIPs.Count() > 0);



        }

    }
}
