using OfertaProcura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Repositorys.Interface
{
    public interface IComentarioRepository : IGenericRepository<Comentario>
    {
        List<Comentario> ObterComentarioPorIdPortifolio(Guid id);
    }
}
