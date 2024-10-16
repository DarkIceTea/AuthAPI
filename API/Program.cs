using Application.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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
