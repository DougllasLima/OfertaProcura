using Microsoft.EntityFrameworkCore;
using OfertaProcura.Context;
using OfertaProcura.Models;
using OfertaProcura.Repositorys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Repositorys.Repository
{
    public class ComentarioRepository : GenericRepository<Comentario>, IComentarioRepository
    {
        public ComentarioRepository(OfertaProcuraContext ofertaProcuraContext) : base(ofertaProcuraContext)
        {
        }

        public List<Comentario> ObterComentarioPorIdPortifolio(Guid id)
        {
            var comentarios = context.Comentario
                                     .Include(x => x.RefCliente)
                                     .Where(x => x.Id_Portifolio == id).ToList();

            return comentarios;
        }
    }
}
