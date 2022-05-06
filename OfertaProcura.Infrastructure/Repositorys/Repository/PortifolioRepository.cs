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
    public class PortifolioRepository : GenericRepository<Portifolio>, IPortifolioRepository
    {
        public PortifolioRepository(OfertaProcuraContext ofertaProcuraContext) : base(ofertaProcuraContext)
        {
        }

        public new Portifolio ObterPorId(Guid idProfissional)
        {
            var portifolio = context.Portifolio.Include(x => x.RefProfissional)
                                               .Include(x => x.RefProfissional.RefContratacoes)
                                               .Include(x => x.RefProfissional.RefUsuario)
                                               .FirstOrDefault(x => x.RefProfissional.Id == idProfissional);

            return portifolio;
        }

        public Portifolio ObterPortifolioPorId(Guid idPortifolio)
        {
            var portifolio = context.Portifolio.Include(x => x.RefProfissional)
                                               .Include(x => x.RefProfissional.RefContratacoes)
                                               .Include(x => x.RefProfissional.RefUsuario)
                                               .FirstOrDefault(x => x.Id == idPortifolio);

            return portifolio;
        }
    }
}
