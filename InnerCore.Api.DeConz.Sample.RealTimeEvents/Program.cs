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
            client.ErrorEvent += Client_ErrorEvent;

            // start listening to events (infinite as long as the server does not close the connection)

            Console.WriteLine("Listening to events...");
            await client.ListenToEvents();
        }

        private static void Client_SensorChanged(object sender, Models.WebSocket.SensorChangedEvent e)
        {
            if (e.Config != null)
            {
                Console.WriteLine($"Sensor {e.Id} has changed it's config");
            }

            if (e.State != null)
            {
                Console.WriteLine($"Sensor {e.Id} has changed it's state");
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
