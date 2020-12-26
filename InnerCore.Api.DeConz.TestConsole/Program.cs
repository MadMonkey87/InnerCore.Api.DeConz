using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnerCore.Api.DeConz.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            Init();
            Console.ReadLine();
        }

        public static async void Init()
        {
            try
            {
                var client = new DeConzClient("deconz.tami", 888, "463608DCF3");
                Console.WriteLine("Client Init Done");
                Console.WriteLine("Read Groups");
                var groups = await client.GetGroupsAsync();
                Console.WriteLine("Read Groups Done. Try to get Küche");
                var group = groups.FirstOrDefault(x => x.Name == "Küche");
                var kitchenobject = await client.GetGroupAsync(group.Id);
                if (group == null)
                {
                    Console.WriteLine("Küche nicht gefunden");
                    return;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
