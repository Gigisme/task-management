
using Domain.Services;
using Domain.Services.IServices;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Configuration.AddJsonFile("appsettings.json");

        builder.Services.AddDbContext<TaskDbContext>(options => 
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ITaskRepository, TaskRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        builder.Services.AddScoped<IPasswordService, PasswordService>();
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("_allowAny",
                policy => { policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();});
        });

        builder.Services.AddControllers();

        builder.Services.AddHttpContextAccessor();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("_allowAny");

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}
