using API.Endpoints;
using API.Extensions;
using MediatR;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.ConfigureApplicationServices();
            var app = builder.Build();

            var provider = builder.Services.BuildServiceProvider();
            AuthEndpoints.SetSender(provider.GetRequiredService<ISender>());

            app.ConfigureMiddlewares();

            app.Run();
        }
    }
}
