using Microsoft.EntityFrameworkCore;
using OfertaProcura.Context;
using OfertaProcura.Models;
using OfertaProcura.Repositorys.Interface;
using OfertaProcura.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfertaProcura.Repositorys.Repository
{
    public class ProfissaoRepository : GenericRepository<Profissao>, IProfissaoRepository
    {

        public ProfissaoRepository(OfertaProcuraContext ofertaProcuraContext) : base(ofertaProcuraContext)
        {
        }

        public Profissao ObterProfissionaisPorProfissao(string prProfissao)
        {
            var profissao = context.Profissao.Include(x => x.RefProfissional)
                                             .ThenInclude(x => x.RefUsuario)
                                             .Where(x => x.Nome_Profissao == prProfissao.ToUpper()).FirstOrDefault();

            return profissao;
        }

        public Profissao ObterProfissionais(string nomeProfissao, List<string> filtroCidades)
        {
            if(filtroCidades.Count <= 0)
            {
                filtroCidades.AddRange(new List<String> { "Santos", "Cubatão", "São Vicente", 
                                                                                      "Bertioga", "Praia Grande", "Mongaguá", 
                                                                                      "Itanhaém", "Peruíbe", "Guarujá" });
            }

            var profissao = context.Profissao.Include(x => x.RefProfissional)
                                             .ThenInclude(x => x.RefUsuario)
                                             .Where(x => x.Nome_Profissao == nomeProfissao.ToUpper()).FirstOrDefault();

            if (profissao != null)
            {
                profissao.RefProfissional.RemoveAll(a => !filtroCidades.Contains(a.RefUsuario.Cidade));
            }

            return profissao;
        }

        public Profissao ObterProfissaoPorNome(string nomeProfissao)
        {
            var profissao = context.Profissao.FirstOrDefault(x => x.Nome_Profissao == nomeProfissao);

            return profissao;
        }
    }
}
