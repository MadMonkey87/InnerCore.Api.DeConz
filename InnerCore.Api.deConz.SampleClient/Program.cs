using InnerCore.Api.deConz.Models.Sensors;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InnerCore.Api.deConz.SampleClient
{
    public class Program
    {
        private static CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Searching for local deConz gateways...");

            var locator = new HttpBridgeLocator();

            var bridges = await locator.LocateBridgesAsync(TimeSpan.FromSeconds(30));

            foreach (var b in bridges)
            {
                Console.WriteLine($"{b.Name} - {b.InternalIpAddress}:{b.InternalPort}");
            }

            var client = new DeConzClient("192.168.1.21", 80, "612D603FB1");

            // var register = await client.RegisterAsync("foobar");

            var online = await client.CheckConnection();

            var bridge = await client.GetBridgeAsync();
            var whiteList = await client.GetWhiteListAsync();
            var config = await client.GetConfigAsync();

            //var groups = await client.GetGroupsAsync();

            //var lights = await client.GetLightsAsync();

            //var rules = await client.GetSensorsAsync();

            //var scenes = await client.GetScenesAsync();

            //var schedulers = await client.GetSchedulesAsync();

            //var sensors = await client.GetSensorsAsync();

            //var sensor = await client.GetSensorAsync("17");
            //var response = await client.ChangeSensorConfigAsync("17", new SensorConfig() { Sensitivity = 20});

            client.SensorChanged += Client_SensorChanged;
            client.ErrorEvent += Client_ErrorEvent;

            var timer = new System.Timers.Timer(10000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            await client.ListenToEvents(_cancellationTokenSource.Token);

            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _cancellationTokenSource.Cancel();
        }

        private static void Client_ErrorEvent(object sender, Models.WebSocket.ErrorEvent e)
        {
            throw e.Ex;
        }

        private static void Client_SensorChanged(object sender, Models.WebSocket.SensorChangedEvent e)
        {
            if(e.State != null)
            {
                Console.WriteLine($"Sensor {e.Id} has changed its state");
            }
            if (e.Config != null)
            {
                Console.WriteLine($"Sensor {e.Id} has changed its config");
            }
        }
    }
}
