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
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(OfertaProcuraContext ofertaProcuraContext) : base(ofertaProcuraContext)
        {
        }

        public new Usuario ObterPorId(Guid id)
        {
            var usuario = context.Usuario
                                         .Include(x => x.RefProfissional)
                                         .ThenInclude(x => x.RefProfissao)
                                         .Include(a => a.RefProfissional)
                                         .ThenInclude(prof => prof.RefPortifolio)
                                         .Include(x => x.RefContratacoes).ThenInclude(x => x.RefContratado).ThenInclude(x => x.RefUsuario)
                                         .Include(x => x.RefContratacoes).ThenInclude(x => x.RefContratante).ThenInclude(x => x.RefProfissional)
                                         .Include(x => x.RefComentario)
                                         .Include(x => x.RefContratacoes).ThenInclude(x => x.RefContratado).ThenInclude(x => x.RefPortifolio)
                                         .FirstOrDefault(x => x.Id == id);

            return usuario;
        }

        public Usuario ObterUsuarioPorCpf(string cpf)
        {
            var usuario = context.Usuario.FirstOrDefault(x => x.Cpf == cpf);

            return usuario;
        }

        public new Usuario Atualizar(Usuario usuario)
        {
            if (usuario == null) throw new ArgumentNullException("entity");

            var entry = context.Usuario.First(e => e.Id == usuario.Id);
            context.Entry(entry).State = EntityState.Detached;
            context.Entry(entry).CurrentValues.SetValues(usuario);
            context.Entry(usuario).State = EntityState.Modified;
            context.SaveChanges();
            return usuario;
        }
    }
}
