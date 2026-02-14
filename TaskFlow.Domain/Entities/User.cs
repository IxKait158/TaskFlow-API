using TaskFlow.Domain.Enums;

namespace TaskFlow.Domain.Entities;

public class User : BaseEntity {
    public string Username { get; set; }
    
    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public EUserRole Role { get; set; }
    
    public List<Project>? Projects { get; set; }
    
    public List<TaskItem>? Tasks { get; set; }

    public User() {
        Role = EUserRole.User;
    }
}