using System;
using System.Collections.Generic;
using System.Linq;

namespace InnerCore.Api.DeConz.Models.Touchlink
{
    public class ScanResult
    {
        internal ScanResult(RawScanResult rawScanResult)
        {
            if (rawScanResult == null)
                throw new ArgumentNullException(nameof(rawScanResult));

            foreach (var discoveredDevices in rawScanResult.DiscoveredDevices)
            {
                discoveredDevices.Value.Id = discoveredDevices.Key;
            }
            DiscoveredDevices = rawScanResult.DiscoveredDevices.Select(l => l.Value).ToList();

            State = rawScanResult.State;
            LastScanned = rawScanResult.LastScanned;
        }

        public ScanState State { get; set; }

        public DateTime? LastScanned { get; set; }

        public IEnumerable<DiscoveredDevice> DiscoveredDevices { get; private set; }
    }
}
