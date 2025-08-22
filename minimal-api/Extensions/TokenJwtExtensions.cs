using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace MinimalApi.Extensions
{
    public static class TokenJwtExtensions
    {
        public static void AddCustomTokenJwt(this IServiceCollection services, IConfiguration configuration, byte[] jwtKey)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("Editor", policy => policy.RequireRole("Editor"));
            });
        }
    }
}