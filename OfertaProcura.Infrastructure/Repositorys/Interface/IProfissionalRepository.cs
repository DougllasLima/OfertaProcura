using OfertaProcura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Repositorys.Interface
{
    public interface IProfissionalRepository : IGenericRepository<Profissional>
    {
        new Profissional ObterPorId(Guid id);
    }
}
