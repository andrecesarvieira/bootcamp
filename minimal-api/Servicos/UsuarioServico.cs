using MinimalApi.Dominio.DTOs;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Infraestrutura.Db;
using MinimalApi.Infraestrutura.Interfaces;

namespace MinimalApi.Dominio.Servicos
{
    public class UsuarioServico(DbContexto contexto) : IUsuarioServico
    {
        private readonly DbContexto _contexto = contexto;

        public Usuario? Login(LoginDTO loginDTO)
        {
            var usuario = _contexto.Usuarios
                .Where(a => a.Email == loginDTO.Username && a.Senha == loginDTO.Password)
                .ToList();

            return usuario.FirstOrDefault();
        }

        public bool IncluirUsuario(Usuario usuario)
        {
            if (_contexto.Usuarios.Any(v => v.Email == usuario.Email)) return false;

            _contexto.Usuarios.Add(usuario);
            _contexto.SaveChanges();

            return true;
        }

        public bool AtualizarUsuario(Usuario usuario)
        {
            if (_contexto.Usuarios.Any(v => v.Id == usuario.Id))
            {
                _contexto.Usuarios.Update(usuario);
                _contexto.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ExcluirUsuario(int id)
        {
            var usuario = _contexto.Usuarios.Find(id);

            if (usuario == null) return false;

            _contexto.Usuarios.Remove(usuario);
            _contexto.SaveChanges();

            return true;
        }

        public List<Usuario> ObterTodosUsuarios(int pagina = 1, string? email = null, string? perfil = null)
        {
            const int itensPorPagina = 10;

            var query = _contexto.Usuarios
                .Where(v => (string.IsNullOrEmpty(email) || v.Email.Contains(email)) &&
                            (string.IsNullOrEmpty(perfil) || v.Perfil.Contains(perfil)));

            return query
                .OrderBy(v => v.Id)
                .Skip((pagina - 1) * itensPorPagina)
                .Take(itensPorPagina)
                .ToList();
        }

        public Usuario? ObterUsuarioPorEmail(string email)
        {
            return _contexto.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        public Usuario? ObterUsuarioPorId(int id)
        {
            return _contexto.Usuarios.Find(id);
        }
    }
}