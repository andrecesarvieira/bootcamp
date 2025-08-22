using MinimalApi.ViewModels;

namespace MinimalApi.Endpoints
{
    public static class HomeEndpoints
    {
        public static void MapHomeEndpoints(this IEndpointRouteBuilder app)
        {
            //Rota principal
            app.MapGet("/", (HttpContext context) =>
            {
                Home home = new();
                string linkSwagger = home.SwaggerUrl;
                string msg = home.Mensagem;

                return Results.Text($"<h2>{msg}</h2><a href='{linkSwagger}'>Swagger</a>", "text/html; charset=utf-8");
                
            }).WithTags("Home").AllowAnonymous();            
        }
    }
}