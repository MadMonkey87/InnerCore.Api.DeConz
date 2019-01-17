using System;
using System.Threading.Tasks;
using Xunit;

namespace InnerCore.Api.DeConz.Tests
{
    public class BridgeDiscoveryTests
    {
        [Fact]
        public async Task TestHttpBridgeLocator()
        {
            var locator = new HttpBridgeLocator();

            // should not throw an exception
            var bridgeIPs = await locator.LocateBridgesAsync(TimeSpan.FromSeconds(5));

            Assert.NotNull(bridgeIPs);
        }
    }
}
