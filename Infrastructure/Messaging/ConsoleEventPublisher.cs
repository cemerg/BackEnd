using Application.Interfaces;
using System.Text.Json;

namespace Infrastructure.Messaging
{
    public class ConsoleEventPublisher : IEventPublisher
    {
        public void Publish<T>(T @event)
        {
            var eventType = @event.GetType().Name;
            var eventData = JsonSerializer.Serialize(@event, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            Console.WriteLine($"[Event Published] Type: {eventType}");
            Console.WriteLine(eventData);
        }
    }
}
