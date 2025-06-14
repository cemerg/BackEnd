// Infrastructure/SignalR/SignalRNotificationPublisher.cs
using Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR;

public class SignalRNotificationPublisher(IHubContext<TransactionHub> hubContext)
    : INotificationPublisher
{
    public Task NotifyAllAsync(string eventName, object payload)
    {
        return hubContext.Clients.All.SendAsync(eventName, payload);
    }

    public Task NotifyUserAsync(string userId, string eventName, object payload)
    {
        return hubContext.Clients.User(userId).SendAsync(eventName, payload);
    }
}
