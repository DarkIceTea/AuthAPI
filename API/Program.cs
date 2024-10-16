using Application.Commands.RegisterUser;
using Infrastructure.Data;
using MediatR;
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
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            var app = builder.Build();

            var provider = builder.Services.BuildServiceProvider();
            ISender _sender = provider.GetRequiredService<ISender>();

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.MapPost("/register", ([FromBody] RegisterUserCommand command, CancellationToken cancellationToken) => _sender.Send(command, cancellationToken));
            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
