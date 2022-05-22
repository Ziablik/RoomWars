using Application.Contracts;
using Infrastructure.Hubs;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Infrastructure.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace WebApi;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<RoomWarsContext>(
            options => options.UseNpgsql(
                Configuration.GetConnectionString("DefaultConnection"),
                x =>
                {
                    x.MigrationsHistoryTable("__EFMigrationsHistory", "public");
                    x.MigrationsAssembly(typeof(RoomWarsContext).Assembly.FullName);
                })
        );

        AddServices(services);
        
        services.AddSignalR();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseRouting();
        app.UseEndpoints(routes =>
        {
            routes.MapControllers();
            routes.MapHub<JoinRoomHub>("join");
        });
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IGameRoomService, GameRoomService>();
        services.AddScoped<IGameStatisticService, GameStatisticService>();
        services.AddScoped<IGameProcessingService, GameProcessingService>();
        services.AddScoped<IDamageDealerService, DamageDealerService>();
        services.AddScoped<IHubMassagerService, HubMassagerService>();
        services.AddTransient<IRandomWrapper, RandomWrapper>();
        services.AddAutoMapper(typeof(AutoMapperService));
    }
}