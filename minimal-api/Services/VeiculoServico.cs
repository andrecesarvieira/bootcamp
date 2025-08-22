using MinimalApi.Entities;
using MinimalApi.Infrastructure.Data;
using MinimalApi.Infrastructure.Interfaces;

namespace MinimalApi.Services
{
    public class VeiculoServico(DbContexto contexto) : IVeiculoServico
    {
        private readonly DbContexto _contexto = contexto;

        public bool IncluirVeiculo(Veiculo veiculo)
        {
            _contexto.Veiculos.Add(veiculo);
            _contexto.SaveChanges();
            
            return true;
        }
        
        public bool AtualizarVeiculo(Veiculo veiculo)
        {
            if (_contexto.Veiculos.Any(v => v.Id == veiculo.Id))
            {
                _contexto.Veiculos.Update(veiculo);
                _contexto.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ExcluirVeiculo(int id)
        {
            var veiculo = _contexto.Veiculos.Find(id);

            if (veiculo == null) return false;

            _contexto.Veiculos.Remove(veiculo);
            _contexto.SaveChanges();

            return true;
        }

        public List<Veiculo> ObterTodosVeiculos(int pagina = 1, string? nome = null, string? marca = null)
        {
            const int itensPorPagina = 10;

            var query = _contexto.Veiculos
                .Where(v => (string.IsNullOrEmpty(nome) || v.Nome.Contains(nome)) &&
                            (string.IsNullOrEmpty(marca) || v.Marca.Contains(marca)));

            return query
                .OrderBy(v => v.Id)
                .Skip((pagina - 1) * itensPorPagina)
                .Take(itensPorPagina)
                .ToList();
        }

        public Veiculo? ObterVeiculoPorId(int id)
        {
            return _contexto.Veiculos.Find(id);
        }
    }
}