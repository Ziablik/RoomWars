using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class GameStatisticConfiguration : IEntityTypeConfiguration<GameStatistic>
{
    public void Configure(EntityTypeBuilder<GameStatistic> builder)
    {
        builder.HasKey(e => e.GameRoomId);
        
        builder.HasOne(e => e.Winner)
            .WithMany(e => e.WinGames)
            .HasForeignKey(e => e.WinnerId);
        
        builder.HasOne(e => e.Loser)
            .WithMany(e => e.LoseGames)
            .HasForeignKey(e => e.LoserId);

        builder.HasOne(e => e.GameRoom)
            .WithOne(e => e.Statistic);
    }
}