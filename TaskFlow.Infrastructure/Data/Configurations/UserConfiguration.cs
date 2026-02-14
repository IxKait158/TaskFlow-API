using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User> {
    public void Configure(EntityTypeBuilder<User> builder) {
        builder.HasKey(u => u.Id);
     
        builder
            .HasMany(u => u.Projects)
            .WithOne(p => p.Owner);
             
        builder
            .HasMany(u => u.Tasks)
            .WithOne(t => t.Assignee);
    }
}