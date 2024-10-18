using Application.Abstractions;
using Application.Commands.RegisterUser;
using Application.Services;
using Domain.Models;
using Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLAuthApi"),
                x => x.MigrationsAssembly("Migrations")));

            builder.Services.AddIdentity<CustomUser, CustomRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            //builder.Services.AddTransient<UserManager<CustomUser>, UserManager<CustomUser>>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            var app = builder.Build();

            var provider = builder.Services.BuildServiceProvider();
            ISender _sender = provider.GetRequiredService<ISender>();

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.MapPost("/register", async ([FromBody] RegisterUserCommand command, CancellationToken cancellationToken) => await _sender.Send(command, cancellationToken));
            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
