namespace MinimalApi.Dominio.ModelViews
{
    public struct Home
    {
        public readonly string Mensagem => "Bem-vindo à API de veículo";
        public readonly string SwaggerUrl => "/swagger";
    }
}