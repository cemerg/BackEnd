using Application.Interfaces;

namespace Infrastructure.Messaging;

public class SignalREventPublisher(IHubContext<TransactionHub> hubContext) : IEventPublisher
{
    public void Publish<T>(T @event)
    {
        var eventName = typeof(T).Name;
        hubContext.Clients.All.SendAsync(eventName, @event);
    }
}
