using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class RoomWarsContext : DbContext
{
    public RoomWarsContext()
    {
    }

    public RoomWarsContext(DbContextOptions<RoomWarsContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoomWarsContext).Assembly);
    }
}