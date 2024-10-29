using API.Configuration;
using API.Endpoints;
using Application.Validators;
using FluentValidation.AspNetCore;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ProgramExtensions
    {
        [Obsolete]
        public static WebApplicationBuilder ConfigureApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLAuthApi"),
                x => x.MigrationsAssembly("Migrations")));

            AuthConfiguration.Configure(builder);
            ServicesConfiguration.Configure(builder);
            builder.Services.AddProblemDetails();

            //builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

            builder.Services.AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();
            });

            return builder;
        }

        public static WebApplication ConfigureMiddlewares(this WebApplication app)
        {
            //app.UseAuthentication();
            //app.UseAuthorization();

            //app.UseExceptionHandler();

            app.MapPost("/register", AuthEndpoints.Registration);
            app.MapPost("/login", AuthEndpoints.Login);
            app.MapPost("/sign-out", AuthEndpoints.SignOut);
            app.MapPost("/refresh", AuthEndpoints.Refresh);

            return app;
        }
    }
}
