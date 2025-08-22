using System.Text;
using MinimalApi.Endpoints;
using MinimalApi.Extensions;
using MinimalApi.Infrastructure.Interfaces;
using MinimalApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUsuarioServico, UsuarioServico>();
builder.Services.AddScoped<IVeiculoServico, VeiculoServico>();

//DbContext, Swagger
builder.Services.AddCustomDbContext(builder.Configuration);
builder.Services.AddCustomSwagger();

//TokenJwt
byte[] jwtKey = Encoding.UTF8.GetBytes(builder.Configuration["Jwt"]!);
builder.Services.AddCustomTokenJwt(builder.Configuration, jwtKey);

var app = builder.Build();

//Endpoints
HomeEndpoints.MapHomeEndpoints(app);
LoginEndpoints.MapLoginEndpoints(app, jwtKey);
UsuariosEndpoints.MapUsuarioEndpoints(app);
VeiculoEndpoints.MapVeiculoEndpoints(app);

//SwaggerUI
app.UseSwagger();
app.UseSwaggerUI();

//Jwt
app.UseAuthentication();
app.UseAuthorization();

app.Run();