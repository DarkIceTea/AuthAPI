using API.Endpoints;
using API.Extensions;
using MediatR;

namespace API
{
    public class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.ConfigureApplicationServices();
            var app = builder.Build();

            AuthEndpoints.SetSender(builder.Services.BuildServiceProvider().GetRequiredService<ISender>());

            app.ConfigureMiddlewares();

            app.Run();
        }
    }
}
