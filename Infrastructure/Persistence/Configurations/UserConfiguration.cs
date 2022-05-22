using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(e => e.Id)
            .HasDefaultValueSql("uuid_generate_v4()");

        builder.Property(e => e.Username).IsRequired();

        builder.HasIndex(e => e.Username).IsUnique();
        
        builder.HasMany(e => e.GameRoomsParticipation)
            .WithMany(e => e.Users);
    }
}