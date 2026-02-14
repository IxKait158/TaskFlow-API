using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Infrastructure.Data.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project> {
    public void Configure(EntityTypeBuilder<Project> builder) {
        builder.HasKey(p => p.Id);
        
        builder
            .HasOne(p => p.Owner)
            .WithMany(u => u.Projects)
            .HasForeignKey(p => p.OwnerId);
        
        builder
            .HasMany(p => p.TaskItems)
            .WithOne(t => t.Project);
    }
}