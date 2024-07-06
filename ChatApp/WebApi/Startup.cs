using Business.Services;
using ChatApp.Hubs;
using Data;
using Data.Models;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ChatApp;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Налаштування контексту бази даних
        services.AddDbContext<ApiDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
        
        // Scopes
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IMessageService, MessageService>();

        // Додаткові налаштування
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddControllers();
        services.AddSignalR();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        { 
            // app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<ChatHub>("/chathub");
        });
    }
}