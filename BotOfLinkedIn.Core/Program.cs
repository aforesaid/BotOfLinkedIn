using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using BotOfLinkedIn.Core.Models.Settings;
using BotOfLinkedIn.Core.Services;

namespace BotOfLinkedIn.Core
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Запуск приложения");
            using var file = new StreamReader("settings.json");
            var content = await file.ReadToEndAsync();
            var config = JsonSerializer.Deserialize<Configuration>(content);
            var sender = new Sender(config.Cookie, config.CsrfToken);
            var observer = new Observer(1, sender);
            Console.ReadKey();
            Console.WriteLine("Приложение запущено");
        }
    }
}