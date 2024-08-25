using System;
using System.Threading.Tasks;

namespace InnerCore.Api.DeConz.Sample.RealTimeEvents
{
    class Program
    {
        public static async Task Main()
        {
            // Initialize the client

            Console.WriteLine("ip address:");
            var ip = Console.ReadLine();

            Console.WriteLine("port (defaults to 80):");
            int port = 80;
            var portAsString = Console.ReadLine();
            int.TryParse(portAsString, out port);

            Console.WriteLine("appkey:");
            var appKey = Console.ReadLine();

            var client = new DeConzClient(ip, port, appKey);

            // test the connection

            Console.WriteLine("connecting...");
            var online = await client.CheckConnection();

            if (!online)
            {
                Console.WriteLine("...unable to connect!");
                Console.WriteLine("Press enter to quit");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("...connected successfully!");

            // setup the events

            client.SensorChanged += Client_SensorChanged;
            client.LightChanged += Client_LightChanged;
            client.GroupChanged += Client_GroupChanged;
            client.ErrorEvent += Client_ErrorEvent;

            // start listening to events (infinite as long as the server does not close the connection)

            Console.WriteLine("Listening to events...");
            await client.ListenToEvents();
        }

        private static void Client_SensorChanged(
            object sender,
            Models.WebSocket.SensorChangedEvent e
        )
        {
            if (e.Config != null)
            {
                Console.WriteLine($"Sensor {e.Id} has changed it's config");
            }
            if (e.State != null)
            {
                Console.WriteLine($"Sensor {e.Id} has changed it's state");
            }

            if (e.Config == null && e.State == null)
            {
                Console.WriteLine($"Empty message from sensor {e.Id}");
            }
        }

        private static void Client_LightChanged(object sender, Models.WebSocket.LightChangedEvent e)
        {
            if (e.State != null)
            {
                Console.WriteLine($"Light {e.Id} has changed it's state");
            }
            else
            {
                Console.WriteLine($"Empty message from light {e.Id}");
            }
        }

        private static void Client_GroupChanged(object sender, Models.WebSocket.GroupChangedEvent e)
        {
            if (e.State != null)
            {
                Console.WriteLine($"Group {e.Id} has changed it's state");
            }
            else
            {
                Console.WriteLine($"Empty message from group {e.Id}");
            }
        }

        private static void Client_ErrorEvent(object sender, Models.WebSocket.ErrorEvent e)
        {
            var originalColor = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"error while handling a message: {e.Ex.Message}");
            Console.BackgroundColor = originalColor;
        }
    }
}
