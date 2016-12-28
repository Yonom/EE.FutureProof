using System;
using PlayerIOClient;

namespace EE.FutureProof.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var client = PlayerIO.QuickConnect.SimpleConnect("everybody-edits-su9rn58o40itdbnw69plyw", "guest", "guest", null);
            var connection = client.Multiplayer.CreateJoinRoom("PW01", "Everybodyedits218", true, null, null).FutureProof(1);

            connection.OnMessage += (sender, message) => { Console.WriteLine(message.Type); };
            connection.Send("i");

            Console.ReadLine();
        }
    }
}