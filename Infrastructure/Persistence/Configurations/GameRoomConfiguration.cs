using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class GameRoomConfiguration : IEntityTypeConfiguration<GameRoom>
{
    public void Configure(EntityTypeBuilder<GameRoom> builder)
    {
        builder.Property(e => e.Id)
            .HasDefaultValueSql("uuid_generate_v4()");

        builder.Property(e => e.RoomName).IsRequired();
        
        builder.Property(e => e.RoomStatus).IsRequired();

        builder.HasOne(e => e.Host)
            .WithMany(e => e.GameRooms)
            .HasForeignKey(e => e.HostId);
        
        builder.HasMany(e => e.Users)
            .WithMany(e => e.GameRoomsParticipation);
    }
}