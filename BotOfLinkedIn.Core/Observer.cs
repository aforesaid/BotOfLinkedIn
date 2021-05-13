using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BotOfLinkedIn.Core.Models.JsonModels.GetUsers.Responses;
using BotOfLinkedIn.Core.Services;

namespace BotOfLinkedIn.Core
{
    public class Observer : IDisposable
    {
        private Timer _timer;
        private int offset = 50;

        private Sender _sender;
        private List<UserInfoItem> _items = new();
        public Observer(int countMinutes, Sender sender)
        {
            _sender = sender;
            _timer = new Timer( async _ => await CreateRequest(),null,TimeSpan.Zero,
                TimeSpan.FromMinutes(countMinutes));
        }

        private async Task CreateRequest()
        {
            if (_items.Count < 1)
            {
                try
                {
                    _items = await _sender.GetUsers(offset);
                    Console.WriteLine($"Следующие 10 пользователей с {offset} получены");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Не удалось запросить информацию по пользователям");
                    return;
                }
            }

            var getUser = _items.FirstOrDefault(item => item.Info.IsValid);
            try
            {
                await _sender.AddToContact(getUser.GetEntityUrl, getUser.TrackingId);
                Console.WriteLine($"По пользователю с trackId {getUser.TrackingId} успешно был отправлен запрос");
                _items.Remove(getUser);
            }
            catch
            {
                Console.WriteLine($"По пользователю с trackId {getUser.TrackingId} возникла ошибка");
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}