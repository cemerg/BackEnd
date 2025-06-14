public abstract class BaseEntity : IEntity
{
    public Guid Id { get; set; }

    public BaseEntity()
    {
        Id = Guid.NewGuid();
    }
}

public abstract class BaseEntityWithDateTime : BaseEntity, IDateTimeEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public BaseEntityWithDateTime()
    {
        CreatedAt = DateTime.UtcNow;
    }
}