using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace CrashItServer.Extensions.Jwt {
    public static class JwtServiceExtensions {

        public static IServiceCollection AddJwtService(this IServiceCollection services, IConfigurationSection jwtConfigArray) {
            JwtService jwtService = new JwtService();
            AuthenticationBuilder authBuilder = services.AddAuthentication();
            foreach (IConfigurationSection jwtConfigSection in jwtConfigArray.GetChildren()) {
                string name = jwtConfigSection["Name"];
                string signingSecurityKey = jwtConfigSection["SigningSecurityKey"];
                string issuer = jwtConfigSection["Issuer"];
                string audience = jwtConfigSection["Audience"];
                int tokenLifeTimeHours = Convert.ToInt32(jwtConfigSection["TokenLifeTimeHours"]);
                JwtConfiguration jwtConfiguration = new JwtConfiguration(name, signingSecurityKey, issuer, audience, tokenLifeTimeHours);
                jwtService.AddConfiguration(jwtConfiguration);
                authBuilder.AddJwtBearer(name, jwtBearerOptions => {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = jwtService.GetSecretKey(name),
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidateAudience = true,
                        ValidAudience = audience,
                        ValidateLifetime = tokenLifeTimeHours > 0,
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };
                });
            }
            services.AddSingleton<IJwtService>(jwtService);
            return services;
        }

    }
}
