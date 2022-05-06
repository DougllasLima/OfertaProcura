using OfertaProcura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Repositorys.Interface
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        new Usuario ObterPorId(Guid id);
        Usuario ObterUsuarioPorCpf(string cpf);
    }
}
