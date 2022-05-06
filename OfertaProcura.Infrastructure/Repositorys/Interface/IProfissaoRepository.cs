using OfertaProcura.Models;
using OfertaProcura.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Repositorys.Interface
{
    public interface IProfissaoRepository : IGenericRepository<Profissao>
    {
        Profissao ObterProfissionaisPorProfissao(string prProfissao);
        Profissao ObterProfissionais(string nomeProfissao, List<string> filtroCidades);
        Profissao ObterProfissaoPorNome(string nomeProfissao);
    }
}
