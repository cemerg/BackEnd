using Application.Interfaces;
using Domain.Entities;
using Domain.Events;
using Domain.Interfaces;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IEventPublisher _eventPublisher;

    public OrderService(IOrderRepository repository, IEventPublisher eventPublisher)
    {
        _repository = repository;
        _eventPublisher = eventPublisher;
    }

    public void CreateOrder(string customerName)
    {
        var order = new Order(customerName);
        _repository.Add(order);

        var orderCreatedEvent = new OrderCreatedEvent(order.Id, order.CustomerName);
        _eventPublisher.Publish(orderCreatedEvent);
    }
}
