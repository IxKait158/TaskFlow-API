namespace TaskFlow.Domain.Entities;

public abstract class BaseEntity {
    public Guid Id { get; set; } = Guid.CreateVersion7();
}