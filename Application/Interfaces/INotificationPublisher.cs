// Application.Interfaces.INotificationPublisher.cs
namespace Application.Interfaces;

public interface INotificationPublisher
{
    Task NotifyAllAsync(string eventName, object payload);
    Task NotifyUserAsync(string userId, string eventName, object payload);
}
