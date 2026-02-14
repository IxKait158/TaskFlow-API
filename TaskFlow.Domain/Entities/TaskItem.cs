using TaskFlow.Domain.Enums;

namespace TaskFlow.Domain.Entities;

public class TaskItem : BaseEntity {
    public string Title { get; set; }

    public string Description { get; set; }

    public ETaskStatus Status { get; set; }
    
    public ETaskPriority Priority { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? Deadline { get; set; } 

    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }

    public Guid? AssigneeId { get; set; }
    public User? Assignee { get; set; }

    public TaskItem() {
        CreatedAt = DateTime.UtcNow;
        Status = ETaskStatus.ToDo;
    }
}