using API.Endpoints;
using Application.Abstractions;
using Application.Services;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extensions
{
    public static class ProgramExtensions
    {
        public static WebApplicationBuilder ConfigureApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLAuthApi"),
                x => x.MigrationsAssembly("Migrations")));

            builder.Services.AddIdentity<CustomUser, CustomRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "AuthApiServer",
                        ValidateAudience = true,
                        ValidAudience = "InnoclinicApi",
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("securitykeysecuritykeysecuritykey")),
                        ValidateIssuerSigningKey = true,
                    };
                });

            builder.Services.AddProblemDetails();

            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAccessTokenService, AccessTokenService>();
            builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            return builder;
        }

        public static WebApplication ConfigureMiddlewares(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseExceptionHandler();

            app.MapPost("/register", AuthEndpoints.Registration);
            app.MapPost("/login", AuthEndpoints.Login);
            app.MapPost("/sign-out", AuthEndpoints.SignOut);
            app.MapPost("/refresh", AuthEndpoints.Refresh);
            //app.MapGet("/", () => "Hello World!");

            return app;
        }
    }
}
