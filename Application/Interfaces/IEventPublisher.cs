namespace Application.Interfaces;

public interface IEventPublisher
{
    void Publish<T>(T @event);
}
