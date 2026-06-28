using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using CombatManager.Api;

namespace CombatManager.Websocket.ConsoleTest
{
    class Program
    {
        private static NotificationApiChat _chat;
        static async Task Main(string[] args)
        {

            System.Console.WriteLine("Hello World!");

            Thread.Sleep(1000); 

            //await NotificationApiChat.RunWebSockets("ws://localhost:12457/api/notification");
            _chat = new NotificationApiChat("ws://localhost:12457/api/notification");
            _chat.StateChanged += (sender, state) =>
            {
                Console.WriteLine(state.Name +":" + (state.Data));
            };
            var cts = new CancellationToken();
            _chat.StartConnection(cts).Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

 
           
    }
}
