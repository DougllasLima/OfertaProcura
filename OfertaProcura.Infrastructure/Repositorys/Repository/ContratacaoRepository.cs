using OfertaProcura.Context;
using OfertaProcura.Models;
using OfertaProcura.Repositorys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Repositorys.Repository
{
    public class ContratacaoRepository : GenericRepository<Contratacao>, IContratacaoRepository
    {
        public ContratacaoRepository(OfertaProcuraContext ofertaProcuraContext) : base(ofertaProcuraContext)
        {
        }
    }
}
