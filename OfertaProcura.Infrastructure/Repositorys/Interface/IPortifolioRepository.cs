using OfertaProcura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Repositorys.Interface
{
    public interface IPortifolioRepository : IGenericRepository<Portifolio>
    {
        new Portifolio ObterPorId(Guid id);
        Portifolio ObterPortifolioPorId(Guid idPortifolio);
    }
}
