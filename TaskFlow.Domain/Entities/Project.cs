namespace TaskFlow.Domain.Entities;

public class Project : BaseEntity {
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public Guid OwnerId { get; set; }
    public User Owner { get; set; }
    
    public List<TaskItem>? TaskItems { get; set; }
    
    public Project() {
        CreatedAt = DateTime.UtcNow;
    }
}