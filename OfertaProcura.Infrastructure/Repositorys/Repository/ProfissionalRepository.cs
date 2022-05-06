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
    public class ProfissionalRepository : GenericRepository<Profissional>, IProfissionalRepository
    {
        public ProfissionalRepository(OfertaProcuraContext ofertaProcuraContext) : base(ofertaProcuraContext)
        {
        }

        public new Profissional ObterPorId(Guid id)
        {
            var profissional = context.Profissional.AsNoTracking().Include(x => x.RefPortifolio)
                                                   .Include(x => x.RefProfissao)
                                                   .Include(x => x.RefUsuario).FirstOrDefault(x => x.Id == id);

            return profissional;
        }
    }
}
