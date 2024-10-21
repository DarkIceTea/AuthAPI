using Application.Abstractions;
using Application.Commands.RegisterUser;
using Application.Services;
using Domain.Models;
using Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAccessTokenService, AccessTokenService>();
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            var app = builder.Build();

            var provider = builder.Services.BuildServiceProvider();
            ISender _sender = provider.GetRequiredService<ISender>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapPost("/register", async ([FromBody] RegisterUserCommand command, CancellationToken cancellationToken) => await _sender.Send(command, cancellationToken));
            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
