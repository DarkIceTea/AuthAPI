using Application.Abstractions;
using Application.Services;
using Infrastructure.Repositories;

namespace API.Configuration
{
    public static class ServicesConfiguration
    {
        public static WebApplicationBuilder Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAccessTokenService, AccessTokenService>();
            builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            return builder;
        }
    }
}
