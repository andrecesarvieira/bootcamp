using Microsoft.OpenApi.Models;

namespace MinimalApi.Extensions
{
    public static class SwaggerConfigExtensions
    {
        public static void AddCustomSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                OpenApiSecurityScheme securityScheme = new()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "Jwt",
                    In = ParameterLocation.Header,
                    Description = "Insira o token Jwt",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                
                options.AddSecurityDefinition("Bearer", securityScheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, Array.Empty<string>() }
                });
            });
        }
    }
}