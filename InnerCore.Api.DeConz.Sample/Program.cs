using InnerCore.Api.DeConz.Exceptions;
using InnerCore.Api.DeConz.Models.Lights;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InnerCore.Api.DeConz.Sample
{
    class Program
    {
        public static async Task Main()
        {
            Console.WriteLine("Searching for bridges in the local network...");

            // search bridges in the same network

            var bridgeLocator = new HttpBridgeLocator();
            var bridges = await bridgeLocator.LocateBridgesAsync(TimeSpan.FromSeconds(30));

            if (!bridges.Any())
            {
                Console.WriteLine(
                    "...no bridges found! Either you have none in your network or they are not connected to the internet.");
                Console.WriteLine(
                    "(you could still could create the DeConzClient manually with the ip and its port)");
                QuitGracefully();
                return;
            }

            Console.WriteLine("...found the following bridges (first one will be used):");
            foreach (var bridge in bridges)
            {
                Console.WriteLine($" - {bridge.Name} ({bridge.InternalIpAddress}:{bridge.InternalPort})");
            }

            // use the first bridge to create the DeConzClient

            var locatedBridge = bridges.First();
            var client = new DeConzClient(locatedBridge.InternalIpAddress, locatedBridge.InternalPort);

            // retrieve an appkey

            var appkey = string.Empty;

            while(appkey == string.Empty)
            {
                try
                {
                    Console.WriteLine("registering on the bridge...");
                    appkey = await client.RegisterAsync("DeConz_Sample_Application");
                    Console.WriteLine($"...successfully registered -> store the key {appkey} to initialize the DeConzClient in the future!");
                }
                catch(LinkButtonNotPressedException)
                {
                    Console.WriteLine("...failed -> please unlock the bridge in the Phoscon app");
                    Console.WriteLine("Press enter to continue");
                    Console.ReadLine();
                }
            }

            // list lights and sensors

            var lights = await client.GetLightsAsync();
            Console.WriteLine($"found {lights.Count()} lights");
            foreach (var light in lights)
            {
                Console.WriteLine($" - {light.Name}");
            }

            var sensors = await client.GetSensorsAsync();
            Console.WriteLine($"found {sensors.Count()} sensors");
            foreach (var sensor in sensors)
            {
                Console.WriteLine($" - {sensor.Name}");
            }

            // Turn on all lights

            Console.WriteLine("Turning on all lights...");

            var command = new LightCommand().TurnOn();//.SetColor(new RGBColor("FF00AA"));

            var result = await client.SendCommandAsync(command);
            Console.WriteLine(result.Errors.Any() ? "...Failure!" : "...Success!");

            QuitGracefully();
        }

        private static void QuitGracefully()
        {
            Console.WriteLine("Press enter to quit...");
            Console.ReadLine();
        }
    }
}
